using BackendTest.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest.Data.Configurations
{
    public class RankingConfiguration : IEntityTypeConfiguration<Ranking>
    {
        public void Configure(EntityTypeBuilder<Ranking> builder)
        {
            builder.HasKey(e => e.IdRanking)
                   .HasName("PK__Ranking__9B4828B382783EE9");

            builder.ToTable("Ranking");

            builder.Property(e => e.IdRanking).HasColumnName("Id_Ranking");

            builder.Property(e => e.LibroId).HasColumnName("Libro_Id");

            builder.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");

            builder.HasOne(d => d.Libro)
                .WithMany(p => p.Rankings)
                .HasForeignKey(d => d.LibroId)
                .HasConstraintName("FK__Ranking__Libro_I__3D5E1FD2");

            builder.HasOne(d => d.Usuario)
                .WithMany(p => p.Rankings)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ranking__Usuario__3E52440B");
        }
    }
}
