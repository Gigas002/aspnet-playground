using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("People").Property(p => p.Name).IsRequired();
        builder.Property(p => p.Id).HasColumnName("user_id");
 
    }
}
