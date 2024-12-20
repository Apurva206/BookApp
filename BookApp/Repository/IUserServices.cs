using BookApp.DTO;
using BookApp.Model;

namespace BookApp.Repository
{
    public interface IUserServices
    {
        Task<bool> RegisterUserAsync(UserRegisterDto registerRequestDto);
        Task<AuthDto> LoginAsync(UserLoginDto loginRequestDto);
    }
}
