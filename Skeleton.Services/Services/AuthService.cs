using Skeleton.Core.Result;
using Skeleton.Data.Repositories;
using Skeleton.DTO;
using Skeleton.Models.Entities;
using Skeleton.Services.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Skeleton.Core.Utilities;

namespace Skeleton.Services
{
    public class AuthService
    {
        private readonly List<ClientOption> _clients;
        private readonly TokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<User> _userRepository;

        public AuthService(IOptions<List<ClientOption>> options, TokenService tokenService, UserManager<User> userManager, IRepository<User> userRepository)
        {
            _clients = options.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public Result<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

            if (client == null)
            {
                return Result<ClientTokenDto>.Fail("ClientId or ClientSecret not found", 404, true);
            }

            var token = _tokenService.CreateTokenByClient(client);

            return Result<ClientTokenDto>.Success(token, 200);
        }

        public async Task<Result<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Result<TokenDto>.Fail("Email or Password is wrong", 400, true);


            if (CryptographyService.Encrypt(loginDto.Password) != user.PasswordHash)
            {
                return Result<TokenDto>.Fail("Email or Password is wrong", 400, true);
            }

            var token = _tokenService.CreateToken(user);

            user.RefreshToken = token.RefreshToken;

            user.RefreshTokenEndDate = token.RefreshTokenExpiration;

            await _userManager.UpdateAsync(user);

            return Result<TokenDto>.Success(token, 200);
        }

        public Result<TokenDto> CreateTokenByRefreshToken(string refreshToken)
        {
            var user = _userRepository.Where(x => x.RefreshToken == refreshToken).FirstOrDefault();

            if (user == null) return Result<TokenDto>.Fail("Refresh token not found", 404, true);

            var tokenDto = _tokenService.CreateToken(user);

            user.RefreshToken = tokenDto.RefreshToken;
            user.RefreshTokenEndDate = tokenDto.RefreshTokenExpiration;

            _userRepository.Update(user);

            return Result<TokenDto>.Success(tokenDto, 200);
        }

        public Result<NoDataDto> RevokeRefreshToken(string refreshToken)
        {
            var user = _userRepository.Where(x => x.RefreshToken == refreshToken).FirstOrDefault();

            if (user == null) return Result<NoDataDto>.Fail("Refresh token not found", 404, true);

            user.RefreshToken = null;
            user.RefreshTokenEndDate = null;

            _userRepository.Update(user);

            return Result<NoDataDto>.Success(200);
        }

    }
}
