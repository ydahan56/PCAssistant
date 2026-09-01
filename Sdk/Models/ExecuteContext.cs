using Telegram.Bot.Types;

namespace Sdk.Models
{

    public class ExecuteContext
    {
        public bool IsErrorSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public ChatId ChatId { get; set; }
        public ReplyParameters ReplyParameters { get; set; }

        public ExecuteResultType ResultType { get; set; }

        public ExecuteContext()
        {
            this.ResultType = ExecuteResultType.Unknown;
        }
    }

    public class StreamContext : ExecuteContext
    {
        public string FileName { get; set; }
        public Stream Stream { get; set; }

        public StreamContext()
        {
            this.ResultType = ExecuteResultType.Stream;
        }
    }

    public class DocumenContext : StreamContext
    {
        public DocumenContext()
        {
            this.ResultType = ExecuteResultType.Document;
        }
    }

    public class ImageContext : StreamContext
    {
        public ImageContext()
        {
            this.ResultType = ExecuteResultType.Image;
        }
    }
}
