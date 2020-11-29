using Microsoft.EntityFrameworkCore;
using System;

namespace ClassLibrary
{
    public class Context : DbContext
    {
        //Atributos ligados ao banco de dados
        public DbSet<Course> Course { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentToCourse> StudentToCourse { get; set; }

        /// <summary>
        /// Inicia a criação do banco se não existir o banco
        /// </summary>
        public void StartConnection()
        {
            this.Database.EnsureCreated();
        }

        /// <summary>
        ///String de conexão
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Conexão utilizando sql server
            optionsBuilder.UseOracle(
                //Usuario da conexão (no oracle o usuario inicia com C##)
                "User Id=NOME_USUARIO;" +
                //Senha do banco
                "Password=SENHA_USUARIO;" +
                //Nome do banco de dados
                "Data Source=localhost;");
        }

        /// <summary>
        /// Iniciar Configurações das tabelas
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigDbCourse(modelBuilder);
            ConfigDbStudent(modelBuilder);
            ConfigDbStudentToCourse(modelBuilder);
        }

        /// <summary>
        /// Configurações da tabela Course
        /// </summary>
        public void ConfigDbCourse(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(etd =>
            {
                //Nome da tabela
                etd.ToTable("tbCourse");
                //Nome da coluna de id
                etd.HasKey(p => p.CourseId).HasName("CourseId");
                //Definição do tipo do campo e o autoincremento
                etd.Property(p => p.CourseId).HasColumnType("int").ValueGeneratedOnAdd();
                //Definição de campo not null e tamanho maximo
                etd.Property(p => p.Name).IsRequired().HasMaxLength(150);
                etd.Property(p => p.Description).IsRequired().HasMaxLength(250);
            });
        }

        /// <summary>
        /// Configurações da tabela Student
        /// </summary>
        public void ConfigDbStudent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(etd =>
            {
                //Nome da tabela
                etd.ToTable("tbStudent");
                //Nome da coluna de id
                etd.HasKey(p => p.StudentId).HasName("StudentId");
                //Definição do tipo do campo e o autoincremento
                etd.Property(p => p.StudentId).HasColumnType("int").ValueGeneratedOnAdd();
                //Definição de campo not null e tamanho maximo
                etd.Property(p => p.Name).IsRequired().HasMaxLength(150);
                etd.Property(p => p.Email).IsRequired().HasMaxLength(250);
                etd.Property(p => p.Text).IsRequired().HasMaxLength(250);
                etd.Property(p => p.DateBirth).IsRequired().HasColumnType("date");
            });
        }

        /// <summary>
        /// Configurações da tabela StudentToCourse
        /// </summary>
        public void ConfigDbStudentToCourse(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<StudentToCourse>(etd =>
            {
                //Nome da tabela
                etd.ToTable("tbStudentToCourse");
                //Nome da coluna de id
                etd.HasKey(p => p.StudentToCourseId).HasName("StudentToCourseId");
                //Definição do tipo do campo e o autoincremento
                etd.Property(p => p.StudentToCourseId).HasColumnType("int").ValueGeneratedOnAdd();
                //Definição de relacionamento 1 para muitos (estudante -> StudentsToCourses)
                etd.HasOne(p => p.Student).WithMany(p => p.StudentsToCourses).HasForeignKey(p => p.StudentId);
                //Definição de relacionamento 1 para muitos (curso -> StudentsToCourses)
                etd.HasOne(p => p.Course).WithMany(p => p.StudentsToCourses).HasForeignKey(p => p.CourseId);
                //Coluna not null
                etd.Property(p => p.CourseId).IsRequired();
                etd.Property(p => p.StudentId).IsRequired();
            });
        }
    }
}
