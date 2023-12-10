using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Extension;
using NineteenSevenFour.Testing.FluentBogus.UnitTest.Faker;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

public class FluentBogusBuilder_UseConfig
{
  [Fact]
  public void Should_StoreFakerconfig_WhenCalled()
  {
    // Arrange
    Action<IAutoGenerateConfigBuilder> fakerConfig = (config) => config.WithTreeDepth(0);
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

    // Act
    builder.UseConfig(fakerConfig);

    // Assert
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
    Assert.NotNull(typedBuilder);
    typedBuilder.fakerConfigBuilder.Should()
                                   .NotBeNull().And
                                   .BeEquivalentTo(fakerConfig);
  }
}
