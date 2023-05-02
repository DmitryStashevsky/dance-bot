using System;
using DanceBotShared.Common;

namespace TelegramClient.Handlers
{
    public interface IBusinessContextHandler
    {
        BusinessContext GetContext(string message);
        string GetString(BusinessContext context);
    }

    internal class BusinessContextHandler : IBusinessContextHandler
    {
        private readonly IRegexHandler regexHandler;

        public BusinessContextHandler(IRegexHandler regexHandler)
        {
            this.regexHandler = regexHandler;
        }

        public BusinessContext GetContext(string message)
        {
            return regexHandler.GetContextFromMessage(message);
        }

        public string GetString(BusinessContext context)
        {
            return regexHandler.GetMessageFromContext(context);
        }
    }
}

