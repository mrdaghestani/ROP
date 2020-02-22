using System;
using System.Threading.Tasks;
using ROP.Services.DTOs;

namespace ROP.Services
{
    public interface IOtpService : Common.IAppService
    {
        Task CreateOtp(CreateOtpData data);
    }
}
