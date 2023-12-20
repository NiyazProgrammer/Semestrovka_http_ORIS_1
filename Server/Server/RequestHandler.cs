using System.Net;
using System.Text.Json;

namespace Server;

public class RequestHandler
{
    private readonly AppSettings _config = new AppSettings();
    public RequestHandler()
    {
    }

    //public Account body = new Account();

    #region GetFile
    public async Task HandleRequestAsync(HttpListenerContext context)
    {
        var uri = context.Request.Url;
        var filePath = GetFilePath(uri);
        
        try
        {
            var response = context.Response;
            var path = File.ReadAllBytes(filePath);
            
            response.ContentType = DictionaryContentType.GetContentType(filePath);
            
            GetFormDataAsync(context.Request);
            
            response.ContentLength64 = path.Length;
            
            using Stream output = response.OutputStream;

            await output.WriteAsync(path);
            await output.FlushAsync();
        }
        catch (FileNotFoundException)
        {
            Server404Page(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    private string filePath { get; set; }
    private string GetFilePath(Uri uri)
    {
        #region MyRegion
// try
        // {
        //     #region Моя прошлая реализация
        //     // /static/buttleNet
        //     var filePath = "";
        //     if (uri.AbsolutePath.StartsWith("/static/buttleNet/") && uri.AbsolutePath.EndsWith('/'))
        //     {
        //         filePath = uri.AbsolutePath.Substring(1, uri.AbsolutePath.Length - 1) + "index.html";
        //         Console.WriteLine(1);
        //         return filePath;
        //     }
        //     // /static/buttleNet/...
        //     else if (uri.AbsolutePath.StartsWith("/static/buttleNet/"))
        //     {
        //         filePath = uri.AbsolutePath.Substring(1);
        //         Console.WriteLine(2);
        //         Console.WriteLine(uri.AbsolutePath);
        //         return filePath;
        //     }
        //     else
        //     {
        //         // static/index.html
        //         filePath = _config.StaticFilesPath + "/buttleNet" + uri.AbsolutePath.Substring(7);
        //         Console.WriteLine("Else:" + uri.AbsolutePath);
        //         return filePath;
        //     }*/
        //     #endregion
        

        #endregion
        try
        {
            if (filePath == null)
            {
                filePath = "static/buttleNet/index.html";
            }
            else 
                filePath = "static/" + uri.AbsolutePath.Trim('/');
            Console.WriteLine(filePath);
            
            return filePath;
        }
        
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка пути запроса" + ex.Message);
            return null;
        }
    }
    #endregion
    
    public async Task<Account> GetFormDataAsync(HttpListenerRequest request)
    {
        if (request.HttpMethod == "POST")
        {
            using (var reader = new StreamReader(request.InputStream))
            {
                var requestBody = await reader.ReadToEndAsync();
                Console.WriteLine(requestBody);
                try
                {
                    var data = JsonSerializer.Deserialize<Account>(requestBody);
                    var email = new EmailSender();
                    //email.SendEmail(data.Email, data.Password);
                    Console.WriteLine($"Приняты данные: Email: {data.Email}, Password: {data.Password}");
                    return data;
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Ошибка десериализации JSON: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка отправки сообщения {ex}");
                }
            }
        }

        return null;
    }

    private void Server404Page(HttpListenerContext context)
    {
        var path = File.ReadAllBytes(Path.Combine(_config.StaticFilesPath, "page404.html"));
        var response = context.Response;
        
        response.ContentLength64 = path.Length;
        using Stream output = response.OutputStream;

        output.WriteAsync(path); 
        output.FlushAsync();
    }
}