using Api.Features.Drivers.Commands;

namespace Api.Test.Features.CommandsTests;

[TestFixture]
public class DeleteDriverValidatorTests
{
    
    [Test]
    public void validation_succeeds_when_id_is_greater_than_zero()
    {
        // Arrange
        var validator = new DeleteDriverValidator();
        var request = new DeleteDriverRequest { Id = 1 };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void validation_fails_when_id_is_equal_to_zero()
    {
        // Arrange
        var validator = new DeleteDriverValidator();
        var request = new DeleteDriverRequest { Id = 0 };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Id must be greater than zero."));
    }

    [Test]
    public void validation_fails_when_id_is_less_than_zero()
    {
        // Arrange
        var validator = new DeleteDriverValidator();
        var request = new DeleteDriverRequest { Id = -1 };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Id must be greater than zero."));
    }


}