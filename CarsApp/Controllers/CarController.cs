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
    private IService<Car> _carService;
    private IService<Engine> _engineService;

    public CarController(IService<Car> carService, IService<Engine> engineService)
    {
        _carService = carService ?? throw new ArgumentNullException(nameof(carService));
        _engineService = engineService ?? throw new ArgumentNullException(nameof(engineService));
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

        var mapper = new MapperConfiguration(config => config.CreateMap<Car, CarViewModel>()).CreateMapper();
        var viewModels = mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(cars);

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

        var mapper = new MapperConfiguration(config => config.CreateMap<Car, CarViewModel>()).CreateMapper();
        var viewModel = mapper.Map<Car, CarViewModel>(car);

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

        var mapper = new MapperConfiguration(config => config.CreateMap<CarViewModel, Car>()).CreateMapper();
        var car = mapper.Map<CarViewModel, Car>(model);

        car.Engine = engine;

        return Ok(await _carService.Create(car));
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

        var mapper = new MapperConfiguration(config => config.CreateMap<CarViewModel, Car>()).CreateMapper();
        var car = mapper.Map<CarViewModel, Car>(model);

        car.Engine = engine;        

        return Ok(await _carService.Update(car));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _carService.Delete(id));
    }
}

