// <copyright file="FluentExpression.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Core;

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

/// <summary>
/// Extension for fluent lambda expression.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class FluentExpression
{
  /// <summary>
  /// Members the name for.
  /// </summary>
  /// <typeparam name="T">The type of the member.</typeparam>
  /// <typeparam name="TProp">The type of the property.</typeparam>
  /// <param name="expression">The expression.</param>
  /// <returns>The name of the member as a string.</returns>
  public static string MemberNameFor<T, TProp>(Expression<Func<T, TProp>> expression)
  {
    var body = expression.Body;
    return body.AsMemberExpression().Member.Name;
  }

  /// <summary>
  /// Members the name for.
  /// </summary>
  /// <typeparam name="T">The type of the member.</typeparam>
  /// <param name="expression">The expression.</param>
  /// <returns>The name of the member as a string.</returns>
  public static string MemberNameFor<T>(Expression<Func<T, object>> expression)
  {
    var body = expression.Body;
    return body.AsMemberExpression().Member.Name;
  }

  /// <summary>
  /// Members the name for.
  /// </summary>
  /// <param name="expression">The expression.</param>
  /// <returns>The name of the member as a string.</returns>
  [ExcludeFromCodeCoverage]
  public static string MemberNameFor(Expression<Func<object>> expression)
  {
    var body = expression.Body;
    return body.AsMemberExpression().Member.Name;
  }

  /// <summary>
  /// Members the type for.
  /// </summary>
  /// <typeparam name="T">The type of the member.</typeparam>
  /// <typeparam name="TProp">The type of the property.</typeparam>
  /// <param name="expression">The expression.</param>
  /// <returns>The type of the member from the given expression.</returns>
  [ExcludeFromCodeCoverage]
  public static Type MemberTypeFor<T, TProp>(Expression<Func<T, TProp>> expression) => expression.Body.MemberTypeFor();

  /// <summary>
  /// Members the type for.
  /// </summary>
  /// <typeparam name="T">The type of the member.</typeparam>
  /// <param name="expression">The expression.</param>
  /// <returns>The type of the member from the given expression.</returns>
  [ExcludeFromCodeCoverage]
  public static Type MemberTypeFor<T>(Expression<Func<T, object>> expression) => expression.Body.MemberTypeFor();

  /// <summary>
  /// Members the type for.
  /// </summary>
  /// <param name="expression">The expression.</param>
  /// <returns>The type of the member from the given expression.</returns>
  [ExcludeFromCodeCoverage]
  public static Type MemberTypeFor(Expression<Func<object>> expression) => expression.Body.MemberTypeFor();

  /// <summary>
  /// Sets the field.
  /// </summary>
  /// <typeparam name="TDep">The type of the dep.</typeparam>
  /// <typeparam name="TKeyProp">The type of the key property.</typeparam>
  /// <param name="target">The target dependency.</param>
  /// <param name="propExpression">The property expression.</param>
  /// <param name="value">The value to set the dependency to.</param>
  [ExcludeFromCodeCoverage]
  public static void SetField<TDep, TKeyProp>(TDep target, Expression<Func<TDep, TKeyProp>> propExpression, TKeyProp value)
  {
    if (propExpression.Body is not MemberExpression memberSelectorExpression)
    {
      return;
    }

    var property = memberSelectorExpression.Member as PropertyInfo;
    property?.SetValue(target, value, null);
  }

  /// <summary>
  /// Ensures the member exists.
  /// </summary>
  /// <typeparam name="TEntity">The type of the entity.</typeparam>
  /// <param name="propNameOrField">The property name or field.</param>
  /// <param name="exceptionMessage">The exception message.</param>
  /// <exception cref="ArgumentException">The exception returned when the member doesn't exist.</exception>
  public static void EnsureMemberExists<TEntity>(string? propNameOrField, string? exceptionMessage = null)
    where TEntity : class
  {
    exceptionMessage ??=
      $"The property or field {propNameOrField} was not found on {typeof(TEntity)}. " +
      $"Can't create a rule for {typeof(TEntity)}.{propNameOrField} when {propNameOrField} " +
      $"cannot be found. Try creating a custom IBinder for Faker<T> with the appropriate " +
      $"System.Reflection.BindingFlags that allows deeper reflection into {typeof(TEntity)}.";
    var typeProperties = typeof(TEntity).GetProperties();

    if (typeProperties.All(p => p.Name != propNameOrField))
    {
      throw new ArgumentException(exceptionMessage);
    }
  }

  /// <summary>
  /// Members the type for.
  /// </summary>
  /// <param name="expression">The expression.</param>
  /// <returns>The type of the member from the given expression.</returns>
  [ExcludeFromCodeCoverage]
  private static Type MemberTypeFor(this Expression expression)
  {
    var memberExp = expression.AsMemberExpression();
    return memberExp.Type.IsGenericType
      ? ((PropertyInfo)memberExp.Member).PropertyType.GetGenericArguments()[0]
      : memberExp.Type;
  }

  /// <summary>
  /// As the member expression.
  /// </summary>
  /// <param name="expression">The expression.</param>
  /// <returns>Return an <see cref="MemberExpression"/> from an <see cref="Expression"/>.</returns>
  /// <exception cref="ArgumentException">
  /// $"Your expression '{expressionString}' cant be used. Nested accessors like 'o => o.NestedObject.Foo' at " +
  ///          $"a parent level are not allowed. You should create a dedicated faker for " +
  ///          $"NestedObject like new Faker[NestedObject]().RuleFor(o => o.Foo, ...) with its own rules " +
  ///          $"that define how 'Foo' is generated.
  /// or
  /// Expression was not of the form 'x => x.Property or x => x.Field'.
  /// </exception>
  [ExcludeFromCodeCoverage]
  private static MemberExpression AsMemberExpression(this Expression expression)
  {
    var expressionString = expression.ToString();
    if (expressionString.IndexOf('.') != expressionString.LastIndexOf('.'))
    {
      throw new ArgumentException(
        $"Your expression '{expressionString}' cant be used. Nested accessors like 'o => o.NestedObject.Foo' at " +
        $"a parent level are not allowed. You should create a dedicated faker for " +
        $"NestedObject like new Faker<NestedObject>().RuleFor(o => o.Foo, ...) with its own rules " +
        $"that define how 'Foo' is generated.");
    }

    MemberExpression? memberExpression;

    if (expression is UnaryExpression unary)
    {
      // In this case the return type of the property was not object,
      // so .Net wrapped the expression inside of a unary Convert()
      // expression that casts it to type object. In this case, the
      // Operand of the Convert expression has the original expression.
      memberExpression = unary.Operand as MemberExpression;
    }
    else
    {
      // when the property is of type object the body itself is the
      // correct expression
      memberExpression = expression as MemberExpression;
    }

    if (memberExpression == null)
    {
      throw new ArgumentException(
        "Expression was not of the form 'x => x.Property or x => x.Field'.");
    }

    return memberExpression;
  }
}
