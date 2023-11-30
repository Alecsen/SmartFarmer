﻿using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class IrrigationMachineController: ControllerBase
{
    private readonly IIrrigationMachineLogic irrigationMachineLogic;

    public IrrigationMachineController(IIrrigationMachineLogic irrigationMachineLogic)
    {
        this.irrigationMachineLogic = irrigationMachineLogic;
    }
    
    [HttpGet("{fieldId}")]
    public async Task<ActionResult<IEnumerable<IrrigationMachineLookupDto>>> GetFieldsById(int fieldId)
    {
        try
        {
            var irrigationMachines = await irrigationMachineLogic.GetAsync(fieldId);
            return Ok(irrigationMachines);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<IrrigationMachine>> CreateAsync([FromBody] IrrigationMachineCreationDto dto)
    {
        try
        {
            IrrigationMachine created = await irrigationMachineLogic.CreateAsync(dto);
            return Created($"/irrigationMachine/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}
    
    
