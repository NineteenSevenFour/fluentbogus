using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Extension;
using NineteenSevenFour.Testing.FluentBogus.UnitTest.Faker;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

public class FluentBogusBuilder_WithTest
{
  [Fact]
  public void Should_Return_ABuilder_WithDefaultAutoFaker_WhenCalling_WithDefault_WithoutArguments()
  {
    // Arrange

    // Act
    var builder = FluentBogusBuilder.Fake<PersonModel>().WithDefault();

    // Assert
    builder.Should()
           .NotBeNull().And
           .BeOfType<FluentBogusBuilder<AutoFaker<PersonModel>, PersonModel>>();
  }

  [Fact]
  public void Should_Return_ABuilder_WithCustomFaker_WhenCalling_With_WithoutArguments()
  {
    // Arrange

    // Act
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

    // Assert
    builder.Should()
           .NotBeNull().And
           .BeOfType<FluentBogusBuilder<PersonFaker, PersonModel>>();
  }

  [Fact]
  public void Should_StoreArguments_WhenCalling_WithDefault_WithArguments()
  {
    // Arrange
    var args = new[] { "argOne", "argTwo" };

    // Act
    var builder = FluentBogusBuilder.Fake<PersonModel>().WithDefault(args);

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<AutoFaker<PersonModel>, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.fakerArgs.Should()
                          .NotBeEmpty().And
                          .HaveCount(args.Length);
  }

  [Fact]
  public void Should_StoreArguments_WhenCalling_With_WithArguments()
  {
    // Arrange
    var args = new[] { "argOne", "argTwo" };

    // Act
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>(args);

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.fakerArgs.Should()
                          .NotBeEmpty().And
                          .HaveCount(args.Length);
  }
}
