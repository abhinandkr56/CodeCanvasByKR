using Application.BlogPost.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.BlogPost;

public class BlogPostController : ControllerBase
{
    private readonly ILogger<BlogPostController> _logger;
    private readonly IEndpointInstance _endpointInstance;

    public BlogPostController(ILogger<BlogPostController> logger, IEndpointInstance endpointInstance)
    {
        _logger = logger;
        _endpointInstance = endpointInstance;
    }
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateBlogPost(CreateBlogPostCommand createBlogPostCommand)
    {
        await _endpointInstance.Send(createBlogPostCommand)
            .ConfigureAwait(false);
        // var response = await _endpointInstance.Send<CreateBlogPostCommandResponse>(createBlogPostCommand,
        //     new RequestOptions { Timeout = TimeSpan.FromMinutes(5) });

        // Return the response back to the client
        return Ok();
    }
}