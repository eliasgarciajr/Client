using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Client.Data.Extensions;
using Client.Model.Data;

namespace Client.Data.Mappings
{
    public class PhoneMap : EntityTypeConfiguration<Phone>
    {
        public override void Map(EntityTypeBuilder<Phone> builder)
        {

			builder.HasKey(c => c.Id);

			builder.Property<int>(x => x.Id)
						.ValueGeneratedOnAdd();

			builder.Property(c => c.NumberPhone);
			builder.HasOne(x => x.Client)
					  .WithMany()
					  .HasForeignKey(x => x.ClientId);

		}
	}
}
