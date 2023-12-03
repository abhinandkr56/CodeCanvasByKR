using System.Net;
using Domain;
using MediatR;

namespace Application.BlogPost.Commands;

public class CreateBlogPostCommand : IMessage
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public DateTime PublishedDate { get; set; }

    public string Author { get; set; }
}

public class CreateBlogPostCommandResponse : IMessage
{
    public Guid Id { get; set; }
    
    public string Message { get; set; }
    
    public HttpStatusCode StatusCode { get; set; }
}