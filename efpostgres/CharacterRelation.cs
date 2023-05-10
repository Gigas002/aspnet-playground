using System;
using System.Collections.Generic;

namespace efpostgres;

public partial class CharacterRelation
{
    public long OriginCharacterId { get; set; }

    public long RelatedCharacterId { get; set; }

    public int RelationType { get; set; }

    public virtual Character OriginCharacter { get; set; } = null!;

    public virtual Character RelatedCharacter { get; set; } = null!;
}
