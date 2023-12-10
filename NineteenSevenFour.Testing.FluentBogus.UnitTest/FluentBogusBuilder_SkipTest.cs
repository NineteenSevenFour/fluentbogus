using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Extension;
using NineteenSevenFour.Testing.FluentBogus.UnitTest.Faker;

using System.Linq.Expressions;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

public class FluentBogusBuilder_SkipTest
{
  [Fact]
  public void Should_AddPropertyToSkipList_WhenCalling_Skip_WithSinglePropertyLambdaExpression()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

    // Act
    builder.Skip(e => e.Addresses);

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.skipProperties.Should()
                               .NotBeNullOrEmpty()
                               .And
                               .HaveCount(1);
  }

  [Fact]
  public void Should_ThrowInvalidOperationException__WhenCalling_Skip_WithDuplicateSinglePropertyLambdaExpression()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();
    builder.Skip(e => e.Addresses);

    // Act
#pragma warning disable IDE0039 // Use local function
    var result = () => builder.Skip(e => e.Addresses);
#pragma warning restore IDE0039 // Use local function

    // Assert
    var exception = Assert.Throws<InvalidOperationException>(result);
    exception.Should().NotBeNull();
    exception.Message.Should().Be($"The property Addresses for type PersonModel is already set to be skipped.");
  }

  [Fact]
  public void Should_AddPropertiesToSkipList_WhenCalling_Skip_WithMultiplePropertyLambdaExpression()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

    // Act
    builder.Skip(e => e.Addresses, equals => equals.Relatives);

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.skipProperties.Should()
                               .NotBeNullOrEmpty()
                               .And
                               .HaveCount(2);
  }

  [Fact]
  public void Should_ThrowArgumentOutOfRangeException_WhenCalling_Skip_WithMultipleNullPropertyLambdaExpression()
  {
    // Arrange
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    Expression<Func<PersonModel, object?>> property1 = null;
    Expression<Func<PersonModel, object?>> property2 = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

    // Act
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable IDE0039 // Use local function
    var result = () => builder.Skip(property1, property2);
#pragma warning restore IDE0039 // Use local function
#pragma warning restore CS8604 // Possible null reference argument.

    // Assert
    var exception = Assert.Throws<ArgumentOutOfRangeException>(result);
    exception.Should()
             .NotBeNull();
    exception.Message.Should()
                     .Be($"A List of properties must be provided. (Parameter 'properties')");
  }

  [Fact]
  public void Should_ThrowArgumentOutOfRangeException_WhenCalling_Skip_WithEmptyArrayOfPropertyLambdaExpression()
  {
    // Arrange
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    Expression<Func<PersonModel, object?>>[] properties = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

    // Act
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable IDE0039 // Use local function
    var result = () => builder.Skip(properties);
#pragma warning restore IDE0039 // Use local function
#pragma warning restore CS8604 // Possible null reference argument.

    // Assert
    var exception = Assert.Throws<ArgumentOutOfRangeException>(result);
    exception.Should().NotBeNull();
    exception.Message.Should()
                     .Be($"A List of properties must be provided. (Parameter 'properties')");
  }
}

public class FluentBogusBuilder_UseRuleTest
{
  [Fact]
  public void Should_AddRulesetToList_WhenCalling_UseRule_WithSingleRuleset()
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
  public void Should_ThrowInvalidOperationException__WhenCalling_UseRule_WithDuplicateSingleRuleset()
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
  public void Should_ThrowArgumentOutOfRangeException__WhenCalling_UseRule_WithEmptySingleRuleset()
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
  public void Should_AddPropertiesToSkipList_WhenCalling_Skip_WithMultipleRuleset()
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
  public void Should_ThrowArgumentOutOfRangeException_WhenCalling_Skip_WithMultipleNullRuleset()
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
  public void Should_ThrowArgumentOutOfRangeException_WhenCalling_Skip_WithEmptyArrayOfRuleset()
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
