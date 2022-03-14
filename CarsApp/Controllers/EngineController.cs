using Microsoft.AspNetCore.Mvc;
using CarsApp.Businesslogic.Interfaces;
using AutoMapper;
using CarsApp.Businesslogic.Entities;
using CarsApp.Models;
using Microsoft.AspNetCore.OData.Query;

namespace CarsApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EngineController : ControllerBase
{
    private readonly IService<Engine> _engineService;
    private readonly IMapper _mapper;

    public EngineController(IService<Engine> engineService, IMapper mapper)
    {
        _engineService = engineService ?? throw new ArgumentNullException(nameof(engineService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetAll()
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

        var viewModels = _mapper.Map<IEnumerable<EngineViewModel>>(engines);

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
        var viewModel = _mapper.Map<EngineViewModel>(engine);

        return Ok(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EngineViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var engine = _mapper.Map<Engine>(model);

        await _engineService.Create(engine);

        return Ok(engine.Id);
    }

    [HttpPut]
    public async Task<IActionResult> Update(EngineViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(model);
        }
        var engine = _mapper.Map<Engine>(model);

        return Ok(await _engineService.Update(engine));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var engine = await _engineService.GetById(id);

        if (engine is null)
        {
            return NotFound();
        }

        await _engineService.Delete(engine);

        return Ok(id);
    }
}

