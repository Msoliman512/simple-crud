using Api.Features.Drivers.Commands;

namespace Api.Test.Features.CommandsTests;

[TestFixture]
public class CreateDriverValidatorTests
{
[Test]
    public void Validation_Succeeds_When_All_Fields_Are_Valid()
    {
        // Arrange
        var validator = new CreateDriverValidator();
        var request = new CreateDriverRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890"
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void Validation_Succeeds_When_PhoneNumber_Is_Null_Or_Empty()
    {
        // Arrange
        var validator = new CreateDriverValidator();
        var request = new CreateDriverRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = null
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void Validation_Succeeds_When_PhoneNumber_Is_Valid()
    {
        // Arrange
        var validator = new CreateDriverValidator();
        var request = new CreateDriverRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890"
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void Validation_Succeeds_When_FirstName_Is_At_Maximum_Length()
    {
        // Arrange
        var validator = new CreateDriverValidator();
        var request = new CreateDriverRequest
        {
            FirstName =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890"
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("First Name must not exceed 50 characters."));
    }

    [Test]
    public void Validation_Succeeds_When_LastName_Is_At_Maximum_Length()
    {
        // Arrange
        var validator = new CreateDriverValidator();
        var request = new CreateDriverRequest
        {
            FirstName = "John",
            LastName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890"
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsFalse(result.IsValid);
    }

    [Test]
    public void Validation_Succeeds_When_Email_Is_At_Maximum_Length()
    {
        // Arrange
        var validator = new CreateDriverValidator();
        var request = new CreateDriverRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "loremipsumdolorsitametconsecteturadipiscingelitseddoeiusmodtemporincididuntutlaboreetdoloremagnaaliqua@example.com",
            PhoneNumber = "1234567890"
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void Validation_Fails_When_FirstName_Is_Null()
    {
        // Arrange
        var validator = new CreateDriverValidator();
        var request = new CreateDriverRequest
        {
            FirstName = null,
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890"
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("First Name must not be empty."));
    }

    [Test]
    public void Validation_Fails_When_FirstName_Is_Empty()
    {
        // Arrange
        var validator = new CreateDriverValidator();
        var request = new CreateDriverRequest
        {
            FirstName = string.Empty,
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890"
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("First Name must not be empty."));
    }

    [Test]
    public void Validation_Fails_When_FirstName_Is_Longer_Than_Maximum_Length()
    {
        // Arrange
        var validator = new CreateDriverValidator();
        var request = new CreateDriverRequest
        {
            FirstName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890"
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("First Name must not exceed 50 characters."));
    }


    [Test]
    public void Validation_Fails_When_LastName_Is_Null()
    {
        // Arrange
        var validator = new CreateDriverValidator();
        var request = new CreateDriverRequest
        {
            FirstName = "John",
            LastName = null,
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890"
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("Last Name must not be empty."));
    }

    [Test]
    public void Validation_Fails_When_LastName_Is_Empty()
    {
        // Arrange
        var validator = new CreateDriverValidator();
        var request = new CreateDriverRequest
        {
            FirstName = "John",
            LastName = string.Empty,
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890"
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("Last Name must not be empty."));
    }

    [Test]
    public void Validation_Fails_When_LastName_Is_Longer_Than_Maximum_Length()
    {
        // Arrange
        var validator = new CreateDriverValidator();
        var request = new CreateDriverRequest
        {
            FirstName = "John",
            LastName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890"
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("Last Name must not exceed 50 characters."));
    }


}