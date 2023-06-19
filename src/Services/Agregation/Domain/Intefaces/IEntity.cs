using System.ComponentModel.DataAnnotations;

namespace Agregation.Domain.Intefaces
{
    /// <summary>
    /// Contains an Id and indicates that this object is obtained from the database
    /// </summary>
    public interface IEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
