using User.Application.Mappers;
using User.Application.Queries;
using User.Application.Responses;
using User.Core.Repositories;
using MediatR;

namespace User.Application.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserCreatedResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository productRepository)
        {
            _userRepository = productRepository;
        }
        public async Task<UserCreatedResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.Id);
            var userRespose = UserMapper.Mapper.Map<UserCreatedResponse>(user);
            return userRespose;
        }
    }
}
