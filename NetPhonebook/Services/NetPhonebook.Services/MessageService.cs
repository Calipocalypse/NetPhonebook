using NetPhonebook.Services.Interfaces;

namespace NetPhonebook.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
