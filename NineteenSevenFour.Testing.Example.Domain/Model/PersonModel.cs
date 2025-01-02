// <copyright file="PersonModel.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace NineteenSevenFour.Testing.Example.Domain.Model;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Defines a person.
/// </summary>
[ExcludeFromCodeCoverage]
public class PersonModel
{
  /// <summary>
  /// Gets or sets the ID.
  /// </summary>
  public int? Id { get; set; }

  /// <summary>
  /// Gets or sets the person type.
  /// </summary>
  public PersonType Type { get; set; }

  /// <summary>
  /// Gets or sets the name.
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  /// Gets or sets the surname.
  /// </summary>
  public string Surname { get; set; }

  /// <summary>
  /// Gets or sets the age.
  /// </summary>
  public int Age { get; set; }

  /// <summary>
  /// Gets or sets the birthday.
  /// </summary>
  public DateTime Birthday { get; set; }

  /// <summary>
  /// Gets or sets the addresses.
  /// </summary>
  public AddressModel? Address { get; set; }

  /// <summary>
  /// Gets or sets the Relatives.
  /// </summary>
  public ICollection<PersonRelativeModel>? Relatives { get; set; }
}
