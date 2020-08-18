using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Shared.Models;

namespace ToDoList.Api.Data
{
    public interface IToDoRepository : IRepository<ToDoItem>
    {
        Task<ToDoItem> GetItem(long Id);

    }
}
