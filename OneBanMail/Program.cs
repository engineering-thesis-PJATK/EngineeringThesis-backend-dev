using System;
using System.Collections;
using System.Collections.Generic;

using MimeKit;
using MailKit;
using MailKit.Search;
using MailKit.Security;
using MailKit.Net.Imap;
using System.IO;

namespace OneBanMail
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

			//DownloadMessages();
			DownloadBodyParts();
		}

		public static void DownloadMessages()
		{
			using (var client = new ImapClient(new ProtocolLogger("imap.log")))
			{
				client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

				client.Authenticate("onebantms@gmail.com", "ZNANE");

				client.Inbox.Open(FolderAccess.ReadOnly);

				var uids = client.Inbox.Search(SearchQuery.All);

				foreach (var uid in uids)
				{
					var message = client.Inbox.GetMessage(uid);

					// write the message to a file
					message.WriteTo(string.Format("{0}.eml", uid));
				}

				client.Disconnect(true);
			}
		}

		public static void DownloadBodyParts()
		{
			using (var client = new ImapClient())
			{
				client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

				client.Authenticate("onebantms@gmail.com", "ZNANE");

				client.Inbox.Open(FolderAccess.ReadOnly);

				// search for messages where the Subject header contains either "MimeKit" or "MailKit"
				//var query = SearchQuery.SubjectContains("MimeKit").Or(SearchQuery.SubjectContains("MailKit"));
				var uids = client.Inbox.Search(SearchQuery.All);

				// fetch summary information for the search results (we will want the UID and the BODYSTRUCTURE
				// of each message so that we can extract the text body and the attachments)
				var items = client.Inbox.Fetch(uids, MessageSummaryItems.UniqueId | MessageSummaryItems.BodyStructure);

				foreach (var item in items)
				{
					// determine a directory to save stuff in
					var directory = Path.Combine("D:\\Szkola\\INZ\\Mail", item.UniqueId.ToString());

					// create the directory
					Directory.CreateDirectory(directory);

					// IMessageSummary.TextBody is a convenience property that finds the 'text/plain' body part for us
					var bodyPart = item.HtmlBody;

					// download the 'text/plain' body part
					var body = (TextPart)client.Inbox.GetBodyPart(item.UniqueId, bodyPart);

					// TextPart.Text is a convenience property that decodes the content and converts the result to
					// a string for us
					var text = body.Text;

					File.WriteAllText(Path.Combine(directory, "body.txt"), text);

					// now iterate over all of the attachments and save them to disk
					foreach (var attachment in item.Attachments)
					{
						// download the attachment just like we did with the body
						var entity = client.Inbox.GetBodyPart(item.UniqueId, attachment);

						// attachments can be either message/rfc822 parts or regular MIME parts
						if (entity is MessagePart)
						{
							var rfc822 = (MessagePart)entity;

							var path = Path.Combine(directory, attachment.PartSpecifier + ".eml");

							rfc822.Message.WriteTo(path);
						}
						else
						{
							var part = (MimePart)entity;

							// note: it's possible for this to be null, but most will specify a filename
							var fileName = part.FileName;

							var path = Path.Combine(directory, fileName);

							// decode and save the content to a file
							using (var stream = File.Create(path))
								part.Content.DecodeTo(stream);
						}
					}
				}

				client.Disconnect(true);
			}
		}
	}
}
