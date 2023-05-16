using MIAUDataBase.Controllers.Models.Abstracts;

namespace MIAUDataBase.Controllers.Models.ServerPatient
{
    public class ServerPatientCreateModel : ICreateModel
    {
        public string Name { get; set; } = null!;
        public string IdAddress { get; set; } = null!;

    }
}
