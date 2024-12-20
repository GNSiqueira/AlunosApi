using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlunosApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AlunosApi.Context
{
    public class AppDbContext: IdentityDbContext<IdentityUser>    
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<Aluno> Alunos {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno{
                    Id = 1,
                    Nome = "Maria Penha", 
                    Email = "mariapenha@yahoo.com",
                    Idade = 22
                },
                new Aluno{
                    Id = 2,
                    Nome = "Manuel Bueno", 
                    Email = "manuelbueno@yahoo.com",
                    Idade = 22
                }

            );
        }

    }
}