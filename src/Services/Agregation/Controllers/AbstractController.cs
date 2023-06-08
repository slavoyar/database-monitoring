using Agregation.Infrastructure.Services.DTO;
using Agregation.ViewModels.Abstracts;
using AutoMapper;
using MIAUDataBase.Controllers.Abstracts;
using MIAUDataBase.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace MIAUDataBase.Controllers
{
    [Route("[controller]")]
    public abstract class AbstractController<TCreate, TEdit, TViewModel, TDto>
        : Controller, IAbstractController<TCreate, TEdit>
        where TCreate : ICreateModel
        where TEdit : IEditModel
        where TViewModel : IViewModel
        where TDto : AbstractDto

    {
        protected readonly ISetService<TDto> setService;
        protected readonly IMapper mapper;
        public AbstractController(ISetService<TDto> setService, IMapper mapper)
        {
            this.setService = setService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IResult> Create(TCreate createModel)
        {
            var dto = mapper.Map<TDto>(createModel);
            var retObj = await setService.AddAsync(dto);
            var retModel = mapper.Map<TViewModel>(retObj);
            return Results.Created("Not uri", retModel);
        }

        [HttpPost("list")]
        public async Task<IResult> CreateRange(IEnumerable<TCreate> createModels) 
        {
            var dtos = mapper.Map<ICollection<TDto>>(createModels);
            await setService.AddRangeAsync(dtos);
            return Results.Created("Not uri", null);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteById(Guid id)
        {
            var result = await setService.TryDeleteAsync(id);
            return result ? Results.Ok() : Results.NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById(Guid id)
        {
            var dto = await setService.GetAsync(id);
            if (dto == null)
            {
                return Results.NotFound();
            }
            var view = mapper.Map<TViewModel>(dto);
            return Results.Ok(view);
        }

        [HttpGet("list/{page}/{itemsPerPage}")]
        public async Task<IResult> GetPaged(int page, int itemsPerPage)
        {
            if (page <= 0 || itemsPerPage <= 0) return Results.ValidationProblem(new Dictionary<string, string[]>() {
                    { "page or items per page less or equal then 0" , new string[]{ "Enter correct numbers" }  },
                }); 
            var dtoPage = await setService.GetPagedAsync(page, itemsPerPage);
            var viewModelPage = mapper.Map<ICollection<TViewModel>>(dtoPage);
            return Results.Ok(viewModelPage);
        }

        [HttpPut("{id}")]
        public async Task<IResult> Edit(TEdit updateModel)
        {
            var dto = mapper.Map<TDto>(updateModel);
            var result = await setService.TryUpdateAsync(dto);
            if (result)
                return Results.Ok();
            else
                return Results.NotFound();
        }
    }
}
