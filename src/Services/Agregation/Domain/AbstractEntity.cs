namespace MIAUDataBase.DataBase
{
    /// <summary>
    /// Содержит Id и указывает, что этот объект получается из бд
    /// </summary>
    public abstract class AbstractEntity
    {
        public Guid Id { get; set; }
    }
}
