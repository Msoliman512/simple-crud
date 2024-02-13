using Api.Features.Drivers.Commands;

namespace Api.Test.Features.CommandsTests;

[TestFixture]
public class UpdateDriverValidatorTests
{
    
[Test]
public void validation_succeeds_when_all_fields_are_valid()
{
    // Arrange
    var validator = new UpdateDriverValidator();
    var request = new UpdateDriverRequest
    {
        Id = 1,
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
public void validation_succeeds_when_phone_number_is_null_or_empty()
{
    // Arrange
    var validator = new UpdateDriverValidator();
    var request = new UpdateDriverRequest
    {
        Id = 1,
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
public void validation_fails_when_phone_number_is_valid_and_email_is_null_or_empty()
{
    // Arrange
    var validator = new UpdateDriverValidator();
    var request = new UpdateDriverRequest
    {
        Id = 1,
        FirstName = "John",
        LastName = "Doe",
        Email = null,
        PhoneNumber = "1234567890"
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsFalse(result.IsValid);
}


[Test]
public void validation_fails_when_id_is_less_than_or_equal_to_zero()
{
    // Arrange
    var validator = new UpdateDriverValidator();
    var request = new UpdateDriverRequest
    {
        Id = 0,
        FirstName = "John",
        LastName = "Doe",
        Email = "john.doe@example.com",
        PhoneNumber = "1234567890"
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsFalse(result.IsValid);
    Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("Id must be greater than zero."));
}

[Test]
public void validation_fails_when_first_name_is_null()
{
    // Arrange
    var validator = new UpdateDriverValidator();
    var request = new UpdateDriverRequest
    {
        Id = 1,
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
public void validation_fails_when_first_name_is_empty()
{
    // Arrange
    var validator = new UpdateDriverValidator();
    var request = new UpdateDriverRequest
    {
        Id = 1,
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
public void validation_fails_when_first_name_is_greater_than_50_characters()
{
    // Arrange
    var validator = new UpdateDriverValidator();
    var request = new UpdateDriverRequest
    {
        Id = 1,
        FirstName = "JohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohn",
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
public void validation_fails_when_last_name_is_null()
{
    // Arrange
    var validator = new UpdateDriverValidator();
    var request = new UpdateDriverRequest
    {
        Id = 1,
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
public void validation_fails_when_last_name_is_empty()
{
    // Arrange
    var validator = new UpdateDriverValidator();
    var request = new UpdateDriverRequest
    {
        Id = 1,
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
public void validation_fails_when_last_name_is_greater_than_50_characters()
{
    // Arrange
    var validator = new UpdateDriverValidator();
    var request = new UpdateDriverRequest
    {
        Id = 1,
        FirstName = "John",
        LastName = "DoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoe",
        Email = "john.doe@example.com",
        PhoneNumber = "1234567890"
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsFalse(result.IsValid);
    Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("Last Name must not exceed 50 characters."));
}

[Test]
public void validation_succeeds_when_phone_number_is_valid()
{
    // Arrange
    var validator = new UpdateDriverValidator();
    var request = new UpdateDriverRequest
    {
        Id = 1,
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
public void validation_fails_when_email_is_null()
{
    // Arrange
    var validator = new UpdateDriverValidator();
    var request = new UpdateDriverRequest
    {
        Id = 1,
        FirstName = "John",
        LastName = "Doe",
        Email = null,
        PhoneNumber = "1234567890"
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsFalse(result.IsValid);
    Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Email must not be empty."));
}

[Test]
public void validation_fails_when_email_is_not_valid_email_address()
{
    // Arrange
    var validator = new UpdateDriverValidator();
    var request = new UpdateDriverRequest
    {
        Id = 1,
        FirstName = "John",
        LastName = "Doe",
        Email = "john.doe",
        PhoneNumber = "1234567890"
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsFalse(result.IsValid);
    Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Invalid email format."));
}


}