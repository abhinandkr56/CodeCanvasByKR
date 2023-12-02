using SharedKernal;

namespace Domain.BlogPost;

public class BlogPostEventCreated : IDomainEvent
{
    public Guid Id { get; }
    public string Title { get; }
    public string Content { get; }
    public Author Author { get; }
    public DateTime PublishedDate { get; }

    public BlogPostEventCreated(Guid id, string title, string content, Author author, DateTime publishedDate)
    {
        Id = id;
        Title = title;
        Content = content;
        Author = author;
        PublishedDate = publishedDate;
    }
}