namespace MaktabBlog.Domain.Users;

public class User : BaseEntity
{
    public User()
    {
        
    }
    public User(string firstName, string lastName, string nationalId, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        NationalId = nationalId;
        Age = age;
        Validate();
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalId { get; set; }
    public int Age { get; set; }
    
    public override void Validate()
    {
    }
}