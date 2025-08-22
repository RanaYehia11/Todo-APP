using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Models;
using TodoApp.TodoData;

namespace TodoApp.Services;

public interface ICategoryService
{
    List<Category> GetCategories();
    Category? GetCategory(int id);
    Category Add(CategoryDto dto);
    Category? Update(int id ,CategoryDto dto);
    Category? Delete(int id);
}


public partial class CategoryService : ICategoryService

{
    // Constructor injection for AppDbContext
    private readonly AppDbContext _context;

    // Constructor to initialize the service with the database context
    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public Category Add(CategoryDto dto)
    {
        // Create a new Category instance from the DTO
        var category = new Category(dto);
        var savedCategory=_context.Categories.Add(category);
        _context.SaveChanges();
        return savedCategory.Entity;

    }

    public Category? Delete(int id)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Id == id);
        if(category != null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
        return category;
    }

    public List<Category> GetCategories()
    {
        return _context.Categories.ToList();
            
    }

    public Category GetCategory(int id)
    {
        return _context.Categories
            .FirstOrDefault(c => c.Id == id) 
            ?? throw new KeyNotFoundException($"Category with ID {id} not found.");
    }

    public Category Update(int id, CategoryDto dto)
    {
        var current= _context.Categories
            .FirstOrDefault(c => c.Id == id);
        current.Update(dto);
        _context.SaveChanges(true);
        return current ?? throw new KeyNotFoundException($"Category with ID {id} not found.");
    }


  
}

