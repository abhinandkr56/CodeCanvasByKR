using System.Net;
using Application.Interfaces;
using Domain;
using Domain.BlogPost;
using Microsoft.Extensions.Logging;

namespace Application.BlogPost.Commands;

public class CreateBlogPostCommandHandler :  IHandleMessages<CreateBlogPostCommand>
{
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly ILogger<CreateBlogPostCommandHandler> _logger;
    public CreateBlogPostCommandHandler(IEventStoreRepository eventStoreRepository, ILogger<CreateBlogPostCommandHandler> logger)
    {
        _eventStoreRepository = eventStoreRepository;
        _logger = logger;
    }

    public async Task Handle(CreateBlogPostCommand message, IMessageHandlerContext context)
    {
        var id = Guid.NewGuid();
        _logger.LogInformation("Id generated for blog post, {id}", id);
        var streamName = _eventStoreRepository.GetEventStreamName(typeof(Domain.BlogPost.BlogPost), id);
        var blogPostCreatedEvent =
            new BlogPostEventCreated(id, message.Title, message.Content, new Author(message.Author),
                message.PublishedDate);
        await _eventStoreRepository.SaveEventAsync(streamName, blogPostCreatedEvent);

        await context.Reply(new CreateBlogPostCommandResponse
            { Id = id, Message = "Success", StatusCode = HttpStatusCode.OK });
        _logger.LogInformation("Blog post created successfully, {id}", id);

    }
}

public class CreateBlogPostCommandResponseHandler :  IHandleMessages<CreateBlogPostCommandResponse>
{
    private readonly ILogger<CreateBlogPostCommandResponse> _logger;

    public CreateBlogPostCommandResponseHandler(ILogger<CreateBlogPostCommandResponse> logger)
    {
        _logger = logger;
    }

    public Task Handle(CreateBlogPostCommandResponse message, IMessageHandlerContext context)
    {
        _logger.LogInformation($"Processing Blog post: {message.Id}");

        return Task.CompletedTask;
    }
}