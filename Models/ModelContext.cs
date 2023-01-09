using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FirstProject.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aboutu> Aboutus { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contactu> Contactus { get; set; }
        public virtual DbSet<Header> Headers { get; set; }
        public virtual DbSet<Home> Homes { get; set; }
        public virtual DbSet<Leavemessage> Leavemessages { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Testmonial> Testmonials { get; set; }

        public virtual DbSet<Visa> Visas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=JOR15_User99;PASSWORD=1234;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("JOR15_USER99")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Aboutu>(entity =>
            {
                entity.HasKey(e => e.Titleimage)
                    .HasName("SYS_C00268225");

                entity.ToTable("ABOUTUS");

                entity.Property(e => e.Titleimage)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLEIMAGE");

                entity.Property(e => e.Choseimage)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CHOSEIMAGE");

                entity.Property(e => e.Descriptionchose1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONCHOSE1");

                entity.Property(e => e.Descriptionchose2)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONCHOSE2");

                entity.Property(e => e.Descriptionchose3)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONCHOSE3");

                entity.Property(e => e.Descriptionchose4)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONCHOSE4");

                entity.Property(e => e.Descriptionchose5)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONCHOSE5");

                entity.Property(e => e.Descriptiontitle1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONTITLE1");

                entity.Property(e => e.Descriptiontitle2)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONTITLE2");

                entity.Property(e => e.Descriptiontitle3)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONTITLE3");

                entity.Property(e => e.Descriptiontitle4)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONTITLE4");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Title1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLE1");

                entity.Property(e => e.Title2)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLE2");

                entity.Property(e => e.Title3)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLE3");

                entity.Property(e => e.Title4)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLE4");

                entity.Property(e => e.Titlechose1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLECHOSE1");

                entity.Property(e => e.Titlechose2)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLECHOSE2");

                entity.Property(e => e.Titlechose3)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLECHOSE3");

                entity.Property(e => e.Titlechose4)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLECHOSE4");

                entity.Property(e => e.Titlechose5)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLECHOSE5");

                entity.Property(e => e.Username)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Aboutus)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("SYS_C00268227");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Aboutus)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("SYS_C00268226");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORY");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Categoryname)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORYNAME");
            });

            modelBuilder.Entity<Contactu>(entity =>
            {
                entity.HasKey(e => e.Mailtitle)
                    .HasName("SYS_C00268235");

                entity.ToTable("CONTACTUS");

                entity.Property(e => e.Mailtitle)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MAILTITLE");

                entity.Property(e => e.Call)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CALL");

                entity.Property(e => e.Descriptioncall)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONCALL");

                entity.Property(e => e.Descriptionmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONMAIL");

                entity.Property(e => e.Descriptionvisit)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONVISIT");

                entity.Property(e => e.Descriptionwork)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONWORK");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Username)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.Property(e => e.Visittitle)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("VISITTITLE");

                entity.Property(e => e.Work)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WORK");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Contactus)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("SYS_C00268237");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Contactus)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("SYS_C00268236");
            });

            modelBuilder.Entity<Header>(entity =>
            {
                entity.HasKey(e => e.Rights)
                    .HasName("SYS_C00268260");

                entity.ToTable("HEADER");

                entity.Property(e => e.Rights)
                    .HasMaxLength(155)
                    .IsUnicode(false)
                    .HasColumnName("RIGHTS");

                entity.Property(e => e.Contact1)
                    .IsRequired()
                    .HasMaxLength(155)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT1");

                entity.Property(e => e.Contact2)
                    .IsRequired()
                    .HasMaxLength(155)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT2");

                entity.Property(e => e.Contact3)
                    .IsRequired()
                    .HasMaxLength(155)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT3");

                entity.Property(e => e.Contact4)
                    .IsRequired()
                    .HasMaxLength(155)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT4");

                entity.Property(e => e.News)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NEWS");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Title1)
                    .IsRequired()
                    .HasMaxLength(155)
                    .IsUnicode(false)
                    .HasColumnName("TITLE1");

                entity.Property(e => e.Title2)
                    .IsRequired()
                    .HasMaxLength(155)
                    .IsUnicode(false)
                    .HasColumnName("TITLE2");

                entity.Property(e => e.Title3)
                    .IsRequired()
                    .HasMaxLength(155)
                    .IsUnicode(false)
                    .HasColumnName("TITLE3");

                entity.Property(e => e.Title4)
                    .IsRequired()
                    .HasMaxLength(155)
                    .IsUnicode(false)
                    .HasColumnName("TITLE4");

                entity.Property(e => e.Username)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Headers)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("SYS_C00268262");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Headers)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("SYS_C00268261");
            });

            modelBuilder.Entity<Home>(entity =>
            {
                entity.HasKey(e => e.Sliderimage)
                    .HasName("SYS_C00268197");

                entity.ToTable("HOME");

                entity.Property(e => e.Sliderimage)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SLIDERIMAGE");

                entity.Property(e => e.Descriptionservice1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONSERVICE1");

                entity.Property(e => e.Descriptionservice2)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONSERVICE2");

                entity.Property(e => e.Descriptionservice3)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONSERVICE3");

                entity.Property(e => e.Descriptionslider1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONSLIDER1");

                entity.Property(e => e.Descriptionslider2)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONSLIDER2");

                entity.Property(e => e.Descriptionslider3)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTIONSLIDER3");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Titleservice1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLESERVICE1");

                entity.Property(e => e.Titleservice2)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLESERVICE2");

                entity.Property(e => e.Titleservice3)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLESERVICE3");

                entity.Property(e => e.Titleslider1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLESLIDER1");

                entity.Property(e => e.Titleslider2)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLESLIDER2");

                entity.Property(e => e.Titleslider3)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TITLESLIDER3");

                entity.Property(e => e.Username)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.Property(e => e.Video)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("VIDEO");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Homes)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("SYS_C00268199");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Homes)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("SYS_C00268198");
            });

            modelBuilder.Entity<Leavemessage>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("SYS_C00268242");

                entity.ToTable("LEAVEMESSAGE");

                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SUBJECT");

                entity.Property(e => e.Username)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Leavemessages)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("SYS_C00268244");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Leavemessages)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("SYS_C00268243");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("SYS_C00268156");

                entity.ToTable("LOGIN");

                entity.Property(e => e.Username)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Phonenumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("SYS_C00268157");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Hallnumber)
                    .HasName("SYS_C00268166");

                entity.ToTable("RESERVATION");

                entity.Property(e => e.Hallnumber)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("HALLNUMBER");

                entity.Property(e => e.Category)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Day)
                    .HasColumnType("DATE")
                    .HasColumnName("DAY");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PLACE");

                entity.Property(e => e.Price)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Timefrom)
                    .HasColumnType("DATE")
                    .HasColumnName("TIMEFROM");

                entity.Property(e => e.Timeto)
                    .HasColumnType("DATE")
                    .HasColumnName("TIMETO");

                entity.Property(e => e.Username)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("SYS_C00271075");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("SYS_C00268167");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Rolename)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<Testmonial>(entity =>
            {
                entity.ToTable("TESTMONIAL");

                entity.Property(e => e.Testmonialid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TESTMONIALID");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Username)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Testmonials)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("SYS_C00268250");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Testmonials)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("SYS_C00268249");
            });

            modelBuilder.Entity<Visa>(entity =>
            {
                entity.HasKey(e => e.Cardnumber)
                    .HasName("SYS_C00268268");

                entity.ToTable("VISA");

                entity.Property(e => e.Cardnumber)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CARDNUMBER");

                entity.Property(e => e.Exp)
                    .HasColumnType("DATE")
                    .HasColumnName("EXP");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Pocket)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("POCKET");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Thrnum)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("THRNUM");

                entity.Property(e => e.Username)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Visas)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("SYS_C00268270");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Visas)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("SYS_C00268269");
            });

            modelBuilder.HasSequence("DEPT_ID");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
