using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Extension;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

public class FluentBogusBuilder_FakeTest
{
  [Fact]
  public void Should_Return_ABuilder_WhenCalling_Fake()
  {
    // Arrange

    // Act
    var builder = FluentBogusBuilder.Fake<PersonModel>();

    // Assert
    builder.Should()
           .NotBeNull().And
           .BeOfType<FluentBogusBuilder<PersonModel>>();
  }
}
