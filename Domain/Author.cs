using SharedKernal;

namespace Domain;

public class Author 
{
    public Author(string name)
    {
        Name = name;
    }
    
    public string Name { get; set; }
}