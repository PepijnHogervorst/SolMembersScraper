namespace Application.Models;
/// <summary>
/// Data model that holds member information
/// Make sure to keep properties in same order as xml table columns!
/// </summary>
public class SolMember
{
    public string Id { get; set; }
    public string Initials { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    /// <summary>
    /// Tussenvoegsel
    /// </summary>
    public string Infix { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string AdditionalAddressInformation { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string HouseNumber { get; set; } = string.Empty;
    public string HouseNumberAddition { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string BirthDate { get; set; } = string.Empty;
    public string BirthCity { get; set; } = string.Empty;
    public string BirthCountry { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string TelephoneNumber { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Parent1Name { get; set; } = string.Empty;
    public string Parent1Telephone { get; set; } = string.Empty;
    public string Parent1Email { get; set; } = string.Empty;
    public string Parent2Name { get; set; } = string.Empty;
    public string Parent2Telephone { get; set; } = string.Empty;
    public string Parent2Email { get; set; } = string.Empty;
    public string Function { get; set; } = string.Empty;
    public string FunctionStartDate { get; set; } = string.Empty;
    public string HealthCareProvider { get; set; } = string.Empty;
    public string HealthCareNumber { get; set; } = string.Empty;
    public string AdditionalInformation { get; set; } = string.Empty;
    public string Age { get; set; } = string.Empty;
}
