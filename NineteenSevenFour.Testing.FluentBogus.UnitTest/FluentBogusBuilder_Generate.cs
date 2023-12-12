using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Faker;
using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Extension;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

public class FluentBogusBuilder_Generate
{
  [Fact]
  public void Should_ReturnAnInstanceOfEntity_WhenCalled_WithoutSkipOrConfigOrSeed()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker> ();
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;

    // Act
    var person = builder.Generate();

    // Assert
    person.Should().NotBeNull();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    typedBuilder.skipProperties.Should().HaveCount(0);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
  }

  [Fact]
  public void Should_ReturnAnInstanceOfEntity_WhenCalled_WithSkip()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;

    // Act
    var person = builder.Skip(p => p.Addresses).Generate();

    // Assert
    person.Should().NotBeNull();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    typedBuilder.skipProperties.Should().HaveCount(1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
  }

  [Fact]
  public void Should_ReturnAnInstanceOfEntity_WhenCalled_WithConfig()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;

    // Act
    var person = builder.UseConfig(b => b.WithTreeDepth(1)).Generate();

    // Assert
    person.Should().NotBeNull();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    typedBuilder.skipProperties.Should().HaveCount(0);
    typedBuilder.fakerConfigBuilder.Should().NotBeNull();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
  }

  [Fact]
  public void Should_ReturnAnArrayOfInstanceOfEntity_WhenCalled_WithoutSkipOrConfigOrSeed()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;

    // Act
    var persons = builder.Generate(2);

    // Assert
    persons.Should().NotBeNullOrEmpty().And.HaveCount(2);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    typedBuilder.skipProperties.Should().HaveCount(0);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
  }

  [Fact]
  public void Should_ReturnAnArrayOfInstanceOfEntity_WhenCalled_WithSkip()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;

    // Act
    var persons = builder.Skip(p => p.Addresses).Generate(2);

    // Assert
    persons.Should().NotBeNullOrEmpty().And.HaveCount(2);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    typedBuilder.skipProperties.Should().HaveCount(1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
  }

  [Fact]
  public void Should_ReturnAnArrayOfInstanceOfEntity_WhenCalled_WithConfig()
  {
    // Arrange
    var builder = FluentBogusBuilder.Fake<PersonModel>().With<PersonFaker>();
    var typedBuilder = builder as FluentBogusBuilder<PersonFaker, PersonModel>;

    // Act
    var persons = builder.UseConfig(b => b.WithTreeDepth(1)).Generate(2);

    // Assert
    persons.Should().NotBeNullOrEmpty().And.HaveCount(2);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    typedBuilder.skipProperties.Should().HaveCount(0);
    typedBuilder.fakerConfigBuilder.Should().NotBeNull();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
  }
}
