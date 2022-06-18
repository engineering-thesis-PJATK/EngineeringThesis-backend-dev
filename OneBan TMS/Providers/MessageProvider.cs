using System.Net;
using OneBan_TMS.Models.DTOs.Messages;

namespace OneBan_TMS.Providers
{
    public static class MessageProvider
    {
        public static MessageResponse GetSuccessfulMessage(string messageContent, string propertyName = null, int? objectId = null)
        {
            return new MessageResponse()
            {
                MessageContent = messageContent,
                StatusCode = HttpStatusCode.OK,
                PropertyName = propertyName,
                ObjectId = objectId
            };
        }
        public static MessageResponse GetBadRequestMessage(string messageContent, string propertyName = null)
        {
            return new MessageResponse()
            {
                MessageContent = messageContent,
                StatusCode = HttpStatusCode.BadRequest,
            };
        }
    }
}