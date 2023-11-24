using FluentAssertions;

using NineteenSevenFour.Testing.FluentBogus.Extension;
using NineteenSevenFour.Testing.FluentBogus.UnitTest.Faker;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest;

public class FluentBogusTest
{
  [Fact]
  public void Should_FakeOne_PersonModel_WithDefaultFaker()
  {
    // Arrange

    // Act
    var model = FluentBogusBuilder
                  .Fake<PersonModel>()
                  .WithDefault()
                  .Generate();

    // Assert
    model.Should().NotBeNull();
    model.Addresses.Should().NotBeNullOrEmpty();
    model.Relatives.Should().NotBeNullOrEmpty();
  }

  [Fact]
  public void Should_FakeOne_PersonModel_WithCustomFaker()
  {
    // Arrange

    // Act
    var model = FluentBogusBuilder
                  .Fake<PersonModel>()
                  .With<PersonFaker>()
                  .Generate();

    // Assert
    model.Should().NotBeNull();
    model.Addresses.Should().BeNullOrEmpty();
    model.Relatives.Should().BeNullOrEmpty();
  }

  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  [InlineData(3)]
  public void Should_FakeMany_PersonModel_WithDefaultFaker(int count)
  {
    // Arrange

    // Act
    var models = FluentBogusBuilder
                  .Fake<PersonModel>()
                  .WithDefault()
                  .Generate(count);

    // Assert
    models.Should()
          .NotBeNull()
          .And
          .HaveCount(count);

    foreach (var model in models)
    {
      model.Should().NotBeNull();
      model.Addresses.Should().NotBeNullOrEmpty(); // Neither PersonModel nor Default Faker initialize the collection
      model.Relatives.Should().NotBeNullOrEmpty(); // Neither PersonModel nor Default Faker initialize the collection
    }
  }

  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  [InlineData(3)]
  public void Should_FakeMany_PersonModel_WithCustomFaker(int count)
  {
    // Arrange

    // Act
    var models = FluentBogusBuilder
                  .Fake<PersonModel>()
                  .With<PersonFaker>()
                  .Skip(e => e.Addresses)
                  .Skip(e => e.Relatives)
                  .Generate(count);

    // Assert
    models.Should()
          .NotBeNull()
          .And
          .HaveCount(count);

    foreach (var model in models)
    {
      model.Should().NotBeNull();
      model.Addresses.Should().BeEmpty(); // The PersonFaker have a rule to initialize the collection
      model.Relatives.Should().BeEmpty(); // The PersonFaker have a rule to initialize the collection
    }
  }

  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  [InlineData(3)]
  public void Should_FakeMany_PersonModel_WithDefaultFaker_And_SkipNavigationProperties(int count)
  {
    // Arrange

    // Act
    var models = FluentBogusBuilder
                  .Fake<PersonModel>()
                  .WithDefault()
                  .Skip(e => e.Addresses)
                  .Skip(e => e.Relatives)
                  .Generate(count);

    // Assert
    models.Should()
          .NotBeNull()
          .And
          .HaveCount(count);

    foreach (var model in models)
    {
      model.Should().NotBeNull();
      model.Addresses.Should().BeNull(); // Neither PersonModel nor Default Faker initialize the collection
      model.Relatives.Should().BeNull(); // Neither PersonModel nor Default Faker initialize the collection
    }
  }

  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  [InlineData(3)]
  public void Should_FakeMany_PersonModel_WithDefaultFaker_And_SkipNavigationPropertiesByArrray(int count)
  {
    // Arrange

    // Act
    var models = FluentBogusBuilder
                  .Fake<PersonModel>()
                  .WithDefault()
                  .Skip(
                    e => e.Addresses,
                    e => e.Relatives)
                  .Generate(count);

    // Assert
    models.Should()
          .NotBeNull()
          .And
          .HaveCount(count);

    foreach (var model in models)
    {
      model.Should().NotBeNull();
      model.Addresses.Should().BeNull(); // Neither PersonModel nor Default Faker initialize the collection
      model.Relatives.Should().BeNull(); // Neither PersonModel nor Default Faker initialize the collection
    }
  }

  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  [InlineData(3)]
  public void Should_FakeMany_PersonModel_WithCustomFaker_And_SkipNavigationProperties(int count)
  {
    // Arrange

    // Act
    var models = FluentBogusBuilder
                  .Fake<PersonModel>()
                  .With<PersonFaker>()
                  .Skip(e => e.Addresses)
                  .Skip(e => e.Relatives)
                  .Generate(count);

    // Assert
    models.Should()
          .NotBeNull()
          .And
          .HaveCount(count);

    foreach (var model in models)
    {
      model.Should().NotBeNull();
      model.Addresses.Should().BeEmpty(); // The PersonFaker have a rule to initialize the collection
      model.Relatives.Should().BeEmpty(); // The PersonFaker have a rule to initialize the collection
    }
  }

  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  [InlineData(3)]
  public void Should_FakeMany_PersonModel_WithCustomFaker_And_SkipNavigationPropertiesByArrray(int count)
  {
    // Arrange

    // Act
    var models = FluentBogusBuilder
                  .Fake<PersonModel>()
                  .With<PersonFaker>()
                  .Skip(
                    e => e.Addresses,
                    e => e.Relatives)
                  .Generate(count);

    // Assert
    models.Should()
          .NotBeNull()
          .And
          .HaveCount(count);

    foreach (var model in models)
    {
      model.Should().NotBeNull();
      model.Addresses.Should().BeEmpty(); // The PersonFaker have a rule to initialize the collection
      model.Relatives.Should().BeEmpty(); // The PersonFaker have a rule to initialize the collection
    }
  }


  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  [InlineData(3)]
  public void Should_FakeMany_PersonModel_WithCustomFaker_And_ArgumentsOnCreation(int count)
  {
    // Arrange
    var ruleSet = "default"; // use , as separator to use multiple ruleset.

    // Act
    var models = FluentBogusBuilder
                  .Fake<PersonModel>()
                  .With<PersonFaker>()
                  .UseSeed(count)
                  .Skip(
                    e => e.Addresses,
                    e => e.Relatives)
                  .UseRuleSet(ruleSet)
                  .Generate(count);

    // Assert
    models.Should()
          .NotBeNull()
          .And
          .HaveCount(count);

    foreach (var model in models)
    {
      model.Should().NotBeNull();
      model.Addresses.Should().BeEmpty(); // The PersonFaker have a rule to initialize the collection
      model.Relatives.Should().BeEmpty(); // The PersonFaker have a rule to initialize the collection
    }
  }

  [Fact]
  public void Should_ThrowInvalidOperationException_OnDuplicateSkipCall()
  {
    // Arrange

    // Act
#pragma warning disable IDE0039 // Use local function
    var result = () => FluentBogusBuilder
                        .Fake<PersonModel>()
                        .With<PersonFaker>()
                        .Skip(
                          e => e.Addresses,
                          e => e.Addresses,
                          e => e.Relatives)
                        .Generate();
#pragma warning restore IDE0039 // Use local function

    // Assert
    var exception = Assert.Throws<InvalidOperationException>(result);
    exception.Should().NotBeNull();
    exception.Message.Should().Be($"The property Addresses for type PersonModel is already set to be skipped.");
  }
}
