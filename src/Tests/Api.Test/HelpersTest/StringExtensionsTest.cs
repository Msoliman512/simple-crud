using Api.Common.Helpers;

namespace Api.Test.HelpersTest;

public class StringExtensionsTest
{
    
// The method returns true for a valid email address.
[Test]
public void returns_true_for_valid_email_address()
{
    // Arrange
    string email = "test@example.com";

    // Act
    bool result = email.IsValidEmail();

    // Assert
    Assert.IsTrue(result);
}

// The method returns false for an invalid email address.
[Test]
public void returns_false_for_invalid_email_address()
{
    // Arrange
    string email = "invalid_email";

    // Act
    bool result = email.IsValidEmail();

    // Assert
    Assert.IsFalse(result);
}

// The method returns false for a null email address.
[Test]
public void returns_false_for_null_email_address()
{
    // Arrange
    string email = null;

    // Act
    bool result = email.IsValidEmail();

    // Assert
    Assert.IsFalse(result);
}

// The method returns false for an email address with no domain name.
[Test]
public void returns_false_for_email_address_with_no_domain_name()
{
    // Arrange
    string email = "test@";

    // Act
    bool result = email.IsValidEmail();

    // Assert
    Assert.IsFalse(result);
}

// The method returns false for an email address with no TLD.
[Test]
public void returns_false_for_email_address_with_no_tld()
{
    // Arrange
    string email = "test@example";

    // Act
    bool result = email.IsValidEmail();

    // Assert
    Assert.IsFalse(result);
}

// The method returns false for an email address with a TLD of less than two characters.
[Test]
public void returns_false_for_email_address_with_tld_less_than_two_characters()
{
    // Arrange
    string email = "test@example.c";

    // Act
    bool result = email.IsValidEmail();

    // Assert
    Assert.IsFalse(result);
}

[Test]
public void test_email_with_multiple_subdomains()
{
    // Arrange
    string email = "test@sub1.sub2.example.com";

    // Act
    bool result = email.IsValidEmail();

    // Assert
    Assert.IsTrue(result);
}

[Test]
public void test_email_with_unicode_characters()
{
    // Arrange
    string email = "test@exämple.com";

    // Act
    bool result = email.IsValidEmail();

    // Assert
    Assert.IsTrue(result);
}

[Test]
public void test_valid_phone_number()
{
    // Arrange
    string phoneNumber = "1234567890";

    // Act
    bool result = phoneNumber.IsValidPhoneNumber();

    // Assert
    Assert.IsTrue(result);
}

[Test]
public void IsValidPhoneNumber_ReturnsFalseForEmptyInput()
{
    // Arrange
    string phoneNumber = "";

    // Act
    bool result = phoneNumber.IsValidPhoneNumber();

    // Assert
    Assert.IsFalse(result);
}

[Test]
public void test_is_valid_phone_number_returns_false_for_null_input()
{
    // Arrange
    string phoneNumber = null;

    // Act
    bool result = phoneNumber.IsValidPhoneNumber();

    // Assert
    Assert.IsFalse(result);
}

[Test]
public void test_invalid_phone_number()
{
    // Arrange
    string phoneNumber = "abc123";

    // Act
    bool result = phoneNumber.IsValidPhoneNumber();

    // Assert
    Assert.IsFalse(result);
}

[Test]
public void test_is_valid_phone_number_with_special_characters()
{
    // Arrange
    string phoneNumber = "190!@#$%^&*()";

    // Act
    bool isValid = phoneNumber.IsValidPhoneNumber();

    // Assert
    Assert.IsFalse(isValid);
}

[Test]
public void test_is_valid_phone_number_for_more_than_15_digit()
{
    // Arrange
    string phoneNumber = "1234567890123456";

    // Act
    bool isValid = StringExtensions.IsValidPhoneNumber(phoneNumber);

    // Assert
    Assert.IsFalse(isValid);
}

}