using System.Runtime.InteropServices;

namespace Hardware.Sdk
{
    // Token: 0x02000004 RID: 4
    public class CpuIdSdk64
    {
        // Token: 0x0600003D RID: 61
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int AddDllDirectory([MarshalAs(UnmanagedType.LPWStr)] string path);

        // Token: 0x0600003E RID: 62
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetDefaultDllDirectories(uint directoryFlags);

        // Token: 0x17000011 RID: 17
        // (get) Token: 0x0600003F RID: 63 RVA: 0x00002168 File Offset: 0x00000368
        public int SensorClassFan
        {
            get
            {
                return 12288;
            }
        }

        // Token: 0x17000012 RID: 18
        // (get) Token: 0x06000040 RID: 64 RVA: 0x0000216F File Offset: 0x0000036F
        public int SensorClassFanPwm
        {
            get
            {
                return 24576;
            }
        }

        // Token: 0x17000013 RID: 19
        // (get) Token: 0x06000041 RID: 65 RVA: 0x00002176 File Offset: 0x00000376
        public int SensorClassTemperature
        {
            get
            {
                return 8192;
            }
        }

        // Token: 0x17000014 RID: 20
        // (get) Token: 0x06000042 RID: 66 RVA: 0x0000217D File Offset: 0x0000037D
        public int SensorClassVoltage
        {
            get
            {
                return 4096;
            }
        }

        // Token: 0x17000015 RID: 21
        // (get) Token: 0x06000043 RID: 67 RVA: 0x00002184 File Offset: 0x00000384
        public int SensorClassUtilization
        {
            get
            {
                return 57344;
            }
        }

        // Token: 0x17000016 RID: 22
        // (get) Token: 0x06000044 RID: 68 RVA: 0x0000218B File Offset: 0x0000038B
        public int SensorClassLevel
        {
            get
            {
                return 49152;
            }
        }

        // Token: 0x17000017 RID: 23
        // (get) Token: 0x06000045 RID: 69 RVA: 0x00002192 File Offset: 0x00000392
        public uint ClassDeviceProcessor
        {
            get
            {
                return 4u;
            }
        }

        // Token: 0x17000018 RID: 24
        // (get) Token: 0x06000046 RID: 70 RVA: 0x00002195 File Offset: 0x00000395
        public uint ClassDeviceDrive
        {
            get
            {
                return 16u;
            }
        }

        // Token: 0x17000019 RID: 25
        // (get) Token: 0x06000047 RID: 71 RVA: 0x00002199 File Offset: 0x00000399
        public uint ClassDeviceMainboard
        {
            get
            {
                return 1024u;
            }
        }

        // Token: 0x1700001A RID: 26
        // (get) Token: 0x06000048 RID: 72 RVA: 0x000021A0 File Offset: 0x000003A0
        public uint ClassDeviceBattery
        {
            get
            {
                return 128u;
            }
        }

        // Token: 0x1700001B RID: 27
        // (get) Token: 0x06000049 RID: 73 RVA: 0x000021A7 File Offset: 0x000003A7
        public uint ClassDeviceDisplayAdapter
        {
            get
            {
                return 32u;
            }
        }

        // Token: 0x1700001C RID: 28
        // (get) Token: 0x0600004A RID: 74 RVA: 0x000021AB File Offset: 0x000003AB
        public uint ClassDeviceMemoryModule
        {
            get
            {
                return 2048u;
            }
        }

        // Token: 0x0600004B RID: 75 RVA: 0x000021B4 File Offset: 0x000003B4
        public int GetNumberOfSMBusControllers()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(374353056u);
                if (intPtr != IntPtr.Zero)
                {
                    IntPtr zero = IntPtr.Zero;
                    CPUIDSDK_fp_GetNumberOfSMBusControllers cpuidsdk_fp_GetNumberOfSMBusControllers = (CPUIDSDK_fp_GetNumberOfSMBusControllers)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfSMBusControllers));
                    result = cpuidsdk_fp_GetNumberOfSMBusControllers(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600004C RID: 76 RVA: 0x00002228 File Offset: 0x00000428
        public int GetSMBusControllerID(int _smb_controller)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3939095955u);
                if (intPtr != IntPtr.Zero)
                {
                    IntPtr zero = IntPtr.Zero;
                    CPUIDSDK_fp_GetSMBusControllerID cpuidsdk_fp_GetSMBusControllerID = (CPUIDSDK_fp_GetSMBusControllerID)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSMBusControllerID));
                    result = cpuidsdk_fp_GetSMBusControllerID(this.objptr, _smb_controller);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600004D RID: 77 RVA: 0x0000229C File Offset: 0x0000049C
        public bool ReadSMBus(int _smb_controller, byte _address, uint _reg, ref byte _pValue)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2626894118u);
                if (intPtr != IntPtr.Zero)
                {
                    IntPtr zero = IntPtr.Zero;
                    CPUIDSDK_fp_ReadSMBus cpuidsdk_fp_ReadSMBus = (CPUIDSDK_fp_ReadSMBus)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_ReadSMBus));
                    int num = cpuidsdk_fp_ReadSMBus(this.objptr, _smb_controller, _address, (int)_reg, ref _pValue);
                    if (num == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x0600004E RID: 78 RVA: 0x00002318 File Offset: 0x00000518
        public int ReadSMBusBlock(int _smb_controller, byte _address, uint _reg, byte[] _pData)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2939117150u);
                if (intPtr != IntPtr.Zero)
                {
                    IntPtr zero = IntPtr.Zero;
                    CPUIDSDK_fp_ReadSMBusBlock cpuidsdk_fp_ReadSMBusBlock = (CPUIDSDK_fp_ReadSMBusBlock)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_ReadSMBusBlock));
                    result = cpuidsdk_fp_ReadSMBusBlock(this.objptr, _smb_controller, _address, (int)_reg, _pData);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600004F RID: 79 RVA: 0x00002390 File Offset: 0x00000590
        public bool WriteSMBus(int _smb_controller, byte _address, uint _reg, byte _value)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(803102652u);
                if (intPtr != IntPtr.Zero)
                {
                    IntPtr zero = IntPtr.Zero;
                    CPUIDSDK_fp_WriteSMBus cpuidsdk_fp_WriteSMBus = (CPUIDSDK_fp_WriteSMBus)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_WriteSMBus));
                    int num = cpuidsdk_fp_WriteSMBus(this.objptr, _smb_controller, _address, (int)_reg, _value);
                    if (num == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x06000050 RID: 80 RVA: 0x0000240C File Offset: 0x0000060C
        public bool WriteSMBusMixedBytes(int _smb_controller, byte _address, byte[] _pMixedData)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(558645912u);
                if (intPtr != IntPtr.Zero)
                {
                    IntPtr zero = IntPtr.Zero;
                    CPUIDSDK_fp_WriteSMBusMixedBytes cpuidsdk_fp_WriteSMBusMixedBytes = (CPUIDSDK_fp_WriteSMBusMixedBytes)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_WriteSMBusMixedBytes));
                    int num = cpuidsdk_fp_WriteSMBusMixedBytes(this.objptr, _smb_controller, _address, _pMixedData, _pMixedData.Length / 2);
                    if (num == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public CpuIdSdk64(string szDllPath, string szDllFilename, out bool status)
        {
            status = this.InitSDK_Quick(szDllPath, szDllFilename);
        }

        // Token: 0x06000052 RID: 82 RVA: 0x00002530 File Offset: 0x00000730
        private static void SetDefaultDllDirectory(string dllPath)
        {
            try
            {
                SetDefaultDllDirectories(3072u);
                AddDllDirectory(dllPath);
            }
            catch (EntryPointNotFoundException)
            {
                string variable = "PATH";
                string environmentVariable = Environment.GetEnvironmentVariable(variable);
                if (!environmentVariable.Contains(dllPath))
                {
                    Environment.SetEnvironmentVariable(variable, dllPath + ";" + environmentVariable, EnvironmentVariableTarget.Process);
                }
            }
        }

        // Token: 0x06000053 RID: 83 RVA: 0x00002590 File Offset: 0x00000790
        public bool IsDefined(float value)
        {
            return this.IS_F_DEFINED(value);
        }

        // Token: 0x06000054 RID: 84 RVA: 0x00002599 File Offset: 0x00000799
        public bool IsDefined(double value)
        {
            return this.IS_F_DEFINED(value);
        }

        // Token: 0x06000055 RID: 85 RVA: 0x000025A2 File Offset: 0x000007A2
        public bool IsDefined(int value)
        {
            return this.IS_I_DEFINED(value);
        }

        // Token: 0x06000056 RID: 86 RVA: 0x000025AB File Offset: 0x000007AB
        public bool IsDefined(uint value)
        {
            return this.IS_I_DEFINED(value);
        }

        // Token: 0x06000057 RID: 87 RVA: 0x000025B4 File Offset: 0x000007B4
        public bool IsDefined(short value)
        {
            return this.IS_I_DEFINED(value);
        }

        // Token: 0x06000058 RID: 88 RVA: 0x000025BD File Offset: 0x000007BD
        public bool IsDefined(ushort value)
        {
            return this.IS_I_DEFINED(value);
        }

        // Token: 0x06000059 RID: 89 RVA: 0x000025C6 File Offset: 0x000007C6
        public bool IsDefined(sbyte value)
        {
            return this.IS_I_DEFINED(value);
        }

        // Token: 0x0600005A RID: 90 RVA: 0x000025CF File Offset: 0x000007CF
        public bool IsDefined(byte value)
        {
            return this.IS_I_DEFINED(value);
        }

        // Token: 0x0600005B RID: 91 RVA: 0x000025D8 File Offset: 0x000007D8
        public void GetMemorySlotsConfig(ref int _nbslots, ref int _nbusedslots, ref long _slotmap, ref int _maxmodulesize)
        {
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            this.GetMemorySlotsConfig(ref num, ref num2, ref num3, ref num4, ref num5);
            _nbslots = num;
            _nbusedslots = num2;
            _slotmap = (long)(num3 + num4);
            _maxmodulesize = num5;
        }

        // Token: 0x0600005C RID: 92 RVA: 0x00002611 File Offset: 0x00000811
        public bool IS_F_DEFINED(float _f)
        {
            return _f > 0f;
        }

        // Token: 0x0600005D RID: 93 RVA: 0x0000261E File Offset: 0x0000081E
        public bool IS_F_DEFINED(double _f)
        {
            return _f > 0.0;
        }

        // Token: 0x0600005E RID: 94 RVA: 0x0000262F File Offset: 0x0000082F
        public bool IS_I_DEFINED(int _i)
        {
            return _i != I_UNDEFINED_VALUE;
        }

        // Token: 0x0600005F RID: 95 RVA: 0x0000263C File Offset: 0x0000083C
        public bool IS_I_DEFINED(uint _i)
        {
            return _i != (uint)I_UNDEFINED_VALUE;
        }

        // Token: 0x06000060 RID: 96 RVA: 0x00002649 File Offset: 0x00000849
        public bool IS_I_DEFINED(short _i)
        {
            return _i != (short)I_UNDEFINED_VALUE;
        }

        // Token: 0x06000061 RID: 97 RVA: 0x00002657 File Offset: 0x00000857
        public bool IS_I_DEFINED(ushort _i)
        {
            return _i != (ushort)I_UNDEFINED_VALUE;
        }

        // Token: 0x06000062 RID: 98 RVA: 0x00002665 File Offset: 0x00000865
        public bool IS_I_DEFINED(sbyte _i)
        {
            return _i != (sbyte)I_UNDEFINED_VALUE;
        }

        // Token: 0x06000063 RID: 99 RVA: 0x00002673 File Offset: 0x00000873
        public bool IS_I_DEFINED(byte _i)
        {
            return _i != (byte)I_UNDEFINED_VALUE;
        }

        // Token: 0x1700001D RID: 29
        // (get) Token: 0x06000064 RID: 100 RVA: 0x00002681 File Offset: 0x00000881
        public string DllFileName
        {
            get
            {
                return "cpuidsdk64.dll";
            }
        }

        // Token: 0x1700001E RID: 30
        // (get) Token: 0x06000065 RID: 101 RVA: 0x00002688 File Offset: 0x00000888
        public string DllBitRate
        {
            get
            {
                return "64-bit";
            }
        }

        // Token: 0x06000066 RID: 102
        [DllImport("cpuidsdk64.dll", EntryPoint = "QueryInterface")]
        protected static extern IntPtr CPUIDSDK_fp_QueryInterface(uint _code);

        // Token: 0x06000067 RID: 103 RVA: 0x00002690 File Offset: 0x00000890
        public bool CreateInstance()
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1522578817u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_CreateInstance cpuidsdk_fp_CreateInstance = (CPUIDSDK_fp_CreateInstance)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_CreateInstance));
                    this.objptr = cpuidsdk_fp_CreateInstance();
                    if (this.objptr != IntPtr.Zero)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    this.objptr = IntPtr.Zero;
                    result = false;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = false;
            }
            return result;
        }

        // Token: 0x06000068 RID: 104 RVA: 0x00002720 File Offset: 0x00000920
        public void DestroyInstance()
        {
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(460994292u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_DestroyInstance cpuidsdk_fp_DestroyInstance = (CPUIDSDK_fp_DestroyInstance)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_DestroyInstance));
                    cpuidsdk_fp_DestroyInstance(this.objptr);
                }
            }
            catch
            {
            }
        }

        public bool InitSDK_Quick(string szDllPath, string szDllFilename)
        {
            this.CreateInstance();

            int num = 0;
            int num2 = 0;

            // use custom config to accelerate cpuid module startup
            uint config = CPUIDSDK_CONFIG_USE_EVERYTHING;
            config ^= CPUIDSDK_CONFIG_USE_ACPI_TIMER;
            config ^= CPUIDSDK_CONFIG_USE_CHIPSET;
            config ^= CPUIDSDK_CONFIG_USE_DISPLAY_API;
            config ^= CPUIDSDK_CONFIG_USE_DMI;
            config ^= CPUIDSDK_CONFIG_USE_SMBUS;
            config ^= CPUIDSDK_CONFIG_USE_SOFTWARE;
            config ^= CPUIDSDK_CONFIG_USE_SPD;

            return this.Init(szDllPath, szDllFilename, config, ref num, ref num2);
        }

        // Token: 0x06000069 RID: 105 RVA: 0x0000277C File Offset: 0x0000097C
        public bool Init(string _szDllPath, string _szDllFilename, uint _config_flag, ref int _errorcode, ref int _extended_errorcode)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1423354285u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_Init cpuidsdk_fp_Init = (CPUIDSDK_fp_Init)Marshal.GetDelegateForFunctionPointer(
                        intPtr, 
                        typeof(CPUIDSDK_fp_Init)
                    );
                    
                    int num = cpuidsdk_fp_Init(
                        this.objptr,
                        _szDllPath,
                        _szDllFilename,
                        (int)_config_flag,
                        ref _errorcode, 
                        ref _extended_errorcode
                    );

                    if (num == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    _errorcode = 16;
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public void UninitSDK()
        {
            this.Close();
            this.DestroyInstance();
        }

        // Token: 0x0600006A RID: 106 RVA: 0x000027F8 File Offset: 0x000009F8
        public void Close()
        {
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3600133419u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_Close cpuidsdk_fp_Close = (CPUIDSDK_fp_Close)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_Close));
                    cpuidsdk_fp_Close(this.objptr);
                }
            }
            catch
            {
            }
        }

        // Token: 0x0600006B RID: 107 RVA: 0x00002854 File Offset: 0x00000A54
        public void RefreshInformation()
        {
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3280963359u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_RefreshInformation cpuidsdk_fp_RefreshInformation = (CPUIDSDK_fp_RefreshInformation)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_RefreshInformation));
                    cpuidsdk_fp_RefreshInformation(this.objptr);
                }
            }
            catch
            {
            }
        }

        // Token: 0x0600006C RID: 108 RVA: 0x000028B0 File Offset: 0x00000AB0
        public void GetDllVersion(ref int _version)
        {
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1084522825u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDllVersion cpuidsdk_fp_GetDllVersion = (CPUIDSDK_fp_GetDllVersion)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDllVersion));
                    cpuidsdk_fp_GetDllVersion(this.objptr, ref _version);
                }
            }
            catch
            {
            }
        }

        // Token: 0x0600006D RID: 109 RVA: 0x00002910 File Offset: 0x00000B10
        public int GetNumberOfProcessors()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2118057085u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetNbProcessors cpuidsdk_fp_GetNbProcessors = (CPUIDSDK_fp_GetNbProcessors)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNbProcessors));
                    result = cpuidsdk_fp_GetNbProcessors(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600006E RID: 110 RVA: 0x0000297C File Offset: 0x00000B7C
        public int GetProcessorFamily(int _proc_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4013678199u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorFamily cpuidsdk_fp_GetProcessorFamily = (CPUIDSDK_fp_GetProcessorFamily)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorFamily));
                    result = cpuidsdk_fp_GetProcessorFamily(this.objptr, _proc_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600006F RID: 111 RVA: 0x000029EC File Offset: 0x00000BEC
        public int GetProcessorCoreCount(int _proc_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(671633424u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorCoreCount cpuidsdk_fp_GetProcessorCoreCount = (CPUIDSDK_fp_GetProcessorCoreCount)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCoreCount));
                    result = cpuidsdk_fp_GetProcessorCoreCount(this.objptr, _proc_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x06000070 RID: 112 RVA: 0x00002A5C File Offset: 0x00000C5C
        public int GetProcessorThreadCount(int _proc_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2155282670u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorThreadCount cpuidsdk_fp_GetProcessorThreadCount = (CPUIDSDK_fp_GetProcessorThreadCount)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorThreadCount));
                    result = cpuidsdk_fp_GetProcessorThreadCount(this.objptr, _proc_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x06000071 RID: 113 RVA: 0x00002ACC File Offset: 0x00000CCC
        public int GetProcessorCoreThreadCount(int _proc_index, int _core_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1983310957u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorCoreThreadCount cpuidsdk_fp_GetProcessorCoreThreadCount = (CPUIDSDK_fp_GetProcessorCoreThreadCount)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCoreThreadCount));
                    result = cpuidsdk_fp_GetProcessorCoreThreadCount(this.objptr, _proc_index, _core_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x06000072 RID: 114 RVA: 0x00002B3C File Offset: 0x00000D3C
        public int GetProcessorThreadAPICID(IntPtr objptr, int _proc_index, int _core_index, int _thread_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2523081926u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorThreadAPICID cpuidsdk_fp_GetProcessorThreadAPICID = (CPUIDSDK_fp_GetProcessorThreadAPICID)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorThreadAPICID));
                    result = cpuidsdk_fp_GetProcessorThreadAPICID(objptr, _proc_index, _core_index, _thread_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x06000073 RID: 115 RVA: 0x00002BA8 File Offset: 0x00000DA8
        public string GetProcessorName(int _proc_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3676943955u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorName cpuidsdk_fp_GetProcessorName = (CPUIDSDK_fp_GetProcessorName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorName));
                    IntPtr ptr = cpuidsdk_fp_GetProcessorName(this.objptr, _proc_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x06000074 RID: 116 RVA: 0x00002C20 File Offset: 0x00000E20
        public string GetProcessorCodeName(int _proc_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3562121379u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorCodeName cpuidsdk_fp_GetProcessorCodeName = (CPUIDSDK_fp_GetProcessorCodeName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCodeName));
                    IntPtr ptr = cpuidsdk_fp_GetProcessorCodeName(this.objptr, _proc_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x06000075 RID: 117 RVA: 0x00002C98 File Offset: 0x00000E98
        public string GetProcessorSpecification(int _proc_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1422436753u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorSpecification cpuidsdk_fp_GetProcessorSpecification = (CPUIDSDK_fp_GetProcessorSpecification)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorSpecification));
                    IntPtr ptr = cpuidsdk_fp_GetProcessorSpecification(this.objptr, _proc_index);
                    result = Marshal.PtrToStringAnsi(ptr);
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x06000076 RID: 118 RVA: 0x00002D04 File Offset: 0x00000F04
        public string GetProcessorPackage(int _proc_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2045703133u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorPackage cpuidsdk_fp_GetProcessorPackage = (CPUIDSDK_fp_GetProcessorPackage)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorPackage));
                    IntPtr ptr = cpuidsdk_fp_GetProcessorPackage(this.objptr, _proc_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x06000077 RID: 119 RVA: 0x00002D7C File Offset: 0x00000F7C
        public string GetProcessorStepping(int _proc_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2277576578u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorStepping cpuidsdk_fp_GetProcessorStepping = (CPUIDSDK_fp_GetProcessorStepping)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorStepping));
                    IntPtr ptr = cpuidsdk_fp_GetProcessorStepping(this.objptr, _proc_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x06000078 RID: 120 RVA: 0x00002DF4 File Offset: 0x00000FF4
        public float GetProcessorTDP(int _proc_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3516113703u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorTDP cpuidsdk_fp_GetProcessorTDP = (CPUIDSDK_fp_GetProcessorTDP)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorTDP));
                    result = cpuidsdk_fp_GetProcessorTDP(this.objptr, _proc_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x06000079 RID: 121 RVA: 0x00002E64 File Offset: 0x00001064
        public float GetProcessorManufacturingProcess(int _proc_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3945256527u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorManufacturingProcess cpuidsdk_fp_GetProcessorManufacturingProcess = (CPUIDSDK_fp_GetProcessorManufacturingProcess)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorManufacturingProcess));
                    result = cpuidsdk_fp_GetProcessorManufacturingProcess(this.objptr, _proc_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600007A RID: 122 RVA: 0x00002ED4 File Offset: 0x000010D4
        public bool IsProcessorInstructionSetAvailable(int _proc_index, int _iset)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3574442523u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_IsProcessorInstructionSetAvailable cpuidsdk_fp_IsProcessorInstructionSetAvailable = (CPUIDSDK_fp_IsProcessorInstructionSetAvailable)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_IsProcessorInstructionSetAvailable));
                    int num = cpuidsdk_fp_IsProcessorInstructionSetAvailable(this.objptr, _proc_index, _iset);
                    if (num == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x0600007B RID: 123 RVA: 0x00002F44 File Offset: 0x00001144
        public float GetProcessorCoreClockFrequency(int _proc_index, int _core_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1993797037u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorCoreClockFrequency cpuidsdk_fp_GetProcessorCoreClockFrequency = (CPUIDSDK_fp_GetProcessorCoreClockFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCoreClockFrequency));
                    result = cpuidsdk_fp_GetProcessorCoreClockFrequency(this.objptr, _proc_index, _core_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600007C RID: 124 RVA: 0x00002FB4 File Offset: 0x000011B4
        public float GetProcessorCoreClockMultiplier(int _proc_index, int _core_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(796024548u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorCoreClockMultiplier cpuidsdk_fp_GetProcessorCoreClockMultiplier = (CPUIDSDK_fp_GetProcessorCoreClockMultiplier)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCoreClockMultiplier));
                    result = cpuidsdk_fp_GetProcessorCoreClockMultiplier(this.objptr, _proc_index, _core_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600007D RID: 125 RVA: 0x00003024 File Offset: 0x00001224
        public float GetProcessorCoreTemperature(int _proc_index, int _core_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4004502879u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorCoreTemperature cpuidsdk_fp_GetProcessorCoreTemperature = (CPUIDSDK_fp_GetProcessorCoreTemperature)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCoreTemperature));
                    result = cpuidsdk_fp_GetProcessorCoreTemperature(this.objptr, _proc_index, _core_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600007E RID: 126 RVA: 0x00003094 File Offset: 0x00001294
        public float GetBusFrequency()
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4231526511u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetBusFrequency cpuidsdk_fp_GetBusFrequency = (CPUIDSDK_fp_GetBusFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetBusFrequency));
                    result = cpuidsdk_fp_GetBusFrequency(this.objptr);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600007F RID: 127 RVA: 0x00003104 File Offset: 0x00001304
        public float GetProcessorRatedBusFrequency(int _proc_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2634889754u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorRatedBusFrequency cpuidsdk_fp_GetProcessorRatedBusFrequency = (CPUIDSDK_fp_GetProcessorRatedBusFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorRatedBusFrequency));
                    result = cpuidsdk_fp_GetProcessorRatedBusFrequency(this.objptr, _proc_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x06000080 RID: 128 RVA: 0x00003174 File Offset: 0x00001374
        public float GetProcessorStockClockFrequency(int _proc_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2631088550u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorStockClockFrequency cpuidsdk_fp_GetProcessorStockClockFrequency = (CPUIDSDK_fp_GetProcessorStockClockFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorStockClockFrequency));
                    result = (float)cpuidsdk_fp_GetProcessorStockClockFrequency(this.objptr, _proc_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x06000081 RID: 129 RVA: 0x000031E4 File Offset: 0x000013E4
        public float GetProcessorStockBusFrequency(int _proc_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3532236051u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorStockBusFrequency cpuidsdk_fp_GetProcessorStockBusFrequency = (CPUIDSDK_fp_GetProcessorStockBusFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorStockBusFrequency));
                    result = (float)cpuidsdk_fp_GetProcessorStockBusFrequency(this.objptr, _proc_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x06000082 RID: 130 RVA: 0x00003254 File Offset: 0x00001454
        public int GetProcessorMaxCacheLevel(int _proc_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1724042629u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorMaxCacheLevel cpuidsdk_fp_GetProcessorMaxCacheLevel = (CPUIDSDK_fp_GetProcessorMaxCacheLevel)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorMaxCacheLevel));
                    result = cpuidsdk_fp_GetProcessorMaxCacheLevel(this.objptr, _proc_index);
                }
                else
                {
                    result = 0;
                }
            }
            catch
            {
                result = 0;
            }
            return result;
        }

        // Token: 0x06000083 RID: 131 RVA: 0x000032BC File Offset: 0x000014BC
        public void GetProcessorCacheParameters(int _proc_index, int _cache_level, int _cache_type, ref int _NbCaches, ref int _size)
        {
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3835676991u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorCacheParameters cpuidsdk_fp_GetProcessorCacheParameters = (CPUIDSDK_fp_GetProcessorCacheParameters)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorCacheParameters));
                    cpuidsdk_fp_GetProcessorCacheParameters(this.objptr, _proc_index, _cache_level, _cache_type, ref _NbCaches, ref _size);
                }
            }
            catch
            {
            }
        }

        // Token: 0x06000084 RID: 132 RVA: 0x00003320 File Offset: 0x00001520
        public uint GetProcessorID(int _proc_index)
        {
            uint result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1210224709u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorID cpuidsdk_fp_GetProcessorID = (CPUIDSDK_fp_GetProcessorID)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorID));
                    result = (uint)cpuidsdk_fp_GetProcessorID(this.objptr, _proc_index);
                }
                else
                {
                    result = 0u;
                }
            }
            catch
            {
                result = 0u;
            }
            return result;
        }

        // Token: 0x06000085 RID: 133 RVA: 0x00003388 File Offset: 0x00001588
        public float GetProcessorVoltageID(int _proc_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3508118067u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorVoltageID cpuidsdk_fp_GetProcessorVoltageID = (CPUIDSDK_fp_GetProcessorVoltageID)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorVoltageID));
                    result = cpuidsdk_fp_GetProcessorVoltageID(this.objptr, _proc_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x06000086 RID: 134 RVA: 0x000033F8 File Offset: 0x000015F8
        public int GetMemoryType()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(84150792u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMemoryType cpuidsdk_fp_GetMemoryType = (CPUIDSDK_fp_GetMemoryType)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryType));
                    result = cpuidsdk_fp_GetMemoryType(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x06000087 RID: 135 RVA: 0x00003464 File Offset: 0x00001664
        public int GetMemorySize()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(674254944u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMemorySize cpuidsdk_fp_GetMemorySize = (CPUIDSDK_fp_GetMemorySize)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemorySize));
                    result = cpuidsdk_fp_GetMemorySize(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x06000088 RID: 136 RVA: 0x000034D0 File Offset: 0x000016D0
        public float GetMemoryClockFrequency()
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2808565454u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMemoryClockFrequency cpuidsdk_fp_GetMemoryClockFrequency = (CPUIDSDK_fp_GetMemoryClockFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryClockFrequency));
                    result = cpuidsdk_fp_GetMemoryClockFrequency(this.objptr);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x06000089 RID: 137 RVA: 0x00003540 File Offset: 0x00001740
        public int GetMemoryNumberOfChannels()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1567537885u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMemoryNumberOfChannels cpuidsdk_fp_GetMemoryNumberOfChannels = (CPUIDSDK_fp_GetMemoryNumberOfChannels)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryNumberOfChannels));
                    result = cpuidsdk_fp_GetMemoryNumberOfChannels(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600008A RID: 138 RVA: 0x000035AC File Offset: 0x000017AC
        public float GetMemoryCASLatency()
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(332277660u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMemoryCASLatency cpuidsdk_fp_GetMemoryCASLatency = (CPUIDSDK_fp_GetMemoryCASLatency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryCASLatency));
                    result = cpuidsdk_fp_GetMemoryCASLatency(this.objptr);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600008B RID: 139 RVA: 0x0000361C File Offset: 0x0000181C
        public int GetMemoryRAStoCASDelay()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3940931019u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMemoryRAStoCASDelay cpuidsdk_fp_GetMemoryRAStoCASDelay = (CPUIDSDK_fp_GetMemoryRAStoCASDelay)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryRAStoCASDelay));
                    result = cpuidsdk_fp_GetMemoryRAStoCASDelay(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600008C RID: 140 RVA: 0x00003688 File Offset: 0x00001888
        public int GetMemoryRASPrecharge()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4060603407u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMemoryRASPrecharge cpuidsdk_fp_GetMemoryRASPrecharge = (CPUIDSDK_fp_GetMemoryRASPrecharge)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryRASPrecharge));
                    result = cpuidsdk_fp_GetMemoryRASPrecharge(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600008D RID: 141 RVA: 0x000036F4 File Offset: 0x000018F4
        public int GetMemoryTRAS()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2371689146u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMemoryTRAS cpuidsdk_fp_GetMemoryTRAS = (CPUIDSDK_fp_GetMemoryTRAS)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryTRAS));
                    result = cpuidsdk_fp_GetMemoryTRAS(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600008E RID: 142 RVA: 0x00003760 File Offset: 0x00001960
        public int GetMemoryTRC()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3567495495u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMemoryTRC cpuidsdk_fp_GetMemoryTRC = (CPUIDSDK_fp_GetMemoryTRC)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryTRC));
                    result = cpuidsdk_fp_GetMemoryTRC(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x0600008F RID: 143 RVA: 0x000037CC File Offset: 0x000019CC
        public int GetMemoryCommandRate()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2177958818u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMemoryCommandRate cpuidsdk_fp_GetMemoryCommandRate = (CPUIDSDK_fp_GetMemoryCommandRate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryCommandRate));
                    result = cpuidsdk_fp_GetMemoryCommandRate(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x06000090 RID: 144 RVA: 0x00003838 File Offset: 0x00001A38
        public string GetNorthBridgeVendor()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2889177194u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetNorthBridgeVendor cpuidsdk_fp_GetNorthBridgeVendor = (CPUIDSDK_fp_GetNorthBridgeVendor)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNorthBridgeVendor));
                    IntPtr ptr = cpuidsdk_fp_GetNorthBridgeVendor(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x06000091 RID: 145 RVA: 0x000038B0 File Offset: 0x00001AB0
        public string GetNorthBridgeModel()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1325964817u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetNorthBridgeModel cpuidsdk_fp_GetNorthBridgeModel = (CPUIDSDK_fp_GetNorthBridgeModel)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNorthBridgeModel));
                    IntPtr ptr = cpuidsdk_fp_GetNorthBridgeModel(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x06000092 RID: 146 RVA: 0x00003928 File Offset: 0x00001B28
        public string GetNorthBridgeRevision()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4225890243u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetNorthBridgeRevision cpuidsdk_fp_GetNorthBridgeRevision = (CPUIDSDK_fp_GetNorthBridgeRevision)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNorthBridgeRevision));
                    IntPtr ptr = cpuidsdk_fp_GetNorthBridgeRevision(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x06000093 RID: 147 RVA: 0x000039A0 File Offset: 0x00001BA0
        public string GetSouthBridgeVendor()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1310628925u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSouthBridgeVendor cpuidsdk_fp_GetSouthBridgeVendor = (CPUIDSDK_fp_GetSouthBridgeVendor)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSouthBridgeVendor));
                    IntPtr ptr = cpuidsdk_fp_GetSouthBridgeVendor(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x06000094 RID: 148 RVA: 0x00003A18 File Offset: 0x00001C18
        public string GetSouthBridgeModel()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2644196150u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSouthBridgeModel cpuidsdk_fp_GetSouthBridgeModel = (CPUIDSDK_fp_GetSouthBridgeModel)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSouthBridgeModel));
                    IntPtr ptr = cpuidsdk_fp_GetSouthBridgeModel(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x06000095 RID: 149 RVA: 0x00003A90 File Offset: 0x00001C90
        public string GetSouthBridgeRevision()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(993818232u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSouthBridgeRevision cpuidsdk_fp_GetSouthBridgeRevision = (CPUIDSDK_fp_GetSouthBridgeRevision)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSouthBridgeRevision));
                    IntPtr ptr = cpuidsdk_fp_GetSouthBridgeRevision(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x06000096 RID: 150 RVA: 0x00003B08 File Offset: 0x00001D08
        public void GetMemorySlotsConfig(ref int _nbslots, ref int _nbusedslots, ref int _slotmap_h, ref int _slotmap_l, ref int _maxmodulesize)
        {
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1246401685u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMemorySlotsConfig cpuidsdk_fp_GetMemorySlotsConfig = (CPUIDSDK_fp_GetMemorySlotsConfig)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemorySlotsConfig));
                    cpuidsdk_fp_GetMemorySlotsConfig(this.objptr, ref _nbslots, ref _nbusedslots, ref _slotmap_h, ref _slotmap_l, ref _maxmodulesize);
                }
            }
            catch
            {
            }
        }

        // Token: 0x06000097 RID: 151 RVA: 0x00003B6C File Offset: 0x00001D6C
        public string GetBIOSVendor()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3911176767u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetBIOSVendor cpuidsdk_fp_GetBIOSVendor = (CPUIDSDK_fp_GetBIOSVendor)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetBIOSVendor));
                    IntPtr ptr = cpuidsdk_fp_GetBIOSVendor(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x06000098 RID: 152 RVA: 0x00003BE4 File Offset: 0x00001DE4
        public string GetBIOSVersion()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1018984824u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetBIOSVersion cpuidsdk_fp_GetBIOSVersion = (CPUIDSDK_fp_GetBIOSVersion)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetBIOSVersion));
                    IntPtr ptr = cpuidsdk_fp_GetBIOSVersion(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x06000099 RID: 153 RVA: 0x00003C5C File Offset: 0x00001E5C
        public string GetBIOSDate()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(581453136u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetBIOSDate cpuidsdk_fp_GetBIOSDate = (CPUIDSDK_fp_GetBIOSDate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetBIOSDate));
                    IntPtr ptr = cpuidsdk_fp_GetBIOSDate(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x0600009A RID: 154 RVA: 0x00003CD4 File Offset: 0x00001ED4
        public string GetMainboardVendor()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(507395196u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMainboardVendor cpuidsdk_fp_GetMainboardVendor = (CPUIDSDK_fp_GetMainboardVendor)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMainboardVendor));
                    IntPtr ptr = cpuidsdk_fp_GetMainboardVendor(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x0600009B RID: 155 RVA: 0x00003D4C File Offset: 0x00001F4C
        public string GetMainboardModel()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(974943288u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMainboardModel cpuidsdk_fp_GetMainboardModel = (CPUIDSDK_fp_GetMainboardModel)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMainboardModel));
                    IntPtr ptr = cpuidsdk_fp_GetMainboardModel(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x0600009C RID: 156 RVA: 0x00003DC4 File Offset: 0x00001FC4
        public string GetMainboardRevision()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(937979856u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMainboardRevision cpuidsdk_fp_GetMainboardRevision = (CPUIDSDK_fp_GetMainboardRevision)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMainboardRevision));
                    IntPtr ptr = cpuidsdk_fp_GetMainboardRevision(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x0600009D RID: 157 RVA: 0x00003E3C File Offset: 0x0000203C
        public string GetMainboardSerialNumber()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(377761032u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMainboardSerialNumber cpuidsdk_fp_GetMainboardSerialNumber = (CPUIDSDK_fp_GetMainboardSerialNumber)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMainboardSerialNumber));
                    IntPtr ptr = cpuidsdk_fp_GetMainboardSerialNumber(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x0600009E RID: 158 RVA: 0x00003EB4 File Offset: 0x000020B4
        public string GetSystemManufacturer()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1399236301u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSystemManufacturer cpuidsdk_fp_GetSystemManufacturer = (CPUIDSDK_fp_GetSystemManufacturer)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSystemManufacturer));
                    IntPtr ptr = cpuidsdk_fp_GetSystemManufacturer(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x0600009F RID: 159 RVA: 0x00003F2C File Offset: 0x0000212C
        public string GetSystemProductName()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1858133377u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSystemProductName cpuidsdk_fp_GetSystemProductName = (CPUIDSDK_fp_GetSystemProductName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSystemProductName));
                    IntPtr ptr = cpuidsdk_fp_GetSystemProductName(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000A0 RID: 160 RVA: 0x00003FA4 File Offset: 0x000021A4
        public string GetSystemVersion()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1637532469u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSystemVersion cpuidsdk_fp_GetSystemVersion = (CPUIDSDK_fp_GetSystemVersion)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSystemVersion));
                    IntPtr ptr = cpuidsdk_fp_GetSystemVersion(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000A1 RID: 161 RVA: 0x0000401C File Offset: 0x0000221C
        public string GetSystemSerialNumber()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2531732942u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSystemSerialNumber cpuidsdk_fp_GetSystemSerialNumber = (CPUIDSDK_fp_GetSystemSerialNumber)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSystemSerialNumber));
                    IntPtr ptr = cpuidsdk_fp_GetSystemSerialNumber(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000A2 RID: 162 RVA: 0x00004094 File Offset: 0x00002294
        public string GetSystemUUID()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1684719829u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSystemUUID cpuidsdk_fp_GetSystemUUID = (CPUIDSDK_fp_GetSystemUUID)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSystemUUID));
                    IntPtr ptr = cpuidsdk_fp_GetSystemUUID(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000A3 RID: 163 RVA: 0x0000410C File Offset: 0x0000230C
        public string GetChassisManufacturer()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1121879485u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetChassisManufacturer cpuidsdk_fp_GetChassisManufacturer = (CPUIDSDK_fp_GetChassisManufacturer)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetChassisManufacturer));
                    IntPtr ptr = cpuidsdk_fp_GetChassisManufacturer(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000A4 RID: 164 RVA: 0x00004184 File Offset: 0x00002384
        public string GetChassisType()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(343681272u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetChassisType cpuidsdk_fp_GetChassisType = (CPUIDSDK_fp_GetChassisType)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetChassisType));
                    IntPtr ptr = cpuidsdk_fp_GetChassisType(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000A5 RID: 165 RVA: 0x000041FC File Offset: 0x000023FC
        public string GetChassisSerialNumber()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3622678491u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetChassisSerialNumber cpuidsdk_fp_GetChassisSerialNumber = (CPUIDSDK_fp_GetChassisSerialNumber)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetChassisSerialNumber));
                    IntPtr ptr = cpuidsdk_fp_GetChassisSerialNumber(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000A6 RID: 166 RVA: 0x00004274 File Offset: 0x00002474
        public bool GetMemoryInfosExt(ref string _szLocation, ref string _szUsage, ref string _szCorrection)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2330662358u);
                if (intPtr != IntPtr.Zero)
                {
                    IntPtr zero = IntPtr.Zero;
                    IntPtr zero2 = IntPtr.Zero;
                    IntPtr zero3 = IntPtr.Zero;
                    CPUIDSDK_fp_GetMemoryInfosExt cpuidsdk_fp_GetMemoryInfosExt = (CPUIDSDK_fp_GetMemoryInfosExt)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryInfosExt));
                    int num = cpuidsdk_fp_GetMemoryInfosExt(this.objptr, ref zero, ref zero2, ref zero3);
                    if (num == 1)
                    {
                        _szLocation = Marshal.PtrToStringAnsi(zero);
                        Marshal.FreeBSTR(zero);
                        _szUsage = Marshal.PtrToStringAnsi(zero2);
                        Marshal.FreeBSTR(zero2);
                        _szCorrection = Marshal.PtrToStringAnsi(zero3);
                        Marshal.FreeBSTR(zero3);
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x060000A7 RID: 167 RVA: 0x00004330 File Offset: 0x00002530
        public int GetNumberOfMemoryDevices()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3261170883u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetNumberOfMemoryDevices cpuidsdk_fp_GetNumberOfMemoryDevices = (CPUIDSDK_fp_GetNumberOfMemoryDevices)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfMemoryDevices));
                    result = cpuidsdk_fp_GetNumberOfMemoryDevices(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000A8 RID: 168 RVA: 0x0000439C File Offset: 0x0000259C
        public bool GetMemoryDeviceInfos(int _device_index, ref int _size, ref string _szFormat)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1769263849u);
                if (intPtr != IntPtr.Zero)
                {
                    IntPtr zero = IntPtr.Zero;
                    CPUIDSDK_fp_GetMemoryDeviceInfos cpuidsdk_fp_GetMemoryDeviceInfos = (CPUIDSDK_fp_GetMemoryDeviceInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryDeviceInfos));
                    int num = cpuidsdk_fp_GetMemoryDeviceInfos(this.objptr, _device_index, ref _size, ref zero);
                    if (num == 1)
                    {
                        _szFormat = Marshal.PtrToStringAnsi(zero);
                        Marshal.FreeBSTR(zero);
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x060000A9 RID: 169 RVA: 0x00004428 File Offset: 0x00002628
        public bool GetMemoryDeviceInfosExt(int _device_index, ref string _szDesignation, ref string _szType, ref int _total_width, ref int _data_width, ref int _speed)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2706981554u);
                if (intPtr != IntPtr.Zero)
                {
                    IntPtr zero = IntPtr.Zero;
                    IntPtr zero2 = IntPtr.Zero;
                    CPUIDSDK_fp_GetMemoryDeviceInfosExt cpuidsdk_fp_GetMemoryDeviceInfosExt = (CPUIDSDK_fp_GetMemoryDeviceInfosExt)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMemoryDeviceInfosExt));
                    int num = cpuidsdk_fp_GetMemoryDeviceInfosExt(this.objptr, _device_index, ref zero, ref zero2, ref _total_width, ref _data_width, ref _speed);
                    if (num == 1)
                    {
                        _szDesignation = Marshal.PtrToStringAnsi(zero);
                        Marshal.FreeBSTR(zero);
                        _szType = Marshal.PtrToStringAnsi(zero2);
                        Marshal.FreeBSTR(zero2);
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x060000AA RID: 170 RVA: 0x000044D0 File Offset: 0x000026D0
        public int GetProcessorSockets()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1182436597u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetProcessorSockets cpuidsdk_fp_GetProcessorSockets = (CPUIDSDK_fp_GetProcessorSockets)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetProcessorSockets));
                    result = cpuidsdk_fp_GetProcessorSockets(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000AB RID: 171 RVA: 0x0000453C File Offset: 0x0000273C
        public string GetSystemSKU()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3935556903u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSystemSKU cpuidsdk_fp_GetSystemSKU = (CPUIDSDK_fp_GetSystemSKU)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSystemSKU));
                    IntPtr ptr = cpuidsdk_fp_GetSystemSKU(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000AC RID: 172 RVA: 0x000045B4 File Offset: 0x000027B4
        public string GetSystemFamily()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2339706602u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSystemFamily cpuidsdk_fp_GetSystemFamily = (CPUIDSDK_fp_GetSystemFamily)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSystemFamily));
                    IntPtr ptr = cpuidsdk_fp_GetSystemFamily(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000AD RID: 173 RVA: 0x0000462C File Offset: 0x0000282C
        public int GetNumberOfSPDModules()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3137566214u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetNumberOfSPDModules cpuidsdk_fp_GetNumberOfSPDModules = (CPUIDSDK_fp_GetNumberOfSPDModules)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfSPDModules));
                    result = cpuidsdk_fp_GetNumberOfSPDModules(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000AE RID: 174 RVA: 0x00004698 File Offset: 0x00002898
        public int GetSPDModuleType(int _spd_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3673667055u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleType cpuidsdk_fp_GetSPDModuleType = (CPUIDSDK_fp_GetSPDModuleType)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleType));
                    result = cpuidsdk_fp_GetSPDModuleType(this.objptr, _spd_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000AF RID: 175 RVA: 0x00004708 File Offset: 0x00002908
        public int GetSPDModuleSize(int _spd_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2245594034u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleSize cpuidsdk_fp_GetSPDModuleSize = (CPUIDSDK_fp_GetSPDModuleSize)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleSize));
                    result = cpuidsdk_fp_GetSPDModuleSize(this.objptr, _spd_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000B0 RID: 176 RVA: 0x00004778 File Offset: 0x00002978
        public string GetSPDModuleFormat(int _spd_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1495183933u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleFormat cpuidsdk_fp_GetSPDModuleFormat = (CPUIDSDK_fp_GetSPDModuleFormat)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleFormat));
                    IntPtr ptr = cpuidsdk_fp_GetSPDModuleFormat(this.objptr, _spd_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000B1 RID: 177 RVA: 0x000047F0 File Offset: 0x000029F0
        public string GetSPDModuleManufacturer(int _spd_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2325812546u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleManufacturer cpuidsdk_fp_GetSPDModuleManufacturer = (CPUIDSDK_fp_GetSPDModuleManufacturer)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleManufacturer));
                    IntPtr ptr = cpuidsdk_fp_GetSPDModuleManufacturer(this.objptr, _spd_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000B2 RID: 178 RVA: 0x00004868 File Offset: 0x00002A68
        public bool GetSPDModuleManufacturerID(int _spd_index, byte[] _id)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1985277097u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleManufacturerID cpuidsdk_fp_GetSPDModuleManufacturerID = (CPUIDSDK_fp_GetSPDModuleManufacturerID)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleManufacturerID));
                    int num = cpuidsdk_fp_GetSPDModuleManufacturerID(this.objptr, _spd_index, _id);
                    if (num == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x060000B3 RID: 179 RVA: 0x000048D8 File Offset: 0x00002AD8
        public string GetSPDModuleDRAMManufacturer(int _spd_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(638864424u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleDRAMManufacturer cpuidsdk_fp_GetSPDModuleDRAMManufacturer = (CPUIDSDK_fp_GetSPDModuleDRAMManufacturer)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleDRAMManufacturer));
                    IntPtr ptr = cpuidsdk_fp_GetSPDModuleDRAMManufacturer(this.objptr, _spd_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000B4 RID: 180 RVA: 0x00004950 File Offset: 0x00002B50
        public int GetSPDModuleMaxFrequency(int _spd_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2758494422u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleMaxFrequency cpuidsdk_fp_GetSPDModuleMaxFrequency = (CPUIDSDK_fp_GetSPDModuleMaxFrequency)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleMaxFrequency));
                    result = cpuidsdk_fp_GetSPDModuleMaxFrequency(this.objptr, _spd_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000B5 RID: 181 RVA: 0x000049C0 File Offset: 0x00002BC0
        public string GetSPDModuleSpecification(int _spd_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1157532157u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleSpecification cpuidsdk_fp_GetSPDModuleSpecification = (CPUIDSDK_fp_GetSPDModuleSpecification)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleSpecification));
                    IntPtr ptr = cpuidsdk_fp_GetSPDModuleSpecification(this.objptr, _spd_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000B6 RID: 182 RVA: 0x00004A38 File Offset: 0x00002C38
        public string GetSPDModulePartNumber(int _spd_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1044937872u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModulePartNumber cpuidsdk_fp_GetSPDModulePartNumber = (CPUIDSDK_fp_GetSPDModulePartNumber)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModulePartNumber));
                    IntPtr ptr = cpuidsdk_fp_GetSPDModulePartNumber(this.objptr, _spd_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000B7 RID: 183 RVA: 0x00004AB0 File Offset: 0x00002CB0
        public string GetSPDModuleSerialNumber(int _spd_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2034692749u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleSerialNumber cpuidsdk_fp_GetSPDModuleSerialNumber = (CPUIDSDK_fp_GetSPDModuleSerialNumber)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleSerialNumber));
                    IntPtr ptr = cpuidsdk_fp_GetSPDModuleSerialNumber(this.objptr, _spd_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000B8 RID: 184 RVA: 0x00004B28 File Offset: 0x00002D28
        public float GetSPDModuleMinTRCD(int _spd_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1471721329u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleMinTRCD cpuidsdk_fp_GetSPDModuleMinTRCD = (CPUIDSDK_fp_GetSPDModuleMinTRCD)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleMinTRCD));
                    result = cpuidsdk_fp_GetSPDModuleMinTRCD(this.objptr, _spd_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000B9 RID: 185 RVA: 0x00004B98 File Offset: 0x00002D98
        public float GetSPDModuleMinTRP(int _spd_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1531229833u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleMinTRP cpuidsdk_fp_GetSPDModuleMinTRP = (CPUIDSDK_fp_GetSPDModuleMinTRP)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleMinTRP));
                    result = cpuidsdk_fp_GetSPDModuleMinTRP(this.objptr, _spd_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000BA RID: 186 RVA: 0x00004C08 File Offset: 0x00002E08
        public float GetSPDModuleMinTRAS(int _spd_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4224186255u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleMinTRAS cpuidsdk_fp_GetSPDModuleMinTRAS = (CPUIDSDK_fp_GetSPDModuleMinTRAS)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleMinTRAS));
                    result = cpuidsdk_fp_GetSPDModuleMinTRAS(this.objptr, _spd_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000BB RID: 187 RVA: 0x00004C78 File Offset: 0x00002E78
        public float GetSPDModuleMinTRC(int _spd_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4070434107u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleMinTRC cpuidsdk_fp_GetSPDModuleMinTRC = (CPUIDSDK_fp_GetSPDModuleMinTRC)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleMinTRC));
                    result = cpuidsdk_fp_GetSPDModuleMinTRC(this.objptr, _spd_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000BC RID: 188 RVA: 0x00004CE8 File Offset: 0x00002EE8
        public int GetSPDModuleManufacturingDate(int _spd_index, ref int _year, ref int _week)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2638822034u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleManufacturingDate cpuidsdk_fp_GetSPDModuleManufacturingDate = (CPUIDSDK_fp_GetSPDModuleManufacturingDate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleManufacturingDate));
                    result = cpuidsdk_fp_GetSPDModuleManufacturingDate(this.objptr, _spd_index, ref _year, ref _week);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000BD RID: 189 RVA: 0x00004D58 File Offset: 0x00002F58
        public int GetSPDModuleNumberOfBanks(int _spd_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1645265953u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleNumberOfBanks cpuidsdk_fp_GetSPDModuleNumberOfBanks = (CPUIDSDK_fp_GetSPDModuleNumberOfBanks)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfBanks));
                    result = cpuidsdk_fp_GetSPDModuleNumberOfBanks(this.objptr, _spd_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000BE RID: 190 RVA: 0x00004DC8 File Offset: 0x00002FC8
        public int GetSPDModuleDataWidth(int _spd_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1649853613u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleDataWidth cpuidsdk_fp_GetSPDModuleDataWidth = (CPUIDSDK_fp_GetSPDModuleDataWidth)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleDataWidth));
                    result = cpuidsdk_fp_GetSPDModuleDataWidth(this.objptr, _spd_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000BF RID: 191 RVA: 0x00004E38 File Offset: 0x00003038
        public int GetSPDModuleNumberOfProfiles(int _spd_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2814463874u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleNumberOfProfiles cpuidsdk_fp_GetSPDModuleNumberOfProfiles = (CPUIDSDK_fp_GetSPDModuleNumberOfProfiles)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfProfiles));
                    result = cpuidsdk_fp_GetSPDModuleNumberOfProfiles(this.objptr, _spd_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000C0 RID: 192 RVA: 0x00004EA8 File Offset: 0x000030A8
        public void GetSPDModuleProfileInfos(int _spd_index, int _profile_index, ref float _frequency, ref float _tCL, ref float _nominal_vdd)
        {
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2175206222u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleProfileInfos cpuidsdk_fp_GetSPDModuleProfileInfos = (CPUIDSDK_fp_GetSPDModuleProfileInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleProfileInfos));
                    cpuidsdk_fp_GetSPDModuleProfileInfos(this.objptr, _spd_index, _profile_index, ref _frequency, ref _tCL, ref _nominal_vdd);
                }
            }
            catch
            {
            }
        }

        // Token: 0x060000C1 RID: 193 RVA: 0x00004F0C File Offset: 0x0000310C
        public int GetSPDModuleNumberOfEPPProfiles(int _spd_index, ref int _epp_revision)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(395194140u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles cpuidsdk_fp_GetSPDModuleNumberOfEPPProfiles = (CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles));
                    result = cpuidsdk_fp_GetSPDModuleNumberOfEPPProfiles(this.objptr, _spd_index, ref _epp_revision);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000C2 RID: 194 RVA: 0x00004F7C File Offset: 0x0000317C
        public void GetSPDModuleEPPProfileInfos(int _spd_index, int _profile_index, ref float _frequency, ref float _tCL, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _tRC, ref float _nominal_vdd)
        {
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(355347036u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleEPPProfileInfos cpuidsdk_fp_GetSPDModuleEPPProfileInfos = (CPUIDSDK_fp_GetSPDModuleEPPProfileInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleEPPProfileInfos));
                    cpuidsdk_fp_GetSPDModuleEPPProfileInfos(this.objptr, _spd_index, _profile_index, ref _frequency, ref _tCL, ref _tRCD, ref _tRAS, ref _tRP, ref _tRC, ref _nominal_vdd);
                }
            }
            catch
            {
            }
        }

        // Token: 0x060000C3 RID: 195 RVA: 0x00004FE8 File Offset: 0x000031E8
        public int GetSPDModuleNumberOfXMPProfiles(int _spd_index, ref int _xmp_revision)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2952486902u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles cpuidsdk_fp_GetSPDModuleNumberOfXMPProfiles = (CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles));
                    result = cpuidsdk_fp_GetSPDModuleNumberOfXMPProfiles(this.objptr, _spd_index, ref _xmp_revision);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000C4 RID: 196 RVA: 0x00005058 File Offset: 0x00003258
        public int GetSPDModuleXMPProfileNumberOfCL(int _spd_index, int _profile_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1410377761u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL cpuidsdk_fp_GetSPDModuleXMPProfileNumberOfCL = (CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL));
                    result = cpuidsdk_fp_GetSPDModuleXMPProfileNumberOfCL(this.objptr, _spd_index, _profile_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000C5 RID: 197 RVA: 0x000050C8 File Offset: 0x000032C8
        public void GetSPDModuleXMPProfileCLInfos(int _spd_index, int _profile_index, int _cl_index, ref float _frequency, ref float _CL)
        {
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1964173861u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos cpuidsdk_fp_GetSPDModuleXMPProfileCLInfos = (CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos));
                    cpuidsdk_fp_GetSPDModuleXMPProfileCLInfos(this.objptr, _spd_index, _profile_index, _cl_index, ref _frequency, ref _CL);
                }
            }
            catch
            {
            }
        }

        // Token: 0x060000C6 RID: 198 RVA: 0x0000512C File Offset: 0x0000332C
        public void GetSPDModuleXMPProfileInfos(int _spd_index, int _profile_index, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _nominal_vdd, ref int _max_freq, ref float _max_CL)
        {
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(26739504u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleXMPProfileInfos cpuidsdk_fp_GetSPDModuleXMPProfileInfos = (CPUIDSDK_fp_GetSPDModuleXMPProfileInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleXMPProfileInfos));
                    cpuidsdk_fp_GetSPDModuleXMPProfileInfos(this.objptr, _spd_index, _profile_index, ref _tRCD, ref _tRAS, ref _tRP, ref _nominal_vdd, ref _max_freq, ref _max_CL);
                }
            }
            catch
            {
            }
        }

        // Token: 0x060000C7 RID: 199 RVA: 0x00005198 File Offset: 0x00003398
        public int GetSPDModuleNumberOfAMPProfiles(int _spd_index, ref int _amp_revision)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(486947340u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles cpuidsdk_fp_GetSPDModuleNumberOfAMPProfiles = (CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles));
                    result = cpuidsdk_fp_GetSPDModuleNumberOfAMPProfiles(this.objptr, _spd_index, ref _amp_revision);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000C8 RID: 200 RVA: 0x00005208 File Offset: 0x00003408
        public void GetSPDModuleAMPProfileInfos(int _spd_index, int _profile_index, ref int _frequency, ref float _min_cycle_time, ref float _tCL, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _tRC)
        {
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1990782289u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleAMPProfileInfos cpuidsdk_fp_GetSPDModuleAMPProfileInfos = (CPUIDSDK_fp_GetSPDModuleAMPProfileInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleAMPProfileInfos));
                    cpuidsdk_fp_GetSPDModuleAMPProfileInfos(this.objptr, _spd_index, _profile_index, ref _frequency, ref _min_cycle_time, ref _tCL, ref _tRCD, ref _tRAS, ref _tRP, ref _tRC);
                }
            }
            catch
            {
            }
        }

        // Token: 0x060000C9 RID: 201 RVA: 0x00005274 File Offset: 0x00003474
        public int GetSPDModuleRawData(int _spd_index, int _offset)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2497653182u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSPDModuleRawData cpuidsdk_fp_GetSPDModuleRawData = (CPUIDSDK_fp_GetSPDModuleRawData)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSPDModuleRawData));
                    result = cpuidsdk_fp_GetSPDModuleRawData(this.objptr, _spd_index, _offset);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000CA RID: 202 RVA: 0x000052E4 File Offset: 0x000034E4
        public int GetNumberOfDisplayAdapter()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2087647453u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetNumberOfDisplayAdapter cpuidsdk_fp_GetNumberOfDisplayAdapter = (CPUIDSDK_fp_GetNumberOfDisplayAdapter)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfDisplayAdapter));
                    result = cpuidsdk_fp_GetNumberOfDisplayAdapter(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000CB RID: 203 RVA: 0x00005350 File Offset: 0x00003550
        public int GetDisplayAdapterID(int _adapter_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1172999125u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterID cpuidsdk_fp_GetDisplayAdapterID = (CPUIDSDK_fp_GetDisplayAdapterID)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterID));
                    result = cpuidsdk_fp_GetDisplayAdapterID(this.objptr, _adapter_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000CC RID: 204 RVA: 0x000053C0 File Offset: 0x000035C0
        public string GetDisplayAdapterName(int _adapter_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(307635372u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterName cpuidsdk_fp_GetDisplayAdapterName = (CPUIDSDK_fp_GetDisplayAdapterName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterName));
                    IntPtr ptr = cpuidsdk_fp_GetDisplayAdapterName(this.objptr, _adapter_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000CD RID: 205 RVA: 0x00005438 File Offset: 0x00003638
        public string GetDisplayAdapterCodeName(int _adapter_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1517860081u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterCodeName cpuidsdk_fp_GetDisplayAdapterCodeName = (CPUIDSDK_fp_GetDisplayAdapterCodeName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterCodeName));
                    IntPtr ptr = cpuidsdk_fp_GetDisplayAdapterCodeName(this.objptr, _adapter_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000CE RID: 206 RVA: 0x000054B0 File Offset: 0x000036B0
        public int GetDisplayAdapterNumberOfPerformanceLevels(int _adapter_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3473514003u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels cpuidsdk_fp_GetDisplayAdapterNumberOfPerformanceLevels = (CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels));
                    result = cpuidsdk_fp_GetDisplayAdapterNumberOfPerformanceLevels(this.objptr, _adapter_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000CF RID: 207 RVA: 0x00005520 File Offset: 0x00003720
        public int GetDisplayAdapterCurrentPerformanceLevel(int _adapter_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1920001249u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel cpuidsdk_fp_GetDisplayAdapterCurrentPerformanceLevel = (CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel));
                    result = cpuidsdk_fp_GetDisplayAdapterCurrentPerformanceLevel(this.objptr, _adapter_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000D0 RID: 208 RVA: 0x00005590 File Offset: 0x00003790
        public string GetDisplayAdapterPerformanceLevelName(int _adapter_index, int _perf_level)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(315762084u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName cpuidsdk_fp_GetDisplayAdapterPerformanceLevelName = (CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName));
                    IntPtr ptr = cpuidsdk_fp_GetDisplayAdapterPerformanceLevelName(this.objptr, _adapter_index, _perf_level);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000D1 RID: 209 RVA: 0x0000560C File Offset: 0x0000380C
        public float GetDisplayAdapterClock(int _adapter_index, int _perf_level, int _domain)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2976604886u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterClock cpuidsdk_fp_GetDisplayAdapterClock = (CPUIDSDK_fp_GetDisplayAdapterClock)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterClock));
                    result = cpuidsdk_fp_GetDisplayAdapterClock(this.objptr, _adapter_index, _perf_level, _domain);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000D2 RID: 210 RVA: 0x00005680 File Offset: 0x00003880
        public float GetDisplayAdapterStockClock(int _adapter_index, int _perf_level, int _domain)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2675785466u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterStockClock cpuidsdk_fp_GetDisplayAdapterStockClock = (CPUIDSDK_fp_GetDisplayAdapterStockClock)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterStockClock));
                    result = cpuidsdk_fp_GetDisplayAdapterStockClock(this.objptr, _adapter_index, _perf_level, _domain);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000D3 RID: 211 RVA: 0x000056F4 File Offset: 0x000038F4
        public float GetDisplayAdapterManufacturingProcess(int _adapter_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2074277701u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess cpuidsdk_fp_GetDisplayAdapterManufacturingProcess = (CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess));
                    result = cpuidsdk_fp_GetDisplayAdapterManufacturingProcess(this.objptr, _adapter_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000D4 RID: 212 RVA: 0x00005764 File Offset: 0x00003964
        public float GetDisplayAdapterTemperature(int _adapter_index, int _domain)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(765483840u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterTemperature cpuidsdk_fp_GetDisplayAdapterTemperature = (CPUIDSDK_fp_GetDisplayAdapterTemperature)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterTemperature));
                    result = cpuidsdk_fp_GetDisplayAdapterTemperature(this.objptr, _adapter_index, _domain);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000D5 RID: 213 RVA: 0x000057D4 File Offset: 0x000039D4
        public int GetDisplayAdapterFanSpeed(int _adapter_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3176364710u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterFanSpeed cpuidsdk_fp_GetDisplayAdapterFanSpeed = (CPUIDSDK_fp_GetDisplayAdapterFanSpeed)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterFanSpeed));
                    result = cpuidsdk_fp_GetDisplayAdapterFanSpeed(this.objptr, _adapter_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000D6 RID: 214 RVA: 0x00005844 File Offset: 0x00003A44
        public int GetDisplayAdapterFanPWM(int _adapter_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1324785133u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterFanPWM cpuidsdk_fp_GetDisplayAdapterFanPWM = (CPUIDSDK_fp_GetDisplayAdapterFanPWM)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterFanPWM));
                    result = cpuidsdk_fp_GetDisplayAdapterFanPWM(this.objptr, _adapter_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000D7 RID: 215 RVA: 0x000058B4 File Offset: 0x00003AB4
        public bool GetDisplayAdapterMemoryType(int _adapter_index, ref int _type)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1480241269u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterMemoryType cpuidsdk_fp_GetDisplayAdapterMemoryType = (CPUIDSDK_fp_GetDisplayAdapterMemoryType)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterMemoryType));
                    int num = cpuidsdk_fp_GetDisplayAdapterMemoryType(this.objptr, _adapter_index, ref _type);
                    if (num == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x060000D8 RID: 216 RVA: 0x00005924 File Offset: 0x00003B24
        public bool GetDisplayAdapterMemorySize(int _adapter_index, ref int _size)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3654923187u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterMemorySize cpuidsdk_fp_GetDisplayAdapterMemorySize = (CPUIDSDK_fp_GetDisplayAdapterMemorySize)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterMemorySize));
                    int num = cpuidsdk_fp_GetDisplayAdapterMemorySize(this.objptr, _adapter_index, ref _size);
                    if (num == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x060000D9 RID: 217 RVA: 0x00005994 File Offset: 0x00003B94
        public bool GetDisplayAdapterMemoryBusWidth(int _adapter_index, ref int _bus_width)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1254004093u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth cpuidsdk_fp_GetDisplayAdapterMemoryBusWidth = (CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth));
                    int num = cpuidsdk_fp_GetDisplayAdapterMemoryBusWidth(this.objptr, _adapter_index, ref _bus_width);
                    if (num == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x060000DA RID: 218 RVA: 0x00005A04 File Offset: 0x00003C04
        public string GetDisplayAdapterMemoryVendor(int _adapter_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(558514836u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterMemoryVendor cpuidsdk_fp_GetDisplayAdapterMemoryVendor = (CPUIDSDK_fp_GetDisplayAdapterMemoryVendor)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterMemoryVendor));
                    IntPtr ptr = cpuidsdk_fp_GetDisplayAdapterMemoryVendor(this.objptr, _adapter_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000DB RID: 219 RVA: 0x00005A7C File Offset: 0x00003C7C
        public string GetDisplayDriverVersion()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1058307624u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayDriverVersion cpuidsdk_fp_GetDisplayDriverVersion = (CPUIDSDK_fp_GetDisplayDriverVersion)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayDriverVersion));
                    IntPtr ptr = cpuidsdk_fp_GetDisplayDriverVersion(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000DC RID: 220 RVA: 0x00005AF4 File Offset: 0x00003CF4
        public string GetDirectXVersion()
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3664622811u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDirectXVersion cpuidsdk_fp_GetDirectXVersion = (CPUIDSDK_fp_GetDirectXVersion)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDirectXVersion));
                    IntPtr ptr = cpuidsdk_fp_GetDirectXVersion(this.objptr);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000DD RID: 221 RVA: 0x00005B6C File Offset: 0x00003D6C
        public bool GetDisplayAdapterBusInfos(int _adapter_index, ref int _bus_type, ref int _multi_vpu)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2874889910u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterBusInfos cpuidsdk_fp_GetDisplayAdapterBusInfos = (CPUIDSDK_fp_GetDisplayAdapterBusInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterBusInfos));
                    int num = cpuidsdk_fp_GetDisplayAdapterBusInfos(this.objptr, _adapter_index, ref _bus_type, ref _multi_vpu);
                    if (num == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x060000DE RID: 222 RVA: 0x00005BE0 File Offset: 0x00003DE0
        public string GetDisplayAdapterCoreFamily(int _adapter_index, ref int _core)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2131295761u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDisplayAdapterCoreFamily cpuidsdk_fp_GetDisplayAdapterCoreFamily = (CPUIDSDK_fp_GetDisplayAdapterCoreFamily)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDisplayAdapterCoreFamily));
                    IntPtr ptr = cpuidsdk_fp_GetDisplayAdapterCoreFamily(this.objptr, _adapter_index, ref _core);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000DF RID: 223 RVA: 0x00005C5C File Offset: 0x00003E5C
        public int GetNumberOfMonitors()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1013741784u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetNumberOfMonitors cpuidsdk_fp_GetNumberOfMonitors = (CPUIDSDK_fp_GetNumberOfMonitors)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfMonitors));
                    result = cpuidsdk_fp_GetNumberOfMonitors(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000E0 RID: 224 RVA: 0x00005CC8 File Offset: 0x00003EC8
        public string GetMonitorName(int _monitor_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(665341776u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMonitorName cpuidsdk_fp_GetMonitorName = (CPUIDSDK_fp_GetMonitorName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorName));
                    IntPtr ptr = cpuidsdk_fp_GetMonitorName(this.objptr, _monitor_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000E1 RID: 225 RVA: 0x00005D40 File Offset: 0x00003F40
        public string GetMonitorVendor(int _monitor_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1245877381u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMonitorVendor cpuidsdk_fp_GetMonitorVendor = (CPUIDSDK_fp_GetMonitorVendor)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorVendor));
                    IntPtr ptr = cpuidsdk_fp_GetMonitorVendor(this.objptr, _monitor_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000E2 RID: 226 RVA: 0x00005DB8 File Offset: 0x00003FB8
        public string GetMonitorID(int _monitor_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(165417912u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMonitorID cpuidsdk_fp_GetMonitorID = (CPUIDSDK_fp_GetMonitorID)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorID));
                    IntPtr ptr = cpuidsdk_fp_GetMonitorID(this.objptr, _monitor_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000E3 RID: 227 RVA: 0x00005E30 File Offset: 0x00004030
        public string GetMonitorSerial(int _monitor_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3626348619u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMonitorSerial cpuidsdk_fp_GetMonitorSerial = (CPUIDSDK_fp_GetMonitorSerial)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorSerial));
                    IntPtr ptr = cpuidsdk_fp_GetMonitorSerial(this.objptr, _monitor_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000E4 RID: 228 RVA: 0x00005EA8 File Offset: 0x000040A8
        public bool GetMonitorManufacturingDate(int _monitor_index, ref int _week, ref int _year)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1219137877u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMonitorManufacturingDate cpuidsdk_fp_GetMonitorManufacturingDate = (CPUIDSDK_fp_GetMonitorManufacturingDate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorManufacturingDate));
                    int num = cpuidsdk_fp_GetMonitorManufacturingDate(this.objptr, _monitor_index, ref _week, ref _year);
                    if (num == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x060000E5 RID: 229 RVA: 0x00005F1C File Offset: 0x0000411C
        public float GetMonitorSize(int _monitor_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2267483726u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMonitorSize cpuidsdk_fp_GetMonitorSize = (CPUIDSDK_fp_GetMonitorSize)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorSize));
                    result = cpuidsdk_fp_GetMonitorSize(this.objptr, _monitor_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000E6 RID: 230 RVA: 0x00005F8C File Offset: 0x0000418C
        public bool GetMonitorResolution(int _monitor_index, ref int _width, ref int _height, ref int _frequency)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3465387291u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMonitorResolution cpuidsdk_fp_GetMonitorResolution = (CPUIDSDK_fp_GetMonitorResolution)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorResolution));
                    int num = cpuidsdk_fp_GetMonitorResolution(this.objptr, _monitor_index, ref _width, ref _height, ref _frequency);
                    if (num == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x060000E7 RID: 231 RVA: 0x00006000 File Offset: 0x00004200
        public int GetMonitorMaxPixelClock(int _monitor_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3573524991u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMonitorMaxPixelClock cpuidsdk_fp_GetMonitorMaxPixelClock = (CPUIDSDK_fp_GetMonitorMaxPixelClock)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorMaxPixelClock));
                    result = cpuidsdk_fp_GetMonitorMaxPixelClock(this.objptr, _monitor_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000E8 RID: 232 RVA: 0x00006070 File Offset: 0x00004270
        public float GetMonitorGamma(int _monitor_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2553491558u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetMonitorGamma cpuidsdk_fp_GetMonitorGamma = (CPUIDSDK_fp_GetMonitorGamma)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetMonitorGamma));
                    result = cpuidsdk_fp_GetMonitorGamma(this.objptr, _monitor_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000E9 RID: 233 RVA: 0x000060E0 File Offset: 0x000042E0
        public int GetNumberOfStorageDevice()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2497784258u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetNumberOfStorageDevice cpuidsdk_fp_GetNumberOfStorageDevice = (CPUIDSDK_fp_GetNumberOfStorageDevice)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfStorageDevice));
                    result = cpuidsdk_fp_GetNumberOfStorageDevice(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000EA RID: 234 RVA: 0x0000614C File Offset: 0x0000434C
        public int GetStorageDriveNumber(int _index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1278908533u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDriveNumber cpuidsdk_fp_GetStorageDriveNumber = (CPUIDSDK_fp_GetStorageDriveNumber)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDriveNumber));
                    result = cpuidsdk_fp_GetStorageDriveNumber(this.objptr, _index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000EB RID: 235 RVA: 0x000061BC File Offset: 0x000043BC
        public string GetStorageDeviceName(int _index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(604522512u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDeviceName cpuidsdk_fp_GetStorageDeviceName = (CPUIDSDK_fp_GetStorageDeviceName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceName));
                    IntPtr ptr = cpuidsdk_fp_GetStorageDeviceName(this.objptr, _index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000EC RID: 236 RVA: 0x00006234 File Offset: 0x00004434
        public string GetStorageDeviceRevision(int _index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1769526001u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDeviceRevision cpuidsdk_fp_GetStorageDeviceRevision = (CPUIDSDK_fp_GetStorageDeviceRevision)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceRevision));
                    IntPtr ptr = cpuidsdk_fp_GetStorageDeviceRevision(this.objptr, _index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000ED RID: 237 RVA: 0x000062AC File Offset: 0x000044AC
        public string GetStorageDeviceSerialNumber(int _index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1425582577u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDeviceSerialNumber cpuidsdk_fp_GetStorageDeviceSerialNumber = (CPUIDSDK_fp_GetStorageDeviceSerialNumber)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceSerialNumber));
                    IntPtr ptr = cpuidsdk_fp_GetStorageDeviceSerialNumber(this.objptr, _index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000EE RID: 238 RVA: 0x00006324 File Offset: 0x00004524
        public int GetStorageDeviceBusType(int _index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1892213137u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDeviceBusType cpuidsdk_fp_GetStorageDeviceBusType = (CPUIDSDK_fp_GetStorageDeviceBusType)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceBusType));
                    result = cpuidsdk_fp_GetStorageDeviceBusType(this.objptr, _index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000EF RID: 239 RVA: 0x00006394 File Offset: 0x00004594
        public int GetStorageDeviceRotationSpeed(int _index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(589055544u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDeviceRotationSpeed cpuidsdk_fp_GetStorageDeviceRotationSpeed = (CPUIDSDK_fp_GetStorageDeviceRotationSpeed)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceRotationSpeed));
                    result = cpuidsdk_fp_GetStorageDeviceRotationSpeed(this.objptr, _index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000F0 RID: 240 RVA: 0x00006404 File Offset: 0x00004604
        public uint GetStorageDeviceFeatureFlag(int _index)
        {
            uint result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3251864487u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDeviceFeatureFlag cpuidsdk_fp_GetStorageDeviceFeatureFlag = (CPUIDSDK_fp_GetStorageDeviceFeatureFlag)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceFeatureFlag));
                    result = (uint)cpuidsdk_fp_GetStorageDeviceFeatureFlag(this.objptr, _index);
                }
                else
                {
                    result = (uint)I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = (uint)I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000F1 RID: 241 RVA: 0x00006474 File Offset: 0x00004674
        public int GetStorageDeviceNumberOfVolumes(int _index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4115393175u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes cpuidsdk_fp_GetStorageDeviceNumberOfVolumes = (CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes));
                    result = cpuidsdk_fp_GetStorageDeviceNumberOfVolumes(this.objptr, _index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000F2 RID: 242 RVA: 0x000064E4 File Offset: 0x000046E4
        public string GetStorageDeviceVolumeLetter(int _index, int _volume_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1402644277u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDeviceVolumeLetter cpuidsdk_fp_GetStorageDeviceVolumeLetter = (CPUIDSDK_fp_GetStorageDeviceVolumeLetter)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceVolumeLetter));
                    IntPtr ptr = cpuidsdk_fp_GetStorageDeviceVolumeLetter(this.objptr, _index, _volume_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000F3 RID: 243 RVA: 0x00006560 File Offset: 0x00004760
        public float GetStorageDeviceVolumeTotalCapacity(int _index, int _volume_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2198799902u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity cpuidsdk_fp_GetStorageDeviceVolumeTotalCapacity = (CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity));
                    result = cpuidsdk_fp_GetStorageDeviceVolumeTotalCapacity(this.objptr, _index, _volume_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000F4 RID: 244 RVA: 0x000065D0 File Offset: 0x000047D0
        public float GetStorageDeviceVolumeAvailableCapacity(int _index, int _volume_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4149997239u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity cpuidsdk_fp_GetStorageDeviceVolumeAvailableCapacity = (CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity));
                    result = cpuidsdk_fp_GetStorageDeviceVolumeAvailableCapacity(this.objptr, _index, _volume_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000F5 RID: 245 RVA: 0x00006640 File Offset: 0x00004840
        public bool GetStorageDeviceSmartAttribute(int _hdd_index, int _attrib_index, ref int _id, ref int _flags, ref int _value, ref int _worst, byte[] _data)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2327385458u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDeviceSmartAttribute cpuidsdk_fp_GetStorageDeviceSmartAttribute = (CPUIDSDK_fp_GetStorageDeviceSmartAttribute)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceSmartAttribute));
                    int num = cpuidsdk_fp_GetStorageDeviceSmartAttribute(this.objptr, _hdd_index, _attrib_index, ref _id, ref _flags, ref _value, ref _worst, _data);
                    if (num == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x060000F6 RID: 246 RVA: 0x000066BC File Offset: 0x000048BC
        public int GetStorageDevicePowerOnHours(int _index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2993775842u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDevicePowerOnHours cpuidsdk_fp_GetStorageDevicePowerOnHours = (CPUIDSDK_fp_GetStorageDevicePowerOnHours)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDevicePowerOnHours));
                    result = cpuidsdk_fp_GetStorageDevicePowerOnHours(this.objptr, _index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000F7 RID: 247 RVA: 0x0000672C File Offset: 0x0000492C
        public int GetStorageDevicePowerCycleCount(int _index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1021737420u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDevicePowerCycleCount cpuidsdk_fp_GetStorageDevicePowerCycleCount = (CPUIDSDK_fp_GetStorageDevicePowerCycleCount)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDevicePowerCycleCount));
                    result = cpuidsdk_fp_GetStorageDevicePowerCycleCount(this.objptr, _index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000F8 RID: 248 RVA: 0x0000679C File Offset: 0x0000499C
        public float GetStorageDeviceTotalCapacity(int _index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(3442055763u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetStorageDeviceTotalCapacity cpuidsdk_fp_GetStorageDeviceTotalCapacity = (CPUIDSDK_fp_GetStorageDeviceTotalCapacity)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetStorageDeviceTotalCapacity));
                    result = cpuidsdk_fp_GetStorageDeviceTotalCapacity(this.objptr, _index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000F9 RID: 249 RVA: 0x0000680C File Offset: 0x00004A0C
        public int GetNumberOfDevices()
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(105516180u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetNumberOfDevices cpuidsdk_fp_GetNumberOfDevices = (CPUIDSDK_fp_GetNumberOfDevices)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfDevices));
                    result = cpuidsdk_fp_GetNumberOfDevices(this.objptr);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000FA RID: 250 RVA: 0x00006878 File Offset: 0x00004A78
        public int GetDeviceClass(int _device_index)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4104644943u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDeviceClass cpuidsdk_fp_GetDeviceClass = (CPUIDSDK_fp_GetDeviceClass)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDeviceClass));
                    result = cpuidsdk_fp_GetDeviceClass(this.objptr, _device_index);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000FB RID: 251 RVA: 0x000068E8 File Offset: 0x00004AE8
        public string GetDeviceName(int _device_index)
        {
            string result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1026980460u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetDeviceName cpuidsdk_fp_GetDeviceName = (CPUIDSDK_fp_GetDeviceName)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetDeviceName));
                    IntPtr ptr = cpuidsdk_fp_GetDeviceName(this.objptr, _device_index);
                    string text = Marshal.PtrToStringAnsi(ptr);
                    Marshal.FreeBSTR(ptr);
                    result = text;
                }
                else
                {
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        // Token: 0x060000FC RID: 252 RVA: 0x00006960 File Offset: 0x00004B60
        public int GetNumberOfSensors(int _device_index, int _sensor_class)
        {
            int result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(1886314717u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetNumberOfSensors cpuidsdk_fp_GetNumberOfSensors = (CPUIDSDK_fp_GetNumberOfSensors)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetNumberOfSensors));
                    result = cpuidsdk_fp_GetNumberOfSensors(this.objptr, _device_index, _sensor_class);
                }
                else
                {
                    result = I_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = I_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x060000FD RID: 253 RVA: 0x000069D0 File Offset: 0x00004BD0
        public bool GetSensorInfos(int _device_index, int _sensor_index, int _sensor_class, ref int _sensor_id, ref string _szName, ref int _raw_value, ref float _value, ref float _min_value, ref float _max_value)
        {
            bool result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(4188402507u);
                if (intPtr != IntPtr.Zero)
                {
                    IntPtr zero = IntPtr.Zero;
                    CPUIDSDK_fp_GetSensorInfos cpuidsdk_fp_GetSensorInfos = (CPUIDSDK_fp_GetSensorInfos)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSensorInfos));
                    int num = cpuidsdk_fp_GetSensorInfos(this.objptr, _device_index, _sensor_index, _sensor_class, ref _sensor_id, ref zero, ref _raw_value, ref _value, ref _min_value, ref _max_value);
                    if (num == 1)
                    {
                        _szName = Marshal.PtrToStringAnsi(zero);
                        Marshal.FreeBSTR(zero);
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        // Token: 0x060000FE RID: 254 RVA: 0x00006A68 File Offset: 0x00004C68
        public void SensorClearMinMax(int _device_index, int _sensor_index, int _sensor_class, ref int _sensor_id, ref string _szName, ref int _raw_value, ref float _value, ref float _min_value, ref float _max_value)
        {
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2693611802u);
                if (intPtr != IntPtr.Zero)
                {
                    IntPtr zero = IntPtr.Zero;
                    CPUIDSDK_fp_SensorClearMinMax cpuidsdk_fp_SensorClearMinMax = (CPUIDSDK_fp_SensorClearMinMax)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_SensorClearMinMax));
                    cpuidsdk_fp_SensorClearMinMax(this.objptr, _device_index, _sensor_index, _sensor_class);
                }
            }
            catch
            {
            }
        }

        // Token: 0x060000FF RID: 255 RVA: 0x00006AD0 File Offset: 0x00004CD0
        public float GetSensorTypeValue(int _sensor_type, ref int _device_index, ref int _sensor_index)
        {
            float result;
            try
            {
                IntPtr intPtr = CPUIDSDK_fp_QueryInterface(2782219178u);
                if (intPtr != IntPtr.Zero)
                {
                    CPUIDSDK_fp_GetSensorTypeValue cpuidsdk_fp_GetSensorTypeValue = (CPUIDSDK_fp_GetSensorTypeValue)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CPUIDSDK_fp_GetSensorTypeValue));
                    result = cpuidsdk_fp_GetSensorTypeValue(this.objptr, _sensor_type, ref _device_index, ref _sensor_index);
                }
                else
                {
                    result = F_UNDEFINED_VALUE;
                }
            }
            catch
            {
                result = F_UNDEFINED_VALUE;
            }
            return result;
        }

        // Token: 0x04000007 RID: 7
        public const uint CPUIDSDK_ERROR_NO_ERROR = 0u;

        // Token: 0x04000008 RID: 8
        public const uint CPUIDSDK_ERROR_EVALUATION = 1u;

        // Token: 0x04000009 RID: 9
        public const uint CPUIDSDK_ERROR_DRIVER = 2u;

        // Token: 0x0400000A RID: 10
        public const uint CPUIDSDK_ERROR_VM_RUNNING = 4u;

        // Token: 0x0400000B RID: 11
        public const uint CPUIDSDK_ERROR_LOCKED = 8u;

        // Token: 0x0400000C RID: 12
        public const uint CPUIDSDK_ERROR_INVALID_DLL = 16u;

        // Token: 0x0400000D RID: 13
        public const uint CPUIDSDK_EXT_ERROR_EVAL_1 = 1u;

        // Token: 0x0400000E RID: 14
        public const uint CPUIDSDK_EXT_ERROR_EVAL_2 = 2u;

        // Token: 0x0400000F RID: 15
        public const uint CPUIDSDK_CONFIG_USE_SOFTWARE = 2u;

        // Token: 0x04000010 RID: 16
        public const uint CPUIDSDK_CONFIG_USE_DMI = 4u;

        // Token: 0x04000011 RID: 17
        public const uint CPUIDSDK_CONFIG_USE_PCI = 8u;

        // Token: 0x04000012 RID: 18
        public const uint CPUIDSDK_CONFIG_USE_ACPI = 16u;

        // Token: 0x04000013 RID: 19
        public const uint CPUIDSDK_CONFIG_USE_CHIPSET = 32u;

        // Token: 0x04000014 RID: 20
        public const uint CPUIDSDK_CONFIG_USE_SMBUS = 64u;

        // Token: 0x04000015 RID: 21
        public const uint CPUIDSDK_CONFIG_USE_SPD = 128u;

        // Token: 0x04000016 RID: 22
        public const uint CPUIDSDK_CONFIG_USE_STORAGE = 256u;

        // Token: 0x04000017 RID: 23
        public const uint CPUIDSDK_CONFIG_USE_GRAPHICS = 512u;

        // Token: 0x04000018 RID: 24
        public const uint CPUIDSDK_CONFIG_USE_HWMONITORING = 1024u;

        // Token: 0x04000019 RID: 25
        public const uint CPUIDSDK_CONFIG_USE_PROCESSOR = 2048u;

        // Token: 0x0400001A RID: 26
        public const uint CPUIDSDK_CONFIG_USE_DISPLAY_API = 4096u;

        // Token: 0x0400001B RID: 27
        public const uint CPUIDSDK_CONFIG_USE_ACPI_TIMER = 16384u;

        // Token: 0x0400001C RID: 28
        public const uint CPUIDSDK_CONFIG_UPDATE_PROCESSOR = 65536u;

        // Token: 0x0400001D RID: 29
        public const uint CPUIDSDK_CONFIG_UPDATE_GRAPHICS = 131072u;

        // Token: 0x0400001E RID: 30
        public const uint CPUIDSDK_CONFIG_UPDATE_STORAGE = 262144u;

        // Token: 0x0400001F RID: 31
        public const uint CPUIDSDK_CONFIG_UPDATE_LPCIO = 524288u;

        // Token: 0x04000020 RID: 32
        public const uint CPUIDSDK_CONFIG_UPDATE_DRAM = 1048576u;

        // Token: 0x04000021 RID: 33
        public const uint CPUIDSDK_CONFIG_CHECK_VM = 16777216u;

        // Token: 0x04000022 RID: 34
        public const uint CPUIDSDK_CONFIG_WAKEUP_HDD = 33554432u;

        // Token: 0x04000023 RID: 35
        public const uint CPUIDSDK_CONFIG_SERVER_SAFE = 2147483648u;

        // Token: 0x04000024 RID: 36
        public const uint CPUIDSDK_CONFIG_USE_EVERYTHING = 2147483647u;

        // Token: 0x04000025 RID: 37
        public const uint CLASS_DEVICE_UNKNOWN = 0u;

        // Token: 0x04000026 RID: 38
        public const uint CLASS_DEVICE_PCI = 1u;

        // Token: 0x04000027 RID: 39
        public const uint CLASS_DEVICE_SMBUS = 2u;

        // Token: 0x04000028 RID: 40
        public const uint CLASS_DEVICE_PROCESSOR = 4u;

        // Token: 0x04000029 RID: 41
        public const uint CLASS_DEVICE_LPCIO = 8u;

        // Token: 0x0400002A RID: 42
        public const uint CLASS_DEVICE_DRIVE = 16u;

        // Token: 0x0400002B RID: 43
        public const uint CLASS_DEVICE_DISPLAY_ADAPTER = 32u;

        // Token: 0x0400002C RID: 44
        public const uint CLASS_DEVICE_HID = 64u;

        // Token: 0x0400002D RID: 45
        public const uint CLASS_DEVICE_BATTERY = 128u;

        // Token: 0x0400002E RID: 46
        public const uint CLASS_DEVICE_EVBOT = 256u;

        // Token: 0x0400002F RID: 47
        public const uint CLASS_DEVICE_NETWORK = 512u;

        // Token: 0x04000030 RID: 48
        public const uint CLASS_DEVICE_MAINBOARD = 1024u;

        // Token: 0x04000031 RID: 49
        public const uint CLASS_DEVICE_MEMORY_MODULE = 2048u;

        // Token: 0x04000032 RID: 50
        public const uint CLASS_DEVICE_PSU = 4096u;

        // Token: 0x04000033 RID: 51
        public const uint CLASS_DEVICE_TYPE_MASK = 2147483647u;

        // Token: 0x04000034 RID: 52
        public const uint CLASS_DEVICE_COMPOSITE = 2147483648u;

        // Token: 0x04000035 RID: 53
        public const uint CPU_MANUFACTURER_MASK = 4278190080u;

        // Token: 0x04000036 RID: 54
        public const uint CPU_FAMILY_MASK = 4294967040u;

        // Token: 0x04000037 RID: 55
        public const uint CPU_MODEL_MASK = 4294967295u;

        // Token: 0x04000038 RID: 56
        public const uint CPU_UNKNOWN = 0u;

        // Token: 0x04000039 RID: 57
        public const uint CPU_INTEL = 16777216u;

        // Token: 0x0400003A RID: 58
        public const uint CPU_AMD = 33554432u;

        // Token: 0x0400003B RID: 59
        public const uint CPU_CYRIX = 67108864u;

        // Token: 0x0400003C RID: 60
        public const uint CPU_VIA = 134217728u;

        // Token: 0x0400003D RID: 61
        public const uint CPU_TRANSMETA = 268435456u;

        // Token: 0x0400003E RID: 62
        public const uint CPU_DMP = 536870912u;

        // Token: 0x0400003F RID: 63
        public const uint CPU_UMC = 1073741824u;

        // Token: 0x04000040 RID: 64
        public const uint CPU_INTEL_386 = 16777472u;

        // Token: 0x04000041 RID: 65
        public const uint CPU_INTEL_486 = 16777728u;

        // Token: 0x04000042 RID: 66
        public const uint CPU_INTEL_P5 = 16778240u;

        // Token: 0x04000043 RID: 67
        public const uint CPU_INTEL_P6 = 16779264u;

        // Token: 0x04000044 RID: 68
        public const uint CPU_INTEL_NETBURST = 16781312u;

        // Token: 0x04000045 RID: 69
        public const uint CPU_INTEL_MOBILE = 16785408u;

        // Token: 0x04000046 RID: 70
        public const uint CPU_INTEL_CORE = 16793600u;

        // Token: 0x04000047 RID: 71
        public const uint CPU_INTEL_CORE_2 = 16809984u;

        // Token: 0x04000048 RID: 72
        public const uint CPU_INTEL_BONNELL = 16842752u;

        // Token: 0x04000049 RID: 73
        public const uint CPU_INTEL_SALTWELL = 16843008u;

        // Token: 0x0400004A RID: 74
        public const uint CPU_INTEL_SILVERMONT = 16843264u;

        // Token: 0x0400004B RID: 75
        public const uint CPU_INTEL_GOLDMONT = 16843776u;

        // Token: 0x0400004C RID: 76
        public const uint CPU_INTEL_NEHALEM = 16908288u;

        // Token: 0x0400004D RID: 77
        public const uint CPU_INTEL_SANDY_BRIDGE = 16908544u;

        // Token: 0x0400004E RID: 78
        public const uint CPU_INTEL_HASWELL = 16908800u;

        // Token: 0x0400004F RID: 79
        public const uint CPU_INTEL_SKYLAKE = 17039360u;

        // Token: 0x04000050 RID: 80
        public const uint CPU_INTEL_ITANIUM = 17825792u;

        // Token: 0x04000051 RID: 81
        public const uint CPU_INTEL_ITANIUM_2 = 17826048u;

        // Token: 0x04000052 RID: 82
        public const uint CPU_INTEL_MIC = 18874368u;

        // Token: 0x04000053 RID: 83
        public const uint CPU_PENTIUM = 16778241u;

        // Token: 0x04000054 RID: 84
        public const uint CPU_PENTIUM_MMX = 16778242u;

        // Token: 0x04000055 RID: 85
        public const uint CPU_PENTIUM_PRO = 16779265u;

        // Token: 0x04000056 RID: 86
        public const uint CPU_PENTIUM_2 = 16779266u;

        // Token: 0x04000057 RID: 87
        public const uint CPU_PENTIUM_2_M = 16779267u;

        // Token: 0x04000058 RID: 88
        public const uint CPU_CELERON_P2 = 16779268u;

        // Token: 0x04000059 RID: 89
        public const uint CPU_XEON_P2 = 16779269u;

        // Token: 0x0400005A RID: 90
        public const uint CPU_PENTIUM_3 = 16779270u;

        // Token: 0x0400005B RID: 91
        public const uint CPU_PENTIUM_3_M = 16779271u;

        // Token: 0x0400005C RID: 92
        public const uint CPU_PENTIUM_3_S = 16779272u;

        // Token: 0x0400005D RID: 93
        public const uint CPU_CELERON_P3 = 16779273u;

        // Token: 0x0400005E RID: 94
        public const uint CPU_XEON_P3 = 16779274u;

        // Token: 0x0400005F RID: 95
        public const uint CPU_PENTIUM_4 = 16781313u;

        // Token: 0x04000060 RID: 96
        public const uint CPU_PENTIUM_4_M = 16781314u;

        // Token: 0x04000061 RID: 97
        public const uint CPU_PENTIUM_4_HT = 16781315u;

        // Token: 0x04000062 RID: 98
        public const uint CPU_PENTIUM_4_EE = 16781316u;

        // Token: 0x04000063 RID: 99
        public const uint CPU_CELERON_P4 = 16781317u;

        // Token: 0x04000064 RID: 100
        public const uint CPU_CELERON_D = 16781318u;

        // Token: 0x04000065 RID: 101
        public const uint CPU_XEON_P4 = 16781319u;

        // Token: 0x04000066 RID: 102
        public const uint CPU_PENTIUM_D = 16781320u;

        // Token: 0x04000067 RID: 103
        public const uint CPU_PENTIUM_XE = 16781321u;

        // Token: 0x04000068 RID: 104
        public const uint CPU_PENTIUM_M = 16785409u;

        // Token: 0x04000069 RID: 105
        public const uint CPU_CELERON_M = 16785410u;

        // Token: 0x0400006A RID: 106
        public const uint CPU_CORE_SOLO = 16793601u;

        // Token: 0x0400006B RID: 107
        public const uint CPU_CORE_DUO = 16793602u;

        // Token: 0x0400006C RID: 108
        public const uint CPU_CORE_CELERON_M = 16793603u;

        // Token: 0x0400006D RID: 109
        public const uint CPU_CORE_CELERON = 16793604u;

        // Token: 0x0400006E RID: 110
        public const uint CPU_CORE_2_DUO = 16809985u;

        // Token: 0x0400006F RID: 111
        public const uint CPU_CORE_2_EE = 16809986u;

        // Token: 0x04000070 RID: 112
        public const uint CPU_CORE_2_XEON = 16809987u;

        // Token: 0x04000071 RID: 113
        public const uint CPU_CORE_2_CELERON = 16809988u;

        // Token: 0x04000072 RID: 114
        public const uint CPU_CORE_2_QUAD = 16809989u;

        // Token: 0x04000073 RID: 115
        public const uint CPU_CORE_2_PENTIUM = 16809990u;

        // Token: 0x04000074 RID: 116
        public const uint CPU_CORE_2_CELERON_DC = 16809991u;

        // Token: 0x04000075 RID: 117
        public const uint CPU_CORE_2_SOLO = 16809992u;

        // Token: 0x04000076 RID: 118
        public const uint CPU_BONNELL_ATOM = 16842753u;

        // Token: 0x04000077 RID: 119
        public const uint CPU_SALTWELL_ATOM = 16843009u;

        // Token: 0x04000078 RID: 120
        public const uint CPU_SILVERMONT_ATOM = 16843265u;

        // Token: 0x04000079 RID: 121
        public const uint CPU_SILVERMONT_CELERON = 16843266u;

        // Token: 0x0400007A RID: 122
        public const uint CPU_SILVERMONT_PENTIUM = 16843267u;

        // Token: 0x0400007B RID: 123
        public const uint CPU_SILVERMONT_ATOM_X7 = 16843268u;

        // Token: 0x0400007C RID: 124
        public const uint CPU_SILVERMONT_ATOM_X5 = 16843269u;

        // Token: 0x0400007D RID: 125
        public const uint CPU_SILVERMONT_ATOM_X3 = 16843270u;

        // Token: 0x0400007E RID: 126
        public const uint CPU_GOLDMONT_ATOM = 16843777u;

        // Token: 0x0400007F RID: 127
        public const uint CPU_GOLDMONT_CELERON = 16843778u;

        // Token: 0x04000080 RID: 128
        public const uint CPU_GOLDMONT_PENTIUM = 16843779u;

        // Token: 0x04000081 RID: 129
        public const uint CPU_NEHALEM_CORE_I7 = 16908289u;

        // Token: 0x04000082 RID: 130
        public const uint CPU_NEHALEM_CORE_I7E = 16908290u;

        // Token: 0x04000083 RID: 131
        public const uint CPU_NEHALEM_XEON = 16908291u;

        // Token: 0x04000084 RID: 132
        public const uint CPU_NEHALEM_CORE_I3 = 16908292u;

        // Token: 0x04000085 RID: 133
        public const uint CPU_NEHALEM_CORE_I5 = 16908293u;

        // Token: 0x04000086 RID: 134
        public const uint CPU_NEHALEM_PENTIUM = 16908295u;

        // Token: 0x04000087 RID: 135
        public const uint CPU_NEHALEM_CELERON = 16908296u;

        // Token: 0x04000088 RID: 136
        public const uint CPU_SANDY_BRIDGE_CORE_I7 = 16908545u;

        // Token: 0x04000089 RID: 137
        public const uint CPU_SANDY_BRIDGE_CORE_I7E = 16908546u;

        // Token: 0x0400008A RID: 138
        public const uint CPU_SANDY_BRIDGE_XEON = 16908547u;

        // Token: 0x0400008B RID: 139
        public const uint CPU_SANDY_BRIDGE_CORE_I3 = 16908548u;

        // Token: 0x0400008C RID: 140
        public const uint CPU_SANDY_BRIDGE_CORE_I5 = 16908549u;

        // Token: 0x0400008D RID: 141
        public const uint CPU_SANDY_BRIDGE_PENTIUM = 16908551u;

        // Token: 0x0400008E RID: 142
        public const uint CPU_SANDY_BRIDGE_CELERON = 16908552u;

        // Token: 0x0400008F RID: 143
        public const uint CPU_HASWELL_CORE_I7 = 16908801u;

        // Token: 0x04000090 RID: 144
        public const uint CPU_HASWELL_CORE_I7E = 16908802u;

        // Token: 0x04000091 RID: 145
        public const uint CPU_HASWELL_XEON = 16908803u;

        // Token: 0x04000092 RID: 146
        public const uint CPU_HASWELL_CORE_I3 = 16908804u;

        // Token: 0x04000093 RID: 147
        public const uint CPU_HASWELL_CORE_I5 = 16908805u;

        // Token: 0x04000094 RID: 148
        public const uint CPU_HASWELL_PENTIUM = 16908807u;

        // Token: 0x04000095 RID: 149
        public const uint CPU_HASWELL_CELERON = 16908808u;

        // Token: 0x04000096 RID: 150
        public const uint CPU_HASWELL_CORE_M = 16908809u;

        // Token: 0x04000097 RID: 151
        public const uint CPU_SKYLAKE_XEON = 17039361u;

        // Token: 0x04000098 RID: 152
        public const uint CPU_SKYLAKE_CORE_I7 = 17039362u;

        // Token: 0x04000099 RID: 153
        public const uint CPU_SKYLAKE_CORE_I5 = 17039363u;

        // Token: 0x0400009A RID: 154
        public const uint CPU_SKYLAKE_CORE_I3 = 17039364u;

        // Token: 0x0400009B RID: 155
        public const uint CPU_SKYLAKE_PENTIUM = 17039365u;

        // Token: 0x0400009C RID: 156
        public const uint CPU_SKYLAKE_CELERON = 17039366u;

        // Token: 0x0400009D RID: 157
        public const uint CPU_SKYLAKE_CORE_M7 = 17039367u;

        // Token: 0x0400009E RID: 158
        public const uint CPU_SKYLAKE_CORE_M5 = 17039368u;

        // Token: 0x0400009F RID: 159
        public const uint CPU_SKYLAKE_CORE_M3 = 17039369u;

        // Token: 0x040000A0 RID: 160
        public const uint CPU_SKYLAKE_CORE_I9EX = 17039370u;

        // Token: 0x040000A1 RID: 161
        public const uint CPU_SKYLAKE_CORE_I9X = 17039371u;

        // Token: 0x040000A2 RID: 162
        public const uint CPU_SKYLAKE_CORE_I7X = 17039372u;

        // Token: 0x040000A3 RID: 163
        public const uint CPU_SKYLAKE_CORE_I5X = 17039373u;

        // Token: 0x040000A4 RID: 164
        public const uint CPU_SKYLAKE_XEON_BRONZE = 17039374u;

        // Token: 0x040000A5 RID: 165
        public const uint CPU_SKYLAKE_XEON_SILVER = 17039375u;

        // Token: 0x040000A6 RID: 166
        public const uint CPU_SKYLAKE_XEON_GOLD = 17039376u;

        // Token: 0x040000A7 RID: 167
        public const uint CPU_SKYLAKE_XEON_PLATINIUM = 17039377u;

        // Token: 0x040000A8 RID: 168
        public const uint CPU_SKYLAKE_PENTIUM_GOLD = 17039378u;

        // Token: 0x040000A9 RID: 169
        public const uint CPU_AMD_386 = 33554688u;

        // Token: 0x040000AA RID: 170
        public const uint CPU_AMD_486 = 33554944u;

        // Token: 0x040000AB RID: 171
        public const uint CPU_AMD_K5 = 33555456u;

        // Token: 0x040000AC RID: 172
        public const uint CPU_AMD_K6 = 33556480u;

        // Token: 0x040000AD RID: 173
        public const uint CPU_AMD_K7 = 33558528u;

        // Token: 0x040000AE RID: 174
        public const uint CPU_AMD_K8 = 33562624u;

        // Token: 0x040000AF RID: 175
        public const uint CPU_AMD_K10 = 33570816u;

        // Token: 0x040000B0 RID: 176
        public const uint CPU_AMD_K12 = 33619968u;

        // Token: 0x040000B1 RID: 177
        public const uint CPU_AMD_K14 = 33685504u;

        // Token: 0x040000B2 RID: 178
        public const uint CPU_AMD_K15 = 33816576u;

        // Token: 0x040000B3 RID: 179
        public const uint CPU_AMD_K16 = 34078720u;

        // Token: 0x040000B4 RID: 180
        public const uint CPU_AMD_K17 = 34603008u;

        // Token: 0x040000B5 RID: 181
        public const uint CPU_K5 = 33555457u;

        // Token: 0x040000B6 RID: 182
        public const uint CPU_K5_GEODE = 33555458u;

        // Token: 0x040000B7 RID: 183
        public const uint CPU_K6 = 33556481u;

        // Token: 0x040000B8 RID: 184
        public const uint CPU_K6_2 = 33556482u;

        // Token: 0x040000B9 RID: 185
        public const uint CPU_K6_3 = 33556483u;

        // Token: 0x040000BA RID: 186
        public const uint CPU_K7_ATHLON = 33558529u;

        // Token: 0x040000BB RID: 187
        public const uint CPU_K7_ATHLON_XP = 33558530u;

        // Token: 0x040000BC RID: 188
        public const uint CPU_K7_ATHLON_MP = 33558531u;

        // Token: 0x040000BD RID: 189
        public const uint CPU_K7_DURON = 33558532u;

        // Token: 0x040000BE RID: 190
        public const uint CPU_K7_SEMPRON = 33558533u;

        // Token: 0x040000BF RID: 191
        public const uint CPU_K7_SEMPRON_M = 33558534u;

        // Token: 0x040000C0 RID: 192
        public const uint CPU_K8_ATHLON_64 = 33562625u;

        // Token: 0x040000C1 RID: 193
        public const uint CPU_K8_ATHLON_64_M = 33562626u;

        // Token: 0x040000C2 RID: 194
        public const uint CPU_K8_ATHLON_64_FX = 33562627u;

        // Token: 0x040000C3 RID: 195
        public const uint CPU_K8_OPTERON = 33562628u;

        // Token: 0x040000C4 RID: 196
        public const uint CPU_K8_TURION_64 = 33562629u;

        // Token: 0x040000C5 RID: 197
        public const uint CPU_K8_SEMPRON = 33562630u;

        // Token: 0x040000C6 RID: 198
        public const uint CPU_K8_SEMPRON_M = 33562631u;

        // Token: 0x040000C7 RID: 199
        public const uint CPU_K8_ATHLON_64_X2 = 33562632u;

        // Token: 0x040000C8 RID: 200
        public const uint CPU_K8_TURION_64_X2 = 33562633u;

        // Token: 0x040000C9 RID: 201
        public const uint CPU_K8_ATHLON_NEO = 33562634u;

        // Token: 0x040000CA RID: 202
        public const uint CPU_K10_PHENOM = 33570817u;

        // Token: 0x040000CB RID: 203
        public const uint CPU_K10_PHENOM_X3 = 33570818u;

        // Token: 0x040000CC RID: 204
        public const uint CPU_K10_PHENOM_FX = 33570819u;

        // Token: 0x040000CD RID: 205
        public const uint CPU_K10_OPTERON = 33570820u;

        // Token: 0x040000CE RID: 206
        public const uint CPU_K10_TURION_64 = 33570821u;

        // Token: 0x040000CF RID: 207
        public const uint CPU_K10_TURION_64_ULTRA = 33570822u;

        // Token: 0x040000D0 RID: 208
        public const uint CPU_K10_ATHLON_64 = 33570823u;

        // Token: 0x040000D1 RID: 209
        public const uint CPU_K10_SEMPRON = 33570824u;

        // Token: 0x040000D2 RID: 210
        public const uint CPU_K10_ATHLON_2 = 33570833u;

        // Token: 0x040000D3 RID: 211
        public const uint CPU_K10_ATHLON_2_X2 = 33570827u;

        // Token: 0x040000D4 RID: 212
        public const uint CPU_K10_ATHLON_2_X3 = 33570829u;

        // Token: 0x040000D5 RID: 213
        public const uint CPU_K10_ATHLON_2_X4 = 33570828u;

        // Token: 0x040000D6 RID: 214
        public const uint CPU_K10_PHENOM_II = 33570825u;

        // Token: 0x040000D7 RID: 215
        public const uint CPU_K10_PHENOM_II_X2 = 33570826u;

        // Token: 0x040000D8 RID: 216
        public const uint CPU_K10_PHENOM_II_X3 = 33570830u;

        // Token: 0x040000D9 RID: 217
        public const uint CPU_K10_PHENOM_II_X4 = 33570831u;

        // Token: 0x040000DA RID: 218
        public const uint CPU_K10_PHENOM_II_X6 = 33570832u;

        // Token: 0x040000DB RID: 219
        public const uint CPU_K15_FXB = 33816577u;

        // Token: 0x040000DC RID: 220
        public const uint CPU_K15_OPTERON = 33816578u;

        // Token: 0x040000DD RID: 221
        public const uint CPU_K15_A10T = 33816579u;

        // Token: 0x040000DE RID: 222
        public const uint CPU_K15_A8T = 33816580u;

        // Token: 0x040000DF RID: 223
        public const uint CPU_K15_A6T = 33816581u;

        // Token: 0x040000E0 RID: 224
        public const uint CPU_K15_A4T = 33816582u;

        // Token: 0x040000E1 RID: 225
        public const uint CPU_K15_ATHLON_X4 = 33816583u;

        // Token: 0x040000E2 RID: 226
        public const uint CPU_K15_FXV = 33816584u;

        // Token: 0x040000E3 RID: 227
        public const uint CPU_K15_A10R = 33816585u;

        // Token: 0x040000E4 RID: 228
        public const uint CPU_K15_A8R = 33816586u;

        // Token: 0x040000E5 RID: 229
        public const uint CPU_K15_A6R = 33816587u;

        // Token: 0x040000E6 RID: 230
        public const uint CPU_K15_A4R = 33816588u;

        // Token: 0x040000E7 RID: 231
        public const uint CPU_K15_SEMPRON = 33816589u;

        // Token: 0x040000E8 RID: 232
        public const uint CPU_K15_ATHLON_X2 = 33816590u;

        // Token: 0x040000E9 RID: 233
        public const uint CPU_K15_FXC = 33816591u;

        // Token: 0x040000EA RID: 234
        public const uint CPU_K15_A10C = 33816592u;

        // Token: 0x040000EB RID: 235
        public const uint CPU_K15_A8C = 33816593u;

        // Token: 0x040000EC RID: 236
        public const uint CPU_K15_A6C = 33816594u;

        // Token: 0x040000ED RID: 237
        public const uint CPU_K15_A4C = 33816595u;

        // Token: 0x040000EE RID: 238
        public const uint CPU_K15_A12 = 33816596u;

        // Token: 0x040000EF RID: 239
        public const uint CPU_K15_RX = 33816597u;

        // Token: 0x040000F0 RID: 240
        public const uint CPU_K15_GX = 33816598u;

        // Token: 0x040000F1 RID: 241
        public const uint CPU_K15_A9 = 33816599u;

        // Token: 0x040000F2 RID: 242
        public const uint CPU_K15_E2 = 33816600u;

        // Token: 0x040000F3 RID: 243
        public const uint CPU_K16_A6 = 34078721u;

        // Token: 0x040000F4 RID: 244
        public const uint CPU_K16_A4 = 34078722u;

        // Token: 0x040000F5 RID: 245
        public const uint CPU_K16_OPTERON = 34078725u;

        // Token: 0x040000F6 RID: 246
        public const uint CPU_K16_ATHLON = 34078726u;

        // Token: 0x040000F7 RID: 247
        public const uint CPU_K16_SEMPRON = 34078727u;

        // Token: 0x040000F8 RID: 248
        public const uint CPU_K16_E1 = 34078728u;

        // Token: 0x040000F9 RID: 249
        public const uint CPU_K16_E2 = 34078729u;

        // Token: 0x040000FA RID: 250
        public const uint CPU_K16_A8 = 34078730u;

        // Token: 0x040000FB RID: 251
        public const uint CPU_K16_A10 = 34078731u;

        // Token: 0x040000FC RID: 252
        public const uint CPU_K16_GX = 34078732u;

        // Token: 0x040000FD RID: 253
        public const uint CPU_RYZEN = 34603009u;

        // Token: 0x040000FE RID: 254
        public const uint CPU_RYZEN_7 = 34603010u;

        // Token: 0x040000FF RID: 255
        public const uint CPU_RYZEN_5 = 34603011u;

        // Token: 0x04000100 RID: 256
        public const uint CPU_RYZEN_3 = 34603012u;

        // Token: 0x04000101 RID: 257
        public const uint CPU_RYZEN_TR = 34603013u;

        // Token: 0x04000102 RID: 258
        public const uint CPU_RYZEN_EPYC = 34603014u;

        // Token: 0x04000103 RID: 259
        public const uint CPU_RYZEN_M = 34603015u;

        // Token: 0x04000104 RID: 260
        public const uint CPU_RYZEN_7_M = 34603016u;

        // Token: 0x04000105 RID: 261
        public const uint CPU_RYZEN_5_M = 34603017u;

        // Token: 0x04000106 RID: 262
        public const uint CPU_RYZEN_3_M = 34603018u;

        // Token: 0x04000107 RID: 263
        public const uint CPU_RYZEN_ATHLON = 34603019u;

        // Token: 0x04000108 RID: 264
        public const uint CPU_CX486 = 67109888u;

        // Token: 0x04000109 RID: 265
        public const uint CPU_CX5X86 = 67110144u;

        // Token: 0x0400010A RID: 266
        public const uint CPU_CX6X86 = 67110400u;

        // Token: 0x0400010B RID: 267
        public const uint CPU_VIA_WINCHIP = 134218752u;

        // Token: 0x0400010C RID: 268
        public const uint CPU_VIA_C3 = 134219776u;

        // Token: 0x0400010D RID: 269
        public const uint CPU_VIA_C7 = 134221824u;

        // Token: 0x0400010E RID: 270
        public const uint CPU_VIA_NANO = 134225920u;

        // Token: 0x0400010F RID: 271
        public const uint CPU_C3 = 134219777u;

        // Token: 0x04000110 RID: 272
        public const uint CPU_C7 = 134221825u;

        // Token: 0x04000111 RID: 273
        public const uint CPU_C7_M = 134221826u;

        // Token: 0x04000112 RID: 274
        public const uint CPU_EDEN = 134221827u;

        // Token: 0x04000113 RID: 275
        public const uint CPU_C7_D = 134221828u;

        // Token: 0x04000114 RID: 276
        public const uint CPU_NANO_X2 = 134225921u;

        // Token: 0x04000115 RID: 277
        public const uint CPU_EDEN_X2 = 134225922u;

        // Token: 0x04000116 RID: 278
        public const uint CPU_NANO_X3 = 134225923u;

        // Token: 0x04000117 RID: 279
        public const uint CPU_EDEN_X4 = 134225924u;

        // Token: 0x04000118 RID: 280
        public const uint CPU_QUADCORE = 134225925u;

        // Token: 0x04000119 RID: 281
        public const uint CPU_CX6X86L = 67110401u;

        // Token: 0x0400011A RID: 282
        public const uint CPU_MEDIAGX = 67110402u;

        // Token: 0x0400011B RID: 283
        public const uint CPU_CX6X86MX = 67110403u;

        // Token: 0x0400011C RID: 284
        public const uint CPU_MII = 67110404u;

        // Token: 0x0400011D RID: 285
        public const uint CPU_CRUSOE = 268435457u;

        // Token: 0x0400011E RID: 286
        public const uint CPU_EFFICEON = 268435458u;

        // Token: 0x0400011F RID: 287
        public const uint CPU_VORTEX86_SX = 536870913u;

        // Token: 0x04000120 RID: 288
        public const uint CPU_VORTEX86_EX = 536870914u;

        // Token: 0x04000121 RID: 289
        public const uint CPU_VORTEX86_DX = 536870915u;

        // Token: 0x04000122 RID: 290
        public const uint CPU_VORTEX86_MX = 536870916u;

        // Token: 0x04000123 RID: 291
        public const uint CPU_VORTEX86_DX3 = 536870917u;

        // Token: 0x04000124 RID: 292
        public const int CACHE_TYPE_DATA = 1;

        // Token: 0x04000125 RID: 293
        public const int CACHE_TYPE_INSTRUCTION = 2;

        // Token: 0x04000126 RID: 294
        public const int CACHE_TYPE_UNIFIED = 3;

        // Token: 0x04000127 RID: 295
        public const int CACHE_TYPE_TRACE_CACHE = 4;

        // Token: 0x04000128 RID: 296
        public const int ISET_MMX = 1;

        // Token: 0x04000129 RID: 297
        public const int ISET_EXTENDED_MMX = 2;

        // Token: 0x0400012A RID: 298
        public const int ISET_3DNOW = 3;

        // Token: 0x0400012B RID: 299
        public const int ISET_EXTENDED_3DNOW = 4;

        // Token: 0x0400012C RID: 300
        public const int ISET_SSE = 5;

        // Token: 0x0400012D RID: 301
        public const int ISET_SSE2 = 6;

        // Token: 0x0400012E RID: 302
        public const int ISET_SSE3 = 7;

        // Token: 0x0400012F RID: 303
        public const int ISET_SSSE3 = 8;

        // Token: 0x04000130 RID: 304
        public const int ISET_SSE4_1 = 9;

        // Token: 0x04000131 RID: 305
        public const int ISET_SSE4_2 = 12;

        // Token: 0x04000132 RID: 306
        public const int ISET_SSE4A = 13;

        // Token: 0x04000133 RID: 307
        public const int ISET_XOP = 14;

        // Token: 0x04000134 RID: 308
        public const int ISET_X86_64 = 16;

        // Token: 0x04000135 RID: 309
        public const int ISET_NX = 17;

        // Token: 0x04000136 RID: 310
        public const int ISET_VMX = 18;

        // Token: 0x04000137 RID: 311
        public const int ISET_AES = 19;

        // Token: 0x04000138 RID: 312
        public const int ISET_AVX = 20;

        // Token: 0x04000139 RID: 313
        public const int ISET_AVX2 = 21;

        // Token: 0x0400013A RID: 314
        public const int ISET_FMA3 = 22;

        // Token: 0x0400013B RID: 315
        public const int ISET_FMA4 = 23;

        // Token: 0x0400013C RID: 316
        public const int ISET_RTM = 24;

        // Token: 0x0400013D RID: 317
        public const int ISET_HLE = 25;

        // Token: 0x0400013E RID: 318
        public const int ISET_AVX512F = 26;

        // Token: 0x0400013F RID: 319
        public const int ISET_SHA = 27;

        // Token: 0x04000140 RID: 320
        public const int HWM_CLASS_LPC = 1;

        // Token: 0x04000141 RID: 321
        public const int HWM_CLASS_CPU = 2;

        // Token: 0x04000142 RID: 322
        public const int HWM_CLASS_HDD = 4;

        // Token: 0x04000143 RID: 323
        public const int HWM_CLASS_DISPLAYADAPTER = 8;

        // Token: 0x04000144 RID: 324
        public const int HWM_CLASS_PSU = 16;

        // Token: 0x04000145 RID: 325
        public const int HWM_CLASS_ACPI = 32;

        // Token: 0x04000146 RID: 326
        public const int HWM_CLASS_RAM = 64;

        // Token: 0x04000147 RID: 327
        public const int HWM_CLASS_CHASSIS = 128;

        // Token: 0x04000148 RID: 328
        public const int HWM_CLASS_WATERCOOLER = 256;

        // Token: 0x04000149 RID: 329
        public const int HWM_CLASS_BATTERY = 512;

        // Token: 0x0400014A RID: 330
        public const int SENSOR_CLASS_VOLTAGE = 4096;

        // Token: 0x0400014B RID: 331
        public const int SENSOR_CLASS_TEMPERATURE = 8192;

        // Token: 0x0400014C RID: 332
        public const int SENSOR_CLASS_FAN = 12288;

        // Token: 0x0400014D RID: 333
        public const int SENSOR_CLASS_CURRENT = 16384;

        // Token: 0x0400014E RID: 334
        public const int SENSOR_CLASS_POWER = 20480;

        // Token: 0x0400014F RID: 335
        public const int SENSOR_CLASS_FAN_PWM = 24576;

        // Token: 0x04000150 RID: 336
        public const int SENSOR_CLASS_PUMP_PWM = 28672;

        // Token: 0x04000151 RID: 337
        public const int SENSOR_CLASS_WATER_LEVEL = 32768;

        // Token: 0x04000152 RID: 338
        public const int SENSOR_CLASS_POSITION = 36864;

        // Token: 0x04000153 RID: 339
        public const int SENSOR_CLASS_CAPACITY = 40960;

        // Token: 0x04000154 RID: 340
        public const int SENSOR_CLASS_CASEOPEN = 45056;

        // Token: 0x04000155 RID: 341
        public const int SENSOR_CLASS_LEVEL = 49152;

        // Token: 0x04000156 RID: 342
        public const int SENSOR_CLASS_COUNTER = 53248;

        // Token: 0x04000157 RID: 343
        public const int SENSOR_CLASS_UTILIZATION = 57344;

        // Token: 0x04000158 RID: 344
        public const int SENSOR_CLASS_CLOCK_SPEED = 61440;

        // Token: 0x04000159 RID: 345
        public const int SENSOR_CLASS_BANDWIDTH = 65536;

        // Token: 0x0400015A RID: 346
        public const int SENSOR_CLASS_PERF_LIMITER = 69632;

        // Token: 0x0400015B RID: 347
        public const int SENSOR_VOLTAGE_VCORE = 4198400;

        // Token: 0x0400015C RID: 348
        public const int SENSOR_VOLTAGE_3V3 = 8392704;

        // Token: 0x0400015D RID: 349
        public const int SENSOR_VOLTAGE_P5V = 12587008;

        // Token: 0x0400015E RID: 350
        public const int SENSOR_VOLTAGE_P12V = 16781312;

        // Token: 0x0400015F RID: 351
        public const int SENSOR_VOLTAGE_M5V = 20975616;

        // Token: 0x04000160 RID: 352
        public const int SENSOR_VOLTAGE_M12V = 25169920;

        // Token: 0x04000161 RID: 353
        public const int SENSOR_VOLTAGE_5VSB = 29364224;

        // Token: 0x04000162 RID: 354
        public const int SENSOR_VOLTAGE_DRAM = 33558528;

        // Token: 0x04000163 RID: 355
        public const int SENSOR_VOLTAGE_CPU_VTT = 37752832;

        // Token: 0x04000164 RID: 356
        public const int SENSOR_VOLTAGE_IOH_VCORE = 41947136;

        // Token: 0x04000165 RID: 357
        public const int SENSOR_VOLTAGE_IOH_PLL = 46141440;

        // Token: 0x04000166 RID: 358
        public const int SENSOR_VOLTAGE_CPU_PLL = 50335744;

        // Token: 0x04000167 RID: 359
        public const int SENSOR_VOLTAGE_PCH = 54530048;

        // Token: 0x04000168 RID: 360
        public const int SENSOR_VOLTAGE_CPU_VID = 58724352;

        // Token: 0x04000169 RID: 361
        public const int SENSOR_VOLTAGE_GPU = 62918656;

        // Token: 0x0400016A RID: 362
        public const int SENSOR_TEMPERATURE_CPU = 4202496;

        // Token: 0x0400016B RID: 363
        public const int SENSOR_TEMPERATURE_VREG = 8396800;

        // Token: 0x0400016C RID: 364
        public const int SENSOR_TEMPERATURE_DRAM = 12591104;

        // Token: 0x0400016D RID: 365
        public const int SENSOR_TEMPERATURE_PCH = 16785408;

        // Token: 0x0400016E RID: 366
        public const int SENSOR_TEMPERATURE_GPU = 20979712;

        // Token: 0x0400016F RID: 367
        public const int SENSOR_TEMPERATURE_CPU_DTS = 25174016;

        // Token: 0x04000170 RID: 368
        public const int SENSOR_FAN_CPU = 4206592;

        // Token: 0x04000171 RID: 369
        public const int SENSOR_FAN_GPU = 25178112;

        // Token: 0x04000172 RID: 370
        public const int SENSOR_POWER_CPU = 4214784;

        // Token: 0x04000173 RID: 371
        public const int SENSOR_POWER_CORE = 8409088;

        // Token: 0x04000174 RID: 372
        public const int SENSOR_POWER_CPU_GT = 12603392;

        // Token: 0x04000175 RID: 373
        public const int SENSOR_POWER_GPU = 25186304;

        // Token: 0x04000176 RID: 374
        public const int SENSOR_UTILIZATION_CPU = 4251648;

        // Token: 0x04000177 RID: 375
        public const int SENSOR_UTILIZATION_GPU = 25223168;

        // Token: 0x04000178 RID: 376
        public const int MEMORY_TYPE_SPM_RAM = 1;

        // Token: 0x04000179 RID: 377
        public const int MEMORY_TYPE_RDRAM = 2;

        // Token: 0x0400017A RID: 378
        public const int MEMORY_TYPE_EDO_RAM = 3;

        // Token: 0x0400017B RID: 379
        public const int MEMORY_TYPE_FPM_RAM = 4;

        // Token: 0x0400017C RID: 380
        public const int MEMORY_TYPE_SDRAM = 5;

        // Token: 0x0400017D RID: 381
        public const int MEMORY_TYPE_DDR_SDRAM = 6;

        // Token: 0x0400017E RID: 382
        public const int MEMORY_TYPE_DDR2_SDRAM = 7;

        // Token: 0x0400017F RID: 383
        public const int MEMORY_TYPE_DDR2_SDRAM_FB = 8;

        // Token: 0x04000180 RID: 384
        public const int MEMORY_TYPE_DDR3_SDRAM = 9;

        // Token: 0x04000181 RID: 385
        public const int MEMORY_TYPE_DDR4_SDRAM = 10;

        // Token: 0x04000182 RID: 386
        public const int DISPLAY_CLOCK_DOMAIN_GRAPHICS = 0;

        // Token: 0x04000183 RID: 387
        public const int DISPLAY_CLOCK_DOMAIN_MEMORY = 4;

        // Token: 0x04000184 RID: 388
        public const int DISPLAY_CLOCK_DOMAIN_PROCESSOR = 7;

        // Token: 0x04000185 RID: 389
        public const int MEMORY_TYPE_SDR = 1;

        // Token: 0x04000186 RID: 390
        public const int MEMORY_TYPE_DDR = 2;

        // Token: 0x04000187 RID: 391
        public const int MEMORY_TYPE_LPDDR2 = 9;

        // Token: 0x04000188 RID: 392
        public const int MEMORY_TYPE_DDR2 = 3;

        // Token: 0x04000189 RID: 393
        public const int MEMORY_TYPE_DDR3 = 7;

        // Token: 0x0400018A RID: 394
        public const int MEMORY_TYPE_GDDR2 = 4;

        // Token: 0x0400018B RID: 395
        public const int MEMORY_TYPE_GDDR3 = 5;

        // Token: 0x0400018C RID: 396
        public const int MEMORY_TYPE_GDDR4 = 6;

        // Token: 0x0400018D RID: 397
        public const int MEMORY_TYPE_GDDR5 = 8;

        // Token: 0x0400018E RID: 398
        public const int MEMORY_TYPE_GDDR5X = 10;

        // Token: 0x0400018F RID: 399
        public const int MEMORY_TYPE_HBM1 = 11;

        // Token: 0x04000190 RID: 400
        public const int MEMORY_TYPE_HBM2 = 12;

        // Token: 0x04000191 RID: 401
        public const int MEMORY_TYPE_SDDR4 = 13;

        // Token: 0x04000192 RID: 402
        public const int MEMORY_TYPE_GDDR6 = 14;

        // Token: 0x04000193 RID: 403
        public const int DRIVE_FEATURE_IS_SSD = 1;

        // Token: 0x04000194 RID: 404
        public const int DRIVE_FEATURE_SMART = 2;

        // Token: 0x04000195 RID: 405
        public const int DRIVE_FEATURE_TRIM = 4;

        // Token: 0x04000196 RID: 406
        public const int DRIVE_FEATURE_IS_REMOVABLE = 16;

        // Token: 0x04000197 RID: 407
        public const int BUS_TYPE_SCSI = 1;

        // Token: 0x04000198 RID: 408
        public const int BUS_TYPE_ATAPI = 2;

        // Token: 0x04000199 RID: 409
        public const int BUS_TYPE_ATA = 3;

        // Token: 0x0400019A RID: 410
        public const int BUS_TYPE_IEEE1394 = 4;

        // Token: 0x0400019B RID: 411
        public const int BUS_TYPE_SSA = 5;

        // Token: 0x0400019C RID: 412
        public const int BUS_TYPE_FIBRE = 6;

        // Token: 0x0400019D RID: 413
        public const int BUS_TYPE_USB = 7;

        // Token: 0x0400019E RID: 414
        public const int BUS_TYPE_RAID = 8;

        // Token: 0x0400019F RID: 415
        public const int BUS_TYPE_ISCSI = 9;

        // Token: 0x040001A0 RID: 416
        public const int BUS_TYPE_SAS = 10;

        // Token: 0x040001A1 RID: 417
        public const int BUS_TYPE_SATA = 11;

        // Token: 0x040001A2 RID: 418
        public const int BUS_TYPE_SD = 12;

        // Token: 0x040001A3 RID: 419
        public const int BUS_TYPE_MMC = 13;

        // Token: 0x040001A4 RID: 420
        public const int BUS_TYPE_VIRTUAL = 14;

        // Token: 0x040001A5 RID: 421
        public const int BUS_TYPE_FILEBACKEDVIRTUAL = 15;

        // Token: 0x040001A6 RID: 422
        public const int BUS_TYPE_SPACES = 16;

        // Token: 0x040001A7 RID: 423
        public const int BUS_TYPE_NVME = 17;

        // Token: 0x040001A8 RID: 424
        public const string szDllPath = "";

        // Token: 0x040001A9 RID: 425
        public const string szDllFilename = "dll";

        // Token: 0x040001AA RID: 426
        public const string szDllBitRate = "64-bit";

        // Token: 0x040001AB RID: 427
        protected const string szDllName = "dll";

        // Token: 0x040001AC RID: 428
        public static int I_UNDEFINED_VALUE = -1;

        // Token: 0x040001AD RID: 429
        public static float F_UNDEFINED_VALUE = -1f;

        // Token: 0x040001AE RID: 430
        public static uint MAX_INTEGER = uint.MaxValue;

        // Token: 0x040001AF RID: 431
        public static float MAX_FLOAT = MAX_INTEGER;

        // Token: 0x040001B0 RID: 432
        protected IntPtr objptr = IntPtr.Zero;

        // Token: 0x02000005 RID: 5
        // (Invoke) Token: 0x06000103 RID: 259
        private delegate int CPUIDSDK_fp_GetNumberOfSMBusControllers(IntPtr objptr);

        // Token: 0x02000006 RID: 6
        // (Invoke) Token: 0x06000107 RID: 263
        private delegate int CPUIDSDK_fp_GetSMBusControllerID(IntPtr objptr, int _smb_controller);

        // Token: 0x02000007 RID: 7
        // (Invoke) Token: 0x0600010B RID: 267
        private delegate int CPUIDSDK_fp_ReadSMBus(IntPtr objptr, int _smb_controller, byte _address, int _reg, ref byte _pValue);

        // Token: 0x02000008 RID: 8
        // (Invoke) Token: 0x0600010F RID: 271
        private delegate int CPUIDSDK_fp_ReadSMBusBlock(IntPtr objptr, int _smb_controller, byte _address, int _reg, byte[] _pData);

        // Token: 0x02000009 RID: 9
        // (Invoke) Token: 0x06000113 RID: 275
        private delegate int CPUIDSDK_fp_WriteSMBus(IntPtr objptr, int _smb_controller, byte _address, int _reg, byte _value);

        // Token: 0x0200000A RID: 10
        // (Invoke) Token: 0x06000117 RID: 279
        private delegate int CPUIDSDK_fp_WriteSMBusMixedBytes(IntPtr objptr, int _smb_controller, byte _address, byte[] _pMixedData, int _len);

        // Token: 0x0200000B RID: 11
        private enum PTR : uint
        {
            // Token: 0x040001B2 RID: 434
            PTR0 = 1522578817u,
            // Token: 0x040001B3 RID: 435
            PTR1 = 460994292u,
            // Token: 0x040001B4 RID: 436
            PTR2 = 1423354285u,
            // Token: 0x040001B5 RID: 437
            PTR3 = 3600133419u,
            // Token: 0x040001B6 RID: 438
            PTR4 = 3280963359u,
            // Token: 0x040001B7 RID: 439
            PTR5 = 1084522825u,
            // Token: 0x040001B8 RID: 440
            PTR6 = 14156208u,
            // Token: 0x040001B9 RID: 441
            PTR7 = 1319935321u,
            // Token: 0x040001BA RID: 442
            PTR8 = 2118057085u,
            // Token: 0x040001BB RID: 443
            PTR9 = 4013678199u,
            // Token: 0x040001BC RID: 444
            PTR10 = 3143071406u,
            // Token: 0x040001BD RID: 445
            PTR11 = 361507608u,
            // Token: 0x040001BE RID: 446
            PTR12 = 1802295001u,
            // Token: 0x040001BF RID: 447
            PTR13 = 1154779561u,
            // Token: 0x040001C0 RID: 448
            PTR14 = 671633424u,
            // Token: 0x040001C1 RID: 449
            PTR15 = 2155282670u,
            // Token: 0x040001C2 RID: 450
            PTR16 = 1983310957u,
            // Token: 0x040001C3 RID: 451
            PTR17 = 2523081926u,
            // Token: 0x040001C4 RID: 452
            PTR18 = 3676943955u,
            // Token: 0x040001C5 RID: 453
            PTR19 = 3562121379u,
            // Token: 0x040001C6 RID: 454
            PTR20 = 1422436753u,
            // Token: 0x040001C7 RID: 455
            PTR21 = 2045703133u,
            // Token: 0x040001C8 RID: 456
            PTR22 = 2277576578u,
            // Token: 0x040001C9 RID: 457
            PTR23 = 3516113703u,
            // Token: 0x040001CA RID: 458
            PTR24 = 3945256527u,
            // Token: 0x040001CB RID: 459
            PTR25 = 1993797037u,
            // Token: 0x040001CC RID: 460
            PTR26 = 2631088550u,
            // Token: 0x040001CD RID: 461
            PTR27 = 3532236051u,
            // Token: 0x040001CE RID: 462
            PTR28 = 4231526511u,
            // Token: 0x040001CF RID: 463
            PTR29 = 2634889754u,
            // Token: 0x040001D0 RID: 464
            PTR30 = 796024548u,
            // Token: 0x040001D1 RID: 465
            PTR31 = 3826501671u,
            // Token: 0x040001D2 RID: 466
            PTR32 = 4004502879u,
            // Token: 0x040001D3 RID: 467
            PTR33 = 3574442523u,
            // Token: 0x040001D4 RID: 468
            PTR34 = 1724042629u,
            // Token: 0x040001D5 RID: 469
            PTR35 = 3835676991u,
            // Token: 0x040001D6 RID: 470
            PTR36 = 4027703331u,
            // Token: 0x040001D7 RID: 471
            PTR37 = 1997205013u,
            // Token: 0x040001D8 RID: 472
            PTR38 = 572539968u,
            // Token: 0x040001D9 RID: 473
            PTR39 = 1210224709u,
            // Token: 0x040001DA RID: 474
            PTR40 = 3508118067u,
            // Token: 0x040001DB RID: 475
            PTR41 = 342239436u,
            // Token: 0x040001DC RID: 476
            PTR42 = 939945996u,
            // Token: 0x040001DD RID: 477
            PTR43 = 1323736525u,
            // Token: 0x040001DE RID: 478
            PTR44 = 1608040369u,
            // Token: 0x040001DF RID: 479
            PTR45 = 44303688u,
            // Token: 0x040001E0 RID: 480
            PTR46 = 3215163206u,
            // Token: 0x040001E1 RID: 481
            PTR47 = 1433053909u,
            // Token: 0x040001E2 RID: 482
            PTR48 = 332015508u,
            // Token: 0x040001E3 RID: 483
            PTR49 = 4254989115u,
            // Token: 0x040001E4 RID: 484
            PTR50 = 674254944u,
            // Token: 0x040001E5 RID: 485
            PTR51 = 84150792u,
            // Token: 0x040001E6 RID: 486
            PTR52 = 1567537885u,
            // Token: 0x040001E7 RID: 487
            PTR53 = 2808565454u,
            // Token: 0x040001E8 RID: 488
            PTR54 = 332277660u,
            // Token: 0x040001E9 RID: 489
            PTR55 = 3940931019u,
            // Token: 0x040001EA RID: 490
            PTR56 = 4060603407u,
            // Token: 0x040001EB RID: 491
            PTR57 = 2371689146u,
            // Token: 0x040001EC RID: 492
            PTR58 = 3567495495u,
            // Token: 0x040001ED RID: 493
            PTR59 = 2177958818u,
            // Token: 0x040001EE RID: 494
            PTR60 = 2889177194u,
            // Token: 0x040001EF RID: 495
            PTR61 = 1325964817u,
            // Token: 0x040001F0 RID: 496
            PTR62 = 4225890243u,
            // Token: 0x040001F1 RID: 497
            PTR63 = 1310628925u,
            // Token: 0x040001F2 RID: 498
            PTR64 = 2644196150u,
            // Token: 0x040001F3 RID: 499
            PTR65 = 993818232u,
            // Token: 0x040001F4 RID: 500
            PTR66 = 2739226250u,
            // Token: 0x040001F5 RID: 501
            PTR67 = 2911460114u,
            // Token: 0x040001F6 RID: 502
            PTR68 = 4233361575u,
            // Token: 0x040001F7 RID: 503
            PTR69 = 862742232u,
            // Token: 0x040001F8 RID: 504
            PTR70 = 3911176767u,
            // Token: 0x040001F9 RID: 505
            PTR71 = 1018984824u,
            // Token: 0x040001FA RID: 506
            PTR72 = 581453136u,
            // Token: 0x040001FB RID: 507
            PTR73 = 790650432u,
            // Token: 0x040001FC RID: 508
            PTR74 = 3232334163u,
            // Token: 0x040001FD RID: 509
            PTR75 = 507395196u,
            // Token: 0x040001FE RID: 510
            PTR76 = 974943288u,
            // Token: 0x040001FF RID: 511
            PTR77 = 937979856u,
            // Token: 0x04000200 RID: 512
            PTR78 = 377761032u,
            // Token: 0x04000201 RID: 513
            PTR79 = 1399236301u,
            // Token: 0x04000202 RID: 514
            PTR80 = 1858133377u,
            // Token: 0x04000203 RID: 515
            PTR81 = 1637532469u,
            // Token: 0x04000204 RID: 516
            PTR82 = 2531732942u,
            // Token: 0x04000205 RID: 517
            PTR83 = 1684719829u,
            // Token: 0x04000206 RID: 518
            PTR84 = 1121879485u,
            // Token: 0x04000207 RID: 519
            PTR85 = 343681272u,
            // Token: 0x04000208 RID: 520
            PTR86 = 3622678491u,
            // Token: 0x04000209 RID: 521
            PTR87 = 1246401685u,
            // Token: 0x0400020A RID: 522
            PTR88 = 2330662358u,
            // Token: 0x0400020B RID: 523
            PTR89 = 3261170883u,
            // Token: 0x0400020C RID: 524
            PTR90 = 1769263849u,
            // Token: 0x0400020D RID: 525
            PTR91 = 2706981554u,
            // Token: 0x0400020E RID: 526
            PTR92 = 1182436597u,
            // Token: 0x0400020F RID: 527
            PTR93 = 3935556903u,
            // Token: 0x04000210 RID: 528
            PTR94 = 2339706602u,
            // Token: 0x04000211 RID: 529
            PTR95 = 723015216u,
            // Token: 0x04000212 RID: 530
            PTR96 = 2827702550u,
            // Token: 0x04000213 RID: 531
            PTR97 = 1368302365u,
            // Token: 0x04000214 RID: 532
            PTR98 = 412234020u,
            // Token: 0x04000215 RID: 533
            PTR99 = 2714321810u,
            // Token: 0x04000216 RID: 534
            PTR100 = 3137566214u,
            // Token: 0x04000217 RID: 535
            PTR101 = 46007676u,
            // Token: 0x04000218 RID: 536
            PTR102 = 3673667055u,
            // Token: 0x04000219 RID: 537
            PTR103 = 2245594034u,
            // Token: 0x0400021A RID: 538
            PTR104 = 1495183933u,
            // Token: 0x0400021B RID: 539
            PTR105 = 2325812546u,
            // Token: 0x0400021C RID: 540
            PTR106 = 1985277097u,
            // Token: 0x0400021D RID: 541
            PTR107 = 1157532157u,
            // Token: 0x0400021E RID: 542
            PTR108 = 1044937872u,
            // Token: 0x0400021F RID: 543
            PTR109 = 2034692749u,
            // Token: 0x04000220 RID: 544
            PTR110 = 2758494422u,
            // Token: 0x04000221 RID: 545
            PTR111 = 1471721329u,
            // Token: 0x04000222 RID: 546
            PTR112 = 1531229833u,
            // Token: 0x04000223 RID: 547
            PTR113 = 4224186255u,
            // Token: 0x04000224 RID: 548
            PTR114 = 4070434107u,
            // Token: 0x04000225 RID: 549
            PTR115 = 2638822034u,
            // Token: 0x04000226 RID: 550
            PTR116 = 1645265953u,
            // Token: 0x04000227 RID: 551
            PTR117 = 1649853613u,
            // Token: 0x04000228 RID: 552
            PTR118 = 85330476u,
            // Token: 0x04000229 RID: 553
            PTR119 = 2814463874u,
            // Token: 0x0400022A RID: 554
            PTR120 = 2175206222u,
            // Token: 0x0400022B RID: 555
            PTR121 = 395194140u,
            // Token: 0x0400022C RID: 556
            PTR122 = 355347036u,
            // Token: 0x0400022D RID: 557
            PTR123 = 2952486902u,
            // Token: 0x0400022E RID: 558
            PTR124 = 1410377761u,
            // Token: 0x0400022F RID: 559
            PTR125 = 1964173861u,
            // Token: 0x04000230 RID: 560
            PTR126 = 26739504u,
            // Token: 0x04000231 RID: 561
            PTR127 = 486947340u,
            // Token: 0x04000232 RID: 562
            PTR128 = 1990782289u,
            // Token: 0x04000233 RID: 563
            PTR129 = 2497653182u,
            // Token: 0x04000234 RID: 564
            PTR130 = 638864424u,
            // Token: 0x04000235 RID: 565
            PTR131 = 1217827117u,
            // Token: 0x04000236 RID: 566
            PTR132 = 4215928467u,
            // Token: 0x04000237 RID: 567
            PTR133 = 879388884u,
            // Token: 0x04000238 RID: 568
            PTR134 = 2573939414u,
            // Token: 0x04000239 RID: 569
            PTR135 = 4080658035u,
            // Token: 0x0400023A RID: 570
            PTR136 = 3404568027u,
            // Token: 0x0400023B RID: 571
            PTR137 = 924347952u,
            // Token: 0x0400023C RID: 572
            PTR138 = 2727560486u,
            // Token: 0x0400023D RID: 573
            PTR139 = 3124458614u,
            // Token: 0x0400023E RID: 574
            PTR140 = 2087647453u,
            // Token: 0x0400023F RID: 575
            PTR141 = 1172999125u,
            // Token: 0x04000240 RID: 576
            PTR142 = 307635372u,
            // Token: 0x04000241 RID: 577
            PTR143 = 1517860081u,
            // Token: 0x04000242 RID: 578
            PTR144 = 3473514003u,
            // Token: 0x04000243 RID: 579
            PTR145 = 1920001249u,
            // Token: 0x04000244 RID: 580
            PTR146 = 315762084u,
            // Token: 0x04000245 RID: 581
            PTR147 = 2976604886u,
            // Token: 0x04000246 RID: 582
            PTR148 = 2675785466u,
            // Token: 0x04000247 RID: 583
            PTR149 = 765483840u,
            // Token: 0x04000248 RID: 584
            PTR150 = 3176364710u,
            // Token: 0x04000249 RID: 585
            PTR151 = 1324785133u,
            // Token: 0x0400024A RID: 586
            PTR152 = 1480241269u,
            // Token: 0x0400024B RID: 587
            PTR153 = 3654923187u,
            // Token: 0x0400024C RID: 588
            PTR154 = 1254004093u,
            // Token: 0x0400024D RID: 589
            PTR155 = 1058307624u,
            // Token: 0x0400024E RID: 590
            PTR156 = 3664622811u,
            // Token: 0x0400024F RID: 591
            PTR157 = 2874889910u,
            // Token: 0x04000250 RID: 592
            PTR158 = 1797314113u,
            // Token: 0x04000251 RID: 593
            PTR159 = 2074277701u,
            // Token: 0x04000252 RID: 594
            PTR160 = 1013741784u,
            // Token: 0x04000253 RID: 595
            PTR161 = 665341776u,
            // Token: 0x04000254 RID: 596
            PTR162 = 1245877381u,
            // Token: 0x04000255 RID: 597
            PTR163 = 165417912u,
            // Token: 0x04000256 RID: 598
            PTR164 = 3626348619u,
            // Token: 0x04000257 RID: 599
            PTR165 = 1219137877u,
            // Token: 0x04000258 RID: 600
            PTR166 = 2267483726u,
            // Token: 0x04000259 RID: 601
            PTR167 = 3465387291u,
            // Token: 0x0400025A RID: 602
            PTR168 = 3573524991u,
            // Token: 0x0400025B RID: 603
            PTR169 = 2553491558u,
            // Token: 0x0400025C RID: 604
            PTR170 = 2131295761u,
            // Token: 0x0400025D RID: 605
            PTR171 = 558514836u,
            // Token: 0x0400025E RID: 606
            PTR172 = 3130750262u,
            // Token: 0x0400025F RID: 607
            PTR173 = 222173820u,
            // Token: 0x04000260 RID: 608
            PTR174 = 3525944403u,
            // Token: 0x04000261 RID: 609
            PTR175 = 2497784258u,
            // Token: 0x04000262 RID: 610
            PTR176 = 1278908533u,
            // Token: 0x04000263 RID: 611
            PTR177 = 604522512u,
            // Token: 0x04000264 RID: 612
            PTR178 = 1769526001u,
            // Token: 0x04000265 RID: 613
            PTR179 = 1425582577u,
            // Token: 0x04000266 RID: 614
            PTR180 = 1892213137u,
            // Token: 0x04000267 RID: 615
            PTR181 = 589055544u,
            // Token: 0x04000268 RID: 616
            PTR182 = 3251864487u,
            // Token: 0x04000269 RID: 617
            PTR183 = 4115393175u,
            // Token: 0x0400026A RID: 618
            PTR184 = 1402644277u,
            // Token: 0x0400026B RID: 619
            PTR185 = 2198799902u,
            // Token: 0x0400026C RID: 620
            PTR186 = 4149997239u,
            // Token: 0x0400026D RID: 621
            PTR187 = 2327385458u,
            // Token: 0x0400026E RID: 622
            PTR188 = 2993775842u,
            // Token: 0x0400026F RID: 623
            PTR189 = 1021737420u,
            // Token: 0x04000270 RID: 624
            PTR190 = 3442055763u,
            // Token: 0x04000271 RID: 625
            PTR191 = 2280853478u,
            // Token: 0x04000272 RID: 626
            PTR192 = 1718930665u,
            // Token: 0x04000273 RID: 627
            PTR193 = 2823901346u,
            // Token: 0x04000274 RID: 628
            PTR194 = 1489416589u,
            // Token: 0x04000275 RID: 629
            PTR195 = 171971712u,
            // Token: 0x04000276 RID: 630
            PTR196 = 947024100u,
            // Token: 0x04000277 RID: 631
            PTR197 = 1227002437u,
            // Token: 0x04000278 RID: 632
            PTR198 = 3298789695u,
            // Token: 0x04000279 RID: 633
            PTR199 = 2879608646u,
            // Token: 0x0400027A RID: 634
            PTR200 = 105516180u,
            // Token: 0x0400027B RID: 635
            PTR201 = 4104644943u,
            // Token: 0x0400027C RID: 636
            PTR202 = 1026980460u,
            // Token: 0x0400027D RID: 637
            PTR203 = 3825584139u,
            // Token: 0x0400027E RID: 638
            PTR204 = 2530422182u,
            // Token: 0x0400027F RID: 639
            PTR205 = 3848260287u,
            // Token: 0x04000280 RID: 640
            PTR206 = 3808413183u,
            // Token: 0x04000281 RID: 641
            PTR207 = 3006359138u,
            // Token: 0x04000282 RID: 642
            PTR208 = 1886314717u,
            // Token: 0x04000283 RID: 643
            PTR209 = 4188402507u,
            // Token: 0x04000284 RID: 644
            PTR210 = 2693611802u,
            // Token: 0x04000285 RID: 645
            PTR211 = 2782219178u,
            // Token: 0x04000286 RID: 646
            PTR212 = 86903388u,
            // Token: 0x04000287 RID: 647
            PTR213 = 745429212u,
            // Token: 0x04000288 RID: 648
            PTR214 = 3565398279u,
            // Token: 0x04000289 RID: 649
            PTR215 = 3062328590u,
            // Token: 0x0400028A RID: 650
            PTR216 = 307373220u,
            // Token: 0x0400028B RID: 651
            PTR217 = 4144098819u,
            // Token: 0x0400028C RID: 652
            PTR218 = 2990367866u,
            // Token: 0x0400028D RID: 653
            PTR219 = 540819576u,
            // Token: 0x0400028E RID: 654
            PTR220 = 568083384u,
            // Token: 0x0400028F RID: 655
            PTR221 = 1624949173u,
            // Token: 0x04000290 RID: 656
            PTR222 = 2876724974u,
            // Token: 0x04000291 RID: 657
            PTR223 = 3811559007u,
            // Token: 0x04000292 RID: 658
            PTR224 = 149033412u,
            // Token: 0x04000293 RID: 659
            PTR225 = 3379139283u,
            // Token: 0x04000294 RID: 660
            PTR226 = 3134289314u,
            // Token: 0x04000295 RID: 661
            PTR227 = 1640285065u,
            // Token: 0x04000296 RID: 662
            PTR228 = 1693632997u,
            // Token: 0x04000297 RID: 663
            PTR229 = 2588882078u,
            // Token: 0x04000298 RID: 664
            PTR230 = 984642912u,
            // Token: 0x04000299 RID: 665
            PTR231 = 2819313686u,
            // Token: 0x0400029A RID: 666
            PTR232 = 3569068407u,
            // Token: 0x0400029B RID: 667
            PTR233 = 3914584743u,
            // Token: 0x0400029C RID: 668
            PTR234 = 11665764u,
            // Token: 0x0400029D RID: 669
            PTR235 = 2741585618u,
            // Token: 0x0400029E RID: 670
            PTR236 = 3040963202u,
            // Token: 0x0400029F RID: 671
            PTR237 = 68159520u,
            // Token: 0x040002A0 RID: 672
            PTR238 = 878995656u,
            // Token: 0x040002A1 RID: 673
            PTR239 = 1783551133u,
            // Token: 0x040002A2 RID: 674
            PTR240 = 3185671106u,
            // Token: 0x040002A3 RID: 675
            PTR241 = 2628991334u,
            // Token: 0x040002A4 RID: 676
            PTR242 = 2086729921u,
            // Token: 0x040002A5 RID: 677
            PTR243 = 2140340005u,
            // Token: 0x040002A6 RID: 678
            PTR244 = 4258659243u,
            // Token: 0x040002A7 RID: 679
            PTR245 = 2067461749u,
            // Token: 0x040002A8 RID: 680
            PTR246 = 1956047149u,
            // Token: 0x040002A9 RID: 681
            PTR247 = 2026041733u,
            // Token: 0x040002AA RID: 682
            PTR248 = 959476320u,
            // Token: 0x040002AB RID: 683
            PTR249 = 828269244u,
            // Token: 0x040002AC RID: 684
            PTR250 = 1195806349u,
            // Token: 0x040002AD RID: 685
            PTR251 = 898263828u,
            // Token: 0x040002AE RID: 686
            PTR252 = 1725353389u,
            // Token: 0x040002AF RID: 687
            PTR253 = 2557685990u,
            // Token: 0x040002B0 RID: 688
            PTR254 = 2016342109u,
            // Token: 0x040002B1 RID: 689
            PTR255 = 1761399289u,
            // Token: 0x040002B2 RID: 690
            PTR256 = 2913950558u,
            // Token: 0x040002B3 RID: 691
            PTR257 = 3073601126u,
            // Token: 0x040002B4 RID: 692
            PTR258 = 3033622946u,
            // Token: 0x040002B5 RID: 693
            PTR259 = 4117228239u,
            // Token: 0x040002B6 RID: 694
            PTR260 = 2630695322u,
            // Token: 0x040002B7 RID: 695
            PTR261 = 676483236u,
            // Token: 0x040002B8 RID: 696
            PTR262 = 2262371762u,
            // Token: 0x040002B9 RID: 697
            PTR263 = 2140733233u,
            // Token: 0x040002BA RID: 698
            PTR264 = 104991876u,
            // Token: 0x040002BB RID: 699
            PTR265 = 3779052159u,
            // Token: 0x040002BC RID: 700
            PTR266 = 1761923593u,
            // Token: 0x040002BD RID: 701
            PTR267 = 3868839219u,
            // Token: 0x040002BE RID: 702
            PTR268 = 2449023986u,
            // Token: 0x040002BF RID: 703
            PTR269 = 1018067292u,
            // Token: 0x040002C0 RID: 704
            PTR270 = 3370095039u,
            // Token: 0x040002C1 RID: 705
            PTR271 = 1426893337u,
            // Token: 0x040002C2 RID: 706
            PTR272 = 947548404u,
            // Token: 0x040002C3 RID: 707
            PTR273 = 958034484u,
            // Token: 0x040002C4 RID: 708
            PTR274 = 2105998093u,
            // Token: 0x040002C5 RID: 709
            PTR275 = 870082488u,
            // Token: 0x040002C6 RID: 710
            PTR276 = 3031132502u,
            // Token: 0x040002C7 RID: 711
            PTR277 = 4140166539u,
            // Token: 0x040002C8 RID: 712
            PTR278 = 3925726203u,
            // Token: 0x040002C9 RID: 713
            PTR279 = 494418672u,
            // Token: 0x040002CA RID: 714
            PTR280 = 972452844u,
            // Token: 0x040002CB RID: 715
            PTR281 = 3105452594u,
            // Token: 0x040002CC RID: 716
            PTR282 = 3613634247u,
            // Token: 0x040002CD RID: 717
            PTR283 = 374353056u,
            // Token: 0x040002CE RID: 718
            PTR284 = 3939095955u,
            // Token: 0x040002CF RID: 719
            PTR285 = 2626894118u,
            // Token: 0x040002D0 RID: 720
            PTR286 = 2524392686u,
            // Token: 0x040002D1 RID: 721
            PTR287 = 2939117150u,
            // Token: 0x040002D2 RID: 722
            PTR288 = 803102652u,
            // Token: 0x040002D3 RID: 723
            PTR289 = 3880504983u,
            // Token: 0x040002D4 RID: 724
            PTR290 = 558645912u,
            // Token: 0x040002D5 RID: 725
            PTR291 = 2074146625u
        }

        // Token: 0x0200000C RID: 12
        // (Invoke) Token: 0x0600011B RID: 283
        private delegate IntPtr CPUIDSDK_fp_CreateInstance();

        // Token: 0x0200000D RID: 13
        // (Invoke) Token: 0x0600011F RID: 287
        private delegate void CPUIDSDK_fp_DestroyInstance(IntPtr objptr);

        // Token: 0x0200000E RID: 14
        // (Invoke) Token: 0x06000123 RID: 291
        private delegate int CPUIDSDK_fp_Init(IntPtr objptr, string _szDllPath, string _szDllFilename, int _config_flag, ref int _errorcode, ref int _extended_errorcode);

        // Token: 0x0200000F RID: 15
        // (Invoke) Token: 0x06000127 RID: 295
        private delegate void CPUIDSDK_fp_Close(IntPtr objptr);

        // Token: 0x02000010 RID: 16
        // (Invoke) Token: 0x0600012B RID: 299
        private delegate void CPUIDSDK_fp_RefreshInformation(IntPtr objptr);

        // Token: 0x02000011 RID: 17
        // (Invoke) Token: 0x0600012F RID: 303
        private delegate void CPUIDSDK_fp_GetDllVersion(IntPtr objptr, ref int _version);

        // Token: 0x02000012 RID: 18
        // (Invoke) Token: 0x06000133 RID: 307
        private delegate int CPUIDSDK_fp_GetNbProcessors(IntPtr objptr);

        // Token: 0x02000013 RID: 19
        // (Invoke) Token: 0x06000137 RID: 311
        private delegate int CPUIDSDK_fp_GetProcessorFamily(IntPtr objptr, int _proc_index);

        // Token: 0x02000014 RID: 20
        // (Invoke) Token: 0x0600013B RID: 315
        private delegate int CPUIDSDK_fp_GetProcessorCoreCount(IntPtr objptr, int _proc_index);

        // Token: 0x02000015 RID: 21
        // (Invoke) Token: 0x0600013F RID: 319
        private delegate int CPUIDSDK_fp_GetProcessorThreadCount(IntPtr objptr, int _proc_index);

        // Token: 0x02000016 RID: 22
        // (Invoke) Token: 0x06000143 RID: 323
        private delegate int CPUIDSDK_fp_GetProcessorCoreThreadCount(IntPtr objptr, int _proc_index, int _core_index);

        // Token: 0x02000017 RID: 23
        // (Invoke) Token: 0x06000147 RID: 327
        private delegate int CPUIDSDK_fp_GetProcessorThreadAPICID(IntPtr objptr, int _proc_index, int _core_index, int _thread_index);

        // Token: 0x02000018 RID: 24
        // (Invoke) Token: 0x0600014B RID: 331
        private delegate IntPtr CPUIDSDK_fp_GetProcessorName(IntPtr objptr, int _proc_index);

        // Token: 0x02000019 RID: 25
        // (Invoke) Token: 0x0600014F RID: 335
        private delegate IntPtr CPUIDSDK_fp_GetProcessorCodeName(IntPtr objptr, int _proc_index);

        // Token: 0x0200001A RID: 26
        // (Invoke) Token: 0x06000153 RID: 339
        private delegate IntPtr CPUIDSDK_fp_GetProcessorSpecification(IntPtr objptr, int _proc_index);

        // Token: 0x0200001B RID: 27
        // (Invoke) Token: 0x06000157 RID: 343
        private delegate IntPtr CPUIDSDK_fp_GetProcessorPackage(IntPtr objptr, int _proc_index);

        // Token: 0x0200001C RID: 28
        // (Invoke) Token: 0x0600015B RID: 347
        private delegate IntPtr CPUIDSDK_fp_GetProcessorStepping(IntPtr objptr, int _proc_index);

        // Token: 0x0200001D RID: 29
        // (Invoke) Token: 0x0600015F RID: 351
        private delegate float CPUIDSDK_fp_GetProcessorTDP(IntPtr objptr, int _proc_index);

        // Token: 0x0200001E RID: 30
        // (Invoke) Token: 0x06000163 RID: 355
        private delegate float CPUIDSDK_fp_GetProcessorManufacturingProcess(IntPtr objptr, int _proc_index);

        // Token: 0x0200001F RID: 31
        // (Invoke) Token: 0x06000167 RID: 359
        private delegate int CPUIDSDK_fp_IsProcessorInstructionSetAvailable(IntPtr objptr, int _proc_index, int _iset);

        // Token: 0x02000020 RID: 32
        // (Invoke) Token: 0x0600016B RID: 363
        private delegate float CPUIDSDK_fp_GetProcessorCoreClockFrequency(IntPtr objptr, int _proc_index, int _core_index);

        // Token: 0x02000021 RID: 33
        // (Invoke) Token: 0x0600016F RID: 367
        private delegate float CPUIDSDK_fp_GetProcessorCoreClockMultiplier(IntPtr objptr, int _proc_index, int _core_index);

        // Token: 0x02000022 RID: 34
        // (Invoke) Token: 0x06000173 RID: 371
        private delegate float CPUIDSDK_fp_GetProcessorCoreTemperature(IntPtr objptr, int _proc_index, int _core_index);

        // Token: 0x02000023 RID: 35
        // (Invoke) Token: 0x06000177 RID: 375
        private delegate float CPUIDSDK_fp_GetBusFrequency(IntPtr objptr);

        // Token: 0x02000024 RID: 36
        // (Invoke) Token: 0x0600017B RID: 379
        private delegate float CPUIDSDK_fp_GetProcessorRatedBusFrequency(IntPtr objptr, int _proc_index);

        // Token: 0x02000025 RID: 37
        // (Invoke) Token: 0x0600017F RID: 383
        private delegate int CPUIDSDK_fp_GetProcessorStockClockFrequency(IntPtr objptr, int _proc_index);

        // Token: 0x02000026 RID: 38
        // (Invoke) Token: 0x06000183 RID: 387
        private delegate int CPUIDSDK_fp_GetProcessorStockBusFrequency(IntPtr objptr, int _proc_index);

        // Token: 0x02000027 RID: 39
        // (Invoke) Token: 0x06000187 RID: 391
        private delegate int CPUIDSDK_fp_GetProcessorMaxCacheLevel(IntPtr objptr, int _proc_index);

        // Token: 0x02000028 RID: 40
        // (Invoke) Token: 0x0600018B RID: 395
        private delegate void CPUIDSDK_fp_GetProcessorCacheParameters(IntPtr objptr, int _proc_index, int _cache_level, int _cache_type, ref int _NbCaches, ref int _size);

        // Token: 0x02000029 RID: 41
        // (Invoke) Token: 0x0600018F RID: 399
        private delegate int CPUIDSDK_fp_GetProcessorID(IntPtr objptr, int _proc_index);

        // Token: 0x0200002A RID: 42
        // (Invoke) Token: 0x06000193 RID: 403
        private delegate float CPUIDSDK_fp_GetProcessorVoltageID(IntPtr objptr, int _proc_index);

        // Token: 0x0200002B RID: 43
        // (Invoke) Token: 0x06000197 RID: 407
        private delegate int CPUIDSDK_fp_GetMemoryType(IntPtr objptr);

        // Token: 0x0200002C RID: 44
        // (Invoke) Token: 0x0600019B RID: 411
        private delegate int CPUIDSDK_fp_GetMemorySize(IntPtr objptr);

        // Token: 0x0200002D RID: 45
        // (Invoke) Token: 0x0600019F RID: 415
        private delegate float CPUIDSDK_fp_GetMemoryClockFrequency(IntPtr objptr);

        // Token: 0x0200002E RID: 46
        // (Invoke) Token: 0x060001A3 RID: 419
        private delegate int CPUIDSDK_fp_GetMemoryNumberOfChannels(IntPtr objptr);

        // Token: 0x0200002F RID: 47
        // (Invoke) Token: 0x060001A7 RID: 423
        private delegate float CPUIDSDK_fp_GetMemoryCASLatency(IntPtr objptr);

        // Token: 0x02000030 RID: 48
        // (Invoke) Token: 0x060001AB RID: 427
        private delegate int CPUIDSDK_fp_GetMemoryRAStoCASDelay(IntPtr objptr);

        // Token: 0x02000031 RID: 49
        // (Invoke) Token: 0x060001AF RID: 431
        private delegate int CPUIDSDK_fp_GetMemoryRASPrecharge(IntPtr objptr);

        // Token: 0x02000032 RID: 50
        // (Invoke) Token: 0x060001B3 RID: 435
        private delegate int CPUIDSDK_fp_GetMemoryTRAS(IntPtr objptr);

        // Token: 0x02000033 RID: 51
        // (Invoke) Token: 0x060001B7 RID: 439
        private delegate int CPUIDSDK_fp_GetMemoryTRC(IntPtr objptr);

        // Token: 0x02000034 RID: 52
        // (Invoke) Token: 0x060001BB RID: 443
        private delegate int CPUIDSDK_fp_GetMemoryCommandRate(IntPtr objptr);

        // Token: 0x02000035 RID: 53
        // (Invoke) Token: 0x060001BF RID: 447
        private delegate IntPtr CPUIDSDK_fp_GetNorthBridgeVendor(IntPtr objptr);

        // Token: 0x02000036 RID: 54
        // (Invoke) Token: 0x060001C3 RID: 451
        private delegate IntPtr CPUIDSDK_fp_GetNorthBridgeModel(IntPtr objptr);

        // Token: 0x02000037 RID: 55
        // (Invoke) Token: 0x060001C7 RID: 455
        private delegate IntPtr CPUIDSDK_fp_GetNorthBridgeRevision(IntPtr objptr);

        // Token: 0x02000038 RID: 56
        // (Invoke) Token: 0x060001CB RID: 459
        private delegate IntPtr CPUIDSDK_fp_GetSouthBridgeVendor(IntPtr objptr);

        // Token: 0x02000039 RID: 57
        // (Invoke) Token: 0x060001CF RID: 463
        private delegate IntPtr CPUIDSDK_fp_GetSouthBridgeModel(IntPtr objptr);

        // Token: 0x0200003A RID: 58
        // (Invoke) Token: 0x060001D3 RID: 467
        private delegate IntPtr CPUIDSDK_fp_GetSouthBridgeRevision(IntPtr objptr);

        // Token: 0x0200003B RID: 59
        // (Invoke) Token: 0x060001D7 RID: 471
        private delegate void CPUIDSDK_fp_GetGraphicBusLinkParameters(IntPtr objptr, ref int _bus_type, ref int _link_width);

        // Token: 0x0200003C RID: 60
        // (Invoke) Token: 0x060001DB RID: 475
        private delegate void CPUIDSDK_fp_GetMemorySlotsConfig(IntPtr objptr, ref int _nbslots, ref int _nbusedslots, ref int _slotmap_h, ref int _slotmap_l, ref int _maxmodulesize);

        // Token: 0x0200003D RID: 61
        // (Invoke) Token: 0x060001DF RID: 479
        private delegate IntPtr CPUIDSDK_fp_GetBIOSVendor(IntPtr objptr);

        // Token: 0x0200003E RID: 62
        // (Invoke) Token: 0x060001E3 RID: 483
        private delegate IntPtr CPUIDSDK_fp_GetBIOSVersion(IntPtr objptr);

        // Token: 0x0200003F RID: 63
        // (Invoke) Token: 0x060001E7 RID: 487
        private delegate IntPtr CPUIDSDK_fp_GetBIOSDate(IntPtr objptr);

        // Token: 0x02000040 RID: 64
        // (Invoke) Token: 0x060001EB RID: 491
        private delegate IntPtr CPUIDSDK_fp_GetMainboardVendor(IntPtr objptr);

        // Token: 0x02000041 RID: 65
        // (Invoke) Token: 0x060001EF RID: 495
        private delegate IntPtr CPUIDSDK_fp_GetMainboardModel(IntPtr objptr);

        // Token: 0x02000042 RID: 66
        // (Invoke) Token: 0x060001F3 RID: 499
        private delegate IntPtr CPUIDSDK_fp_GetMainboardRevision(IntPtr objptr);

        // Token: 0x02000043 RID: 67
        // (Invoke) Token: 0x060001F7 RID: 503
        private delegate IntPtr CPUIDSDK_fp_GetMainboardSerialNumber(IntPtr objptr);

        // Token: 0x02000044 RID: 68
        // (Invoke) Token: 0x060001FB RID: 507
        private delegate IntPtr CPUIDSDK_fp_GetSystemManufacturer(IntPtr objptr);

        // Token: 0x02000045 RID: 69
        // (Invoke) Token: 0x060001FF RID: 511
        private delegate IntPtr CPUIDSDK_fp_GetSystemProductName(IntPtr objptr);

        // Token: 0x02000046 RID: 70
        // (Invoke) Token: 0x06000203 RID: 515
        private delegate IntPtr CPUIDSDK_fp_GetSystemVersion(IntPtr objptr);

        // Token: 0x02000047 RID: 71
        // (Invoke) Token: 0x06000207 RID: 519
        private delegate IntPtr CPUIDSDK_fp_GetSystemSerialNumber(IntPtr objptr);

        // Token: 0x02000048 RID: 72
        // (Invoke) Token: 0x0600020B RID: 523
        private delegate IntPtr CPUIDSDK_fp_GetSystemUUID(IntPtr objptr);

        // Token: 0x02000049 RID: 73
        // (Invoke) Token: 0x0600020F RID: 527
        private delegate IntPtr CPUIDSDK_fp_GetSystemSKU(IntPtr objptr);

        // Token: 0x0200004A RID: 74
        // (Invoke) Token: 0x06000213 RID: 531
        private delegate IntPtr CPUIDSDK_fp_GetSystemFamily(IntPtr objptr);

        // Token: 0x0200004B RID: 75
        // (Invoke) Token: 0x06000217 RID: 535
        private delegate IntPtr CPUIDSDK_fp_GetChassisManufacturer(IntPtr objptr);

        // Token: 0x0200004C RID: 76
        // (Invoke) Token: 0x0600021B RID: 539
        private delegate IntPtr CPUIDSDK_fp_GetChassisType(IntPtr objptr);

        // Token: 0x0200004D RID: 77
        // (Invoke) Token: 0x0600021F RID: 543
        private delegate IntPtr CPUIDSDK_fp_GetChassisSerialNumber(IntPtr objptr);

        // Token: 0x0200004E RID: 78
        // (Invoke) Token: 0x06000223 RID: 547
        private delegate int CPUIDSDK_fp_GetMemoryInfosExt(IntPtr objptr, ref IntPtr _szLocation, ref IntPtr _szUsage, ref IntPtr _szCorrection);

        // Token: 0x0200004F RID: 79
        // (Invoke) Token: 0x06000227 RID: 551
        private delegate int CPUIDSDK_fp_GetNumberOfMemoryDevices(IntPtr objptr);

        // Token: 0x02000050 RID: 80
        // (Invoke) Token: 0x0600022B RID: 555
        private delegate int CPUIDSDK_fp_GetMemoryDeviceInfos(IntPtr objptr, int _device_index, ref int _size, ref IntPtr _szFormat);

        // Token: 0x02000051 RID: 81
        // (Invoke) Token: 0x0600022F RID: 559
        private delegate int CPUIDSDK_fp_GetMemoryDeviceInfosExt(IntPtr objptr, int _device_index, ref IntPtr _szDesignation, ref IntPtr _szType, ref int _total_width, ref int _data_width, ref int _speed);

        // Token: 0x02000052 RID: 82
        // (Invoke) Token: 0x06000233 RID: 563
        private delegate int CPUIDSDK_fp_GetProcessorSockets(IntPtr objptr);

        // Token: 0x02000053 RID: 83
        // (Invoke) Token: 0x06000237 RID: 567
        private delegate int CPUIDSDK_fp_GetNumberOfSPDModules(IntPtr objptr);

        // Token: 0x02000054 RID: 84
        // (Invoke) Token: 0x0600023B RID: 571
        private delegate int CPUIDSDK_fp_GetSPDModuleType(IntPtr objptr, int _spd_index);

        // Token: 0x02000055 RID: 85
        // (Invoke) Token: 0x0600023F RID: 575
        private delegate int CPUIDSDK_fp_GetSPDModuleSize(IntPtr objptr, int _spd_index);

        // Token: 0x02000056 RID: 86
        // (Invoke) Token: 0x06000243 RID: 579
        private delegate IntPtr CPUIDSDK_fp_GetSPDModuleFormat(IntPtr objptr, int _spd_index);

        // Token: 0x02000057 RID: 87
        // (Invoke) Token: 0x06000247 RID: 583
        private delegate IntPtr CPUIDSDK_fp_GetSPDModuleManufacturer(IntPtr objptr, int _spd_index);

        // Token: 0x02000058 RID: 88
        // (Invoke) Token: 0x0600024B RID: 587
        private delegate int CPUIDSDK_fp_GetSPDModuleManufacturerID(IntPtr objptr, int _spd_index, byte[] _id);

        // Token: 0x02000059 RID: 89
        // (Invoke) Token: 0x0600024F RID: 591
        private delegate IntPtr CPUIDSDK_fp_GetSPDModuleDRAMManufacturer(IntPtr objptr, int _spd_index);

        // Token: 0x0200005A RID: 90
        // (Invoke) Token: 0x06000253 RID: 595
        private delegate int CPUIDSDK_fp_GetSPDModuleMaxFrequency(IntPtr objptr, int _spd_index);

        // Token: 0x0200005B RID: 91
        // (Invoke) Token: 0x06000257 RID: 599
        private delegate IntPtr CPUIDSDK_fp_GetSPDModuleSpecification(IntPtr objptr, int _spd_index);

        // Token: 0x0200005C RID: 92
        // (Invoke) Token: 0x0600025B RID: 603
        private delegate IntPtr CPUIDSDK_fp_GetSPDModulePartNumber(IntPtr objptr, int _spd_index);

        // Token: 0x0200005D RID: 93
        // (Invoke) Token: 0x0600025F RID: 607
        private delegate IntPtr CPUIDSDK_fp_GetSPDModuleSerialNumber(IntPtr objptr, int _spd_index);

        // Token: 0x0200005E RID: 94
        // (Invoke) Token: 0x06000263 RID: 611
        private delegate float CPUIDSDK_fp_GetSPDModuleMinTRCD(IntPtr objptr, int _spd_index);

        // Token: 0x0200005F RID: 95
        // (Invoke) Token: 0x06000267 RID: 615
        private delegate float CPUIDSDK_fp_GetSPDModuleMinTRP(IntPtr objptr, int _spd_index);

        // Token: 0x02000060 RID: 96
        // (Invoke) Token: 0x0600026B RID: 619
        private delegate float CPUIDSDK_fp_GetSPDModuleMinTRAS(IntPtr objptr, int _spd_index);

        // Token: 0x02000061 RID: 97
        // (Invoke) Token: 0x0600026F RID: 623
        private delegate float CPUIDSDK_fp_GetSPDModuleMinTRC(IntPtr objptr, int _spd_index);

        // Token: 0x02000062 RID: 98
        // (Invoke) Token: 0x06000273 RID: 627
        private delegate int CPUIDSDK_fp_GetSPDModuleManufacturingDate(IntPtr objptr, int _spd_index, ref int _year, ref int _week);

        // Token: 0x02000063 RID: 99
        // (Invoke) Token: 0x06000277 RID: 631
        private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfBanks(IntPtr objptr, int _spd_index);

        // Token: 0x02000064 RID: 100
        // (Invoke) Token: 0x0600027B RID: 635
        private delegate int CPUIDSDK_fp_GetSPDModuleDataWidth(IntPtr objptr, int _spd_index);

        // Token: 0x02000065 RID: 101
        // (Invoke) Token: 0x0600027F RID: 639
        private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfProfiles(IntPtr objptr, int _spd_index);

        // Token: 0x02000066 RID: 102
        // (Invoke) Token: 0x06000283 RID: 643
        private delegate void CPUIDSDK_fp_GetSPDModuleProfileInfos(IntPtr objptr, int _spd_index, int _profile_index, ref float _frequency, ref float _tCL, ref float _nominal_vdd);

        // Token: 0x02000067 RID: 103
        // (Invoke) Token: 0x06000287 RID: 647
        private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfEPPProfiles(IntPtr objptr, int _spd_index, ref int _epp_revision);

        // Token: 0x02000068 RID: 104
        // (Invoke) Token: 0x0600028B RID: 651
        private delegate void CPUIDSDK_fp_GetSPDModuleEPPProfileInfos(IntPtr objptr, int _spd_index, int _profile_index, ref float _frequency, ref float _tCL, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _tRC, ref float _nominal_vdd);

        // Token: 0x02000069 RID: 105
        // (Invoke) Token: 0x0600028F RID: 655
        private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfXMPProfiles(IntPtr objptr, int _spd_index, ref int _xmp_revision);

        // Token: 0x0200006A RID: 106
        // (Invoke) Token: 0x06000293 RID: 659
        private delegate int CPUIDSDK_fp_GetSPDModuleXMPProfileNumberOfCL(IntPtr objptr, int _spd_index, int _profile_index);

        // Token: 0x0200006B RID: 107
        // (Invoke) Token: 0x06000297 RID: 663
        private delegate void CPUIDSDK_fp_GetSPDModuleXMPProfileCLInfos(IntPtr objptr, int _spd_index, int _profile_index, int _cl_index, ref float _frequency, ref float _CL);

        // Token: 0x0200006C RID: 108
        // (Invoke) Token: 0x0600029B RID: 667
        private delegate void CPUIDSDK_fp_GetSPDModuleXMPProfileInfos(IntPtr objptr, int _spd_index, int _profile_index, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _nominal_vdd, ref int _max_freq, ref float _max_CL);

        // Token: 0x0200006D RID: 109
        // (Invoke) Token: 0x0600029F RID: 671
        private delegate int CPUIDSDK_fp_GetSPDModuleNumberOfAMPProfiles(IntPtr objptr, int _spd_index, ref int _amp_revision);

        // Token: 0x0200006E RID: 110
        // (Invoke) Token: 0x060002A3 RID: 675
        private delegate void CPUIDSDK_fp_GetSPDModuleAMPProfileInfos(IntPtr objptr, int _spd_index, int _profile_index, ref int _frequency, ref float _min_cycle_time, ref float _tCL, ref float _tRCD, ref float _tRAS, ref float _tRP, ref float _tRC);

        // Token: 0x0200006F RID: 111
        // (Invoke) Token: 0x060002A7 RID: 679
        private delegate int CPUIDSDK_fp_GetSPDModuleRawData(IntPtr objptr, int _spd_index, int _offset);

        // Token: 0x02000070 RID: 112
        // (Invoke) Token: 0x060002AB RID: 683
        private delegate int CPUIDSDK_fp_GetNumberOfDisplayAdapter(IntPtr objptr);

        // Token: 0x02000071 RID: 113
        // (Invoke) Token: 0x060002AF RID: 687
        private delegate int CPUIDSDK_fp_GetDisplayAdapterID(IntPtr objptr, int _adapter_index);

        // Token: 0x02000072 RID: 114
        // (Invoke) Token: 0x060002B3 RID: 691
        private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterName(IntPtr objptr, int _adapter_index);

        // Token: 0x02000073 RID: 115
        // (Invoke) Token: 0x060002B7 RID: 695
        private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterCodeName(IntPtr objptr, int _adapter_index);

        // Token: 0x02000074 RID: 116
        // (Invoke) Token: 0x060002BB RID: 699
        private delegate int CPUIDSDK_fp_GetDisplayAdapterNumberOfPerformanceLevels(IntPtr objptr, int _adapter_index);

        // Token: 0x02000075 RID: 117
        // (Invoke) Token: 0x060002BF RID: 703
        private delegate int CPUIDSDK_fp_GetDisplayAdapterCurrentPerformanceLevel(IntPtr objptr, int _adapter_index);

        // Token: 0x02000076 RID: 118
        // (Invoke) Token: 0x060002C3 RID: 707
        private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterPerformanceLevelName(IntPtr objptr, int _adapter_index, int _perf_level);

        // Token: 0x02000077 RID: 119
        // (Invoke) Token: 0x060002C7 RID: 711
        private delegate float CPUIDSDK_fp_GetDisplayAdapterClock(IntPtr objptr, int _perf_level, int _adapter_index, int _domain);

        // Token: 0x02000078 RID: 120
        // (Invoke) Token: 0x060002CB RID: 715
        private delegate float CPUIDSDK_fp_GetDisplayAdapterStockClock(IntPtr objptr, int _perf_level, int _adapter_index, int _domain);

        // Token: 0x02000079 RID: 121
        // (Invoke) Token: 0x060002CF RID: 719
        private delegate float CPUIDSDK_fp_GetDisplayAdapterTemperature(IntPtr objptr, int _adapter_index, int _domain);

        // Token: 0x0200007A RID: 122
        // (Invoke) Token: 0x060002D3 RID: 723
        private delegate int CPUIDSDK_fp_GetDisplayAdapterFanSpeed(IntPtr objptr, int _adapter_index);

        // Token: 0x0200007B RID: 123
        // (Invoke) Token: 0x060002D7 RID: 727
        private delegate int CPUIDSDK_fp_GetDisplayAdapterFanPWM(IntPtr objptr, int _adapter_index);

        // Token: 0x0200007C RID: 124
        // (Invoke) Token: 0x060002DB RID: 731
        private delegate int CPUIDSDK_fp_GetDisplayAdapterMemoryType(IntPtr objptr, int _adapter_index, ref int _type);

        // Token: 0x0200007D RID: 125
        // (Invoke) Token: 0x060002DF RID: 735
        private delegate int CPUIDSDK_fp_GetDisplayAdapterMemorySize(IntPtr objptr, int _adapter_index, ref int _size);

        // Token: 0x0200007E RID: 126
        // (Invoke) Token: 0x060002E3 RID: 739
        private delegate int CPUIDSDK_fp_GetDisplayAdapterMemoryBusWidth(IntPtr objptr, int _adapter_index, ref int _bus_width);

        // Token: 0x0200007F RID: 127
        // (Invoke) Token: 0x060002E7 RID: 743
        private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterMemoryVendor(IntPtr objptr, int _adapter_index);

        // Token: 0x02000080 RID: 128
        // (Invoke) Token: 0x060002EB RID: 747
        private delegate IntPtr CPUIDSDK_fp_GetDisplayDriverVersion(IntPtr objptr);

        // Token: 0x02000081 RID: 129
        // (Invoke) Token: 0x060002EF RID: 751
        private delegate IntPtr CPUIDSDK_fp_GetDirectXVersion(IntPtr objptr);

        // Token: 0x02000082 RID: 130
        // (Invoke) Token: 0x060002F3 RID: 755
        private delegate int CPUIDSDK_fp_GetDisplayAdapterBusInfos(IntPtr objptr, int _adapter_index, ref int _bus_type, ref int _multi_vpu);

        // Token: 0x02000083 RID: 131
        // (Invoke) Token: 0x060002F7 RID: 759
        private delegate float CPUIDSDK_fp_GetDisplayAdapterManufacturingProcess(IntPtr objptr, int _adapter_index);

        // Token: 0x02000084 RID: 132
        // (Invoke) Token: 0x060002FB RID: 763
        private delegate IntPtr CPUIDSDK_fp_GetDisplayAdapterCoreFamily(IntPtr objptr, int _adapter_index, ref int _core);

        // Token: 0x02000085 RID: 133
        // (Invoke) Token: 0x060002FF RID: 767
        private delegate int CPUIDSDK_fp_GetNumberOfMonitors(IntPtr objptr);

        // Token: 0x02000086 RID: 134
        // (Invoke) Token: 0x06000303 RID: 771
        private delegate IntPtr CPUIDSDK_fp_GetMonitorName(IntPtr objptr, int _monitor_index);

        // Token: 0x02000087 RID: 135
        // (Invoke) Token: 0x06000307 RID: 775
        private delegate IntPtr CPUIDSDK_fp_GetMonitorVendor(IntPtr objptr, int _monitor_index);

        // Token: 0x02000088 RID: 136
        // (Invoke) Token: 0x0600030B RID: 779
        private delegate IntPtr CPUIDSDK_fp_GetMonitorID(IntPtr objptr, int _monitor_index);

        // Token: 0x02000089 RID: 137
        // (Invoke) Token: 0x0600030F RID: 783
        private delegate IntPtr CPUIDSDK_fp_GetMonitorSerial(IntPtr objptr, int _monitor_index);

        // Token: 0x0200008A RID: 138
        // (Invoke) Token: 0x06000313 RID: 787
        private delegate int CPUIDSDK_fp_GetMonitorManufacturingDate(IntPtr objptr, int _monitor_index, ref int _week, ref int _year);

        // Token: 0x0200008B RID: 139
        // (Invoke) Token: 0x06000317 RID: 791
        private delegate float CPUIDSDK_fp_GetMonitorSize(IntPtr objptr, int _monitor_index);

        // Token: 0x0200008C RID: 140
        // (Invoke) Token: 0x0600031B RID: 795
        private delegate int CPUIDSDK_fp_GetMonitorResolution(IntPtr objptr, int _monitor_index, ref int _width, ref int _height, ref int _frequency);

        // Token: 0x0200008D RID: 141
        // (Invoke) Token: 0x0600031F RID: 799
        private delegate int CPUIDSDK_fp_GetMonitorMaxPixelClock(IntPtr objptr, int _monitor_index);

        // Token: 0x0200008E RID: 142
        // (Invoke) Token: 0x06000323 RID: 803
        private delegate float CPUIDSDK_fp_GetMonitorGamma(IntPtr objptr, int _monitor_index);

        // Token: 0x0200008F RID: 143
        // (Invoke) Token: 0x06000327 RID: 807
        private delegate int CPUIDSDK_fp_GetNumberOfStorageDevice(IntPtr objptr);

        // Token: 0x02000090 RID: 144
        // (Invoke) Token: 0x0600032B RID: 811
        private delegate int CPUIDSDK_fp_GetStorageDriveNumber(IntPtr objptr, int _index);

        // Token: 0x02000091 RID: 145
        // (Invoke) Token: 0x0600032F RID: 815
        private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceName(IntPtr objptr, int _index);

        // Token: 0x02000092 RID: 146
        // (Invoke) Token: 0x06000333 RID: 819
        private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceRevision(IntPtr objptr, int _index);

        // Token: 0x02000093 RID: 147
        // (Invoke) Token: 0x06000337 RID: 823
        private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceSerialNumber(IntPtr objptr, int _index);

        // Token: 0x02000094 RID: 148
        // (Invoke) Token: 0x0600033B RID: 827
        private delegate int CPUIDSDK_fp_GetStorageDeviceBusType(IntPtr objptr, int _index);

        // Token: 0x02000095 RID: 149
        // (Invoke) Token: 0x0600033F RID: 831
        private delegate int CPUIDSDK_fp_GetStorageDeviceRotationSpeed(IntPtr objptr, int _index);

        // Token: 0x02000096 RID: 150
        // (Invoke) Token: 0x06000343 RID: 835
        private delegate int CPUIDSDK_fp_GetStorageDeviceFeatureFlag(IntPtr objptr, int _index);

        // Token: 0x02000097 RID: 151
        // (Invoke) Token: 0x06000347 RID: 839
        private delegate int CPUIDSDK_fp_GetStorageDeviceNumberOfVolumes(IntPtr objptr, int _index);

        // Token: 0x02000098 RID: 152
        // (Invoke) Token: 0x0600034B RID: 843
        private delegate IntPtr CPUIDSDK_fp_GetStorageDeviceVolumeLetter(IntPtr objptr, int _index, int _volume_index);

        // Token: 0x02000099 RID: 153
        // (Invoke) Token: 0x0600034F RID: 847
        private delegate float CPUIDSDK_fp_GetStorageDeviceVolumeTotalCapacity(IntPtr objptr, int _index, int _volume_index);

        // Token: 0x0200009A RID: 154
        // (Invoke) Token: 0x06000353 RID: 851
        private delegate float CPUIDSDK_fp_GetStorageDeviceVolumeAvailableCapacity(IntPtr objptr, int _index, int _volume_index);

        // Token: 0x0200009B RID: 155
        // (Invoke) Token: 0x06000357 RID: 855
        private delegate int CPUIDSDK_fp_GetStorageDeviceSmartAttribute(IntPtr objptr, int _index, int _attrib_index, ref int _id, ref int _flags, ref int _value, ref int _worst, byte[] _data);

        // Token: 0x0200009C RID: 156
        // (Invoke) Token: 0x0600035B RID: 859
        private delegate int CPUIDSDK_fp_GetStorageDevicePowerOnHours(IntPtr objptr, int _index);

        // Token: 0x0200009D RID: 157
        // (Invoke) Token: 0x0600035F RID: 863
        private delegate int CPUIDSDK_fp_GetStorageDevicePowerCycleCount(IntPtr objptr, int _index);

        // Token: 0x0200009E RID: 158
        // (Invoke) Token: 0x06000363 RID: 867
        private delegate float CPUIDSDK_fp_GetStorageDeviceTotalCapacity(IntPtr objptr, int _index);

        // Token: 0x0200009F RID: 159
        // (Invoke) Token: 0x06000367 RID: 871
        private delegate int CPUIDSDK_fp_GetNumberOfDevices(IntPtr objptr);

        // Token: 0x020000A0 RID: 160
        // (Invoke) Token: 0x0600036B RID: 875
        private delegate int CPUIDSDK_fp_GetDeviceClass(IntPtr objptr, int _device_index);

        // Token: 0x020000A1 RID: 161
        // (Invoke) Token: 0x0600036F RID: 879
        private delegate IntPtr CPUIDSDK_fp_GetDeviceName(IntPtr objptr, int _device_index);

        // Token: 0x020000A2 RID: 162
        // (Invoke) Token: 0x06000373 RID: 883
        private delegate int CPUIDSDK_fp_GetNumberOfSensors(IntPtr objptr, int _device_index, int _sensor_class);

        // Token: 0x020000A3 RID: 163
        // (Invoke) Token: 0x06000377 RID: 887
        private delegate int CPUIDSDK_fp_GetSensorInfos(IntPtr objptr, int _device_index, int _sensor_index, int _sensor_class, ref int _sensor_id, ref IntPtr _szNamePtr, ref int _raw_value, ref float _value, ref float _min_value, ref float _max_value);

        // Token: 0x020000A4 RID: 164
        // (Invoke) Token: 0x0600037B RID: 891
        private delegate void CPUIDSDK_fp_SensorClearMinMax(IntPtr objptr, int _device_index, int _sensor_index, int _sensor_class);

        // Token: 0x020000A5 RID: 165
        // (Invoke) Token: 0x0600037F RID: 895
        private delegate float CPUIDSDK_fp_GetSensorTypeValue(IntPtr objptr, int _sensor_type, ref int _device_index, ref int _sensor_index);
    }
}
