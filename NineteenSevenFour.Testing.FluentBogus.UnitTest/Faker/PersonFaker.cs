using NineteenSevenFour.Testing.FluentBogus.Extension;

namespace NineteenSevenFour.Testing.FluentBogus.UnitTest.Faker;

public class PersonFaker : AutoFaker<PersonModel>
{
  public PersonFaker(int Id) : this() {
    RuleFor(o => o.Id, () => Id);
  }

  public PersonFaker() : base()
  {
    StrictMode(true);

    RuleFor(o => o.Id, f => f.Random.Int(1));
    RuleFor(o => o.Name, f => f.Person.FirstName);
    RuleFor(o => o.Surname, f => f.Name.LastName().ToUpper());
    RuleFor(o => o.Age, f => f.Random.Int(0, 99));
    RuleFor(o => o.Birthday, f => f.Person.DateOfBirth);
    RuleFor(o => o.Type, (f, o) =>
    {
      return o.Age switch
      {
        int age when age <= 5 => PersonType.Infant,
        int age when age > 5 && age < 18 => PersonType.Child,
        _ => PersonType.Adult,
      };
    });
    RuleFor(o => o.Addresses, _ => new List<AddressModel>());
    RuleFor(o => o.Relatives, _ => new List<PersonRelativeModel>());

    FinishWith((f, o) =>
    {
      o.HasMany(parent => parent.Addresses)
        .HasKey(parent => parent.Id)
        .WithOne(child => child.Person)
        .WithForeignKey(child => child.PersonId)
        .Apply();
    });
  }
}
