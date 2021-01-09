using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Client.Data.Extensions;
using Client.Model.Data;

namespace Client.Data.Mappings
{
    public class ClientMap : EntityTypeConfiguration<AClient>
    {
        public override void Map(EntityTypeBuilder<AClient> builder)
        {
			
				builder.HasKey(c => c.Id);

				builder.Property<int>(x => x.Id)
							.ValueGeneratedOnAdd();

				builder.Property(c => c.Name);
				builder.Property(c => c.LastName);
				builder.Property(c => c.DateBirth);
				builder.Property(c => c.Email);
				builder.Property(c => c.Password);			

		}
	}
}
