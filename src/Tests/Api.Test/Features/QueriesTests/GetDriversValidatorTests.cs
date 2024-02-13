using Api.Features.Drivers.Queries;

namespace Api.Test.Features.QueriesTests;

[TestFixture]
public class GetDriversValidatorTests
{
    
[Test]
public void validator_validates_valid_request_no_errors()
{
    // Arrange
    var validator = new GetDriversValidator();
    var request = new GetDriversRequest
    {
        PageIndex = 1,
        PageSize = 10,
        Keyword = "keyword",
        OrderByColumn = "FirstName",
        OrderBy = true
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsTrue(result.IsValid);
    Assert.IsEmpty(result.Errors);
}

[Test]
public void validator_validates_request_with_page_index_and_size_no_errors()
{
    // Arrange
    var validator = new GetDriversValidator();
    var request = new GetDriversRequest
    {
        PageIndex = 1,
        PageSize = 10
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsTrue(result.IsValid);
    Assert.IsEmpty(result.Errors);
}

[Test]
public void validator_validates_request_with_keyword_no_errors()
{
    // Arrange
    var validator = new GetDriversValidator();
    var request = new GetDriversRequest
    {
        Keyword = "keyword"
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsTrue(result.IsValid);
    Assert.IsEmpty(result.Errors);
}

[Test]
public void validator_validates_request_with_order_by_no_errors()
{
    // Arrange
    var validator = new GetDriversValidator();
    var request = new GetDriversRequest
    {
        OrderByColumn = "LastName",
        OrderBy = true
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsTrue(result.IsValid);
    Assert.IsEmpty(result.Errors);
}

[Test]
public void validator_returns_error_when_page_index_less_than_or_equal_to_zero()
{
    // Arrange
    var validator = new GetDriversValidator();
    var request = new GetDriversRequest
    {
        PageIndex = 0,
        PageSize = 10
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsFalse(result.IsValid);
    Assert.That(result.Errors.Count, Is.EqualTo(1));
    Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("Page Index must be greater than 0."));
}

[Test]
public void validator_returns_error_when_page_size_less_than_or_equal_to_zero()
{
    // Arrange
    var validator = new GetDriversValidator();
    var request = new GetDriversRequest
    {
        PageIndex = 1,
        PageSize = 0
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsFalse(result.IsValid);
    Assert.That(result.Errors.Count, Is.EqualTo(1));
    Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("Page Size must be greater than 0."));
}

[Test]
public void validator_returns_error_when_keyword_length_exceeds_100_characters()
{
    // Arrange
    var validator = new GetDriversValidator();
    var request = new GetDriversRequest
    {
        Keyword = "keywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeywordkeyword"
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsFalse(result.IsValid);
    Assert.That(result.Errors.Count, Is.EqualTo(1));
    Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("Keyword length must not exceed 100 characters."));
}

[Test]
public void validator_returns_error_when_keyword_not_in_recommended_search_pattern()
{
    // Arrange
    var validator = new GetDriversValidator();
    var request = new GetDriversRequest
    {
        Keyword = "1234#%@!&*5678/*\\[]'\\?'90"
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsFalse(result.IsValid);
    Assert.That(result.Errors.Count, Is.EqualTo(1));
    Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("Keyword is Invalid."));
}

[Test]
public void validator_returns_error_when_order_by_column_invalid()
{
    // Arrange
    var validator = new GetDriversValidator();
    var request = new GetDriversRequest
    {
        OrderByColumn = "invalid"
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsFalse(result.IsValid);
    Assert.That(result.Errors.Count, Is.EqualTo(1));
    Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("OrderBy Column is Invalid."));
}

[Test]
public void validator_allows_keyword_to_be_null_or_empty()
{
    // Arrange
    var validator = new GetDriversValidator();
    var request = new GetDriversRequest
    {
        PageIndex = 1,
        PageSize = 10,
        Keyword = null,
        OrderByColumn = "Email",
        OrderBy = true
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsTrue(result.IsValid);
    Assert.IsEmpty(result.Errors);
}

[Test]
public void validator_allows_order_by_column_to_be_null_or_empty()
{
    // Arrange
    var validator = new GetDriversValidator();
    var request = new GetDriversRequest
    {
        PageIndex = 1,
        PageSize = 10,
        Keyword = "keyword",
        OrderByColumn = null,
        OrderBy = true
    };

    // Act
    var result = validator.Validate(request);

    // Assert
    Assert.IsTrue(result.IsValid);
    Assert.IsEmpty(result.Errors);
}


}