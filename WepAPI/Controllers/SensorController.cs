using Application.LogicInterface;
using Domain.DTOs;
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
}