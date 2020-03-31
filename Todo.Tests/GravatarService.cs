using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.Services;
using Xunit;

namespace Todo.Tests
{
    public class GravatarService
    {
        [Fact]
        public async Task GetUserProfile_ValidProfile_ShouldReturnData()
        {
            var validEmail = "beau@automattic.com";
            var profile = await Gravatar.GetUserProfile(validEmail);
            Assert.NotNull(profile);
            Assert.NotNull(profile.DisplayName);
            Assert.NotNull(profile.Id);
        }

        [Fact]
        public async Task GetUserProfile_InvaliddProfile_ShouldReturnNull()
        {
            var notValidEmail = "notvalid";
            var profile = await Gravatar.GetUserProfile(notValidEmail);
            Assert.Null(profile);
        }
    }
}
