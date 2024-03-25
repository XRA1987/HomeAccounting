using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Application.UseCases.Client.Commands;
using HomeAccounting.Domain.Entities;
using HomeAccounting.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace HomeAccounting.Application.UseCases.Auth.Commands
{
    public class LoginCommand : ICommand<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginCommandHandler : ICommandHandler<LoginCommand, string>
    {
        private readonly IApplicationDbContext _dBcontext;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<ClientRegisterCommandHandler> _logger;

        public LoginCommandHandler(IApplicationDbContext context,
            IHashService hashService,
            ITokenService tokenService,
            ILogger<ClientRegisterCommandHandler> logger)
        {
            _dBcontext = context;
            _hashService = hashService;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = new User();
            var claims = new List<Claim>();
            try
            {
                user = await _dBcontext.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);

                if (user == null)
                {
                    throw new LoginException(new EntityNotFoundExceptions(nameof(User)));
                }

                if (user.PasswordHash != _hashService.GetHash(request.Password))
                {
                    throw new LoginException();
                }

                claims.Add(new(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new(ClaimTypes.Name, user.UserName));
                claims.Add(new(ClaimTypes.Email, user.Email));

                if (await _dBcontext.Admins.AnyAsync(x => x.Id == user.Id, cancellationToken))
                {
                    claims.Add(new Claim(ClaimTypes.Role, nameof(Domain.Entities.Admin)));
                }
            }
            catch (LoginException ex)
            {
                _logger.LogError(ex.InnerException.Message, ex.StackTrace);
                Console.WriteLine($"Login error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Login error: {ex.Message}");
            }

            return _tokenService.GetAccessToken(claims.ToArray());
        }
    }
}
