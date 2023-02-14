using System;

namespace TelegramClient.Abstract
{
    public interface IReceiverService
    {
        Task ReceiveAsync(CancellationToken stoppingToken);
    }
}

