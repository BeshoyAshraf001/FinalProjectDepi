namespace FinalProjectDepi.Controllers;

using FinalProjectDepi.DAL.Entities;
using FinalProjectDepi.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientRepo _clientRepo;

    public ClientController(IClientRepo clientRepo)
    {
        _clientRepo = clientRepo;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var clients = _clientRepo.GetAll();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var client = _clientRepo.GetById(id);
        if (client == null)
        {
            return NotFound(new { message = $"Client with ID {id} not found." });
        }
        return Ok(client);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Client client)
    {
        if (client == null)
        {
            return BadRequest();
        }

        _clientRepo.Add(client);
        return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Client client)
    {
        if (client == null || client.Id != id)
        {
            return BadRequest(new { message = "Client ID mismatch or invalid body." });
        }

        var existingClient = _clientRepo.GetById(id);
        if (existingClient == null)
        {
            return NotFound(new { message = $"Client with ID {id} not found." });
        }

        existingClient.Name = client.Name;
        _clientRepo.Update(existingClient);

        return Ok(existingClient);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existingClient = _clientRepo.GetById(id);
        if (existingClient == null)
        {
            return NotFound(new { message = $"Client with ID {id} not found." });
        }

        _clientRepo.Delete(id);
        return NoContent();
    }
}