namespace FinalProjectDepi.Controllers;

using FinalProjectDepi.DAL.Entities;
using FinalProjectDepi.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PhoneController : ControllerBase
{
    private readonly IPhoneRepo _phoneRepo;

    public PhoneController(IPhoneRepo phoneRepo)
    {
        _phoneRepo = phoneRepo;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var phones = _phoneRepo.GetAll();
        return Ok(phones);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var phone = _phoneRepo.GetById(id);
        if (phone == null)
        {
            return NotFound(new { message = $"Phone with ID {id} not found." });
        }
        return Ok(phone);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Phone phone)
    {
        if (phone == null)
        {
            return BadRequest();
        }

        _phoneRepo.Add(phone);
        return CreatedAtAction(nameof(GetById), new { id = phone.Id }, phone);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Phone phone)
    {
        if (phone == null || phone.Id != id)
        {
            return BadRequest(new { message = "Phone ID mismatch or invalid body." });
        }

        var existingPhone = _phoneRepo.GetById(id);
        if (existingPhone == null)
        {
            return NotFound(new { message = $"Phone with ID {id} not found." });
        }

        existingPhone.Model = phone.Model;
        _phoneRepo.Update(existingPhone);

        return Ok(existingPhone);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existingPhone = _phoneRepo.GetById(id);
        if (existingPhone == null)
        {
            return NotFound(new { message = $"Phone with ID {id} not found." });
        }

        _phoneRepo.Delete(id);
        return NoContent();
    }
}