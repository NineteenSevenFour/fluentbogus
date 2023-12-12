using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Extension;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

public class FluentBogusBuilder_RuleSetString
{
  [Fact]
  public void Should_ReturnJoinedList_WhenCalled_WithRuleset()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

    // Act
    builder.UseRuleSet("rule1", "rule2");

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.RuleSetString.Should()
                              .NotBeNullOrEmpty()
                              .And
                              .Be("rule1,rule2");
  }

  [Fact]
  public void Should_ReturnEmptyString_WhenCalled_WithoutRuleset()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

    // Act
    
    // Assert
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.RuleSetString.Should()
                              .BeNullOrEmpty();
  }
}
