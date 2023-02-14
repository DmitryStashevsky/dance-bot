using System;
using TelegramClient.Abstract;

namespace TelegramClient.Services
{
    public class PollingService : PollingServiceBase<ReceiverService>
    {
        public PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
            : base(serviceProvider, logger)
        {
        }
    }
}

