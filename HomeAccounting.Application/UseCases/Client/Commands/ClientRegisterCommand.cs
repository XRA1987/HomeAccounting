using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeAccounting.Application.UseCases.Client.Commands
{
    public class ClientRegisterCommand : ICommand<Unit>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }

    public class ClientRegisterCommandHandler : ICommandHandler<ClientRegisterCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IHashService _hashService;

        public ClientRegisterCommandHandler(IApplicationDbContext dbContext, IHashService hashService)
        {
            _dbContext = dbContext;
            _hashService = hashService;
        }

        public async Task<Unit> Handle(ClientRegisterCommand command, CancellationToken cancellationToken)
        {
            if (await _dbContext.Clients.AnyAsync(x => x.UserName == command.UserName, cancellationToken))
            {
                throw new RegisterExceptions();
            }

            var client = new Domain.Entities.Client()
            {
                UserName = command.UserName,
                PasswordHash = _hashService.GetHash(command.Password),
                FullName = command.FullName,
                Birthday = command.Birthday,
                Gender = command.Gender,
                PhoneNumber = command.PhoneNumber,
                Email = command.Email
            };

            await _dbContext.Clients.AddAsync(client, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
