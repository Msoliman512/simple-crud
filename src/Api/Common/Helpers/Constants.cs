namespace Api.Common.Helpers;

public static class Constants
{
    public const int NamesMaxLength = 50;
    public const int SearchMaxLength = 100;
    
    public const string RecommendedSearchPattern = @"^[a-zA-Z0-9\s\-\.\,]*$";

    public static readonly string[] ColumnsNames = { "FirstName", "LastName", "Email", "PhoneNumber" };

}