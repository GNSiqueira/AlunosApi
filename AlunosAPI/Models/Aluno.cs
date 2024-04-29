using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Models
{
    [Table("Alunos")]
    public class Aluno
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        [StringLength(80)]
        public required string Nome { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }   
        [Required]
        public int Idade { get; set; }  
    }
}