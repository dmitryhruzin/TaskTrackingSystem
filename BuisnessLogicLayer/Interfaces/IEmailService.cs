using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces
{
    /// <summary>
    ///  Describes an email service
    /// </summary>
    public interface IEmailService
    {
        /// <summary>Sends the message asynchronous.</summary>
        /// <param name="model">The message model.</param>
        Task SendEmailAsync(MessageModel model);
    }
}
