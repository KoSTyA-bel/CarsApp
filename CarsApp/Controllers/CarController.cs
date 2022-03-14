using AutoMapper;
using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace CarsApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly IService<Car> _carService;
    private readonly IService<Engine> _engineService;
    private IMapper _mapper;

    public CarController(IService<Car> carService, IService<Engine> engineService, IMapper mapper)
    {
        _carService = carService ?? throw new ArgumentNullException(nameof(carService));
        _engineService = engineService ?? throw new ArgumentNullException(nameof(engineService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetAll()
    {
        var cars = await _carService.GetAll();

        if (cars is null)
        {
            return NotFound();
        }

        if (!cars.Any())
        {
            return NoContent();
        }

        var viewModels = _mapper.Map<IEnumerable<CarViewModel>>(cars);

        return Ok(viewModels);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var car = await _carService.GetById(id);

        if (car is null)
        {
            return NotFound();
        }

        var viewModel = _mapper.Map<CarViewModel>(car);

        return Ok(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CarViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return NoContent();
        }

        var engine = await _engineService.GetById(model.EngineId);

        if (engine is null)
        {
            return NoContent();
        }

        var car = _mapper.Map<Car>(model);

        car.Engine = engine;
        await _carService.Create(car);

        return Ok(car.Id);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CarViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(model);
        }

        var engine = await _engineService.GetById(model.EngineId);

        if (engine is null)
        {
            return NoContent();
        }

        var car = _mapper.Map<Car>(model);

        car.Engine = engine;
        await _carService.Update(car);

        return Ok(car.Id);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var car = await _carService.GetById(id);
        
        if (car is null)
        {
            return NotFound();
        }

        await _carService.Delete(car);

        return Ok(id);
    }
}

