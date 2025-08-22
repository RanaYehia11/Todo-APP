using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Services;
using TodoApp.TodoData;
using TodoApp.Models;
using TodoAPI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;




namespace TodoAPI.Controllers;
[ApiController]
[Route("api/[controller]")]


public class CategoryController : ControllerBase
{

    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    //Get category///

    [HttpGet]
    public IActionResult GetCategories()
    {
        try
        {
            var categories = _categoryService.GetCategories();
            var result = new ResultViewModel<List<Category>>()
            {
                IsSuccess = true,
                Message = "Categories retrieved successfully",
                Data = categories
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new ResultViewModel<List<Category>>
            {
                IsSuccess = false,
                Message = $"An error occurred while retrieving categories: {ex.Message}",
            });
        }
    }

    [HttpGet("{id}")]

    public IActionResult GetByID([FromRoute] int id)
    {
        try
        {
            var category = _categoryService.GetCategory(id);
            var result = new ResultViewModel<Category>()
            {
                IsSuccess = category != null,
                Message = category != null ? "Success" : "Category not found",
                Data = category
            };
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }
        catch (Exception e)
        {
            return BadRequest(new ResultViewModel<Category>
            {
                IsSuccess = false,
                Message = $"An error occurred while retrieving the category: {e.Message}"
            });

        }

    }


    //Post Category ////

    [HttpPost]
    public IActionResult PostCategory([FromBody] CategoryDto model)
    {
        try
        {
            var insertedCategory = _categoryService.Add(model);
            if (insertedCategory != null)
            {
                return Ok(new ResultViewModel<Category>
                {
                    IsSuccess = true,
                    Message = "Category added successfully",
                    Data = insertedCategory
                });
            }

            return BadRequest(new ResultViewModel<object>
            {
                IsSuccess = false,
                Message = "Failed to add category",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<object>
            {
                IsSuccess = false,
                Message = $"Internal server error: {ex.Message}",
                Data = null
            });
        }
    }

    //Update The Category////

    [HttpPut("{id}")]
    public IActionResult PutCategory([FromRoute] int id, [FromBody] CategoryDto model)
    {
        if (id == default)
        {
            return BadRequest(new ResultViewModel<Category>
            {
                IsSuccess = false,
                Message = "Category id is required",
            });
        }

        try
        {
            var updatedCategory = _categoryService.Update(id, model);

            if (updatedCategory != null)
            {
                return Ok(new ResultViewModel<Category>
                {
                    IsSuccess = true,
                    Message = "Category updated successfully",
                    Data = updatedCategory
                });
            }

            return BadRequest(new ResultViewModel<object>
            {
                IsSuccess = false,
                Message = "Failed to update category",
                Data = null
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<object>
            {
                IsSuccess = false,
                Message = $"Internal server error: {e.Message}",
                Data = null
            });
        }
    }
    [HttpDelete("{id}")]

    public IActionResult DeleteCategory([FromRoute] int id)
    {
        if (id == null)
        {
            return BadRequest(new ResultViewModel<Category>
            {
                IsSuccess = false,
                Message = "Category id is required",
            });
        }
        try
        {
            var isDeleted = _categoryService.Delete(id);
            if (isDeleted != null)
            {
                return Ok(new ResultViewModel<Category>
                {
                    IsSuccess = true,
                    Message = "Category deleted successfully",
                    Data = null
                });
            }

            return NotFound(new ResultViewModel<object>
            {
                IsSuccess = false,
                Message = "Category not found",
                Data = null
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<object>
            {
                IsSuccess = false,
                Message = $"Internal server error: {ex.Message}",
                Data = null
            });
        }
    }
}

