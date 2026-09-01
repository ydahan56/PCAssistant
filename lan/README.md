# LAN Plugin - Network Scanner and Monitor

## Overview
The LAN plugin provides network scanning and monitoring capabilities for PCAssistant. It uses the `wnet.exe` utility to discover devices on the local network and can monitor for devices connecting/disconnecting in real-time.

## Features

### 1. **Network Scan** (`/lan --operation scan`)
- Performs a one-time scan of the local network
- Discovers all active devices
- Reports device information (IP, MAC, vendor, name)
- Uses timeout of 2 minutes for scan completion

### 2. **Network Monitor** (`/lan --operation listen`)
- Continuously monitors the network for changes
- Detects when devices connect
- Detects when devices disconnect
- Runs in background until disabled
- Scans every 3 seconds

### 3. **Disable Monitor** (`/lan --operation disable`)
- Stops the network monitoring
- Terminates background scan processes
- Cleans up resources

## Architecture

### Class Structure

```
lan/
├── DllMain.cs                    # Plugin entry point
├── HostComparison.cs             # Host equality comparer
├── Models/
│   ├── Host.cs                   # Network device model
│   └── HostsArg.cs               # XML deserializ model
├── Operations/
│   ├── Scanner.cs                # One-time network scan
│   └── Listener.cs               # Continuous monitoring
└── Types/
	├── OperationBase.cs          # Base class for operations
	└── OperationType.cs          # Operation enum
```

### Base Class: OperationBase

Provides common functionality for all operations:

```csharp
public abstract class OperationBase
{
	protected readonly string directoryuri;  // App directory
	protected readonly string programuri;    // wnet.exe path
	protected readonly string scanPath;      // XML output path

	protected List<Host> ReadHosts(string path);           // Parse XML results
	protected void RaiseDiscovered(List<Host> hosts);      // Format scan results
	protected void RaiseConnected(List<Host> hosts);       // Format connect events
	protected void RaiseDisconnected(List<Host> hosts);    // Format disconnect events
	protected abstract void RaiseFeedback(string message); // Send feedback to user
	public abstract void Execute();                        // Execute operation
}
```

### Scanner Operation

Performs a single network scan:

1. Validates `wnet.exe` exists
2. Starts scan process with XML output
3. Waits up to 2 minutes for completion
4. Reads and parses XML results
5. Displays discovered devices

```csharp
public class Scanner : OperationBase
{
	public override void Execute()
	{
		// Start wnet.exe process
		// Wait for completion
		// Read and display results
	}
}
```

### Listener Operation

Continuously monitors network changes:

1. Runs initial scan to get baseline
2. Scans every 3 seconds
3. Compares current scan with previous scan
4. Reports new connections
5. Reports disconnections
6. Runs until explicitly disabled

```csharp
public class Listener : OperationBase
{
	private bool _active;
	private Thread workerThread;

	public override void Execute()
	{
		// Start background monitoring
	}

	public void Disable()
	{
		// Stop monitoring
	}
}
```

## Usage

### Scan Network Once
```
/lan --operation scan
```

**Output:**
```
🔍 Scanning local network...
📡 Found 5 device(s) on the network:

🖥️ Desktop-PC
   IP: 192.168.1.100
   MAC: AA:BB:CC:DD:EE:FF
   Vendor: Intel Corporation

🖥️ iPhone
   IP: 192.168.1.101
   MAC: 11:22:33:44:55:66
   Vendor: Apple Inc.
...
```

### Start Network Monitoring
```
/lan --operation listen
```

**Output:**
```
👂 Network monitor is now listening...

✅ 1 device(s) connected:
  • Smart-TV (192.168.1.150)

❌ 1 device(s) disconnected:
  • Laptop (192.168.1.105)
```

### Stop Network Monitoring
```
/lan --operation disable
```

**Output:**
```
🛑 Network monitoring disabled.
```

## Dependencies

### External Tool
- **wnet.exe**: Network scanning utility
  - Must be in the same directory as the plugin
  - Generates XML output with device information
  - Location: `{AppDirectory}/wnet.exe`

### XML Output Format
The scanner generates an XML file (`networkscan.xml`) with this structure:

```xml
<devices_connected_to_your_network>
  <item>
	<ip_address>192.168.1.100</ip_address>
	<device_name>Desktop-PC</device_name>
	<mac_address>AA:BB:CC:DD:EE:FF</mac_address>
	<network_adapter_company>Intel Corporation</network_adapter_company>
	<device_information>Computer</device_information>
	<user_text></user_text>
	<first_detected_on>2024-01-01</first_detected_on>
	<last_detected_on>2024-01-01</last_detected_on>
	<detection_count>1</detection_count>
	<active>Yes</active>
  </item>
  <!-- more items -->
</devices_connected_to_your_network>
```

## Implementation Details

### Host Comparison
Hosts are compared by MAC address (primary) or IP address (fallback):

```csharp
public class HostComparison : IEqualityComparer<Host>
{
	public bool Equals(Host x, Host y)
	{
		// Compare by MAC address, fallback to IP
	}

	public int GetHashCode(Host obj)
	{
		// Hash based on MAC or IP
	}
}
```

### Thread Safety
The Listener uses thread-safe patterns:
- `_cancel_lock` for process termination
- Background threads for scanning
- Clean shutdown mechanism

### Error Handling
- Missing `wnet.exe`: Clear error message
- Scan timeout: 2-minute limit with notification
- Process failures: Caught and reported
- Empty results: Handled gracefully

## Configuration

### Scan Interval
Listener scans every **3 seconds** (configurable in `Listener.cs`):
```csharp
Thread.Sleep(3000); // Wait between scans
```

### Scan Timeout
Scanner waits maximum **2 minutes** per scan:
```csharp
var timeout = TimeSpan.FromMinutes(2);
```

### File Paths
- **Program**: `{AppDirectory}/wnet.exe`
- **Output**: `{AppDirectory}/networkscan.xml`

## Error Messages

| Message | Meaning |
|---------|---------|
| `❌ Network scanner not found` | `wnet.exe` not in app directory |
| `❌ Failed to start network scanner process` | Process start failed |
| `⏱️ Scan timed out after 2 minutes` | Scan took too long |
| `❌ Scan completed but no results file` | XML file not created |
| `❌ Scan failed: {error}` | General scan error |
| `ℹ️ Network is already being monitored` | Listener already active |
| `ℹ️ Network is not being monitored` | Trying to disable

 inactive listener |

## Best Practices

### For Scanning
1. Wait for scan to complete before issuing another
2. Check for `wnet.exe` presence before deploying
3. Ensure write permissions for XML output

### For Monitoring
1. Disable listener before re-enabling
2. Only one listener instance per application
3. Disable before application shutdown

### For Development
1. Test with various network configurations
2. Handle missing `wnet.exe` gracefully
3. Test timeout scenarios
4. Verify XML parsing with malformed data

## Performance Notes

- **Scan Duration**: Depends on network size (typically 10-60 seconds)
- **Memory Usage**: Minimal - only stores current and previous scan
- **CPU Usage**: Low - mostly waiting for external process
- **Network Impact**: Passive scanning, no aggressive probing

## Future Enhancements

Potential improvements:
- [ ] Configurable scan interval
- [ ] Filter devices by type
- [ ] Export results to file
- [ ] Integration with notification system
- [ ] Device history tracking
- [ ] Custom device names/aliases
- [ ] Port scanning
- [ ] Service detection

## Troubleshooting

### Scan Returns No Devices
1. Check `wnet.exe` is present
2. Verify network adapter is active
3. Check firewall settings
4. Run as administrator

### Monitoring Not Detecting Changes
1. Verify scan interval is appropriate
2. Check device has stable MAC/IP
3. Ensure `networkscan.xml` is being updated

### Process Hangs
1. Check for multiple listener instances
2. Verify `wnet.exe` isn't corrupted
3. Restart application
4. Check available disk space

## Summary

The LAN plugin provides robust network scanning and monitoring capabilities with:
- ✅ Simple interface (3 operations)
- ✅ Real-time monitoring
- ✅ Clean thread management
- ✅ Comprehensive error handling
- ✅ Minimal resource usage
- ✅ Extensible architecture

Perfect for monitoring home/office networks through Telegram bot commands!
