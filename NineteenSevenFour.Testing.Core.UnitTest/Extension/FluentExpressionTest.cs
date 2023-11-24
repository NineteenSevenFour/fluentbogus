using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NineteenSevenFour.Testing.Core.Extension;
using NineteenSevenFour.Testing.Domain.UnitTest.Model;
using System;
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
            .NotBeNull().And
            .Be(nameof(PersonModel.Surname));
    }

    [Fact]
    public void MemberNameFor_Should_Throw_ArgumentException_When_Expression_IsNested()
    {
      // Arrange
      Expression<Func<PersonModel, object>> expression = (p) => p.Addresses.Count;

      // Act
#pragma warning disable CS8603 // Possible null reference return.
      var result = () => FluentExpression.MemberNameFor(expression);
#pragma warning restore CS8603 // Possible null reference return.

      // Assert
      var exception = Assert.Throws<ArgumentException>(result);
      exception.Should().NotBeNull();
      exception.Message.Should().Be($"Your expression 'Convert(p.Addresses.Count, Object)' cant be used. Nested accessors like 'o => o.NestedObject.Foo' at a parent level are not allowed. You should create a dedicated faker for NestedObject like new Faker<NestedObject>().RuleFor(o => o.Foo, ...) with its own rules that define how 'Foo' is generated."
      );
    }

    [Fact]
    public void MemberNameFor_Should_Throw_ArgumentException_When_Expression_HasInvalidForm()
    {
      // Arrange
      Expression<Func<object>> expression = () => null;

      // Act
#pragma warning disable CS8603 // Possible null reference return.
      var result = () => FluentExpression.MemberNameFor(expression);
#pragma warning restore CS8603 // Possible null reference return.

      // Assert
      var exception = Assert.Throws<ArgumentException>(result);
      exception.Should().NotBeNull();
      exception.Message.Should().Be($"Expression was not of the form 'x => x.Property or x => x.Field'.");
    }

    [Fact]
    public void EnsureMemberExists_Should_Throw_ArgumentException_When_PropertyNotExists()
    {
      // Arrange
      var wrongPropName = "SomeProp";
      Expression<Func<PersonModel, object>> expression = (p) => p.Addresses.Count;

      // Act
#pragma warning disable CS8603 // Possible null reference return.
      var result = () => FluentExpression.EnsureMemberExists<PersonModel>(wrongPropName);
#pragma warning restore CS8603 // Possible null reference return.

      // Assert
      var exception = Assert.Throws<ArgumentException>(result);
      exception.Should().NotBeNull();
      exception.Message.Should().Be($"The property or field {wrongPropName} was not found on NineteenSevenFour.Testing.Domain.UnitTest.Model.PersonModel. Can't create a rule for NineteenSevenFour.Testing.Domain.UnitTest.Model.PersonModel.SomeProp when SomeProp cannot be found. Try creating a custom IBinder for Faker<T> with the appropriate System.Reflection.BindingFlags that allows deeper reflection into NineteenSevenFour.Testing.Domain.UnitTest.Model.PersonModel.");
    }
  }
}
