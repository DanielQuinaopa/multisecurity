using Microsoft.IdentityModel.Tokens;

namespace multisecurity.Utilities
{
    public class TokenLifeTimeValidator
    {
            public static bool Validate(
        DateTime? notBefore,
        DateTime? expires,
        SecurityToken tokenToValidate,
        TokenValidationParameters @param
        )
            {
                return (expires != null && expires > DateTime.UtcNow);
            }
    }
}
