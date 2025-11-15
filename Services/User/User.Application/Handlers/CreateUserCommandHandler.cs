using MediatR;
using User.Application.Commands;
using User.Application.Interfaces;
using User.Application.Mappers;
using User.Application.Responses;
using User.Core.Entities;
using User.Core.Repositories;

namespace User.Application.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserCreatedResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IKafkaProducer _kafka;

        public CreateUserCommandHandler(IUserRepository userRepository, IKafkaProducer kafka)
        {
            _userRepository = userRepository;
            _kafka = kafka;
        }
        public async Task<UserCreatedResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = UserMapper.Mapper.Map<Users>(request);
            if (user is null) 
            {
                throw new ApplicationException("There is an issue with mapping while creating new user");
            }

            // Publish UserCreated event
            await _kafka.PublishAsync("user-created", user);

            var newUser = await _userRepository.CreateUser(user);
            var userResponse = UserMapper.Mapper.Map<UserCreatedResponse>(newUser);
            userResponse.Message = "User created & event published";
            return userResponse;
            
        }
    }
}
