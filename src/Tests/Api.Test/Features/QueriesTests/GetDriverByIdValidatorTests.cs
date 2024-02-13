using Api.Features.Drivers.Queries;

namespace Api.Test.Features.QueriesTests;

[TestFixture]
public class GetDriverByIdValidatorTests
{
    
    [Test]
    public void validation_succeeds_when_id_is_greater_than_0()
    {
        // Arrange
        var validator = new GetDriverByIdValidator();
        var request = new GetDriverByIdRequest { Id = 1 };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void validation_fails_when_id_is_0()
    {
        // Arrange
        var validator = new GetDriverByIdValidator();
        var request = new GetDriverByIdRequest { Id = 0 };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Id must be greater than 0."));
    }

    [Test]
    public void validation_fails_when_id_is_negative()
    {
        // Arrange
        var validator = new GetDriverByIdValidator();
        var request = new GetDriverByIdRequest { Id = -1 };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Id must be greater than 0."));
    }


}