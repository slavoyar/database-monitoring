using Agregation.ViewModels.Abstracts;

namespace MIAUDataBase.Controllers.Abstracts
{
    public interface IAbstractController<TCreate, TEdit>
        where TCreate : ICreateModel
        where TEdit : IEditModel
    {
        public Task<IResult> GetById(Guid id);
        public Task<IResult> GetPaged(int page, int itemsPerPage);
        public Task<IResult> Create(TCreate createModel);
        public Task<IResult> CreateRange(IEnumerable<TCreate> createModels);
        public Task<IResult> Edit(TEdit updateModel);
        public Task<IResult> DeleteById(Guid id);

    }
}
