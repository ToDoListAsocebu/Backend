using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces.Repositories;
using ToDoApp.Domain.Entities;

namespace ToDoApp.API.Controllers;

[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    public TaskController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _taskRepository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        return task is null ? NotFound(new { message = "Tarea no encontrada" }) : Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskDto taskDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Verificar si ya existe una tarea con el mismo título
        var existingTask = await _taskRepository.GetByTitleAsync(taskDto.Title);
        if (existingTask != null)
        {
            return Conflict(new { message = "Ya existe una tarea con el mismo título" });
        }

        var task = new TaskItem
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            IsCompleted = taskDto.IsCompleted,
            CreatedAt = DateTime.UtcNow,
            CompletedAt = taskDto.IsCompleted ? taskDto.CompletedAt : null
        };

        await _taskRepository.AddAsync(task);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TaskDto taskDto)
    {
        var existingTask = await _taskRepository.GetByIdAsync(id);
        if (existingTask is null) return NotFound(new { message = "Tarea no encontrada" });

        // Verificar si ya existe una tarea con el mismo título (que no sea la misma tarea)
        var duplicateTask = await _taskRepository.GetByTitleAsync(taskDto.Title);
        if (duplicateTask != null && duplicateTask.Id != id)
        {
            return Conflict(new { message = "Ya existe una tarea con el mismo título" });
        }

        existingTask.Title = taskDto.Title;
        existingTask.Description = taskDto.Description;
        existingTask.IsCompleted = taskDto.IsCompleted;
        existingTask.CompletedAt = taskDto.IsCompleted ? taskDto.CompletedAt : null;

        try
        {
            await _taskRepository.UpdateAsync(existingTask);
            return Ok(existingTask);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingTask = await _taskRepository.GetByIdAsync(id);
        if (existingTask is null) return NotFound(new { message = "Tarea no encontrada" });

        try
        {
            await _taskRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
