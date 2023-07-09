namespace Sdk.Extensions
{
    public static class PrimitiveExtensions
    {
        public static string ToReadable(this bool value)
        {
            return value ? "Active ✅" : "Inactive ❌";
        }
    }
}
