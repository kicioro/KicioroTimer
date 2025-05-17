using System.Text.RegularExpressions;
using BlazorWorker.WorkerCore;

namespace KicioroTimer.Logic;

public class CoreCountService
{
    public static readonly string Progress = $"Events.{nameof(CountService.Progress)}";
    public static readonly string ResultMessage = $"Methods.{nameof(CountService.Count)}.Result";

    private readonly CountService mathsService;
    private readonly IWorkerMessageService messageService;

    public CoreCountService(IWorkerMessageService messageService)
    {
        this.messageService = messageService;
        this.messageService.IncomingMessage += OnMessage;
        mathsService = new CountService();
        mathsService.Progress += (s, progress) => messageService.PostMessageAsync($"{Progress}:{progress.Value}");
    }

    private void OnMessage(object sender, string message)
    {
        if(message.StartsWith("stop"))
            mathsService.Stop();
        
        if (message.StartsWith(nameof(mathsService.Count)))
        {
            var messageParams = message.Substring(nameof(mathsService.Count).Length).Trim();
            var rx = new Regex(@"\((?<arg>[^\)]+)\)");
            var arg0 = rx.Match(messageParams).Groups["arg"].Value.Trim();
            var iterations = int.Parse(arg0);
            mathsService.Count(iterations).ContinueWith(t =>
                messageService.PostMessageAsync($"{ResultMessage}:{t.Result}"));
            return;
        }
    }
}