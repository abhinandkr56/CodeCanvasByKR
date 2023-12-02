using SharedKernal;

namespace Domain;

public class Comment : Entity
{
    public Comment(Guid id, string content) : base(id)
    {
        Content = content;
    }
    public string Content { get; set; }
}