using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Services;

namespace TodoApp.Models;
     public record CategoryDto( string Name ,bool IsActive);

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }= true;
        public List<TodoItem> Items { get; set; }

    public Category()
    {
    }


    public Category(CategoryDto dto)
        {
           
            Name =dto. Name;
            IsActive = dto.IsActive;
        }
        public void Update(CategoryDto dto)
        {
            this.Name = dto.Name;
             this.IsActive = dto.IsActive;

    }

        public CategoryReadModel ToReadModel()
        {
            return new CategoryReadModel
            {
                Id = this.Id,
                Name = this.Name,
                IsActive = this.IsActive
            };
    }

}

