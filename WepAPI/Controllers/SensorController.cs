using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorController: ControllerBase
{
    private readonly ISensorLogic sensorLogic;

    public SensorController(ISensorLogic sensorLogic)
    {
        this.sensorLogic = sensorLogic;
    }

    [HttpGet("{fieldId}")]
    public async Task<ActionResult<IEnumerable<SensorLookupDto>>> GetSensorBySensorId(int fieldId)
    {
        try
        {
            var sensors = await sensorLogic.GetAsync(fieldId);
            return Ok(sensors);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Sensor>> CreateSensorAsync(SensorCreationDto dto)
    {
        try
        {
            var sensor = await sensorLogic.CreateSensorAsync(dto);
            return Ok(sensor);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500,e.Message);
        }
        
    }
}