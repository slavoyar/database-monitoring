using MIAUDataBase.DataBase;

namespace MIAUDataBase.Domain.Entities.NotUsedHere
{
    public class User : AbstractEntity
    {
        public string Name { get; set; }
        public User(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
