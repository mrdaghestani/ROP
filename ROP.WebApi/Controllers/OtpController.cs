using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ROP.Services;
using ROP.Services.DTOs;

namespace ROP.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OtpController : ControllerBase
    {
        private IOtpService _otpService;

        public OtpController(IOtpService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateOtpData data)
        {
            await _otpService.CreateOtp(data);
            return NoContent();
        }
    }
}
