namespace MaktabBlog.Business;

public interface ITestService
{
    void Test(int? number = null);
    int GetNumber();
    
}