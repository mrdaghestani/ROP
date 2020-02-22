using System;

namespace ROP.Models
{
    public class OTP 
    {
        public OTP(string value)
        {
            Value = value;
            CreationTime = DateTime.Now;
        }
        public string Value { get; private set; }
        public DateTime CreationTime { get; private set; }
    }
}