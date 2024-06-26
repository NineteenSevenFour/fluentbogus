using NineteenSevenFour.Testing.Example.Domain.Model;
using NineteenSevenFour.Testing.FluentBogus.Relation.Extension;

using System;
using System.Diagnostics.CodeAnalysis;

namespace NineteenSevenFour.Testing.Example.Domain.Faker
{
  [ExcludeFromCodeCoverage]
  public class PersonFakerWithRelation : PersonFaker
  {
    private readonly Action<Bogus.Faker, PersonModel> finishWith = (f, o) =>
      {
        o.HasMany(parent => parent.Addresses)
          .HasKey(parent => parent.Id)
          .WithOne(child => child.Person)
          .WithForeignKey(child => child.PersonId)
          .Apply();
      };

    public PersonFakerWithRelation(int Id) : base(Id)
    {
      FinishWith(finishWith);
    }

    public PersonFakerWithRelation() : base()
    {
      FinishWith(finishWith);
    }
  }
}
