// <copyright file="FluentExpressionMemberNameFor.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Core.UnitTest;

using System;
using System.Linq.Expressions;

using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;

using Xunit;

public class FluentExpressionMemberNameFor
{
  [Fact]
  public void ShouldReturnAStringWhenExpressionIsValid()
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
  public void ShouldThrowArgumentExceptionWhenExpressionIsNested()
  {
    // Arrange
#pragma warning disable CS8602
    Expression<Func<PersonModel, object>> expression = (p) => p.Relatives.Count;
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
      .Be($"Your expression 'Convert(p.Relatives.Count, Object)' cant be used. Nested accessors like 'o => o.NestedObject.Foo' at a parent level are not allowed. You should create a dedicated faker for NestedObject like new Faker<NestedObject>().RuleFor(o => o.Foo, ...) with its own rules that define how 'Foo' is generated.");
  }

  [Fact]
  public void ShouldThrowArgumentExceptionWhenExpressionHasInvalidForm()
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
}
