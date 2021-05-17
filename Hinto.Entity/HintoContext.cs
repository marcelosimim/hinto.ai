using System;
using Hinto.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Hinto.Entity
{
    public partial class HintoContext : DbContext
    {
        public HintoContext()
        {
        }

        public HintoContext(DbContextOptions<HintoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artistum> Artista { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<ListaFavorito> ListaFavoritos { get; set; }
        public virtual DbSet<ListaFavoritosMidia> ListaFavoritosMidias { get; set; }
        public virtual DbSet<ListaInteresse> ListaInteresses { get; set; }
        public virtual DbSet<ListaInteresseMidia> ListaInteresseMidias { get; set; }
        public virtual DbSet<MidiaGenero> MidiaGeneros { get; set; }
        public virtual DbSet<MidiaProdutore> MidiaProdutores { get; set; }
        public virtual DbSet<Midium> Midia { get; set; }
        public virtual DbSet<Produtore> Produtores { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=ec2-35-174-35-242.compute-1.amazonaws.com;Port=5432;Database=d8rcl63s5og0bo;User ID=ftcjdcjvnezkys;Password=a3b99b2c98e4f36d4c06b93c1b6b51f92ed88cf0a2e0c362b85a30a6abbf57de;SSL Mode=Require;Trust Server Certificate=true");
                //optionsBuilder.UseNpgsql("Server=localhost;Database=hinto;User ID=hinto;Password=hinto");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Portuguese_Brazil.1252");

            modelBuilder.Entity<Artistum>(entity =>
            {
                entity.ToTable("artista");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("nome");

                entity.Property(e => e.Profissao)
                    .HasMaxLength(75)
                    .HasColumnName("profissao");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.ToTable("genero");

                entity.HasIndex(e => e.Descricao, "uk_t52wxt385kqggv5pxlwqulmdg")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(112)
                    .HasColumnName("descricao");
            });

            modelBuilder.Entity<ListaFavorito>(entity =>
            {
                entity.ToTable("lista_favoritos");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DataAtualizacao).HasColumnName("data_atualizacao");

                entity.Property(e => e.DataCriacao).HasColumnName("data_criacao");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.ListaFavoritos)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("fk7ui1xac78umgtp6m91pw9qevt");
            });

            modelBuilder.Entity<ListaFavoritosMidia>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("lista_favoritos_midias");

                entity.HasIndex(e => e.MidiasId, "uk_831mi3ixh07fydiks93qglrdl")
                    .IsUnique();

                entity.Property(e => e.ListaFavoritosId).HasColumnName("lista_favoritos_id");

                entity.Property(e => e.MidiasId).HasColumnName("midias_id");

                entity.HasOne(d => d.Midias)
                    .WithOne()
                    .HasForeignKey<ListaFavoritosMidia>(d => d.MidiasId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkqfart2xschmc9r7w8tv7e7x7d");
            });

            modelBuilder.Entity<ListaInteresse>(entity =>
            {
                entity.ToTable("lista_interesse");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DataAtualizacao).HasColumnName("data_atualizacao");

                entity.Property(e => e.DataCriacao).HasColumnName("data_criacao");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.ListaInteresses)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("fkgemfurrspbf85eypnetgoit03");
            });

            modelBuilder.Entity<ListaInteresseMidia>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("lista_interesse_midias");

                entity.HasIndex(e => e.MidiasId, "uk_sw9xpyvd3ue86j3c5s9baqjby")
                    .IsUnique();

                entity.Property(e => e.ListaInteresseId).HasColumnName("lista_interesse_id");

                entity.Property(e => e.MidiasId).HasColumnName("midias_id");

                entity.HasOne(d => d.ListaInteresse)
                    .WithMany()
                    .HasForeignKey(d => d.ListaInteresseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk4luu79xrqn5apqto75f264h13");

                entity.HasOne(d => d.Midias)
                    .WithOne()
                    .HasForeignKey<ListaInteresseMidia>(d => d.MidiasId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk7x178jrw0s8p7ph50kftp6i1i");
            });

            modelBuilder.Entity<MidiaGenero>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("midia_generos");

                entity.Property(e => e.GenerosId).HasColumnName("generos_id");

                entity.Property(e => e.MidiaId).HasColumnName("midia_id");

                entity.HasOne(d => d.Generos)
                    .WithMany()
                    .HasForeignKey(d => d.GenerosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkrxplkmvytyskbjdod49yy39vi");

                entity.HasOne(d => d.Midia)
                    .WithMany()
                    .HasForeignKey(d => d.MidiaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkf3xgubqeroq6owfyo75rledre");
            });

            modelBuilder.Entity<MidiaProdutore>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("midia_produtores");

                entity.Property(e => e.MidiaId).HasColumnName("midia_id");

                entity.Property(e => e.ProdutoresId).HasColumnName("produtores_id");

                entity.HasOne(d => d.Midia)
                    .WithMany()
                    .HasForeignKey(d => d.MidiaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkc12c4casnbjo88w7csr3s0vr5");

                entity.HasOne(d => d.Produtores)
                    .WithMany()
                    .HasForeignKey(d => d.ProdutoresId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkoxe6un3shromfy0enovuxlt7f");
            });

            modelBuilder.Entity<Midium>(entity =>
            {
                entity.ToTable("midia");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Afinidade).HasColumnName("afinidade");

                entity.Property(e => e.DataLancamento).HasColumnName("data_lancamento");

                entity.Property(e => e.ImagemUrl)
                    .HasMaxLength(255)
                    .HasColumnName("imagem_url");

                entity.Property(e => e.Sinopse)
                    .HasMaxLength(1440)
                    .HasColumnName("sinopse");

                entity.Property(e => e.Tipo).HasColumnName("tipo");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(125)
                    .HasColumnName("titulo");
            });

            modelBuilder.Entity<Produtore>(entity =>
            {
                entity.ToTable("produtores");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.HasIndex(e => e.Email, "uk_5171l57faosmj8myawaucatdw")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Ativo).HasColumnName("ativo");

                entity.Property(e => e.DataCriacao).HasColumnName("data_criacao");

                entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.NomeUsuario)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("nome_usuario");

                entity.Property(e => e.Perfil).HasColumnName("perfil");

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("senha");

                entity.Property(e => e.Sexo).HasColumnName("sexo");

                entity.Property(e => e.UltimoAcesso).HasColumnName("ultimo_acesso");
            });

            modelBuilder.HasSequence("hibernate_sequence");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
