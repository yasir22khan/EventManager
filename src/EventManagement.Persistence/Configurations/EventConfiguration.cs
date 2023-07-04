using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventManagement.Domain.Entities;
using EventManagement.Persistence.Constants;

namespace EventManagement.Persistence.Configurations {
    internal sealed class EventConfiguration : IEntityTypeConfiguration<Event> {
        public void Configure(EntityTypeBuilder<Event> builder) {
            _ = builder
                .ToTable("events");
            
            _ = builder
            .HasIndex(x => new { x.Title })
            .IsUnique(true);
        }
    }
}