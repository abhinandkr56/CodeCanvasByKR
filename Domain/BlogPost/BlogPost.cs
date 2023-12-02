using SharedKernal;

namespace Domain.BlogPost;

public class BlogPost : AggregateRoot
{
    public BlogPost(Guid id, string title, string content, DateTime publishedDate, Author author) : base(id)
    {
        Apply(new BlogPostEventCreated(id, title, content, author, publishedDate));
    }

    public string Title { get; set; }

    public string Content { get; set; }

    public DateTime PublishedDate { get; set; }

    public Author Author { get; set; }

    public List<Tag> Tags { get; set; }

    public List<Comment> Comments { get; set; }
    
}