using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBinDown
{
    public class DeviceData
    {
        public int PID;
        public CommandsFlags flags;

        public byte errCmd;
        public UInt32 uAddr;
        public UInt32 uTotalLen;
        public UInt32 uCurrenLen;
        public string strStep;
        public DateTime dtStart, dtEnd;


        public const byte const_ACK = 0x79;
        public const UInt32 ADDR_F0_OPB = 0x1FFFF800;
        public DeviceData()
        {
            flags.GET_CMD = false;
            flags.GET_VER_ROPS_CMD = false;
            flags.GET_ID_CMD = false;
            flags.SET_SPEED_CMD = false;
            flags.READ_CMD = false;
            flags.GO_CMD = false;
            flags.WRITE_CMD = false;
            flags.ERASE_CMD = false;
            flags.ERASE_EXT_CMD = false;
            flags.WRITE_PROTECT_CMD = false;
            flags.WRITE_TEMP_UNPROTECT_CMD = false;
            flags.WRITE_PERM_UNPROTECT_CMD = false;
            flags.READOUT_PROTECT_CMD = false;
            flags.READOUT_TEMP_UNPROTECT_CMD = false;
            flags.READOUT_PERM_UNPROTECT_CMD = false;
        }
    }
    public struct CommandsFlags
    {
        public bool GET_CMD; //Get the version and the allowed commands supported by the current version of the boot loader
        public bool GET_VER_ROPS_CMD; //Get the BL version and the Read Protection status of the NVM
        public bool GET_ID_CMD; //Get the chip ID
        public bool SET_SPEED_CMD; //Change the CAN baudrate
        public bool READ_CMD; //Read up to 256 bytes of memory starting from an address specified by the user
        public bool GO_CMD; //Jump to an address specified by the user to execute (a loaded) code
        public bool WRITE_CMD; //Write maximum 256 bytes to the RAM or the NVM starting from an address specified by the user
        public bool ERASE_CMD; //Erase from one to all the NVM sectors
        public bool ERASE_EXT_CMD; //Erase from one to all the NVM sectors
        public bool WRITE_PROTECT_CMD; //Enable the write protection in a permanent way for some sectors
        public bool WRITE_TEMP_UNPROTECT_CMD; //Disable the write protection in a temporary way for all NVM sectors
        public bool WRITE_PERM_UNPROTECT_CMD; //Disable the write protection in a permanent way for all NVM sectors
        public bool READOUT_PROTECT_CMD; //Enable the readout protection in a permanent way
        public bool READOUT_TEMP_UNPROTECT_CMD; //Disable the readout protection in a temporary way
        public bool READOUT_PERM_UNPROTECT_CMD; //Disable the readout protection in a permanent way
    };
    enum CmdList
    {
        INIT_CON = 0x7F,

        GET_CMD = 0x00, //Get the version and the allowed commands supported by the current version of the boot loader
        GET_VER_ROPS_CMD = 0x01, //Get the BL version and the Read Protection status of the NVM
        GET_ID_CMD = 0x02, //Get the chip ID
        SET_SPEED_CMD = 0x03, //set the new baudrate
        READ_CMD = 0x11, //Read up to 256 bytes of memory starting from an address specified by the user
        GO_CMD = 0x21, //Jump to an address specified by the user to execute (a loaded) code
        WRITE_CMD = 0x31, //Write maximum 256 bytes to the RAM or the NVM starting from an address specified by the user
        ERASE_CMD = 0x43, //Erase from one to all the NVM sectors
        ERASE_EXT_CMD = 0x44, //Erase from one to all the NVM sectors
        WRITE_PROTECT_CMD = 0x63, //Enable the write protection in a permanent way for some sectors
        WRITE_TEMP_UNPROTECT_CMD = 0x71, //Disable the write protection in a temporary way for all NVM sectors
        WRITE_PERM_UNPROTECT_CMD = 0x73, //Disable the write protection in a permanent way for all NVM sectors
        READOUT_PROTECT_CMD = 0x82, //Enable the readout protection in a permanent way
        READOUT_TEMP_UNPROTECT_CMD = 0x91, //Disable the readout protection in a temporary way
        READOUT_PERM_UNPROTECT_CMD = 0x92, //Disable the readout protection in a permanent way
    }
    
    //public  static partial class HelperPort
    //{
                        
   // }
    
}
