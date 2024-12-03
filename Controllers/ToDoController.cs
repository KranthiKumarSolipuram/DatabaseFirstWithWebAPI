using DatabaseFirstWithWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirstWithWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly TestdbContext _context;

        public ToDoController(TestdbContext context)
        {
            _context = context;
        }

        // GET: api/ToDo
        [HttpGet]
        public IActionResult GetAllToDos()
        {
            try
            {
                var toDos = _context.ToDos.ToList();
                if (toDos == null || !toDos.Any())
                {
                    return NotFound("No ToDos found.");
                }
                return Ok(toDos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ToDo/5
        [HttpGet("{id}")]
        public IActionResult GetToDoById(int id)
        {
            try
            {
                var toDo = _context.ToDos.FirstOrDefault(t => t.Id == id);
                if (toDo == null)
                {
                    return NotFound($"ToDo with ID {id} not found.");
                }
                return Ok(toDo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/ToDo
        [HttpPost]
        public IActionResult CreateToDo([FromBody] ToDo toDo)
        {
            try
            {
                if (toDo == null)
                {
                    return BadRequest("ToDo object is null.");
                }

                _context.ToDos.Add(toDo);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetToDoById), new { id = toDo.Id }, toDo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/ToDo/5
        [HttpPut("{id}")]
        public IActionResult UpdateToDo(int id, [FromBody] ToDo toDo)
        {
            try
            {
                if (toDo == null)
                {
                    return BadRequest("ToDo object is null.");
                }

                var existingToDo = _context.ToDos.FirstOrDefault(t => t.Id == id);
                if (existingToDo == null)
                {
                    return NotFound($"ToDo with ID {id} not found.");
                }

                existingToDo.Task = toDo.Task;
                existingToDo.IsCompleted = toDo.IsCompleted;

                _context.ToDos.Update(existingToDo);
                _context.SaveChanges();

                return NoContent(); // Successfully updated
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/ToDo/5
        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(int id)
        {
            try
            {
                var toDo = _context.ToDos.FirstOrDefault(t => t.Id == id);
                if (toDo == null)
                {
                    return NotFound($"ToDo with ID {id} not found.");
                }

                _context.ToDos.Remove(toDo);
                _context.SaveChanges();

                return NoContent(); // Successfully deleted
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
