using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Models
{
    public record TodoItemDto(int Id, string Title, string Description, int CategoryId, TodoStatus Status = TodoStatus.New);
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public TodoStatus Status { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public TodoItem()
        {
        }


        public TodoItem(TodoItemDto dto)
        {
            Id = dto.Id;
            Title = dto.Title;
            Description = dto.Description;
            CategoryId = dto.CategoryId;
            Status = dto.Status;
            CreateDate = DateTime.Now;
           
        }
        public void UpdateTodoItem(TodoItemDto dto)
        {
            this.Title = dto.Title;
            this.Description = dto.Description;
            this.CategoryId = dto.CategoryId;
            this.Status = dto.Status;
            

        }
        public void ChangeStatus(TodoStatus status)
        {
            this.Status = status;
        }
        public void UpdateDate()
        {
            this.CreateDate = DateTime.Now;
        }
    }
}
