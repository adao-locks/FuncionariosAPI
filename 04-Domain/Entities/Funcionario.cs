using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Funcionario
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cargo { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Salario { get; set; }

        [Required]
        public string Departamento { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
