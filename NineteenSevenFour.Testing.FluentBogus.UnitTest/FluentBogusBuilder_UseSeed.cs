using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Extension;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest
{
  public class FluentBogusBuilderUseSeed
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
      typedBuilder.Seed.Should()
                       .Be(seed);
    }
  }
}
