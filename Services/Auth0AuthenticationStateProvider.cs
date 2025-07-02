using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using System.Security.Claims;

namespace MemoryPalaceApp.Services
{
    public class Auth0AuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<Auth0AuthenticationStateProvider> _logger;

        public Auth0AuthenticationStateProvider(
            ILoggerFactory loggerFactory,
            IServiceScopeFactory scopeFactory) : base(loggerFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = loggerFactory.CreateLogger<Auth0AuthenticationStateProvider>();
        }

        protected override TimeSpan RevalidateInterval => TimeSpan.FromMinutes(30);

        protected override Task<bool> ValidateAuthenticationStateAsync(
            AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            return Task.FromResult(authenticationState.User?.Identity?.IsAuthenticated ?? false);
        }
    }
}