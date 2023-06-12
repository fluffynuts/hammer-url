using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading;
using hammer_url;
using PeanutButter.EasyArgs;

var opts = args.ParseTo<IOptions>();
var semaphore = new SemaphoreSlim(opts.Concurrency, opts.Concurrency);
var threads = new ConcurrentBag<Thread>();
var queue = new ConcurrentQueue<string>();
var client = new HttpClient();
var quiet = opts.Quiet;

var queueThread = new Thread(() =>
{
    var url = opts.Url;
    while (true)
    {
        while (queue.Count < opts.Concurrency)
        {
            queue.Enqueue(url);
        }
        Thread.Sleep(50);
    }
});
queueThread.Start();

for (var i = 0; i < opts.Concurrency; i++)
{
    var t = new Thread(TryDownloadQueuedUrl);
    t.Start();
    threads.Add(t);
}

async void TryDownloadQueuedUrl()
{
    while (true)
    {
        if (queue.TryDequeue(out var url))
        {
            await client.GetAsync(url);
            if (!quiet)
            {
                Console.Out.Write(".");
            }
        }
    }
}
