using System;
using System.Collections.Generic;

namespace efpostgres;

public partial class Character
{
    public long Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FullName { get; set; }

    public string? AlternativeNames { get; set; }

    public string? Species { get; set; }

    public string? AdditionalDetails { get; set; }

    public string? Description { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Birthday { get; set; }

    public int? Age { get; set; }

    public string? Picture { get; set; }

    public bool? IsCosplay { get; set; }

    public string? Genitails { get; set; }

    public virtual ICollection<CharacterRelation> CharacterRelationOriginCharacters { get; set; } = new List<CharacterRelation>();

    public virtual ICollection<CharacterRelation> CharacterRelationRelatedCharacters { get; set; } = new List<CharacterRelation>();

    public virtual ICollection<Creation> Creations { get; set; } = new List<Creation>();
}
