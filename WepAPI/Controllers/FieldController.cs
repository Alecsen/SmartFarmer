﻿using Application.LogicInterface;
using Domain.DTOs;
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
    public async Task<ActionResult<IEnumerable<FieldLookupDto>>> GetFieldsByOwner(int ownerId)
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

    [HttpPost]
    public async Task<ActionResult<Field>> CreateAsync([FromBody] FieldCreationDto dto)
    {
        try
        {
            Field created = await fieldLogic.CreateAsync(dto);
            return Created($"/fields/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}