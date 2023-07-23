namespace AzureWebApp.Services.Queue
{
    public interface IQueueService
    {
        void SendMessage(string queueName, string message);
    }
}
