using System;
using System.Collections.Generic;

namespace efpostgres;

public partial class CreationRelation
{
    public long OriginCreationId { get; set; }

    public long RelatedCreationId { get; set; }

    public int RelationType { get; set; }

    public virtual Creation OriginCreation { get; set; } = null!;

    public virtual Creation RelatedCreation { get; set; } = null!;
}
