using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMAgility.Tests.Model;
using WMAgility2.Controllers;
using WMAgility2.Data;
using WMAgility2.Models;
using WMAgility2.Models.ViewModels;
using Xunit;

namespace WMAgility.Tests
{
    ////*NOT WORKING* error in constructor for PracticeController

    //public class PracticesControllerTest
    //{
    //    [Fact]
    //    public void Index_ReturnsAViewResult_ContainsAllPractices()
    //    {
    //        //arrange
    //        var mockDogRepository = RepositoryMocks.GetDogRepository();
    //        var mockSkillRepository = RepositoryMocks.GetSkillRepository();
    //        var mockPracticeRepository = RepositoryMocks.GetPracticeRepository();

    //        var practicesController = new PracticesController(mockDogRepository.Object, mockSkillRepository.Object, mockPracticeRepository.Object);

    //// alt constructor:
    //        var practicesController = new PracticesController(ApplicationDbContext db, IDogRepository dogRepository, UserManager < IdentityUser > userManager);

    //        //act
    //        var result = practicesController.Index();

    //        //assert
    //        var viewResult = Assert.IsType<ViewResult>(result);
    //        var practices = Assert.IsAssignableFrom<IEnumerable<Practice>>(viewResult.ViewData.Model);
    //        Assert.Equal(10, practices.Count());
    //    }

    //    [Fact]
    //    public void AddPractice_Redirects_ValidPracticeViewModel()
    //    {
    //        //arrange
    //        var practiceViewModel = new PracticeViewModel();
    //        var mockPracticeRepository = RepositoryMocks.GetPracticeRepository();
    //        var practice = mockPracticeRepository.Object.GetPracticeById(1);
    //        practiceViewModel.Practice = practice;
    //        practiceViewModel.Id = 1;

    //        var mockDogRepository = new Mock<IDogRepository>();

    //        var practicesController = new PracticesController();

    //        //act
    //        var result = practicesController.Create(practiceViewModel);

    //        //assert
    //        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
    //        Assert.Equal("Index", redirectToActionResult.ActionName);
    //    }
    //}
}
