using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Extension;
using NineteenSevenFour.Testing.FluentBogus.UnitTest.Faker;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

public class FluentBogusBuilder_UseSeed
{
  [Fact]
  public void Should_StoreSeed_WhenCalled()
  {
    // Arrange
    var seed = 456;
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

    // Act
    builder.UseSeed(seed);

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.seed.Should()
                     .Be(seed);
  }
}
