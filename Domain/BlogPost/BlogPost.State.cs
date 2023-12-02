using SharedKernal;

namespace Domain.BlogPost;

public class BlogPostState : AggregateRootState
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public string Content { get; set; }

    public DateTime PublishedDate { get; set; }

    public Author Author { get; set; }

    public List<Tag> Tags { get; set; }

    public List<Comment> Comments { get; set; }
    
    public void When(BlogPostEventCreated @event)
    {
        Id = @event.Id;
        Title = @event.Title;
        Content = @event.Content;
        Author = @event.Author;
        PublishedDate = @event.PublishedDate;
    }
}