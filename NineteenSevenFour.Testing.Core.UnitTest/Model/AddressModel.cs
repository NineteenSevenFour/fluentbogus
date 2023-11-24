namespace NineteenSevenFour.Testing.Core.UnitTest.Model;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class AddressModel
{
  public int Id { get; set; }
  public AddressType Type { get; set; }
  public int Number { get; set; }
  public string Street { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string PostalCode { get; set; }
  public string Country { get; set; }
  public int PersonId { get; set; }
  public virtual PersonModel? Person { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
