using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMAgility2.Data;
using WMAgility2.Models;
using Xunit;

namespace WMAgility.Tests
{
    public class CompetitionTests
    {
        [Fact]
        public void GetCompById()
        {
            //Arrange
            ICompRepository sut = GetInMemoryCompRepository();
            Competition comp1 = new Competition()
            {
                CompId = 1,
                CompName = "Tymon Trials",
                Date = DateTime.Parse("2020-12-10"),
                Placement = Placement.First,
                Surface = Surface.Grass,
                Length = "500m",
                Location = "Tymon Park",
                Notes = "Jo performed very well.",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            Competition comp2 = new Competition()
            {
                CompId = 2,
                CompName = "Covid Cup",
                Date = DateTime.Parse("2020-12-11"),
                Placement = Placement.Second,
                Surface = Surface.Artificial,
                Length = "250m",
                Location = "Stepaside",
                Notes = "Jo did great.",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            //Act
            Competition savedComp1 = sut.CreateComp(comp1);
            Competition savedComp2 = sut.CreateComp(comp2);

            //Assert
            var foundCompById = sut.GetCompById(1);
            Assert.Equal("Tymon Trials", foundCompById.CompName);
        }

        [Fact]
        public void Index_Competition()
        {
            //Arrange
            ICompRepository sut = GetInMemoryCompRepository();
            Competition comp = new Competition()
            {
                CompId = 1,
                CompName = "Tymon Cup",
                Date = DateTime.Parse("2020-12-10"),
                Placement = Placement.First,
                Surface = Surface.Grass,
                Length = "500m",
                Location = "Tymon Park",
                Notes = "Jo performed very well.",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            //Act
            Competition savedComp = sut.CreateComp(comp);

            //Assert
            Assert.Single(sut.AllComps);
            Assert.Equal(1, savedComp.CompId);
            Assert.Equal("Tymon Cup", savedComp.CompName);
            Assert.Equal(DateTime.Parse("2020-12-10"), savedComp.Date);
            Assert.Equal(Placement.First, savedComp.Placement);
            Assert.Equal(Surface.Grass, savedComp.Surface);
            Assert.Equal("500m", savedComp.Length);
            Assert.Equal("Tymon Park", savedComp.Location);
            Assert.Equal("Jo performed very well.", savedComp.Notes);
            Assert.Equal(3, savedComp.DogId);
            Assert.Equal("d4eb7d23-d641-4c2d-8cd3-a036e08a3c65", savedComp.ApplicationUserId);

        }

        [Fact]
        public void Index_Update_Comp_Length()
        {
            //Arrange
            ICompRepository sut = GetInMemoryCompRepository();
            Competition comp1 = new Competition()
            {
                CompId = 1,
                CompName = "Tymon Cup",
                Date = DateTime.Parse("2020-12-10"),
                Placement = Placement.First,
                Surface = Surface.Grass,
                Length = "500m",
                Location = "Tymon Park",
                Notes = "Jo performed very well.",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            Competition comp2 = new Competition()
            {
                CompId = 1,
                CompName = "Tymon Cup",
                Date = DateTime.Parse("2020-12-10"),
                Placement = Placement.First,
                Surface = Surface.Grass,
                Length = "750m",
                Location = "Tymon Park",
                Notes = "Jo performed very well.",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            //Act
            Competition savedComp = sut.CreateComp(comp1);
            Competition savedComp1 = sut.UpdateComp(comp2);

            //Assert
            Assert.Single(sut.AllComps);
            Assert.Equal("750m", comp2.Length);
        }

        [Fact]
        public void Index_DeleteCompetition()
        {
            //Arrange
            ICompRepository sut = GetInMemoryCompRepository();
            Competition comp = new Competition()
            {
                CompId = 1,
                CompName = "Tymon Cup",
                Date = DateTime.Parse("2020-12-10"),
                Placement = Placement.First,
                Surface = Surface.Grass,
                Length = "500m",
                Location = "Tymon Park",
                Notes = "Jo performed very well.",
                DogId = 3,
                ApplicationUserId = "d4eb7d23-d641-4c2d-8cd3-a036e08a3c65"
            };

            //Act
            Competition savedComp = sut.CreateComp(comp);
            Competition savedComp1 = sut.DeleteComp(comp);
            
            //Assert
            Assert.Empty(sut.AllComps);
        }

        private ICompRepository GetInMemoryCompRepository()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "WM_db").Options;
            ApplicationDbContext applicationDataContext = new ApplicationDbContext(builder);
            applicationDataContext.Database.EnsureDeleted();
            applicationDataContext.Database.EnsureCreated();
            return new CompRepository(applicationDataContext);
        }
    }
}