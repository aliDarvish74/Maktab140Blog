namespace MaktabBlog.Business.Abstraction.Contracts.Results;

public class GeneralResult
{
    public GeneralResult(Guid id)
    {
        ResourceId = id;
    }

    public GeneralResult(string id)
    {
        var isParsed = Guid.TryParse(id, out var guid);
        if (!isParsed)
            throw new ArgumentException($"Invalid GUID: {id}");
        
        ResourceId = guid;
    }
    public Guid ResourceId { get; set; }
}