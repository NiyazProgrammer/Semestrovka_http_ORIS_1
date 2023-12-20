namespace Server;

public interface IProductDAO
{
    void Create(String product);
    String Read(int id);
    void Update(String product);
    void Delete(String product);
}