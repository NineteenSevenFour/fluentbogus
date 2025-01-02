// <copyright file="FluentExpressionTestEnsureMemberExists.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Core.UnitTest;

using System;
using System.Linq.Expressions;

using FluentAssertions;

using NineteenSevenFour.Testing.Example.Domain.Model;

using Xunit;

public class FluentExpressionTestEnsureMemberExists
{
  [Fact]
  public void ShouldThrowArgumentExceptionWhenPropertyNotExists()
  {
    // Arrange
    const string fqdn = "NineteenSevenFour.Testing.Example.Domain.Model.PersonModel";
    var wrongPropName = "SomeProp";
#pragma warning disable CS8602
    Expression<Func<PersonModel, object>> expression = (p) => p.Relatives.Count;
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
