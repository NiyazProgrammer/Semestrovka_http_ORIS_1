namespace Server;

public class SqlProductDao : IProductDAO
{
    public void Create(string product)
    {
        // Логика создания нового продукта в базе данных
    }

    public string Read(int id)
    {
        // Логика чтения продукта из базы данных по идентификатору
        return "";
    }

    public void Update(string product)
    {
        // Логика обновления информации о продукте в базе данных
    }

    public void Delete(string product)
    {
        // Логика удаления продукта из базы данных
    }
}