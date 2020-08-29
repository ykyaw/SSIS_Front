using System.Collections.Generic;
using System.Security.Claims;

/**
 * @author WUYUDING
 */
namespace SSIS_FRONT.Components.JWT.Interfaces
{
    public interface IAuthService
    {
        public string SecretKey { get; set; }
        public int ExpireMinutes { get; set; }
        public bool IsTokenValid(string token);
        public IEnumerable<Claim> GetTokenClaims(string token);
    }
}
