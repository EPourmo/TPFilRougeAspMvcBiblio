using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using AspMvcBiblio.Entities;


namespace AspMvcBiblio.Data
{
    public class BiblioContext : DbContext
    {

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Theme> Themes => Set<Theme>();
        public DbSet<Keyword> KeyWords => Set<Keyword>();

        public DbSet<Reader> Readers => Set<Reader>();


        public BiblioContext(DbContextOptions<BiblioContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            //Book
            modelBuilder.Entity<Book>()
               .Property(b => b.ISBN)
               .IsRequired()
               .HasMaxLength(20);

            modelBuilder.Entity<Book>()
               .Property(b => b.CopiesNumber)
               .IsRequired();

            modelBuilder.Entity<Book>()
               .Property(b => b.ServiceDate)
               .HasDefaultValueSql("getdate()");


            //Theme		
            modelBuilder.Entity<Theme>()
                .Property(t => t.DomainName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Theme>()
                .Property(t => t.Description)
                .HasMaxLength(2000);


            //Keyword
            modelBuilder.Entity<Keyword>()
                .Property(k => k.Word)
                .IsRequired()
                .HasMaxLength(50);



            modelBuilder.Entity<Theme>().HasData(
                new Theme { Id = 1, DomainName = "Suspense ", Description = "Les livres de suspense sont des thrillers qui tiennent les lecteurs en haleine jusqu'à la fin. Ils ont souvent des intrigues complexes, des personnages intrigants et des rebondissements inattendus. \"Gone Girl\" de Gillian Flynn est un exemple de livre de suspense qui raconte l'histoire d'un mari qui devient le suspect principal dans la disparition de sa femme" },
                new Theme { Id = 2, DomainName = "Biographie ", Description = "Les biographies sont des livres qui racontent la vie d'une personne réelle. Ils peuvent se concentrer sur la vie entière de la personne ou sur une période spécifique de sa vie. Les biographies sont souvent utilisées pour en apprendre davantage sur des personnalités célèbres telles que des musiciens, des acteurs ou des politiciens. \"Steve Jobs\" de Walter Isaacson est un exemple de biographie bien connue." },
                new Theme { Id = 3, DomainName = "Fantasy", Description = "Les livres de fantasy sont des histoires qui se déroulent dans des mondes imaginaires remplis de magie et de créatures fantastiques. Ils peuvent se concentrer sur des aventures, des quêtes ou des batailles épiques. \"Le Seigneur des Anneaux\" de J.R.R. Tolkien est un exemple bien connu de livre de fantasy." },
                new Theme { Id = 4, DomainName = "Romance ", Description = "Les livres de romance sont des histoires d'amour passionnantes et souvent dramatiques. Ils peuvent se concentrer sur des relations amoureuses, des triangles amoureux et des obstacles à surmonter. \"Orgueil et Préjugés\" de Jane Austen est un exemple classique de livre de romance." },
                new Theme { Id = 5, DomainName = "Policier ", Description = "Les livres policiers sont des histoires qui se concentrent sur les enquêtes et les résolutions de crimes. Les auteurs de livres policiers doivent créer des intrigues compliquées et des personnages convaincants pour garder les lecteurs captivés. \"Le Silence des Agneaux\" de Thomas Harris est un exemple célèbre de livre policier." },
                new Theme { Id = 6, DomainName = "Autobiographie ", Description = "Les autobiographies sont des livres qui racontent la vie d'une personne écrite par elle-même. Ils peuvent se concentrer sur des moments clés de la vie de l'auteur, des leçons apprises ou des défis surmontés. \"Born a Crime\" de Trevor Noah est un exemple récent de livre autobiographique qui raconte l'histoire de l'humoriste et présentateur sud-africain." });

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    ISBN = "2-7654-1005-4",
                    CopiesNumber = 1,
                    ServiceDate = DateTime.Parse("2012-09-01"),
                    Title = "Orgueil et Préjugés",
                }
                );

            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    FirstName = "Jane",
                    LastName = "Austen"
                });

            modelBuilder.Entity<Reader>().HasData(
               new Reader
               {
                   Id = 1,
                   FirstName = "Ermane",
                   LastName = "Pourmohtasham",
                   Email= "Ermane.Pourmohtasham@mail.com",
                   Phone="0606060606"
               },
               new Reader
               {
                   Id = 2,
                   FirstName = "Jean",
                   LastName = "Valjean",
                   Email = "Jean.Valjean@mail.com",
                   Phone = "0676869616"
               }
               );


            modelBuilder.Entity<Keyword>().HasData(
                new Keyword { Id = 1, Word = "mariage" },
                new Keyword { Id = 2, Word = "meurtre" },
                new Keyword { Id = 3, Word = "été" },
                new Keyword { Id = 4, Word = "voyage" }
                );

            modelBuilder.Entity<Book>().HasMany(b => b.Authors).WithMany(b => b.Books).UsingEntity(j =>
                { j.HasData(
                new[]
                {
                    new {BooksId = 1, AuthorsId=1},

                    });
				});
            modelBuilder.Entity<Book>().HasMany(b => b.Themes).WithMany(b => b.Books).UsingEntity(j =>
            {
                j.HasData(
            new[]
            {
                    new {BooksId = 1, ThemesId=1},

                });
            });

            modelBuilder.Entity<Book>().HasMany(b => b.KeyWords).WithMany(b => b.Books).UsingEntity(j =>
            {
                j.HasData(
            new[]
            {
                    new {BooksId = 1, KeyWordsId=2},

                });
            });

        }
    }
}
