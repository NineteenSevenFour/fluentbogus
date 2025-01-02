// <copyright file="PersonRelationType.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Example.Domain.Model;

/// <summary>
/// Defines the type of relation between persons.
/// </summary>
public enum PersonRelationType
{
  /// <summary>
  /// Grand parent.
  /// </summary>
  GrandParent,

  /// <summary>
  /// Father.
  /// </summary>
  Father,

  /// <summary>
  /// Mother.
  /// </summary>
  Mother,

  /// <summary>
  /// Son.
  /// </summary>
  Son,

  /// <summary>
  /// Daughter.
  /// </summary>
  Daughter,

  /// <summary>
  /// Sibling.
  /// </summary>
  Sibling,

  /// <summary>
  /// Aunt.
  /// </summary>
  Aunt,

  /// <summary>
  /// Uncle.
  /// </summary>
  Uncle,

  /// <summary>
  /// Cousin.
  /// </summary>
  Cousin,
}
