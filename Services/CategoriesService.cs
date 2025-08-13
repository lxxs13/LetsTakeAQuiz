using GeekQuiz.Helpers;
using GeekQuiz.Interfaces;
using GeekQuiz.Models;
using GeekQuiz.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeekQuiz.Services
{
    public class CategoriesService : ICategory
    {
        private readonly GeekQuizContext _dbContext;
        public CategoriesService(GeekQuizContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ServiceResult<bool> CreateCategory(CategoriesDTO categoryDTO)
        {
           try
            {
                Category category = new Category
                {
                    Name = categoryDTO.CategoryName,
                    Inactive = (bool)categoryDTO.Status,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
            } catch(Exception e)
            {
                return ServiceResult<bool>.Fail("Ocurrió un error al tratar de guardar la información.");

            }

            return ServiceResult<bool>.Ok(true);
        }

        public ServiceResult<IEnumerable<CategoriesDTO>> GetCategories()
        {
            var result = _dbContext.Categories.Where(e => !e.Deleted)
                .Select(c => new CategoriesDTO
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.Name ?? ""
                }).ToList();

            if (result == null)
                return ServiceResult<IEnumerable<CategoriesDTO>>.Fail("Error al obtener las categorias");

            return ServiceResult<IEnumerable<CategoriesDTO>>.Ok(result);
        }

        public ServiceResult<CategoriesByStatusDTO> GetCategoriesByStatus()
        {
            var activesCategoeries = _dbContext.Categories.Where(e => !e.Deleted && !e.Inactive)
                .Select(c => new CategoriesDTO
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.Name ?? "",
                    Status = c.Inactive
                }).ToList();

            var inactivesCategoeries = _dbContext.Categories.Where(e => !e.Deleted && e.Inactive)
                .Select(c => new CategoriesDTO
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.Name ?? "",
                    Status = c.Inactive
                }).ToList();

            CategoriesByStatusDTO result = new CategoriesByStatusDTO
            {
                ActivesCategories = activesCategoeries,
                InactivesCategories = inactivesCategoeries,
            };

            return ServiceResult<CategoriesByStatusDTO>.Ok(result);
        }

        public ServiceResult<bool> UpdateCategory(CategoriesDTO categoryDTO)
        {
            Category? ct = _dbContext.Categories.Find(categoryDTO.CategoryId);

            if (ct == null)
                return ServiceResult<bool>.Fail("No se encontró el ID de la categoría a actualizar");

            try
            {
                ct.Name = categoryDTO.CategoryName;
                ct.UpdatedAt = DateTime.Now;

                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return ServiceResult<bool>.Fail("Error al intentar eliminar el registro");
            }

            return ServiceResult<bool>.Ok(true);
        }

        public ServiceResult<bool> UpdateCategoryStatus(int[] categoriesId)
        {
            try
            {
                _dbContext.Categories.ExecuteUpdate(e => e.SetProperty(e => e.Inactive, true));
            } catch (Exception e)
            {
                return ServiceResult<bool>.Fail("Error al actualizar estatus generales");
            }

            if (categoriesId.Length == 0)
                return ServiceResult<bool>.Ok(true);

            try
            {
                List<Category> categories = _dbContext.Categories.Where(e => categoriesId.Contains(e.CategoryId)).ToList();

                //if (categories.Count == 0)
                //    return ServiceResult<bool>.Fail("No se encotraron registros con los ID ingresados");

                foreach (var item in categories)
                {
                    item.Inactive = !item.Inactive;
                    item.UpdatedAt = DateTime.Now;
                }

                _dbContext.SaveChanges();
            } catch(Exception e)
            {
                return ServiceResult<bool>.Fail("Error al actualizar estatus generales");
            }

            return ServiceResult<bool>.Ok(true);
        }

        public ServiceResult<bool> DeleteCategory(int categoryId)
        {
            Category? ct = _dbContext.Categories.Find(categoryId);

            if (ct == null)
                return ServiceResult<bool>.Fail("No se encontró el ID de la categoría a eliminar");

           try
            {
                ct.Deleted = true;
                ct.UpdatedAt = DateTime.Now;

                _dbContext.SaveChanges();
            } catch(Exception e)
            {
                return ServiceResult<bool>.Fail("Error al intentar eliminar el registro");
            }

            return ServiceResult<bool>.Ok(true);
        }
    }
}
