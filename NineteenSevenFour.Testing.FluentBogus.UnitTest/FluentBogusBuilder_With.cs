using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Extension;
using NineteenSevenFour.Testing.FluentBogus.UnitTest.Faker;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

public class FluentBogusBuilder_With
{
  [Fact]
  public void Should_Return_ABuilder_WithCustomFaker_WhenCalled_WithoutArguments()
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
  public void Should_StoreArguments_WhenCalled_WithArguments()
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
