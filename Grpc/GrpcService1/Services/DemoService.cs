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
}