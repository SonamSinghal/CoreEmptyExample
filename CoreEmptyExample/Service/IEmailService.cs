using CoreEmptyExample.Model;
using System.Threading.Tasks;

namespace CoreEmptyExample.Service
{
    public interface IEmailService
    {
        object NetworkCredentials { get; }

        Task SendTestEmail(SendEmailModel sendEmailModel);

        Task SendConfirmationEmailService(SendEmailModel sendEmailModel);
    }
}