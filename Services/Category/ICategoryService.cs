using RESTwebAPI.Models;

namespace RESTwebAPI.Services
{
    public interface ICategoryService
    {
        Task<ResponseModel<Category>> GetCategoryAsync(int id);
        Task<ResponseModel<IEnumerable<Category>>> GetAllCategorysAsync();
        Task<ResponseModel<Category>> AddCategoryAsync(Category category);
        Task<ResponseModel<Category>> UpdateCategoryAsync(int id, Category category);
        Task<ResponseModel<Category>> DeleteCategoryAsync(int id);
    }
}
