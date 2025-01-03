// <copyright file="AddressFaker.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Example.Domain.Faker;

using System.Diagnostics.CodeAnalysis;

using AutoBogus;

using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Relation;

/// <summary>
/// Defines a <see cref="AddressFaker"/> without a <see cref="FluentBogusRelation"/> configured.
/// </summary>
[ExcludeFromCodeCoverage]
public class AddressFaker : AutoFaker<AddressModel>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="AddressFaker"/> class.
  /// </summary>
  /// <param name="id">The ID of the <see cref="AddressModel"/>.</param>
  public AddressFaker(int? id)
  {
    this.StrictMode(true);

    this.RuleFor(o => o.Id, f => id ?? f.Random.Int(1));
    this.RuleFor(o => o.Number, f => f.Random.Int(1));
    this.RuleFor(o => o.Type, f => f.Random.Enum<AddressType>());
    this.RuleFor(o => o.Street, f => f.Address.StreetAddress());
    this.RuleFor(o => o.City, f => f.Address.City());
    this.RuleFor(o => o.State, f => f.Address.StateAbbr());
    this.RuleFor(o => o.PostalCode, f => f.Address.ZipCode());
    this.RuleFor(o => o.Country, f => f.Address.Country());

    // Foreign Key
    this.RuleFor(o => o.OwnerId, f => null);

    // Navigation property to One-to-One relation
    this.RuleFor(o => o.Owner, f => null);
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="AddressFaker"/> class.
  /// </summary>
  public AddressFaker()
    : this(null)
  {
  }
}
