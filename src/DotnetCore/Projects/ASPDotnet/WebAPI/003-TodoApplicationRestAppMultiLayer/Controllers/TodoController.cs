﻿using CSD.TodoApplicationRestApp.Entities;
using CSD.TodoApplicationRestApp.Errors;

using CSD.Util.Data.Service;
using Microsoft.AspNetCore.Mvc;

namespace CSD.TodoApplicationRestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoAppService m_todoAppService;

        public TodoController(TodoAppService todoAppService)
        {
            m_todoAppService = todoAppService;
        }

        [HttpGet("todos/count")]
        public IActionResult CountTodos()
        {
            try
            {
                return new ObjectResult(new { Count =  m_todoAppService.CountTodos() });
            }
            catch (DataServiceException ex)
            {
                return NotFound(new ErrorInfo { Message = ex.Message, Status = 404, Detail = "Internal DB problem"});
            }
        }

        [HttpGet("todos/all")]
        public IActionResult FindAll()
        {
            try
            {
                return new ObjectResult(m_todoAppService.FindAllTodos());
            }
            catch (DataServiceException ex)
            {
                return NotFound(new ErrorInfo { Message = ex.Message, Status = 404, Detail = "Internal DB problem" });
            }
        }

        [HttpGet("todos/find/month")]
        public IActionResult FindTodosByMonth(int mon)
        {
            try
            {
                return new ObjectResult(m_todoAppService.FindTodosByMonth(mon));
            }
            catch (DataServiceException ex)
            {
                return NotFound(new ErrorInfo { Message = ex.Message, Status = 404, Detail = "Internal DB problem" });
            }
        }

        [HttpPost]
        public IActionResult SaveTodo([FromBody] TodoInfo todoInfo)
        {
            try
            {
                return new ObjectResult(m_todoAppService.SaveTodo(todoInfo));
            }
            catch (DataServiceException ex)
            {
                return NotFound(new ErrorInfo { Message = ex.Message, Status = 404, Detail = "Internal DB problem" });
            }
        }

    }
}
