using System;
using Xunit;

namespace ROP.Models.Tests
{
    public class UserTests
    {
        [Fact]
        public void ConstructorShouldAssignPropertiesCorrectly()
        {
            var instance = new Models.User(1, "09124445566");

            Assert.Equal(1, instance.Id);
            Assert.Equal("09124445566", instance.MobileNumber);
            Assert.Equal(DateTime.Now, instance.CreationTime, TimeSpan.FromMilliseconds(200));
            Assert.Null(instance.FirstName);
            Assert.Null(instance.LastName);
            Assert.Equal(Gender.Unknown, instance.Gender);
        }
    }
}
