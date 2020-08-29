using System.Security.Claims;

/**
 * @author WUYUDING
 */
namespace SSIS_FRONT.Components.JWT.Interfaces
{
    public interface IAuthContainerModel
    {
        string SecretKey { get; set; }
        string SecurityAlgorithm { get; set; }
        Claim[] Claims { get; set; }
    }
}
