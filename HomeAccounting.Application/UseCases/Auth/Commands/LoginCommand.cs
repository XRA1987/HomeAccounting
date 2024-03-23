using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Domain.Entities;
using HomeAccounting.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
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

        public LoginCommandHandler(IApplicationDbContext context, IHashService hashService, ITokenService tokenService)
        {
            _dBcontext = context;
            _hashService = hashService;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _dBcontext.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);

            if (user == null)
            {
                throw new LoginException(new EntityNotFoundExceptions(nameof(User)));
            }

            if (user.PasswordHash != _hashService.GetHash(request.Password))
            {
                throw new LoginException();
            }

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Name, user.UserName),
                new (ClaimTypes.Email, user.Email),
            };

            if (await _dBcontext.Admins.AnyAsync(x => x.Id == user.Id, cancellationToken))
            {
                claims.Add(new Claim(ClaimTypes.Role, nameof(Domain.Entities.Admin)));
            }

            return _tokenService.GetAccessToken(claims.ToArray());

        }
    }
}
