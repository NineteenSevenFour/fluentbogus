// <copyright file="PersonRelativeFaker.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Example.Domain.Faker;

using System.Diagnostics.CodeAnalysis;

using AutoBogus;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Relation;

/// <summary>
/// Defines a <see cref="PersonRelativeFaker"/> without a <see cref="FluentBogusRelation"/> configured.
/// </summary>
[ExcludeFromCodeCoverage]
public class PersonRelativeFaker : AutoFaker<PersonRelativeModel>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="PersonRelativeFaker"/> class.
  /// </summary>
  /// <param name="id">The ID of the <see cref="PersonRelativeModel"/>.</param>
  public PersonRelativeFaker(int? id)
  {
    this.StrictMode(true);

    this.RuleFor(o => o.Id, f => id ?? f.Random.Int(1));
    this.RuleFor(o => o.Relation, f => f.Random.Enum<PersonRelationType>());

    // Foreign Key
    this.RuleFor(o => o.RelativeId, f => null);

    // Navigation property to One-to-One relation
    this.RuleFor(o => o.Relative, f => null);
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="PersonRelativeFaker"/> class.
  /// </summary>
  public PersonRelativeFaker()
    : this(null)
  {
  }
}
