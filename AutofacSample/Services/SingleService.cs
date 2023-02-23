namespace AutofacSample.Services;

public interface ISingleService
{
    string GetNewGUID();
}
public class SingleService: ISingleService
{
    public string GetNewGUID()
    {
        return Guid.NewGuid().ToString();
    }
}