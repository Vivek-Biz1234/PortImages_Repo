using MediatR;
using PORTIMAGES.Application.Auth.AuthUser.Commands;
using PORTIMAGES.Application.Auth.AuthUser.DTOs;
using PORTIMAGES.Application.Auth.AuthUser.Interfaces;

namespace PORTIMAGES.Application.Auth.AuthUser.Handlers
{
    public class UserLoginCommandHandler
        : IRequestHandler<UserLoginCommand, UserLoginResultDTO>
    {
        private readonly IUserRepository _userRepository;

        public UserLoginCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserLoginResultDTO> Handle(UserLoginCommand request,CancellationToken cancellationToken)
        {
            return await _userRepository.LoginAsync(request.Username,request.Password);
        }
    }
}
