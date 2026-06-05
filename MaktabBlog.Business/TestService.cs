namespace MaktabBlog.Business;

public class TestService : ITestService
{
    private  int counter = 0;
    public void Test(int? number = null)
    {
        if (number is not null && number != counter)
            throw new ArgumentException("Invalid number");
        
        counter++;
        Console.WriteLine(counter);
    }

    public int GetNumber() => counter;
}