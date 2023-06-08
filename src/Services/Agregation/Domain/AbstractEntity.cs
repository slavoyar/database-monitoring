using System.ComponentModel.DataAnnotations;

namespace Agregation.Domain
{
    /// <summary>
    /// Содержит Id и указывает, что этот объект получается из бд
    /// </summary>
    public abstract class AbstractEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
