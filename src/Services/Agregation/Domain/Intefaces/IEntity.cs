using System.ComponentModel.DataAnnotations;

namespace Agregation.Domain.Intefaces
{
    /// <summary>
    /// Содержит Id и указывает, что этот объект получается из бд
    /// </summary>
    public interface IEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
