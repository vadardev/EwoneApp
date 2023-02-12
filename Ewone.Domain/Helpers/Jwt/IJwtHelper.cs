using Ewone.Data.Entities;
using Google.Apis.Auth;

namespace Ewone.Domain.Helpers.Jwt;

public interface IJwtHelper
{
    Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string idToken);
    string CreateAccessToken(User user);
}