using System;
using ClassLibrary;

namespace SqlServerConnection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Classe de conexão com o banco
            Console.WriteLine("Iniciou projeto...");
            Context Context = new Context();

            //Iniciar conexão com o banco
            Console.WriteLine("Iniciando conexão com banco...");
            Context.StartConnection();

            //Mocar dados no banco
            StartDataDB(Context);
        }

        /// <summary>
        /// Método para mocar dados no banco
        /// </summary>
        private static void StartDataDB(Context context)
        {
            //Mocando dados no banco do aluno 1
            Console.WriteLine("Mocando dados do aluno 1...");
            Student student1 = new Student();
            student1.Name = "Aluno 1";
            student1.Email = "aluno1@email.com";
            student1.Text = "Texto do aluno 1";
            student1.DateBirth = DateTime.Now;
            context.Add(student1);

            //Mocando dados no banco do aluno 2
            Console.WriteLine("Mocando dados do aluno 2...");
            Student student2 = new Student();
            student2.Name = "Aluno 2";
            student2.Email = "aluno2@email.com";
            student2.Text = "Texto do aluno 2";
            student2.DateBirth = DateTime.Now;
            context.Add(student2);

            //Mocando dados do curso 1
            Console.WriteLine("Mocando dados do curso 1...");
            Course course1 = new Course();
            course1.Name = "Curso 1";
            course1.Description = "Descrição do curso 1";
            context.Add(course1);

            //Mocando dados do curso 2
            Console.WriteLine("Mocando dados do curso 2...");
            Course course2 = new Course();
            course2.Name = "Curso 2";
            course2.Description = "Descrição do curso 2";
            context.Add(course2);

            //Enviando dados para o banco
            Console.WriteLine("Enviando dados para banco...");
            context.SaveChanges();

            //Mocando relacionamento: curso 1 com estudante 2
            Console.WriteLine("Mocando relacionamento: curso 1 com estudante 2...");
            StudentToCourse studentToCourse = new StudentToCourse();
            studentToCourse.CourseId = course1.CourseId;
            studentToCourse.StudentId = student2.StudentId;
            context.Add(studentToCourse);

            //Enviando dados para o banco
            Console.WriteLine("Enviando dados para banco...");
            context.SaveChanges();
        }
    }
}
