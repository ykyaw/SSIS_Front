using Microsoft.IdentityModel.Tokens;
using SSIS_FRONT.Components.JWT.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


/**
 * JWT contaner model
 * @author WUYUDING
 */
namespace SSIS_FRONT.Components.JWT.Impl
{
    public class JWTContainerModel : IAuthContainerModel
    {
        public string SecretKey { get; set ; }
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public Claim[] Claims { get ; set ; }
    }
}
