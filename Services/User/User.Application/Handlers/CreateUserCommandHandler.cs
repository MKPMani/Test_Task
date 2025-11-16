using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.Commands;
using User.Application.Interfaces;
using User.Application.Mappers;
using User.Application.Responses;
using User.Core.Entities;
using User.Core.Repositories;

namespace User.Application.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IKafkaProducer _kafka;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        public CreateUserCommandHandler(IUserRepository userRepository, IKafkaProducer kafka, ILogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _kafka = kafka;
            _logger = logger;
        }
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = UserMapper.Mapper.Map<Users>(request);
            if (user is null) 
            {
                throw new ApplicationException("There is an issue with mapping while creating new user");
            }

            var newUser = await _userRepository.CreateUser(user);
            _logger.LogInformation($"User with Id {newUser.Id} successfully created");           

            // Publish UserCreated event
            await _kafka.PublishAsync("user-created", user);
            _logger.LogInformation($"User created & event published for User Id - {newUser.Id}");

            var userResponse = UserMapper.Mapper.Map<UserCreatedResponse>(newUser);
            return userResponse.Id;
            
        }
    }
}
