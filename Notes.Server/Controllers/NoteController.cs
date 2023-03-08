using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notes.Server.Services;

namespace Notes.Server.Controllers;

[ApiController]
[Route("/notes")]
public class NoteController : ControllerBase
{
    private readonly INoteService _service;

    public NoteController(INoteService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromQuery] string name, [FromQuery] string content = "")
    {
        // Creates new note in database with given name and content
        if (string.IsNullOrWhiteSpace(name)) return BadRequest();
        var created = await _service.Create(name, content);
        if (created is not null)
        {
            return Created($"/notes/{name}/{content}", created);
        }

        return StatusCode(500);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Returns all notes
        var notes = await _service.GetAll();
        if (notes != null)
        {
            return Ok(notes);
        }

        return NotFound();
    }

    [HttpGet("/note-by-id")]
    public async Task<IActionResult> Get([FromQuery] Guid id)
    {
        // Returns note specified by ID
        if (id == Guid.Empty)
        {
            return BadRequest();
        }

        var note = await _service.FindById(id);
        if (note is null)
        {
            return NotFound();
        }

        return Ok(note);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        // Deletes note specified by ID
        if (id == Guid.Empty)
        {
            return BadRequest();
        }
        
        await _service.Delete(id);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] Guid id, [FromQuery] string name, [FromQuery] string content = "")
    {
        // Updates node specified by ID
        var newId = id;
        if (newId == Guid.Empty)
        {
            var foundNote = await _service.FindByName(name);
            Console.WriteLine(foundNote.Id);
            newId = foundNote.Id;
        }
        
        var note = await _service.Update(newId, name, content);
        if (note != null)
        {
            return Ok(note);
        }

        return BadRequest();
    }
}