using System.Collections.Generic;
using System.Threading.Tasks;
using RESTwebAPI.Models;

namespace RESTwebAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly List<Category> _categorys;

        public CategoryService()
        {
            _categorys = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Побутова техніка"},
                new Category { CategoryId = 2, CategoryName = "Ноутбуки"},
                new Category { CategoryId = 3, CategoryName = "Електроніка для геймерів"},
                new Category { CategoryId = 4, CategoryName = "Аксесуари"},
                new Category { CategoryId = 5, CategoryName = "Комп'ютери"},
                new Category { CategoryId = 6, CategoryName = "Навушники"},
                new Category { CategoryId = 7, CategoryName = "Колонки"},
                new Category { CategoryId = 8, CategoryName = "Флешки"},
                new Category { CategoryId = 9, CategoryName = "Квадракоптери"},
                new Category { CategoryId = 10, CategoryName = "Лецензії для техніки"},

            };
        }
        public async Task<ResponseModel<Category>> AddCategoryAsync(Category category)
        {
            category.CategoryId = _categorys.Any() ? _categorys.Max(p => p.CategoryId) + 1 : 1;
            _categorys.Add(category);
            return new ResponseModel<Category>
            {
                Data = category,
                Success = true,
                Message = "Category added successfully."
            };
        }

        public async Task<ResponseModel<Category>> DeleteCategoryAsync(int id)
        {
            var categoryToRemove = _categorys.FirstOrDefault(p => p.CategoryId == id);
            if (categoryToRemove == null)
            {
                return new ResponseModel<Category>
                {
                    Data = null,
                    Success = false,
                    Message = $"Category with id {id} not found."
                };
            }
            _categorys.Remove(categoryToRemove);
            return new ResponseModel<Category>
            {
                Data = categoryToRemove,
                Success = true,
                Message = $"Category with id {id} deleted successfully."
            };
        }

        public async Task<ResponseModel<IEnumerable<Category>>> GetAllCategorysAsync()
        {
            return new ResponseModel<IEnumerable<Category>>
            {
                Data = _categorys,
                Success = true,
                Message = "Successfully retrieved all Categorys."
            };
        }

        public async Task<ResponseModel<Category>> GetCategoryAsync(int id)
        {
            var Category = _categorys.FirstOrDefault(p => p.CategoryId == id);
            if (Category == null)
            {
                return new ResponseModel<Category>
                {
                    Data = null,
                    Success = false,
                    Message = $"Category with id {id} not found."
                };
            }
            return new ResponseModel<Category>
            {
                Data = Category,
                Success = true,
                Message = $"Successfully retrieved Category with id {id}."
            };
        }

        public async Task<ResponseModel<Category>> UpdateCategoryAsync(int id, Category category)
        {
            var existingCategory = _categorys.FirstOrDefault(p => p.CategoryId == id);
            if (existingCategory == null)
            {
                return new ResponseModel<Category>
                {
                    Data = null,
                    Success = false,
                    Message = $"Category with id {id} not found."
                };
            }
            existingCategory.CategoryName = category.CategoryName;
            return new ResponseModel<Category>
            {
                Data = existingCategory,
                Success = true,
                Message = $"Category with id {id} updated successfully."
            };
        }
    }
}
