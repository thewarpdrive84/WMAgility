using System;
using Xunit;
using WMAgility2.Models;

namespace WMAgility.Tests
{
    public class FaultTests
    {
        [Fact]
        public void CanUpdateFaultName()
        {
            // Arrange
            var fault = new Fault { Name = "Knocked Bar" };
            // Act
            fault.Name = "Knocked Pole";
            //Assert
            Assert.Equal("Knocked Pole", fault.Name);
        }
    }
}
