using msinfo32.Helpers;

namespace msinfo32.Components
{
    public class UpTimeComponnet : IComponent
    {
        public string GetInformation()
        {
            var _tick = Kernel32Helper.GetTickCount64();

            var ts = TimeSpan.FromMilliseconds(_tick);
            var fmt = this.FormatTick(ts);

            return $"*Up Time*: {fmt}";
        }

        private string FormatTick(TimeSpan ts)
        {
            if (ts.TotalSeconds <= 1)
            {
                return $@"{ts:s\.ff} second(s)";
            }

            if (ts.TotalMinutes <= 1)
            {
                return $@"{ts:%s} second(s)";
            }

            if (ts.TotalHours <= 1)
            {
                return $@"{ts:%m} minute(s)";
            }

            if (ts.TotalDays <= 1)
            {
                return $@"{ts:%h} hour(s)";
            }

            return $@"{ts:%d} day(s)";
        }
    }
}
