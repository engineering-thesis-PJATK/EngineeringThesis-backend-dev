using System.Net;

namespace OneBan_TMS.Models.DTOs.Messages
{
    public class MessageResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string MessageContent { get; set; }
        public string PropertyName { get; set; }
        public int ObjectId { get; set; }
    }
}