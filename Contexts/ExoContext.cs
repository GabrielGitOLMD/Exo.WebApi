using Exo.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;


//https://youtu.be/iGL4cgwtrY0    aula





namespace Exo.WebApi.Context
{

    public class ExoContext : DbContext
    {

        public ExoContext()
        {

        }
        public ExoContext(DbContextOpions<ExoContext> opions) : base(options)
        {

        }

        protected override void OnConfiguring(DbContexteOptionsBuilder optionsBuilder)
        {


            if (!optionsBuilder.IsConfigured)
            {
                //essa string de conexao foi depende da sua maquina.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;"
                               + "Database=ExoApi;Trusted_Connection=True;");

                //Exemplo 1 de string de conexao:
                //user ID=sa; Password=admin;Server=localhost;Database=ExoApi;-
                // + Trusted_Connection=False;

                //Exemplo 2 de string de conexao:
                //Server=localhost\\SQLEXPRESS;Database=ExoApi;Trusted_Connection=True;

            }
        }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Usuario> Usuarios {get; set;}

    }

}