using System.Threading.Tasks;

namespace ROP.Services.ACL
{
    public interface ISmsService : ROP.Common.IAppService
    {
        Task Send(string number, string msg);
        Task SendVerificationCode(string number, string code);
    }
}