using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendTest.Entities;

namespace BackendTest.Data.Configurations
{
    public class LibroConfiguration : IEntityTypeConfiguration<Libro>
    {
        public void Configure(EntityTypeBuilder<Libro> builder)
        {
            builder.HasKey(e => e.IdLibro)
                   .HasName("PK__Libro__FFFE46402636AD27");

            builder.ToTable("Libro");

            builder.Property(e => e.IdLibro).HasColumnName("Id_Libro");

            builder.Property(e => e.Autor)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.FechaPublicacion)
                .HasColumnType("date")
                .HasColumnName("Fecha_Publicacion");

            builder.Property(e => e.Titulo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");

            builder.HasOne(d => d.Usuario)
                .WithMany(p => p.Libros)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Libro__Usuario_I__3A81B327");
        }
    }
}
