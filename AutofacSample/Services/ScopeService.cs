namespace AutofacSample.Services;

public interface IScopeService
{
    string GetNewGUID();
}
public class ScopeService: IScopeService
{
    public string GetNewGUID()
    {
        return Guid.NewGuid().ToString();
    }
}