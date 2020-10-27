using System;
using Xunit;
using WMAgility2.Models;

namespace WMAgility.Tests
{
    public class SampleTests
    {
        [Fact]
        public void CanUpdateSkillName()
        {
            // Arrange
            var skill = new Skill { Name = "Jump" };
            // Act
            skill.Name = "Long Jump";
            //Assert
            Assert.Equal("Long Jump", skill.Name);
        }

        [Fact]
        public void CanUpdateSkillDescription()
        {
            // Arrange
            var skill = new Skill { Name = "Seesaw", Description = "Follow Instruction" };
            // Act
            skill.Description = "Contact";
            //Assert
            Assert.Equal("Contact", skill.Description);
        }
    }
}
