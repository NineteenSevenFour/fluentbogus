using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Extension;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

public class FluentBogusBuilder_UseConfig
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
    typedBuilder.fakerConfigBuilder.Should()
                                   .NotBeNull().And
                                   .BeEquivalentTo(fakerConfig);
  }
}

public class FluentBogusBuilder_EnsureFakerInternal
{
  // Arrange
  // Act
  // Assert
}

public class FluentBogusBuilder_ConfigureFakerInternal
{
  // Arrange
  // Act
  // Assert
}

public class FluentBogusBuilder_GenerateOne
{
  // Validate EnsureFakerInternal is called

  // Validate ConfigureFakerInternal is called

}

public class FluentBogusBuilder_GenerateMany
{
  // Validate EnsureFakerInternal is called

  // Validate ConfigureFakerInternal is called

}
