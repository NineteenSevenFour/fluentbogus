// <copyright file="PersonFaker.cs" company="NineteenSevenFour">
// Copyright (c) NineteenSevenFour. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace NineteenSevenFour.Testing.Example.Domain.Faker
{
  using System.Diagnostics.CodeAnalysis;
  using AutoBogus;
  using NineteenSevenFour.Testing.Example.Domain.Model;

  [ExcludeFromCodeCoverage]
  public class PersonFaker : AutoFaker<PersonModel>
  {
    public PersonFaker(int id)
      : this()
    {
      this.RuleFor(o => o.Id, () => id);
    }

    public PersonFaker()
      : base()
    {
      this.StrictMode(true);

      this.RuleFor(o => o.Id, f => f.Random.Int(1));
      this.RuleFor(o => o.Name, f => f.Person.FirstName);
      this.RuleFor(o => o.Surname, f => f.Name.LastName().ToUpperInvariant());
      this.RuleFor(o => o.Age, f => f.Random.Int(0, 99));
      this.RuleFor(o => o.Birthday, f => f.Person.DateOfBirth);
      this.RuleFor(o => o.Type, (f, o) =>
      {
        return o.Age switch
        {
          int age when age <= 5 => PersonType.Infant,
          int age when age > 5 && age < 18 => PersonType.Child,
          _ => PersonType.Adult,
        };
      });
      this.RuleFor(o => o.Addresses, _ => []);
      this.RuleFor(o => o.Relatives, _ => []);

      // FinishWith((f, e) =>
      // {
      //  e.HasMany(p => p.Addresses)
      //    .HasKey(p => p.Id)
      //    .WithOne(a => a.Person)
      //    .WithForeignKey(a => a.PersonId)
      //    .Apply();
      // });
    }
  }
}
