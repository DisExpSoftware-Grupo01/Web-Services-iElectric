using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using web_services_ielectric.ApplianceBrands.Domain.Models;
using web_services_ielectric.ApplianceBrands.Domain.Repositories;
using web_services_ielectric.ApplianceBrands.Services;
using web_services_ielectric.ApplianceModels.Domain.Models;
using web_services_ielectric.Shared.Domain.Repositories;
using Xunit;
namespace web_services_ielectric.Tests.UnitTests;

public class ApplianceModelTest
{
    [Fact]
    public async Task SaveAsync_ValidBrand_ShouldReturnSuccessResponse()
    {
        // Arrange
        var mockBrandRepository = new Mock<IApplianceBrandRepository>();
        mockBrandRepository.Setup(repo => repo.AddAsync(It.IsAny<ApplianceBrand>()))
            .Returns(Task.CompletedTask); // Simular que la operación de guardar tiene éxito

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(uow => uow.CompleteAsync())
            .Returns(Task.CompletedTask); // Simular que la operación de unidad de trabajo tiene éxito

        var applianceBrandService = new ApplianceBrandService(
            mockBrandRepository.Object,
            mockUnitOfWork.Object
        );

        var brandToSave = new ApplianceBrand
        {
            Name = "Marca Ejemplo",
            ImgPath = "/images/marca-ejemplo.png",
            ApplianceModels = new List<ApplianceModel>
            {
                new ApplianceModel
                {
                    Name = "Modelo 1",
                    Model = "12345",
                    ImgPath = "/images/modelo-1.png",
                    ApplianceBrandId = 1
                },
                new ApplianceModel
                {
                    Name = "Modelo 2",
                    Model = "67890",
                    ImgPath = "/images/modelo-2.png",
                    ApplianceBrandId = 1
                }
            }
        };


        // Act
        var response = await applianceBrandService.SaveAsync(brandToSave);

        // Assert
        Assert.True(response.Success);
        Assert.Null(response.Message);
    }
}