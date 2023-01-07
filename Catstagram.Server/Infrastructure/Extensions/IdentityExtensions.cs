namespace Catstagram.Server.Infrastructure.Extensions
{
    using System.Security.Claims;

    public static class IdentityExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
          => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
