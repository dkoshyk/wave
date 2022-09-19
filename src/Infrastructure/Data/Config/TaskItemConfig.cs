using ApplicationCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class TaskItemConfig : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("Tasks");

        builder.Property(e => e.Title)
            .IsRequired();

        builder
            .HasOne(x => x.Owner)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.OwnerId);
    }
}
