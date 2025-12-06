using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Extension;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest
{
  public class FluentBogusBuilderUseConfig
  {
    [Fact]
    public void Should_StoreFakerconfig_WhenCalled()
    {
      // Arrange
#pragma warning disable IDE0039 // Use local function
      Action<IAutoGenerateConfigBuilder> fakerConfig = (config) => config.WithTreeDepth(0);
#pragma warning restore IDE0039 // Use local function
      var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();

      // Act
      builder.UseConfig(fakerConfig);

      // Assert
      var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;
      Assert.NotNull(typedBuilder);
      typedBuilder.FakerConfigBuilder.Should().NotBeNull().And.BeEquivalentTo(fakerConfig);
    }
  }
}
