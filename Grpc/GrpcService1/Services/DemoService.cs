using Grpc.Core;

namespace GrpcService1.Services;

public class DemoService : Demo.DemoBase
{
    private readonly ILogger<DemoService> _logger;

    public DemoService(ILogger<DemoService> logger)
    {
        _logger = logger;
    }

    public override Task<DemoResponse> SayDemo(DemoRequest request, ServerCallContext context)
    {
        return Task.FromResult(new DemoResponse
        {
            Message = "Demo " + request.Name
        });
    }

    public override async Task DemoManyTimes(DemoManyTimeRequest request, IServerStreamWriter<DemoManyTimeResponse> responseStream, ServerCallContext context)
    {
        var res = String.Format("demo hi {0}", request.DemoReq.Name);
        foreach (var i in Enumerable.Range(1, 10))
        {
            await responseStream.WriteAsync(new DemoManyTimeResponse
            {
                Result = i.ToString()
            });
        }
    }

    public override async Task<LongDemoResponse> LongDemo(IAsyncStreamReader<LongDemoRequest> requestStream, ServerCallContext context)
    {
        string result = "";
        while (await requestStream.MoveNext())
        {
            result += $"Hello {requestStream.Current.DemoReq.Name}";
        }

        return new LongDemoResponse()
        {
            Result = result
        };
    }

    public override async Task DemoEveryOne(IAsyncStreamReader<DemoEveryOneRequest> requestStream, IServerStreamWriter<DemoEveryOneResponse> responseStream, ServerCallContext context)
    {
        while (await requestStream.MoveNext())
        {
            var result = $"Hi {requestStream.Current.DemoReq.Name}";
            Console.WriteLine("Received:" + result);
            await responseStream.WriteAsync(new DemoEveryOneResponse { Result = result });
        }

    }
}