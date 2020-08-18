using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Shared.Models;

namespace ToDoList.Api.Data
{
    public class ToDoRepository : Repository<ToDoItem>, IToDoRepository
    { 
        private readonly DataContext _context;

        public ToDoRepository(DataContext context) : base(context)
        {
           _context = context;
        }

        public async Task<ToDoItem> GetItem(long Id)
        {
            var item = await _context.ToDoItems.FirstOrDefaultAsync(u => u.Id == Id);
            return item;
        }

    }
}
