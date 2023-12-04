using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{

    private readonly IUserLogic userLogic;

    public UsersController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }
    
    
    [HttpPost]
    [Route("CreateUser")]
    public async Task<ActionResult<User>> CreateAsync(UserCreationDTO dto)
    {
        try
        {
            User user = await userLogic.CreateAsync(dto);
            return Created($"/users/{user.Username}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("ViewProfile")]
    public async Task<ActionResult<User>> GetAsync(string username)
    {
        try
        {
            UserLookupDto parameter = new(username);
            var user = await userLogic.GetAsync(parameter);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [Authorize]
    [HttpPatch]
    [Route("EditUser")]
    public async Task<ActionResult> UpdateAsync([FromBody] ProfileUpdateDto dto)
    {
        try
        {
            await userLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
   
    
}
