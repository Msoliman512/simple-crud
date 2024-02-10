using System.Globalization;
using System.Text.RegularExpressions;

namespace Api.Common.Helpers;

public static class StringExtensions
{
    #region Validations 
     // microsoft recommended email regex validation
    public static bool IsValidEmail(this string email) 
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
    
    public static bool IsValidPhoneNumber(this string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        return Regex.IsMatch(phoneNumber, @"^\d{1,15}$");
    }

    #endregion
    
}