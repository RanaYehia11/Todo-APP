using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.TodoData;

namespace TodoApp.Services;
public interface ITodoListServices
{
    List<TodoItem> GetCategories();
    List<TodoItem> GetAll(int categoryId);
    TodoItem GetTodoItem(int category ,int id);
    TodoItem Add(TodoItemDto item);
    TodoItem Update(int id,TodoItemDto item);
    bool UpdateStatus(int id, TodoStatus status);

}

public partial class CategoryItemsServices : ITodoListServices
{
    private readonly AppDbContext _context;
   
    public List<TodoItem> GetCategories()
    {
       return _context.TodoItems.ToList();
    }
    public List<TodoItem> GetAll(int categoryId)
    {
        return _context.TodoItems
            .Where(c => c.CategoryId == categoryId).ToList();
    }
    // Method to get a specific TodoItem by categoryId and id
    public TodoItem GetTodoItem(int categoryId, int id) 
    {
        return _context.TodoItems
            .FirstOrDefault(c => c.CategoryId == categoryId && c.Id==id);

    }

    public TodoItem Add(TodoItemDto item)
    {
      var newTodo=new TodoItem(item);
       var savedItem= _context.TodoItems.Add(newTodo);
        _context.SaveChanges();
        return savedItem.Entity;
    }

    public TodoItem Update(int id,TodoItemDto item)
    {
      var current = _context.TodoItems
            .FirstOrDefault(c => c.Id == id);
        if (current == null)
        {
            throw new KeyNotFoundException($"Todo item with ID {id} not found.");
        }
        current.UpdateTodoItem(item);
        _context.SaveChanges();
        return current;
    }

    public bool UpdateStatus(int id, TodoStatus status)
    {
     var current= _context.TodoItems
            .FirstOrDefault(c => c.Id == id);
        current.ChangeStatus(status);
        _context.SaveChanges();
        return _context.SaveChanges() >0;
      
    }

}

