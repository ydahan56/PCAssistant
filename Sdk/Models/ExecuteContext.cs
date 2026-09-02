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

    public class ExecuteParameters
    {
        public ChatId ChatId { get; set; }
        public ReplyParameters ReplyParameters { get; set; }
    }

    public abstract class ExecuteContext : IExecuteContext
    {
        public bool IsErrorSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public ChatId ChatId { get; set; }
        public ReplyParameters ReplyParameters { get; set; }

        public abstract Task SendPackage(IPCAssistant client);
    }

    public abstract class StreamContext : ExecuteContext
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
            if (this.ErrorMessage.Length <= 4096)
            {
                await client.SendMessage(this.ChatId, this.ErrorMessage, replyParameters: this.ReplyParameters);
                return;
            }

            while (this.ErrorMessage.Length > 4096)
            {
                string part = this.ErrorMessage.Substring(0, 4096);
                await client.SendMessage(this.ChatId, part, replyParameters: this.ReplyParameters);
                this.ErrorMessage = this.ErrorMessage.Substring(4096);
            }

            if (this.ErrorMessage.Length > 0)
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
}