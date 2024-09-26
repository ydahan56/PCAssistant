namespace Sdk.Models
{

    public class ExecuteResult
    {
        public bool Success { get; set; }
        public string StatusText { get; set; }

        public ExecuteResultType ResultType { get; set; }

        public ExecuteResult()
        {
            this.ResultType = ExecuteResultType.Text;
        }
    }

    public class ExecuteStreamResult : ExecuteResult
    {
        public string FileName { get; set; }
        public Stream Stream { get; set; }

        public ExecuteStreamResult()
        {
            this.ResultType = ExecuteResultType.Stream;
        }
    }
}
