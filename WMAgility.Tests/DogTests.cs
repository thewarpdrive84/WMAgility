using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WMAgility2.Data;
using WMAgility2.Models;
using Xunit;

namespace WMAgility.Tests
{
    public class DogTests
    {
        [Fact]
        public void GetDogById()
        {
            //Arrange
            IDogRepository sut = GetInMemoryDogRepository();
            Dog dog1 = new Dog()
            {
                Id = 3,
                DogName = "Jo",
                Breed = "Terrier x",
                DOB = DateTime.Parse("2012-07-26"),
                Image = "jo.jpg",
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            Dog dog2 = new Dog()
            {
                Id = 4,
                DogName = "Maddie",
                Breed = "Irish Terrier",
                DOB = DateTime.Parse("2013-09-17"),
                Image = "maddie.jpg",
                ApplicationUserId = "20f95e44-8f29-42d8-b3f9-ff4c3e683ce5"
            };

            //Act
            Dog savedDog1 = sut.CreateDog(dog1);
            Dog savedDog2 = sut.CreateDog(dog2);

            //Assert
            var foundDogById = sut.GetDogById(4);
            Assert.Equal("Maddie", foundDogById.DogName);
        }

        [Fact]
        public void Index_Dogs()
        {
            //Arrange
            IDogRepository sut = GetInMemoryDogRepository();
            Dog dog = new Dog()
            {
                Id = 3,
                DogName = "Jo",
                Breed = "Terrier x",
                DOB = DateTime.Parse("2012-07-26"),
                Image = "jo.jpg",
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            //Act
            Dog savedDog = sut.CreateDog(dog);

            //Assert
            Assert.Single(sut.Dogs);
            Assert.Equal(3, savedDog.Id);
            Assert.Equal("Jo", savedDog.DogName);
            Assert.Equal("Terrier x", savedDog.Breed);
            Assert.Equal("jo.jpg", savedDog.Image);
            Assert.Equal(DateTime.Parse("2012-07-26"), savedDog.DOB);
            Assert.Equal("d4eb7d23-d641-4c2d-8cd3-a036e08a3c65", savedDog.ApplicationUserId);
        }

        [Fact]
        public void Index_Update_Dog_Name()
        {
            //Arrange
            IDogRepository sut = GetInMemoryDogRepository();
            Dog dog1 = new Dog()
            {
                Id = 3,
                DogName = "Jo",
                Breed = "Terrier x",
                DOB = DateTime.Parse("2012-07-26"),
                Image = "jo.jpg",
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            Dog dog2 = new Dog()
            {
                Id = 3,
                DogName = "Josephine",
                Breed = "Terrier x",
                DOB = DateTime.Parse("2012-07-26"),
                Image = "jo.jpg",
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            //Act
            Dog savedDog = sut.CreateDog(dog1);
            Dog savedDog1 = sut.UpdateDog(dog2);

            //Assert
            Assert.Single(sut.Dogs);
            Assert.Equal("Josephine", dog2.DogName);
        }

        [Fact]
        public void Index_DeleteDog()
        {
            //Arrange
            IDogRepository sut = GetInMemoryDogRepository();
            Dog dog = new Dog()
            {
                Id = 3,
                DogName = "Jo",
                Breed = "Terrier x",
                DOB = DateTime.Parse("2012-07-26"),
                Image = "jo.jpg",
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            //Act
            Dog savedDog = sut.CreateDog(dog);
            Dog savedDog1 = sut.DeleteDog(dog);

            //Assert
            Assert.Empty(sut.Dogs);
        }

        private IDogRepository GetInMemoryDogRepository()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "WMDb").Options;
            ApplicationDbContext applicationDataContext = new ApplicationDbContext(builder);
            applicationDataContext.Database.EnsureDeleted();
            applicationDataContext.Database.EnsureCreated();
            return new DogRepository(applicationDataContext);
        }
    }
}
