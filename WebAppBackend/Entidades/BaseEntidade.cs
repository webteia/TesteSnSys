using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppBackend.Entidades
{
    public class BaseEntidade
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; set; }
    }
}
