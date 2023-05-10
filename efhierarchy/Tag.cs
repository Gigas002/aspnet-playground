public class Tag
{
    public int Id { get; set; }

    public Tag? Master { get; set; }

    public List<Tag> Slaves { get; set; } = new();
    
    public TagCategory Category { get; set; }
    
    public string Value { get; set; } = null!;
    
    public string Description { get; set; } = null!;

    public Tag(string value, string description, TagCategory category)
    {
        (Value, Description, Category) = (value, description, category);
    }

    public static IEnumerable<Tag> CreateTags()
    {
        var tag1 = new Tag("action", "action genre", TagCategory.Genre);
        var tag2 = new Tag("fantasy", "fantasy genre", TagCategory.Genre);
        var tag3 = new Tag("isekai", "fantasy isekai", TagCategory.Genre);
        var tag4 = new Tag("isekai2", "yet another isekai", TagCategory.Genre);
        var tag5 = new Tag("deeper_isekai", "very deep isekai", TagCategory.Genre);
    
        tag2.Slaves = new() { tag3, tag4 };
        tag3.Slaves.Add(tag5);
    
        // for EFCore same as
        // tag3.Master = tag2;
        // tag4.Master = tag2;
        // tag5.Master = tag3;

        return new List<Tag>() { tag1, tag2, tag3, tag4, tag5 };
    }

    public static IEnumerable<Tag> GetTagWithSlaves(IEnumerable<Tag> tags, string value)
    {
        var tag = tags.FirstOrDefault(t => t.Value == value);

        return GetAllTags(tag);
    }

    public static IEnumerable<Tag> GetAllTags(Tag tag)
    {
        yield return tag;

        if (tag.Slaves?.Count > 0)
        {
            foreach (var slave in tag.Slaves.SelectMany(s => GetAllTags(s)))
            {
                yield return slave;
            }
        }
    }
}
