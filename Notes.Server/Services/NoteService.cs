using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Server.Database;
using Notes.Server.Entities;

namespace Notes.Server.Services;

public class NoteService : INoteService
{
    private readonly NotesDatabaseContext _context;

    public NoteService(NotesDatabaseContext context) {
        _context = context;
    }
    
    public async Task<Note> Create(string name, string content = "")
    {
        var note = new Note(Guid.NewGuid(), name, content);
        await _context.Notes.AddAsync(note);
        await _context.SaveChangesAsync();
        return note;
    }

    public async Task<Note> FindByName(string name)
    {
        var note = await _context.Notes.FirstOrDefaultAsync(note => note.Name == name);
        return note;
    }

    public async Task<Note> FindById(Guid id)
    {
        var note = await _context.Notes.FindAsync(id);
        return note;
    }

    public async Task<Note[]> GetAll()
    {
        var notes = await Task.FromResult(_context.Notes.ToArray());
        return notes;
    }

    public async Task Delete(Guid id)
    {
        var note = await FindById(id);
        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
    }

    public async Task<Note> Update(Guid id, string name, string content)
    {
        var note = new Note(id, name, content);
        _context.Notes.Update(note);
        await _context.SaveChangesAsync();
        return note;
    }
}