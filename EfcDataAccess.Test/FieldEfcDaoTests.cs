using Domain.Models;
using EfcDataAccess.DAOs;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EfcDataAccess.Test;

public class FieldEfcDaoTests
{
    private readonly Mock<SmartFarmerAppContext> mockContext;
    private readonly FieldEfcDao sut;

    public FieldEfcDaoTests()
    {
        mockContext = new Mock<SmartFarmerAppContext>();
        sut = new FieldEfcDao(mockContext.Object);
    }

    /*
[Fact]
    public async Task GetFieldsByOwnerId_ReturnAListOfFields_WhenAllCriteriasAreMeet()
    {
        //Arrange 
        var ownerId = 1;
        var fields = new List<Field>
        {
            new Field { Id = 1, OwnerId = ownerId, Name = "field1", LocationData = "location" },
            new Field { Id = 2, OwnerId = ownerId, Name = "field2", LocationData = "location" }
        };
        var mockDbSet = new Mock<DbSet<Field>>();
        mockDbSet.As<IQueryable<Field>>().Setup(m => m.Provider).Returns(fields.AsQueryable().Provider);
        mockDbSet.As<IQueryable<Field>>().Setup(m => m.Expression).Returns(fields.AsQueryable().Expression);
        mockDbSet.As<IQueryable<Field>>().Setup(m => m.ElementType).Returns(fields.AsQueryable().ElementType);
        mockDbSet.As<IQueryable<Field>>().Setup(m => m.GetEnumerator()).Returns(fields.GetEnumerator());
        mockContext.Setup(c => c.Fields).Returns(mockDbSet.Object);
        
        var result = await sut.GetFieldsByOwnerId(ownerId);
        
        Assert.NotNull(result);
        Assert.Equal(fields.Count, result.Count());
    }

    */
    
}