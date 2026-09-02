using Sdk.Telegram;
using System.Reflection.Metadata;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Sdk.Models
{

    public interface IExecuteContext
    {
        Task SendPackage(IPCAssistant client);
    }

    public abstract class ExecuteContext : IExecuteContext
    {
        public bool IsErrorSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public ChatId ChatId { get; set; }
        public ReplyParameters ReplyParameters { get; set; }

        public ExecuteResultType ResultType { get; set; }

        public abstract Task SendPackage(IPCAssistant client);
    }

    public class StreamContext : ExecuteContext
    {
        public string FileName { get; set; }
        public Stream Stream { get; set; }

        public override Task SendPackage(IPCAssistant client)
        {
            throw new NotImplementedException("SendPackage must be implemented in derived classes.");
        }
    }

    public class TextContext : ExecuteContext
    {
        public override async Task SendPackage(IPCAssistant client)
        {
            await client.SendMessage(this.ChatId, this.ErrorMessage, replyParameters: this.ReplyParameters);
        }
    }

    public class DocumenContext : StreamContext
    {
        public override async Task SendPackage(IPCAssistant client)
        {
            try
            {
                if (Stream.CanSeek)
                    Stream.Position = 0;

                await client.SendDocument(this.ChatId, InputFile.FromStream(Stream, FileName));
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine($"Inner: {ex.InnerException}");
                //throw;
            }
        }
    }

    public class ImageContext : StreamContext
    {
        public override async Task SendPackage(IPCAssistant client)
        {
            try
            {
                if (Stream.CanSeek)
                    Stream.Position = 0;

                await client.SendPhoto(this.ChatId, InputFile.FromStream(Stream, FileName));
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine($"Inner: {ex.InnerException}");
                //throw;
            }
        }
    }
}
