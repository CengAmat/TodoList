using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public TodoController(IMemoryCache memoryCache)
        {
            // Inject IMemoryCache interface in the constructor
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<List<Todos>> GetTodoCacheAsync()
        {
            var output = _memoryCache.Get<List<Todos>>("todos");
            if(output != null) return output;

            output = new List<Todos>()
            {
                new Todos() { Id = 1, Todo = "Go to Hell!", Status= "Active" },
                new Todos() { Id = 2, Todo = "Go to Hell Again!", Status= "Active" },
                new Todos() { Id = 3, Todo = "Go to Hell and Stay There!", Status= "Active" }
            };

            await Task.Delay(500);

            _memoryCache.Set("todos", output, TimeSpan.FromMinutes(1));

            return output;
        }
    }
}
