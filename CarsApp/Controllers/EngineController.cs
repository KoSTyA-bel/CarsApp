using Microsoft.AspNetCore.Mvc;
using CarsApp.Businesslogic.Interfaces;
using AutoMapper;
using CarsApp.Businesslogic.Entities;
using CarsApp.Models;

namespace CarsApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EngineController : ControllerBase
{
    private readonly IService<Engine> _engineService;

    public EngineController(IService<Engine> engineService)
    {
        _engineService = engineService ?? throw new ArgumentNullException(nameof(engineService));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var engines = await _engineService.GetAll();

        if (engines is null)
        {
            return NotFound();
        }

        if (!engines.Any())
        {
            return NoContent();
        }

        var mapper = new MapperConfiguration(config => config.CreateMap<Engine, EngineViewModel>()).CreateMapper();
        var viewModels = mapper.Map<IEnumerable<Engine>, IEnumerable<EngineViewModel>>(engines);

        return Ok(viewModels);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var engine = await _engineService.GetById(id);

        if (engine is null)
        {
            return NotFound();
        }

        var mapper = new MapperConfiguration(config => config.CreateMap<Engine, EngineViewModel>()).CreateMapper();
        var viewModel = mapper.Map<Engine, EngineViewModel>(engine);

        return Ok(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EngineViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var mapper = new MapperConfiguration(config => config.CreateMap<EngineViewModel, Engine>()).CreateMapper();
        var engine = mapper.Map<EngineViewModel, Engine>(model);
         
        return Ok(await _engineService.Create(engine));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EngineViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(model);
        }

        var mapper = new MapperConfiguration(config => config.CreateMap<EngineViewModel, Engine>()).CreateMapper();
        var engine = mapper.Map<EngineViewModel, Engine>(model);

        return Ok(await _engineService.Update(engine));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _engineService.Delete(id));
    }
}

