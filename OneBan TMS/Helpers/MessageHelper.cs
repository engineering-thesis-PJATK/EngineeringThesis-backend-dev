using System.Net;
using OneBan_TMS.Models.DTOs.Messages;

namespace OneBan_TMS.Helpers
{
    public static class MessageHelper
    {
        public static MessageResponse GetSuccessfulMessage(string messageContent)
        {
            return new MessageResponse()
            {
                MessageContent = messageContent,
                StatusCode = HttpStatusCode.OK
            };
        }
        public static MessageResponse GetSuccessfulMessage(string messageContent, string propertyName)
        {
            return new MessageResponse()
            {
                MessageContent = messageContent,
                StatusCode = HttpStatusCode.OK,
                PropertyName = propertyName
            };
        }
    }
}