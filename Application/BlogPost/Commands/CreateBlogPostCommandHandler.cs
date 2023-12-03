using System.Net;
using Application.Interfaces;
using Domain;
using Domain.BlogPost;

namespace Application.BlogPost.Commands;

public class CreateBlogPostCommandHandler :  IHandleMessages<CreateBlogPostCommand>
{
    private readonly IEventStoreRepository _eventStoreRepository;

    public CreateBlogPostCommandHandler(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }

    public async Task Handle(CreateBlogPostCommand message, IMessageHandlerContext context)
    {
        var id = Guid.NewGuid();
        var streamName = _eventStoreRepository.GetEventStreamName(typeof(Domain.BlogPost.BlogPost), id);
        var blogPostCreatedEvent =
            new BlogPostEventCreated(id, message.Title, message.Content, new Author(message.Author),
                message.PublishedDate);
        await _eventStoreRepository.SaveEventAsync(streamName, blogPostCreatedEvent);

        await context.Reply(new CreateBlogPostCommandResponse
            { Id = id, Message = "Success", StatusCode = HttpStatusCode.OK });
    }
}