using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cBinDown
{
    public static partial class HelperPort
    {
        private static SerialBase sPort = new SerialBase();
        private static Task PortTask=null;
        private static CancellationTokenSource PortTokenSrc = null;
        static DeviceData devData = new DeviceData();
        private static readonly int CHUNK_SIZE = 128;

        public static double GetProcessInfo()
        {
            double val = 0;
            if (devData.uTotalLen > 0)
                val = (double)(100.0*devData.uCurrenLen / devData.uTotalLen);
            return val;
        }
        public static string GetProcessCurrentLen()
        {
            double val = 0;
            if (devData.uTotalLen > 0)
                val = (double)(100.0 * devData.uCurrenLen / devData.uTotalLen);
            return val.ToString("f1")+"%%";
        }
        public static string GetOpStep()
        {
            return devData.strStep;
        }
        public static int GetDownELapse()
        {
            TimeSpan ts = devData.dtEnd - devData.dtStart;
            return (int)ts.TotalSeconds;
        }

        public static async Task ProcessPort(CancellationToken cancellationToken)
        {

            while (true)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100));//, cancellationToken);
                //bool success = false;
                try
                {
                    CommParam.mTick++;
                    
                   // success = true;
                }
                catch { /* ignore errors */ }
                //progress.Report(success);
            }
            
        }

        public static void  Start()
        {
            PortTokenSrc = new CancellationTokenSource();
            PortTask = Task.Run(() => ProcessPort(PortTokenSrc.Token));
        }
        public static void Stop()
        {
            try
            {
                PortTokenSrc.Cancel();
            }
            catch { }
        }
        public static void PortOpen(string port)
        {
            if (sPort.IsOpen())
                sPort.Close();
            sPort.SerialPortInni(port, 115200);
            sPort.ReceiveTimeout = 600;            
            sPort.Open();
            devData.strStep = "打开串口"+port;
        }
        public static bool PortIsOpen()
        {
            return sPort.IsOpen();
        }
        public static void PortClose()
        {
            sPort.Close();
            devData.strStep = "关闭串口";
        }
        public static int  PortGetID()
        {
            if (!sPort.IsOpen()) return 0;
            OperateResult<byte[]> result = sPort.ReadBase(new byte[] { 0x02, 0xFD });//GET_ID_CMD
            if (!result.IsSuccess)
            {
                return 0;
            }
            int outPid=0;
            byte[]dat = result.Content;
            if (dat.Length > 1)
            {
                int CmdCount = dat[1] + 1;
                if (CmdCount < dat.Length - 3)
                    CmdCount = dat.Length - 3;

                if (CmdCount == 2) outPid = dat[2] * 256 + dat[3];
                else if (CmdCount == 1) outPid = dat[2];
            }
            return outPid;
        }
        
        public static async Task<bool> PortInitBL(string strBinPath)
        {
            devData.strStep = "连接设备";
            if (!sPort.IsOpen()) sPort.Open();
            devData.dtStart = DateTime.Now;
            OperateResult<byte[]> result=sPort.ReadBase(new byte[] { 0x7F});
            int m=3;
            while((!result.IsSuccess)&&(m>0))
            {                
                await Task.Delay(50);
                result = sPort.ReadBase(new byte[] { 0x7F });
                m--;
            }
            //if (!result.IsSuccess) return;
            ///////////
            result = sPort.ReadBase(new byte[] { 0x00, 0xFF });//GET_CMD
            if (!result.IsSuccess)
            {
                devData.strStep = "下载失败,请检查设备是否上电和串口";
                return false;
            }
            
            byte[] dat = result.Content;
            int CmdCount=0;
            if (dat==null||dat.Length < 3) CmdCount = 0;
            else if(dat[0]== DeviceData.const_ACK)
            {
                CmdCount = dat[1];
                if ( CmdCount< dat.Length-3)
                    CmdCount = dat.Length - 3;
            }
            for (int i = 0; i < CmdCount; i++)
            {
                CmdList k =(CmdList) dat[i + 3];
                switch (k)
                {
                    case CmdList.GET_CMD:
                        devData.flags.GET_CMD = true;
                        break;
                    case CmdList.GET_VER_ROPS_CMD:
                        devData.flags.GET_VER_ROPS_CMD = true;
                        break;
                    case CmdList.GET_ID_CMD:
                        devData.flags.GET_ID_CMD = true;
                        break;
                    case CmdList.READ_CMD:
                        devData.flags.READ_CMD = true;
                        break;
                    case CmdList.GO_CMD:
                        devData.flags.GO_CMD = true;
                        break;
                    case CmdList.WRITE_CMD:
                        devData.flags.WRITE_CMD = true;
                        break;
                    case CmdList.ERASE_CMD:
                        devData.flags.ERASE_CMD = true;
                        break;
                    case CmdList.ERASE_EXT_CMD:
                        devData.flags.ERASE_EXT_CMD = true;
                        break;
                    case CmdList.WRITE_PROTECT_CMD:
                        devData.flags.WRITE_PROTECT_CMD = true;
                        break;
                    case CmdList.WRITE_TEMP_UNPROTECT_CMD:
                        devData.flags.WRITE_TEMP_UNPROTECT_CMD = true;
                        break;
                    case CmdList.WRITE_PERM_UNPROTECT_CMD:
                        devData.flags.WRITE_PERM_UNPROTECT_CMD = true;
                        break;
                    case CmdList.READOUT_PROTECT_CMD:
                        devData.flags.READOUT_PROTECT_CMD = true;
                        break;
                    case CmdList.READOUT_TEMP_UNPROTECT_CMD:
                        devData.flags.READOUT_TEMP_UNPROTECT_CMD = true;
                        break;
                    case CmdList.READOUT_PERM_UNPROTECT_CMD:
                        devData.flags.READOUT_PERM_UNPROTECT_CMD = true;
                        break;
                }
            }
            /////////////////////
            devData.PID = PortGetID();
            byte[] startDat = await FunREADCMD(0x08000000, 4);
            if(startDat==null)
            {
                //芯片读保护
                await FunReadUnProtect();
                await Task.Delay(200);
                result = sPort.ReadBase(new byte[] { 0x7F });
                m = 3;
                while ((!result.IsSuccess) && (m > 0))
                {
                    await Task.Delay(50);
                    result = sPort.ReadBase(new byte[] { 0x7F });
                    m--;
                }
                startDat =await FunREADCMD(0x08000000, 4);
                if(startDat==null||startDat[0]!=0xff)
                {
                    devData.strStep = "擦除失败";
                    return false;
                }
            }
            byte[] FlashSizeArr=await FunREADCMD(0x1FFFF7CC, 2);
            byte[] BIDArr =await FunREADCMD(0x1FFFF6A6, 2);
            devData.strStep = "擦除数据";
            if (devData.flags.ERASE_CMD)
            {
                FunERASECMD(0xFF, null);
            }
            else if(devData.flags.ERASE_EXT_CMD)
            {
                FunERASEEXTCMD(0xFFFF, null);
                //UInt16[] edat = new UInt16[10] { 0x1E, 0x1F, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27 };
                //FunERASEEXTCMD(10, edat);
            }
            else
            {
                //不能擦除,出错
            }
            bool bflag=await FunDownBin(strBinPath);
            if(bflag)
                bflag=await FunVerify(strBinPath);
            if(bflag)
                bflag=await FunReadProtect();
            devData.dtEnd = DateTime.Now;
            int totalSec = (int)((devData.dtEnd - devData.dtStart).TotalSeconds);
            devData.strStep = "下载完成,耗时"+totalSec.ToString()+"秒";            
            return true;
        }
        static private async Task<bool> FunDownBin(string strBinPath)
        {    
            if (!File.Exists(strBinPath)) return false;
            
            BinaryReader binReader =
                new BinaryReader(File.Open(strBinPath, FileMode.Open));

            FileInfo fileInfo = new FileInfo(strBinPath);
            devData.uTotalLen = (UInt32)fileInfo.Length;
            devData.strStep = "下载数据中,请稍等...";
            try
            {
                UInt32 addr = 0x08000000;
                devData.uCurrenLen = 0;
                byte[] chunk;
                do
                {
                    // chunk.Length will be 0 if end of file is reached.
                    chunk = binReader.ReadBytes(CHUNK_SIZE);
                    if (chunk.Length > 0)
                    {
                        devData.uAddr = addr;
                        devData.uCurrenLen += (UInt32)chunk.Length;
                        bool bflag=await FunWRITECMD(addr,chunk);
                        if (!bflag) return false;
                        addr += (UInt32)chunk.Length;
                    }
                }
                while (chunk.Length > 0);

            }
            finally
            {
                binReader.Close();
            }
            return true;
        }
        static private async Task<bool> FunVerify(string strBinPath)
        {
            if (!File.Exists(strBinPath)) return false;
            BinaryReader binReader =
                new BinaryReader(File.Open(strBinPath, FileMode.Open));
            bool bflag=true;
            devData.uCurrenLen = 0;
            devData.strStep = "校验数据中,请稍等...";
            try
            {
                await Task.Delay(10);
                UInt32 addr = 0x08000000;
                byte[] chunk;
                do
                {
                    // chunk.Length will be 0 if end of file is reached.
                    chunk = binReader.ReadBytes(CHUNK_SIZE);
                    if (chunk.Length > 0)
                    {
                        devData.uAddr = addr;
                        devData.uCurrenLen += (UInt32)chunk.Length;
                        byte[] rDat =await FunREADCMD(addr, (uint)chunk.Length);
                        //bflag= rDat.Equals(chunk);
                        //if (!bflag)
                        //{
                        //    devData.errCmd =(byte)CmdList.READ_CMD;
                       //     devData.errAddr = addr;
                            //break;
                        //}
                        int k = 0;
                        while(k<chunk.Length)
                        {
                            if(rDat[k]!=chunk[k])
                            {
                                bflag = false;
                                devData.errCmd = (byte)CmdList.READ_CMD;
                                devData.uAddr = addr;
                                break;
                            }
                            k++;
                        }
                        
                        addr += (UInt32)chunk.Length;
                    }
                   
                }
                while (chunk.Length > 0);

            }
            finally
            {
                binReader.Close();
            }
            return bflag;
        }
        static private async Task<bool> FunReadProtect()
        {
            await Task.Delay(10);
            OperateResult<byte[]> result = sPort.ReadBase(new byte[] { 0x82, 0x7D });
            return true;
        }
        static private async Task<bool> FunReadUnProtect()
        {
            await Task.Delay(10);
            OperateResult<byte[]> result = sPort.ReadBase(new byte[] { 0x92, 0x6D });
            return true;
        }
        static private async Task<bool>  FunWRITECMD(UInt32 addr, byte[] buf)
        {
            if (buf == null) return false;
            
            int len = buf.Length;            
            if(len%2!=0)
            {
                len++;
                Array.Resize(ref buf, len);
                buf[len - 1] = 0xff;
            }
            //命令
            OperateResult<byte[]> result = sPort.ReadBase(new byte[] { 0x31, 0xCE });
            if (!result.IsSuccess)
            {
                return false;
            }
            byte[] rdat = result.Content;
            if (rdat == null || rdat.Length == 0 || rdat[0] != DeviceData.const_ACK)
                return false ;
            //地址
            result = sPort.ReadBase(FunIntToBytes(addr));
            if (!result.IsSuccess)
            {
                return false;
            }
            rdat = result.Content;
            if (rdat == null || rdat.Length == 0 || rdat[0] != DeviceData.const_ACK)
                return false;
            //数据
            byte[] sendDat = new byte[len+2];
            sendDat[0] = (byte)(len-1);
            Array.Copy(buf, 0, sendDat, 1, len);
            //byte checksum = sendDat[0];
            //for(int k=0;k<len;k++)
            //{
            //    checksum = (byte)(checksum ^ sendDat[1 + k]);
            //}
            //sendDat[1 + len] = checksum;
            FunCheckSum(sendDat, len+1);
            await Task.Delay(10);
            result = sPort.ReadBase(sendDat);
            return true;
        }
        static private byte FunCheckSum(byte[] dat,int len)
        {
            int tmp = 0;
            int k = 0;
            for(k=0;k<len;k++)
            {
                tmp = tmp ^ dat[k];
            }
            dat[len] = (byte)tmp;
            return (byte)tmp;
        }
        static private bool FunERASEEXTCMD(UInt16 wbSectors,UInt16[] secDat)
        {
            OperateResult<byte[]> result = sPort.ReadBase(new byte[] { 0x44, 0xBB });
            if (!result.IsSuccess)
            {
                return false;
            }
            byte[] rdat = result.Content;
            if (rdat == null || rdat.Length == 0 || rdat[0] != DeviceData.const_ACK)
                return false;
            byte[] tmp = new byte[] { 0xFF,0xFF,0x00};    // 擦除全部
            if ((wbSectors&0xFF00)!=0xFF00)
            {
                //非擦除全部
                byte checksum = 0;
                Array.Resize(ref tmp,wbSectors * 2 + 3);
                tmp[0] = (byte)((wbSectors-1) >> 8);
                tmp[1] = (byte)(wbSectors-1);
                checksum = (byte)(tmp[0] ^ tmp[1]);
                int k;
                for(k=0;k<wbSectors;k++)
                {
                    tmp[2 + k * 2] = (byte)(secDat[k] >> 8);
                    tmp[2 + k * 2 + 1] = (byte)secDat[k];
                    checksum = (byte)(checksum ^ tmp[2 + k * 2]);
                    checksum = (byte)(checksum ^ tmp[2 + k * 2+1]);
                }
                tmp[2 + 2 * wbSectors] = checksum;
            }
            result = sPort.ReadBase(tmp);

            return true;
        }
        static private bool FunERASECMD(byte nbSectors, byte[] secDat)
        {
            OperateResult<byte[]> result = sPort.ReadBase(new byte[] { 0x43, 0xBC });
            if (!result.IsSuccess)
            {
                return false;
            }
            byte[] rdat = result.Content;
            if (rdat == null || rdat.Length == 0 || rdat[0] != DeviceData.const_ACK)
                return false;
            byte[] tmp = new byte[] { 0xFF,  0x00 };
            if ((nbSectors) != 0xFF)
            {
                byte checksum = 0;
                Array.Resize(ref tmp, nbSectors  + 2);                
                tmp[0] = (byte)(nbSectors - 1);
                checksum = (byte)(tmp[0]);
                int k;
                for (k = 0; k < nbSectors; k++)
                {
                    tmp[1 + k ] = (byte)(secDat[k]);                    
                    checksum = (byte)(checksum ^ tmp[1 + k ]);                   
                }
                tmp[1 +  nbSectors] = checksum;
            }
            result = sPort.ReadBase(tmp);

            return true;
        }
        static private async Task<byte[]> FunREADCMD(UInt32 addr,UInt32 rlen)
        {
            await Task.Delay(10);
            OperateResult<byte[]> result = sPort.ReadBase(new byte[] { 0x11, 0xEE });//READ_CMD
            if (!result.IsSuccess)
            {
                return null;
            }
            byte[] dat = result.Content;
            if (dat == null || dat.Length == 0 || dat[0] != DeviceData.const_ACK)
                return null;
             result = sPort.ReadBase(FunIntToBytes(addr));
            if (!result.IsSuccess)
            {
                return null;
            }
             dat = result.Content;
            if (dat == null || dat.Length == 0 || dat[0] != DeviceData.const_ACK)
                return null;
            byte[] tmp = new byte[2];
            
            tmp[0] = (byte)(rlen-1);
            tmp[1] = 0;
            tmp[1] = (byte)(0xff - tmp[0]);
            result = sPort.ReadBase(tmp);
            if (!result.IsSuccess)
            {
                return null;
            }
            dat = result.Content;
            if (dat.Length < 2) return null;
            byte[] outDat = new byte[dat.Length-1];
            Array.Copy(dat, 1, outDat, 0, dat.Length - 1);
            return outDat;
        }
        static private byte[] FunIntToBytes(UInt32 dat)
        {
            bool b = BitConverter.IsLittleEndian;

            byte[] byteArr = BitConverter.GetBytes(dat);
            int cnt = byteArr.Length;
            int k;
            if (b)
            {
                
                for(k=0;k<cnt/4;k++)
                {
                    byte tmp = byteArr[k * 4 + 0];
                    byteArr[k * 4 + 0] = byteArr[k * 4 + 3];
                    byteArr[k * 4 + 3] = tmp;
                    tmp = byteArr[k * 4 + 1];
                    byteArr[k * 4 + 1] = byteArr[k * 4 + 2];
                    byteArr[k * 4 + 2] = tmp;
                }
            }
            Array.Resize(ref byteArr, byteArr.Length + 1);
            //int sum=0;
            
            //for (k = 0; k < cnt; k++)
            //{
            //    sum ^= byteArr[k];
            //}
            //byteArr[cnt] = (byte)sum;
            FunCheckSum(byteArr, cnt);
            return byteArr;
        }

    }
    
}
