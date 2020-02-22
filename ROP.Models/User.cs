using System;

namespace ROP.Models
{
    public class User : ModelBase<int>
    {
        public User(int id, string mobileNumber)
        : base(id)
        {
            MobileNumber = mobileNumber;
            Gender = Gender.Unknown;
        }
        public string MobileNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Gender Gender { get; private set; }
    }
}
