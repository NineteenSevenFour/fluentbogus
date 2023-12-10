using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Extension;
using NineteenSevenFour.Testing.FluentBogus.UnitTest.Faker;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

public class FluentBogusBuilder_UseArg
{
  [Fact]
  public void Should_StoreFakerArg_WhenCalled()
  {
    // Arrange
    var args = new[] { "argOne", "argTwo" };
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

    // Act
    builder.UseArgs(args);

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.fakerArgs.Should()
                          .NotBeEmpty().And
                          .HaveCount(args.Length);
  }
}
