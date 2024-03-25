using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Client.Commands
{
    public class ClientRegisterCommand : ICommand<Unit>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }

    public class ClientRegisterCommandHandler : ICommandHandler<ClientRegisterCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IHashService _hashService;
        private readonly ILogger<ClientRegisterCommandHandler> _logger;

        public ClientRegisterCommandHandler(IApplicationDbContext dbContext,
            IHashService hashService,
            ILogger<ClientRegisterCommandHandler> logger)
        {
            _dbContext = dbContext;
            _hashService = hashService;
            _logger = logger;
        }

        public async Task<Unit> Handle(ClientRegisterCommand command, CancellationToken cancellationToken)
        {
            var client = new Domain.Entities.Client();
            try
            {
                if (await _dbContext.Clients.AnyAsync(x => x.UserName == command.UserName, cancellationToken))
                {
                    throw new RegisterExceptions();
                }

                client.UserName = command.UserName;
                client.PasswordHash = _hashService.GetHash(command.Password);
                client.FullName = command.FullName;
                client.Gender = command.Gender;
                client.PhoneNumber = command.PhoneNumber;
                client.Email = command.Email;

            }
            catch (RegisterExceptions ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Register error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Register error: {ex.Message}");
            }
            finally
            {
                await _dbContext.Clients.AddAsync(client, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
