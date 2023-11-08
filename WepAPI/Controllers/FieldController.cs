using Application.LogicInterface;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FieldController: ControllerBase
{
    private readonly IFieldLogic fieldLogic;

    public FieldController(IFieldLogic fieldLogic)
    {
        this.fieldLogic = fieldLogic;
    }

    [HttpGet("{ownerId}")]
    public async Task<ActionResult<IEnumerable<Field>>> GetFieldsByOwner(int ownerId)
    {
        try
        {
            var fields = await fieldLogic.GetAsync(ownerId);
            return Ok(fields);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}