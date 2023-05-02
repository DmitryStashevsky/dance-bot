using System;
using DanceBotShared.Common;

namespace TelegramClient.Handlers
{
    public interface IBusinessContextHandler
    {
        BusinessContext GetContext(string message);
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
    }
}

