using FluentAssertions;

using NineteenSevenFour.Testing.Core.Extension;
using NineteenSevenFour.Testing.Example.Domain.Model;

using System.Linq.Expressions;

using Xunit;

namespace NineteenSevenFour.Testing.Core.UnitTest.Extension
{
  public class FluentExpressionTest
  {
    [Fact]
    public void MemberNameFor_Should_Return_A_String_When_Expression_IsValid()
    {

      // Arrange
      Expression<Func<PersonModel, object>> expression = (p) => p.Surname;

      // Act
      var result = FluentExpression.MemberNameFor(expression);

      // Assert
      result.Should()
            .NotBeNull()
            .And
            .Be(nameof(PersonModel.Surname));
    }

    [Fact]
    public void MemberNameFor_Should_Throw_ArgumentException_When_Expression_IsNested()
    {
      // Arrange
#pragma warning disable CS8602
      Expression<Func<PersonModel, object>> expression = (p) => p.Addresses.Count;
#pragma warning restore CS8602

      // Act
#pragma warning disable IDE0039 // Use local function
      var result = () => FluentExpression.MemberNameFor(expression);
#pragma warning restore IDE0039 // Use local function

      // Assert
      var exception = Assert.Throws<ArgumentException>(result);
      exception.Should()
               .NotBeNull();
      exception.Message
               .Should()
               .Be($"Your expression 'Convert(p.Addresses.Count, Object)' cant be used. Nested accessors like 'o => o.NestedObject.Foo' at a parent level are not allowed. You should create a dedicated faker for NestedObject like new Faker<NestedObject>().RuleFor(o => o.Foo, ...) with its own rules that define how 'Foo' is generated."
      );
    }

    [Fact]
    public void MemberNameFor_Should_Throw_ArgumentException_When_Expression_HasInvalidForm()
    {
      // Arrange
#pragma warning disable CS8603 // Possible null reference return.
      Expression<Func<object>> expression = () => null;
#pragma warning restore CS8603 // Possible null reference return.

      // Act
#pragma warning disable IDE0039 // Use local function
      var result = () => FluentExpression.MemberNameFor(expression);
#pragma warning restore IDE0039 // Use local function

      // Assert
      var exception = Assert.Throws<ArgumentException>(result);
      exception.Should()
               .NotBeNull();
      exception.Message.Should()
                       .Be($"Expression was not of the form 'x => x.Property or x => x.Field'.");
    }

    [Fact]
    public void EnsureMemberExists_Should_Throw_ArgumentException_When_PropertyNotExists()
    {
      // Arrange
      var fqdn = "NineteenSevenFour.Testing.Example.Domain.Model.PersonModel";
      var wrongPropName = "SomeProp";
#pragma warning disable CS8602
      Expression<Func<PersonModel, object>> expression = (p) => p.Addresses.Count;
#pragma warning restore CS8602

      // Act
#pragma warning disable IDE0039 // Use local function
      var result = () => FluentExpression.EnsureMemberExists<PersonModel>(wrongPropName);
#pragma warning restore IDE0039 // Use local function

      // Assert
      var exception = Assert.Throws<ArgumentException>(result);
      exception.Should()
               .NotBeNull();
      exception.Message.Should()
                       .Be($"The property or field {wrongPropName} was not found on {fqdn}. " +
                       $"Can't create a rule for {fqdn}.SomeProp when " +
                       $"SomeProp cannot be found. Try creating a custom IBinder for Faker<T> with the appropriate " +
                       $"System.Reflection.BindingFlags that allows deeper reflection into {fqdn}.");
    }
  }
}
