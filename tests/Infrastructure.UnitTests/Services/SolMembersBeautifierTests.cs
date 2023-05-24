using Application.Models;
using Application.Stores;
using Infrastructure.Services;
namespace Infrastructure.UnitTests.Services;
internal class SolMembersBeautifierTests
{
    #region Private properties
    private SolMembersBeautifier _sut = null!;

    private Mock<IAreaCodesStore> _areaCodesStoreMock = null!;
    #endregion


    #region Tests
    [TestCase("", "")]
    [TestCase("06-12345678", "06-12345678")]
    [TestCase("0612345678", "06-12345678")]
    [TestCase("612345678", "06-12345678")]
    [TestCase("6-12345678", "06-12345678")]
    [TestCase("06 12345678", "06-12345678")]
    public void BeautifyMobilePhoneNumbersList(string phoneNumber, string expectedNumber)
    {
        SolMember[] members = new[] { new SolMember { TelephoneNumber = phoneNumber } };

        _sut.Beautify(members);

        Assert.That(members[0].TelephoneNumber, Is.EqualTo(expectedNumber));
    }

    [TestCase("0222-123456", "0222-123456")]
    [TestCase("0222123456", "0222-123456")]
    [TestCase("0252123456", "0252-123456")]
    [TestCase("0315123456", "0315-123456")]
    [TestCase("222123456", "0222-123456")]
    [TestCase("222 123456", "0222-123456")]
    public void BeautifyArea3DigitsTelephoneNumbersList(string phoneNumber, string expectedNumber)
    {
        SolMember[] members = new[] { new SolMember { TelephoneNumber = phoneNumber } };
        _areaCodesStoreMock.Setup(a => a.IsValidCode(It.Is<string>(s => s.Length == 3))).Returns(true);

        _sut.Beautify(members);

        Assert.That(members[0].TelephoneNumber, Is.EqualTo(expectedNumber));
    }

    [TestCase("0010-123456", "0010-123456")]
    [TestCase("070123456", "0070-123456")]
    [TestCase("70123456", "0070-123456")]
    [TestCase("070 123456", "0070-123456")]
    [TestCase("70 123456", "0070-123456")]
    public void BeautifyArea2DigitsTelephoneNumbersList(string phoneNumber, string expectedNumber)
    {
        SolMember[] members = new[] { new SolMember { TelephoneNumber = phoneNumber } };
        _areaCodesStoreMock.Setup(a => a.IsValidCode(It.Is<string>(s => s.Length == 2))).Returns(true);

        _sut.Beautify(members);

        Assert.That(members[0].TelephoneNumber, Is.EqualTo(expectedNumber));
    }
    #endregion


    #region Setup and cleanup
    [SetUp]
    public void Setup()
    {
        _areaCodesStoreMock = new();

        _sut = new(_areaCodesStoreMock.Object);
    }
    #endregion
}
