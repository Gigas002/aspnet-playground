using (var db = new ApplicationContext())
{
    var tags = Tag.CreateTags();

    db.Tags.AddRange(tags);
    db.SaveChanges();
}

using (var db = new ApplicationContext())
{
    var tags = db.Tags.ToList();

    Console.WriteLine("Tags:");
    foreach (var tag in tags)
    {
        Console.WriteLine($"Tag: {tag.Value}");
        
        if (tag.Master is not null) Console.WriteLine($"Master: {tag.Master.Value}");

        if (tag.Slaves?.Count > 0)
        {
            Console.WriteLine("Slaves:");
            foreach (var slave in tag.Slaves)
            {
                Console.WriteLine($"- {slave.Value}");
            }
        }
    }

    var fantasyTags = Tag.GetTagWithSlaves(tags, "fantasy");
    foreach (var tag in fantasyTags)
        Console.WriteLine($"Fantasy tag: {tag.Value}");
}
