// <copyright file="PersonType.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Example.Domain.Model;

/// <summary>
/// Defines the type of Person.
/// </summary>
public enum PersonType
{
  /// <summary>
  /// Person age over 18.
  /// </summary>
  Adult,

  /// <summary>
  /// Child age between 5 and 18.
  /// </summary>
  Child,

  /// <summary>
  /// Infant age under 5.
  /// </summary>
  Infant,
}
