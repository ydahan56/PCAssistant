[int]$brightness = $args[0] #read 'brightness' from Agent
[int]$delay = 1
$myMonitor = Get-WmiObject -Namespace root\wmi -Class WmiMonitorBrightnessMethods
[int]$result = $myMonitor.wmisetbrightness($delay, $brightness)

if($result -eq 0)
{
	write-host("Successfully adjusted brightness to {0}%." -f $brightness)
}
else
{
	write-host("Function returned with error code {0}" -f $result)
}