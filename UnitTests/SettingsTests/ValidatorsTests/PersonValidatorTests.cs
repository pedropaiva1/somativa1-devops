using FluentValidation.API.Entities;
using FluentValidation.API.Settings.ValidationSettings;
using TestBuilders;

namespace UnitTests.SettingsTests.ValidatorsTests;
public sealed class PersonValidatorTests
{
    private readonly PersonValidator _personValidator;

    public PersonValidatorTests()
    {
        _personValidator = new PersonValidator();
    }

    [Fact]
    public async Task ValidatePersonAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var personToValidate = PersonBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personToValidate);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Fact]
    public async Task ValidatePersonAsync_InvalidAddress_ReturnsFalse()
    {
        // A
        var invalidDistrict = "a";
        var addressInvalid = AddressBuilder.NewObject().WithDistrict(invalidDistrict).DomainBuild();
        var personWithInvalidAddress = PersonBuilder.NewObject().WithAddress(addressInvalid).DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personWithInvalidAddress);

        // A
        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public async Task ValidatePersonAsync_InvalidPhoneList_ReturnsFalse()
    {
        // A
        var invalidPhoneList = new List<Phone>()
        {
            PhoneBuilder.NewObject().WithPhoneNumber("a").DomainBuild(),
            PhoneBuilder.NewObject().WithPhoneNumber("4545").DomainBuild()
        };
        var personWithInvalidPhoneList = PersonBuilder.NewObject().WithPhoneList(invalidPhoneList).DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personWithInvalidPhoneList);

        // A
        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public async Task ValidatePersonAsync_InvalidSkillList_ReturnsFalse()
    {
        // A
        var invalidSkillList = new List<Skill>()
        {
            SkillBuilder.NewObject().WithExperienceYears(-1).DomainBuild()
        };
        var personWithInvalidSkillList = PersonBuilder.NewObject().WithSkillList(invalidSkillList).DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personWithInvalidSkillList);

        // A
        Assert.False(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(Invalid3To50StringLengthParameters))]
    public async Task ValidatePersonAsyc_InvalidFirstName_ReturnsFalse(string firstName)
    {
        // A
        var personWithInvalidFirstName = PersonBuilder.NewObject().WithFirstName(firstName).DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personWithInvalidFirstName);

        // A
        Assert.False(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(Invalid3To50StringLengthParameters))]
    public async Task ValidatePersonAsyc_InvalidLastName_ReturnsFalse(string lastName)
    {
        // A
        var personWithInvalidLastName = PersonBuilder.NewObject().WithLastName(lastName).DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personWithInvalidLastName);

        // A
        Assert.False(validationResult.IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-120)]
    [InlineData(-1)]
    public async Task ValidatePersonAsyc_InvalidAge_ReturnsFalse(int age)
    {
        // A
        var personWithInvalidAge = PersonBuilder.NewObject().WithAge(age).DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personWithInvalidAge);

        // A
        Assert.False(validationResult.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid mail")]
    [InlineData("ran")]
    public async Task ValidatePersonAsync_InvalidEmail_ReturnsFalse(string email)
    {
        // A
        var personWithInvalidEmail = PersonBuilder.NewObject().WithEmail(email).DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personWithInvalidEmail);

        // A
        Assert.False(validationResult.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid cpf")]
    [InlineData("00000")]
    [InlineData("000000000")]
    [InlineData("123912039")]
    public async Task ValidatePersonAsync_InvalidCpf_ReturnsFalse(string cpf)
    {
        // A
        var personWithInvalidCpf = PersonBuilder.NewObject().WithCpf(cpf).DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personWithInvalidCpf);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static IEnumerable<object[]> Invalid3To50StringLengthParameters()
    {
        yield return new object[]
        {
            ""
        };

        yield return new object[]
        {
            "a"
        };

        yield return new object[]
        {
            new string('a', 51)
        };

        yield return new object[]
        {
            new string('a', 102)
        };
    }

    public static IEnumerable<object[]> InvalidBirthDateParameters()
    {
        yield return new object[]
        {
            DateTime.Now.AddYears(-2)
        };

        yield return new object[]
        {
            DateTime.Now.AddYears(-5)
        };

        yield return new object[]
        {
            new DateTime(2008, 01, 01)
        };
    }
}
