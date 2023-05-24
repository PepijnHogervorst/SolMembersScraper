using Application.Models;
using Application.Services;
using Application.Stores;
using Domain.Common.Extensions;

namespace Infrastructure.Services;
internal class SolMembersBeautifier : ISolMembersBeautifier
{
    #region Private fields
    private readonly IAreaCodesStore _areaCodes;
    #endregion


    #region Constructor
    public SolMembersBeautifier(IAreaCodesStore areaCodes)
    {
        _areaCodes = areaCodes;
    }
    #endregion


    #region Public methods
    public void Beautify(IEnumerable<SolMember> members)
    {
        foreach (var member in members)
        {
            BeautifyMemberPhoneNumbers(member);
        }
    }
    #endregion


    #region Private methods
    private void BeautifyMemberPhoneNumbers(SolMember member)
    {
        member.TelephoneNumber = BeautifyPhoneNumbers(member.TelephoneNumber);
        member.MobileNumber = BeautifyPhoneNumbers(member.MobileNumber);
        member.Parent1Telephone = BeautifyPhoneNumbers(member.Parent1Telephone);
        member.Parent2Telephone = BeautifyPhoneNumbers(member.Parent2Telephone);
    }

    private string BeautifyPhoneNumbers(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber)) return phoneNumber;
        phoneNumber = phoneNumber.RemoveWhitespace();
        if (phoneNumber.Length > 10) return phoneNumber;

        int minusSignIndex = phoneNumber.IndexOf('-');
        bool containsMinusSign = minusSignIndex >= 0;
        if (phoneNumber.StartsWith("06") && phoneNumber.Length == 10 && !containsMinusSign) return $"{phoneNumber[..2]}-{phoneNumber[2..]}";
        if (phoneNumber.StartsWith('6'))
        {
            if (phoneNumber.Length == 10 && minusSignIndex == 1) return $"0{phoneNumber}";
            if (phoneNumber.Length == 9 && !containsMinusSign) return $"06-{phoneNumber[1..]}";
        }
        if (containsMinusSign) return phoneNumber;

        int amountOfStartZeros = GetAmountOfStartZeros(phoneNumber);
        if ((amountOfStartZeros == 0 && phoneNumber.Length == 10) || amountOfStartZeros > 4) return phoneNumber;

        if (TryFormatNumberWithAreaCode(phoneNumber, amountOfStartZeros, 2, out var newNumber)) return newNumber;
        if (TryFormatNumberWithAreaCode(phoneNumber, amountOfStartZeros, 3, out newNumber)) return newNumber;

        return $"{phoneNumber}?";
    }

    private bool TryFormatNumberWithAreaCode(string phoneNumber, int offset, int areaLength, out string newPhoneNumber)
    {
        string area = phoneNumber.Substring(offset, areaLength);
        if (_areaCodes.IsValidCode(area))
        {
            newPhoneNumber = $"{area}-{phoneNumber[(offset + areaLength)..]}".PadLeft(11, '0');
            return true;
        }
        newPhoneNumber = phoneNumber;
        return false;
    }

    private static int GetAmountOfStartZeros(string phoneNumber)
    {
        int amount = 0;

        while (amount < phoneNumber.Length)
        {
            if (phoneNumber[amount] != '0') return amount;
            amount++;
        }

        return amount;
    }
    #endregion
}
