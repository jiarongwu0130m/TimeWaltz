using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Services;
using static WebApplication1.Controllers.Api.VacationDetailsApiController;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VacationDetailsApiController : ControllerBase
    {
        private readonly VacationTypeService _vacationTypeService;

        public VacationDetailsApiController(VacationTypeService vacationTypeService)
        {
            _vacationTypeService = vacationTypeService;
        }
        public CreateVacationTypeViewModel DropDownList()
        {
            var model = new CreateVacationTypeViewModel
            {
                GenderSelectItems = DropDownHelper.GetGenderDropDownList(),
                CycleSelectItems = DropDownHelper.GetCycleDropDownList()
            };
            return model;
        }


        //參考用
        [HttpPost("add")]
        public ActionResult<TodoItem> CreateVacationDetail([FromBody] CreateVacationTypeViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var entity = ViewModelHelper.ToEntity(model);
            _vacationTypeService.CreateVacationType(entity);
            
            return Ok();
        }

        // Controllers/TodoController.cs



        [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> _todoItems = new List<TodoItem>
    {
        new TodoItem { Id = 1, Task = "Task 1", IsCompleted = false },
        new TodoItem { Id = 2, Task = "Task 2", IsCompleted = true }
    };

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            return Ok(_todoItems);
        }

        [HttpPost("add")]
        public ActionResult<TodoItem> AddTodoItem([FromBody] TodoItem todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest();
            }

            todoItem.Id = _todoItems.Count + 1;
            _todoItems.Add(todoItem);
            return Ok(todoItem);
        }

        [HttpPost("update")]
        public ActionResult UpdateTodoItem([FromBody] TodoItem updatedTodoItem)
        {
            var existingTodoItem = _todoItems.FirstOrDefault(item => item.Id == updatedTodoItem.Id);
            if (existingTodoItem == null)
            {
                return NotFound();
            }

            existingTodoItem.Task = updatedTodoItem.Task;
            existingTodoItem.IsCompleted = updatedTodoItem.IsCompleted;

            return Ok(existingTodoItem);
        }

        [HttpPost("delete/{id}")]
        public ActionResult DeleteTodoItem(int id)
        {
            var todoItemToRemove = _todoItems.FirstOrDefault(item => item.Id == id);
            if (todoItemToRemove == null)
            {
                return NotFound();
            }

            _todoItems.Remove(todoItemToRemove);
            return NoContent();
        }
    }

    public class TodoItem
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool IsCompleted { get; set; }
    }

}
}
