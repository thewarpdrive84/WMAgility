using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WMAgility2.Data;
using WMAgility2.Models;
using Xunit;

namespace WMAgility.Tests
{
    public class SkillTests
    {
        [Fact]
        public void GetSkillById()
        {
            //Arrange
            ISkillRepository sut = GetInMemorySkillRepository();
            Skill skill1 = new Skill()
            {
                Id = 1,
                Name = "Weaves",
                Description = "Follow Instruction",
                IKC = "The minimum number of poles should be five...",
                Image = "weaves.jpeg"
            };

            Skill skill2 = new Skill()
            {
                Id = 2,
                Name = "Dog Walk",
                Description = "Contact",
                IKC = "A walk plank of 1.2m minimum...",
                Image = "dogwalk.jpg"
            };

            //Act
            Skill savedSkill = sut.CreateSkill(skill1);
            Skill savedSkill2 = sut.CreateSkill(skill2);

            //Assert
            var foundSkillById = sut.GetSkillById(2);
            Assert.Equal("Dog Walk", foundSkillById.Name);
        }


        [Fact]
        public void Index_Skills()
        {
            //Arrange
            ISkillRepository sut = GetInMemorySkillRepository();
            Skill skill = new Skill()
            {
                Id = 1,
                Name = "Weaves",
                Description = "Follow Instruction",
                IKC = "The minimum number of poles should be five...",
                Image = "weaves.jpeg"
            };

            //Act
            Skill savedSkill = sut.CreateSkill(skill);

            //Assert
            Assert.Single(sut.AllSkills);
            Assert.Equal(1, savedSkill.Id);
            Assert.Equal("Weaves", savedSkill.Name);
            Assert.Equal("Follow Instruction", savedSkill.Description);
            Assert.Equal("The minimum number of poles should be five...", savedSkill.IKC);
            Assert.Equal("weaves.jpeg", savedSkill.Image);
        }

        [Fact]
        public void Index_Update_Skill_Name()
        {
            //Arrange
            ISkillRepository sut = GetInMemorySkillRepository();
            Skill skill1 = new Skill()
            {
                Id = 1,
                Name = "Weaves",
                Description = "Follow Instruction",
                IKC = "The minimum number of poles should be five...",
                Image = "weaves.jpeg"
            };

            Skill skill2 = new Skill()
            {
                Id = 1,
                Name = "Weave Poles",
                Description = "Follow Instruction",
                IKC = "The minimum number of poles should be five...",
                Image = "weaves.jpeg"
            };

            //Act
            Skill savedSkill = sut.CreateSkill(skill1);
            Skill savedSkill1 = sut.UpdateSkill(skill2);

            //Assert
            Assert.Single(sut.AllSkills);
            Assert.Equal("Weave Poles", skill2.Name);
        }

        [Fact]
        public void Index_DeleteSkill()
        {
            //Arrange
            ISkillRepository sut = GetInMemorySkillRepository();
            Skill skill = new Skill()
            {
                Id = 1,
                Name = "Weaves",
                Description = "Follow Instruction",
                IKC = "The minimum number of poles should be five...",
                Image = "weaves.jpeg"
            };

            //Act
            Skill savedSkill = sut.CreateSkill(skill);
            Skill savedSkill1 = sut.DeleteSkill(skill);

            //Assert
            Assert.Empty(sut.AllSkills);
        }

        private ISkillRepository GetInMemorySkillRepository()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "WM_db").Options;
            ApplicationDbContext applicationDataContext = new ApplicationDbContext(builder);
            applicationDataContext.Database.EnsureDeleted();
            applicationDataContext.Database.EnsureCreated();
            return new SkillRepository(applicationDataContext);
        }
    }
}
