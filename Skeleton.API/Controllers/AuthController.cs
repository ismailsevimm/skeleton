using Skeleton.Core.Result;
using Skeleton.DTO;
using Skeleton.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Skeleton.API.Controllers
{  /// <summary>
   /// 
   /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authenticationService;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="authenticationService"></param>
        public AuthController(AuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// User Login method.
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<TokenDto>))]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDto);

            return Ok(result);
        }

        /// <summary>
        /// Creates token by client credentials
        /// </summary>
        /// <param name="clientLoginDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<ClientTokenDto>))]
        public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var result = _authenticationService.CreateTokenByClient(clientLoginDto);

            return Ok(result);
        }

        /// <summary>
        /// Removes refresh token
        /// </summary>
        /// <param name="refreshTokenDto"></param>
        /// <remarks>Use when user set 'rememberme' value to false </remarks>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<NoDataDto>))]
        public IActionResult RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = _authenticationService.RevokeRefreshToken(refreshTokenDto.RefreshToken);

            return Ok(result);
        }

        /// <summary>
        /// Creates token by refresh token
        /// </summary>
        /// <param name="refreshTokenDto"></param>
        /// <remarks> Used for quick login when the user has the remember me feature correct</remarks>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<TokenDto>))]
        public IActionResult CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = _authenticationService.CreateTokenByRefreshToken(refreshTokenDto.RefreshToken);

            return Ok(result);
        }

        /// <summary>
        /// Logout method 
        /// </summary>
        /// <remarks>
        /// sends information to server</remarks>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<NoDataDto>))]
        public IActionResult Logout()
        {
            return Ok(Result<NoDataDto>.Success(200));
        }


    }
}
