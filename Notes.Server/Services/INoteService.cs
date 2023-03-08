using System;
using System.Threading.Tasks;
using Notes.Server.Entities;

namespace Notes.Server.Services;

public interface INoteService
{
    Task<Note> Create(string name, string content);

    Task<Note> FindByName(string name);

    Task<Note> FindById(Guid id);

    Task<Note[]> GetAll();

    Task Delete(Guid id);

    Task<Note> Update(Guid id, string name, string content);
}