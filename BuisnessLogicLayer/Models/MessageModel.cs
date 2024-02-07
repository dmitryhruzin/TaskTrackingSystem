using MimeKit;

namespace BuisnessLogicLayer.Models
{
    /// <summary>
    ///   Describes a messageModel
    /// </summary>
    public class MessageModel
    {
        /// <summary>Gets or sets to.</summary>
        /// <value>To.</value>
        public List<MailboxAddress> To { get; set; }

        /// <summary>Gets or sets the subject.</summary>
        /// <value>The subject.</value>
        public string Subject { get; set; }

        /// <summary>Gets or sets the content.</summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MessageModel" /> class.</summary>
        /// <param name="to">To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="content">The content.</param>
        public MessageModel(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("email", x)));
            Subject = subject;
            Content = content;
        }

        /// <summary>Initializes a new instance of the <see cref="MessageModel" /> class.</summary>
        /// <param name="to">To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="content">The content.</param>
        public MessageModel(string to, string subject, string content)
        {
            To = new List<MailboxAddress>
            {
                new MailboxAddress("email", to)
            };
            Subject = subject;
            Content = content;
        }
    }
}
