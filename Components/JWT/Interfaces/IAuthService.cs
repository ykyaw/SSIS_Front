using SSIS_FRONT.Components.JWT.Impl;
using SSIS_FRONT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


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
