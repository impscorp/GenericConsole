namespace GenericClass.Lib;

public class Person
{
    #region Properties
    public int PersonID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    #endregion

    #region Constructors

    public Person(int personId, string firstName, string lastName, DateTime birthday)
    {
        PersonID = personId;
        FirstName = firstName;
        LastName = lastName;
        Birthday = birthday;
    }
    #endregion
}