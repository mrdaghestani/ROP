using System;
using System.Threading.Tasks;

namespace ROP.Kavenegar
{
    public class SmsService : ROP.Services.ACL.ISmsService
    {
        public Task Send(string number, string msg)
        {
            throw new NotImplementedException();
        }

        public Task SendVerificationCode(string number, string code)
        {
            throw new NotImplementedException();
        }
    }
}
