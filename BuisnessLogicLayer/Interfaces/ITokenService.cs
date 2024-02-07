using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces
{
    /// <summary>
    ///   Describes a token service
    /// </summary>
    public interface ITokenService
    {
        /// <summary>Refreshes the token.</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   TokenModel
        /// </returns>
        Task<TokenModel> Refresh(TokenModel model);

        /// <summary>Revokes the token.</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task Revoke(TokenModel model);
    }
}
