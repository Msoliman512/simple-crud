
using Api.Features.Drivers.Commands;

namespace Api.Test.Features.CommandsTests;

[TestFixture]
public class CreateRandomBulkDriversValidatorTests
{
[Test]
public void validation_succeeds_when_count_is_greater_than_zero()
{
    // Arrange
    var validator = new CreateRandomBulkDriversValidator();
    var request = new CreateRandomBulkDriversRequest { Count = 1 };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsTrue(result.IsValid);
}

[Test]
public void validator_returns_no_errors_when_count_is_positive_integer()
{
    // Arrange
    var validator = new CreateRandomBulkDriversValidator();
    var request = new CreateRandomBulkDriversRequest { Count = 10 };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsFalse(result.Errors.Any());
}

[Test]
public void validation_fails_when_count_is_zero()
{
    // Arrange
    var validator = new CreateRandomBulkDriversValidator();
    var request = new CreateRandomBulkDriversRequest { Count = 0 };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsFalse(result.IsValid);
}

[Test]
public void validation_fails_when_count_is_negative()
{
    // Arrange
    var validator = new CreateRandomBulkDriversValidator();
    var request = new CreateRandomBulkDriversRequest { Count = -1 };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsFalse(result.IsValid);
}

[Test]
public void validator_returns_error_message_when_count_is_zero()
{
    // Arrange
    var validator = new CreateRandomBulkDriversValidator();
    var request = new CreateRandomBulkDriversRequest { Count = 0 };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Count must be greater than zero."));
}


}
