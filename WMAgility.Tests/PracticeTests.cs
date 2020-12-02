using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WMAgility2.Data;
using WMAgility2.Models;
using Xunit;

namespace WMAgility.Tests
{
    public class PracticeTests
    {
        [Fact]
        public void GetPracticeById()
        {
            //Arrange
            IPracticeRepository sut = GetInMemoryPracticeRepository();
            Practice practice1 = new Practice()
            {
                Id = 1,
                Date = DateTime.Parse("2020-12-11"),
                Rounds = 1,
                Score = 5,
                SkillId = 2,
                Notes = "Jo missed second contact on dogwalk.",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            Practice practice2 = new Practice()
            {
                Id = 2,
                Date = DateTime.Parse("2020-12-12"),
                Rounds = 1,
                Score = 9,
                SkillId = 1,
                Notes = "Jo did very well.",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            //Act
            Practice savedPractice1 = sut.CreatePractice(practice1);
            Practice savedPractice2 = sut.CreatePractice(practice2);

            //Assert
            var foundPracticeById = sut.GetPracticeById(1);
            Assert.Equal(5, foundPracticeById.Score);
        }

        [Fact]
        public void Index_Practices()
        {
            //Arrange
            IPracticeRepository sut = GetInMemoryPracticeRepository();
            Practice practice = new Practice()
            {
                Id = 1,
                Date = DateTime.Parse("2020-12-11"),
                Rounds = 1,
                Score = 5,
                SkillId = 2,
                Notes = "Jo missed second contact on dogwalk.",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            //Act
            Practice savedPractice = sut.CreatePractice(practice);

            //Assert
            Assert.Single(sut.AllPractices);
            Assert.Equal(1, savedPractice.Id);
            Assert.Equal(DateTime.Parse("2020-12-11"), savedPractice.Date);
            Assert.Equal(1, savedPractice.Rounds);
            Assert.Equal(5, savedPractice.Score);
            Assert.Equal(2, savedPractice.SkillId);
            Assert.Equal(3, savedPractice.DogId);
            Assert.Equal("Jo missed second contact on dogwalk.", savedPractice.Notes);
            Assert.Equal("d4eb7d23-d641-4c2d-8cd3-a036e08a3c65", savedPractice.ApplicationUserId);

        }

        [Fact]
        public void Index_Update_Practice_Score()
        {
            //Arrange
            IPracticeRepository sut = GetInMemoryPracticeRepository();
            Practice prac1 = new Practice()
            {
                Id = 1,
                Date = DateTime.Parse("2020-12-11"),
                Rounds = 1,
                Score = 5,
                SkillId = 2,
                Notes = "Jo missed second contact on dogwalk.",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            Practice prac2 = new Practice()
            {
                Id = 1,
                Date = DateTime.Parse("2020-12-11"),
                Rounds = 1,
                Score = 7,
                SkillId = 2,
                Notes = "Jo missed second contact on dogwalk.",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            //Act
            Practice savedPrac = sut.CreatePractice(prac1);
            Practice savedPrac1 = sut.UpdatePractice(prac2);

            //Assert
            Assert.Single(sut.AllPractices);
            Assert.Equal(7, prac2.Score);
        }

        [Fact]
        public void Index_DeletePractice()
        {
            //Arrange
            IPracticeRepository sut = GetInMemoryPracticeRepository();
            Practice practice = new Practice()
            {
                Id = 1,
                Date = DateTime.Parse("2020-12-11"),
                Rounds = 1,
                Score = 5,
                SkillId = 2,
                Notes = "Jo missed second contact on dogwalk.",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            //Act
            Practice savedPrac = sut.CreatePractice(practice);
            Practice savedPrac1 = sut.DeletePractice(practice);

            //Assert
            Assert.Empty(sut.AllPractices);
        }

        private IPracticeRepository GetInMemoryPracticeRepository()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "WMDb").Options;
            ApplicationDbContext applicationDataContext = new ApplicationDbContext(builder);
            applicationDataContext.Database.EnsureDeleted();
            applicationDataContext.Database.EnsureCreated();
            return new PracticeRepository(applicationDataContext);
        }
    }
}
