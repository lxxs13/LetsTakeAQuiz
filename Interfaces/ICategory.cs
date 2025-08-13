using GeekQuiz.Helpers;
using GeekQuiz.Models.DTOs;

namespace GeekQuiz.Interfaces
{
    public interface ICategory
    {
        public ServiceResult<IEnumerable<CategoriesDTO>> GetCategories();
        public ServiceResult<bool> CreateCategory(CategoriesDTO categoryDTO);
        public ServiceResult<CategoriesByStatusDTO> GetCategoriesByStatus();
        public ServiceResult<bool> UpdateCategory(CategoriesDTO categoryDTO);
        public ServiceResult<bool> UpdateCategoryStatus(int[] categoriesId);
        public ServiceResult<bool> DeleteCategory(int categoryId);
    }
}
