using OneBan_TMS.Models.DTOs.Email;

namespace OneBan_TMS.Interfaces.Handlers
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}