using Microsoft.IdentityModel.Tokens;
using SSIS_FRONT.Components.JWT.Interfaces;
using SSIS_FRONT.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

/**
 * JWT service
 * responsible to generate token, validate token and decrypt token
 * 
 * @author WUYUDING
 */
namespace SSIS_FRONT.Components.JWT.Impl
{
    public class JWTService:IAuthService
    {
        //The secret key we use to encrypt out token with.
        public string SecretKey { get; set; }= "TW9zaGVFcmV6UHJpdmF0ZUtleQ==";

        public int ExpireMinutes { get; set; } = 60*24*7; //7 days

        private SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Convert.FromBase64String(SecretKey);
            return new SymmetricSecurityKey(symmetricKey);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }

        //Validate whether a given token is valid or not. 
        //And return true in case the token is valid otherwise it will return false.
        public bool IsTokenValid(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Given token is null or empty.");
            }
            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, 
                    tokenValidationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        //Receive the claims of token by given token as string.
        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Given token is null or empty.");
            }

            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return tokenValid.Claims;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
