
using System.Threading.Tasks;
using ROP.Services.ACL;
using ROP.Services.DTOs;
using ROP.Services.Repositories;

namespace ROP.Services.Implementations
{
    public class OtpService : IOtpService
    {
        private const int OtpLength = 6;
        private IUserRepository _userRepository;
        private ISmsService _smsService;

        public OtpService(IUserRepository userRepository, ISmsService smsService)
        {
            _userRepository = userRepository;
            _smsService = smsService;
        }
        public async Task CreateOtp(CreateOtpData data)
        {
            var user = await _userRepository.GetByMobileNumber(data.MobileNumber);

            if (user == null)
            {
                var newUserId = await _userRepository.GetNextKey();
                user = new Models.User(newUserId, data.MobileNumber);
            }

            var otp = ROP.Common.Helpers.RandomGenerator.GetDigits(OtpLength);
            user.AddOtp(otp);

            await _userRepository.CreateOrModify(user);

            //todo: add send otp to queue
            await _smsService.SendVerificationCode(data.MobileNumber, otp);
        }
    }
}
