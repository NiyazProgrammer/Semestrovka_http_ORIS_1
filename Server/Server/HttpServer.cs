using System;
using System.Net;
using Server.Handler;

namespace Server;

public class HttpServer: IDisposable
{
    private static AppSettings _config = ConfigManager.GetConfig();

    private HttpListener _server = new HttpListener();
    //private RequestHandler _handler;
    private Handler.Handler _staticFileHandler = new StaticFileHandler();
    private CancellationTokenSource _tokenSource = new();

    public HttpServer()
    {
        _server.Prefixes.Add($"http://{_config.Address}:{_config.Port}/");
        //_handler = new RequestHandler();
    }

    public async Task StartAsync()
    {
        _server.Start();
        Console.WriteLine("Сервер успешно запущен");
        var token = _tokenSource.Token;
        Task.Run(async () => { await Lisenning(token); });
        Console.WriteLine("Запрос обработан");
    }

    private async Task Lisenning(CancellationToken token)
    {
        try
        {
            while (_server.IsListening)
            {
                token.ThrowIfCancellationRequested();
                
                var context = await _server.GetContextAsync();
                //_staticFileHandler.Successor = _controllerHandler;
                _staticFileHandler.HandleRequest(context);
              //  var post = new RequestHandler();
               // post.GetFormDataAsync(context.Request);
                // await _handler.HandleRequestAsync(context);
            }
        }
        catch (OperationCanceledException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            _server.Close();
            _tokenSource.Cancel();
            ((IDisposable)_server).Dispose();
            Console.WriteLine("Server has been stopped.");   
            ProcessStop();
        }
    }

    public void ProcessStop()
    {
        while (true)
        {
            Console.WriteLine("Для завершения работы сервера напишите \"stop\" ");
            string key = Console.ReadLine();
            if (key == "stop")
            {
                _tokenSource.Cancel();
                Console.WriteLine("Сервер завершил работу");
                break;
            }
            continue;
        }
    }

    public void Dispose()
    {
        
    }
}