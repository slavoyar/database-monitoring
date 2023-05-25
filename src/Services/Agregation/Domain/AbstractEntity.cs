using System.ComponentModel.DataAnnotations;

namespace MIAUDataBase.DataBase
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
