using System;
using System.Collections.Generic;

namespace ROP.Models
{
    public class User : ModelBase<int>
    {
        private User() : base(0) { }
        public User(int id, string mobileNumber)
        : base(id)
        {
            MobileNumber = mobileNumber;
            Gender = Gender.Unknown;
            Otps = new List<OTP>();
        }
        public string MobileNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Gender Gender { get; private set; }
        public List<OTP> Otps { get; private set; }

        public void AddOtp(string otp)
        {
            Otps.Add(new OTP(otp));
        }
    }
}
