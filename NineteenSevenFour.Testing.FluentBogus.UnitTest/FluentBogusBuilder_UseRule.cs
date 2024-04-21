using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Extension;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest
{
  public class FluentBogusBuilder_UseRule
  {
    [Fact]
    public void Should_AddRulesetToList_WhenCalled_WithSingleRuleset()
    {
      // Arrange
      var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

      // Act
      builder.UseRuleSet("SomeRule");

      // Assert
      var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
      Assert.NotNull(typedBuilder);
      typedBuilder.ruleSets.Should()
                           .NotBeNullOrEmpty()
                           .And
                           .HaveCount(1);
    }

    [Fact]
    public void Should_ThrowInvalidOperationException__WhenCalled_WithDuplicateSingleRuleset()
    {
      // Arrange
      var ruleSet = "SomeRule";
      var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();
      builder.UseRuleSet(ruleSet);

      // Act
#pragma warning disable IDE0039 // Use local function
      var result = () => builder.UseRuleSet(ruleSet);
#pragma warning restore IDE0039 // Use local function

      // Assert
      var exception = Assert.Throws<InvalidOperationException>(result);
      exception.Should().NotBeNull();
      exception.Message.Should().Be($"The ruleset SomeRule is already set to be used.");
    }

    [Fact]
    public void Should_ThrowArgumentOutOfRangeException__WhenCalled_WithEmptySingleRuleset()
    {
      // Arrange
      var ruleSet = "";
      var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

      // Act
#pragma warning disable IDE0039 // Use local function
      var result = () => builder.UseRuleSet(ruleSet);
#pragma warning restore IDE0039 // Use local function

      // Assert
      var exception = Assert.Throws<ArgumentOutOfRangeException>(result);
      exception.Should().NotBeNull();
      exception.Message.Should().Be($"A ruleset must be provided. (Parameter 'ruleset')");
    }

    [Fact]
    public void Should_AddPropertiesToSkipList_WhenCalled_WithMultipleRuleset()
    {
      // Arrange
      var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

      // Act
      builder.UseRuleSet("rule1", "rule2");

      // Assert
      var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
      Assert.NotNull(typedBuilder);
      typedBuilder.ruleSets.Should()
                           .NotBeNullOrEmpty()
                           .And
                           .HaveCount(2);
    }

    [Fact]
    public void Should_ThrowArgumentOutOfRangeException_WhenCalled_WithMultipleNullRuleset()
    {
      // Arrange
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
      string rule1 = null;
      string rule2 = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
      var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

      // Act
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable IDE0039 // Use local function
      var result = () => builder.UseRuleSet(rule1, rule2);
#pragma warning restore IDE0039 // Use local function
#pragma warning restore CS8604 // Possible null reference argument.

      // Assert
      var exception = Assert.Throws<ArgumentOutOfRangeException>(result);
      exception.Should()
               .NotBeNull();
      exception.Message.Should()
                       .Be($"A List of ruleset must be provided. (Parameter 'rulesets')");
    }

    [Fact]
    public void Should_ThrowArgumentOutOfRangeException_WhenCalled_WithEmptyArrayOfRuleset()
    {
      // Arrange
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
      string[] rules = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
      var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

      // Act
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable IDE0039 // Use local function
      var result = () => builder.UseRuleSet(rules);
#pragma warning restore IDE0039 // Use local function
#pragma warning restore CS8604 // Possible null reference argument.

      // Assert
      var exception = Assert.Throws<ArgumentOutOfRangeException>(result);
      exception.Should().NotBeNull();
      exception.Message.Should()
                       .Be($"A List of ruleset must be provided. (Parameter 'rulesets')");
    }
  }
}
