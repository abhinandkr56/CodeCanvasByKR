namespace Domain;

public class Tag
{
    public string Name { get; private set; }

    public Tag(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Tag name cannot be empty.");
        }

        Name = name;
    }
}