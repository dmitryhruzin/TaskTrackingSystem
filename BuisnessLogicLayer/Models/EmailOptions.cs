namespace BuisnessLogicLayer.Models
{
    /// <summary>
    ///   Describes a emailOptions
    /// </summary>
    public class EmailOptions
    {
        /// <summary>Gets or sets from.</summary>
        /// <value>From.</value>
        public string From { get; set; }

        /// <summary>Gets or sets the SMTP server.</summary>
        /// <value>The SMTP server.</value>
        public string SmtpServer { get; set; }

        /// <summary>Gets or sets the port.</summary>
        /// <value>The port.</value>
        public int Port { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string Password { get; set; }
    }
}
