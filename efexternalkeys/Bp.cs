using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

// [Table("blog")]
public class Blog
{
    // [Column("id")]
    public int Id { get; set; }

    // [Column("name")]
    public string Name { get; set; }

    public IEnumerable<Post> Posts { get; set; }
}

public class Post
{
    public int Id { get; set; }

    public string Value { get; set; }

    public Blog Blog { get; set; }

    public IEnumerable<Tag> Tags { get; set; }
}

public class Tag
{
    public int Id { get; set; }

    public string Value { get; set; }

    public IEnumerable<Post> Posts { get; set; }
}
