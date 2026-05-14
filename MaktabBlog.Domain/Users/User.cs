using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    [Column(TypeName =  "nvarchar(51)")]
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string NationalId { get; private set; }
    public int? Age { get; private set; }

    public void UpdateUserInfo(string firstName, string lastName, string nationalId, int? age = null)
    {
        FirstName = firstName;
        LastName = lastName;
        NationalId = nationalId;
        Age = age ?? Age;
        Validate();
        ModifiedAt = DateTime.UtcNow;
    }
    
    public override void Validate()
    {
    }
}