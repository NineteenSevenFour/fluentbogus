// <copyright file="IFluentBogusRelation.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.FluentBogus.Relation;

/// <summary>
/// Provides the final action of the relation configuration.
/// </summary>
public interface IFluentBogusRelation
{
  /// <summary>
  /// Apply the relation configuration to the source and dependency.
  /// </summary>
  void Apply();
}
