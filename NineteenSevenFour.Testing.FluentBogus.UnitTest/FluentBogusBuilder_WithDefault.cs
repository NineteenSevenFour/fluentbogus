using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Extension;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest
{
  public class FluentBogusBuilder_WithDefault
  {
    [Fact]
    public void Should_Return_ABuilder_WithDefaultAutoFaker_WhenCalled_WithoutArguments()
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
    public void Should_StoreArguments_WhenCalled_WithArguments()
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
  }
}
