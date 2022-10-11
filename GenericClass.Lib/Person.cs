namespace GenericClass.Lib;

public class Person
{
    #region Properties
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthday { get; set; }
    #endregion

    #region Constructors

    public Person(int id, string firstName, string lastName, DateOnly birthday)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Birthday = birthday;
    }
    #endregion
}