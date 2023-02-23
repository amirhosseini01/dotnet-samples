namespace AutofacSample.Services;

public interface ITransientService
{
    string GetNewGUID();
}
public class TransientService: ITransientService
{
    public string GetNewGUID()
    {
        return Guid.NewGuid().ToString();
    }
}