// <copyright file="AddressModel.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace NineteenSevenFour.Testing.Example.Domain.Model;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Define the address.
/// </summary>
[ExcludeFromCodeCoverage]
public class AddressModel
{
  /// <summary>
  /// Gets or sets the ID.
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// Gets or sets the Type.
  /// </summary>
  public AddressType Type { get; set; }

  /// <summary>
  /// Gets or sets the Number.
  /// </summary>
  public int Number { get; set; }

  /// <summary>
  /// Gets or sets the Street.
  /// </summary>
  public string Street { get; set; }

  /// <summary>
  /// Gets or sets the City.
  /// </summary>
  public string City { get; set; }

  /// <summary>
  /// Gets or sets the State.
  /// </summary>
  public string State { get; set; }

  /// <summary>
  /// Gets or sets the PostalCode.
  /// </summary>
  public string PostalCode { get; set; }

  /// <summary>
  /// Gets or sets the Country.
  /// </summary>
  public string Country { get; set; }

  /// <summary>
  /// Gets or sets the OwnerId.
  /// </summary>
  public int? OwnerId { get; set; }

  /// <summary>
  /// Gets or sets the Owner.
  /// </summary>
  public virtual PersonModel? Owner { get; set; }
}
