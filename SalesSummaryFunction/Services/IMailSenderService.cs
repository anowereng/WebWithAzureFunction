using System.Threading.Tasks;

namespace SalesSummaryFunction.Services
{
    public interface IMailSenderService
    {
        Task<bool> SendAsync(string to, string subject, string content);
    }
}