// <copyright file="PersonFaker.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Example.Domain.Faker;

using System;
using System.Diagnostics.CodeAnalysis;

using AutoBogus;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Relation;

/// <summary>
/// Defines a <see cref="PersonFaker"/> without a <see cref="FluentBogusRelation"/> configured.
/// </summary>
[ExcludeFromCodeCoverage]
public class PersonFaker : AutoFaker<PersonModel>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="PersonFaker"/> class.
  /// </summary>
  /// <param name="id">The ID of the <see cref="PersonModel"/>.</param>
  public PersonFaker(int? id)
  {
    StrictMode(true);

    RuleFor(o => o.Id, f => id ?? f.Random.Int(1));
    RuleFor(o => o.Name, f => f.Person.FirstName);
    RuleFor(o => o.Surname, f => f.Name.LastName().ToUpperInvariant());
    RuleFor(o => o.Age, f => f.Random.Int(0, 99));
    RuleFor(o => o.Type, (f, o) =>
    {
      return o.Age switch
      {
        <= 5 => PersonType.Infant,
        > 5 and < 18 => PersonType.Child,
        _ => PersonType.Adult,
      };
    });
    RuleFor(o => o.Birthday, (f, o) => DateTime.UtcNow.AddYears(-o.Age));

    // Navigation property to One-to-Many relation.
    RuleFor(o => o.Relatives, _ => null);

    // Navigation property to One-to-One relation.
    RuleFor(o => o.Address, _ => null);
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="PersonFaker"/> class.
  /// </summary>
  public PersonFaker()
    : this(null)
  {
  }
}
