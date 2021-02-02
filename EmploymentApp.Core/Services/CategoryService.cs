using Ardalis.Result;
using EmploymentApp.Core.Entities;
using EmploymentApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace EmploymentApp.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Category>> Add(Category category)
        {
            try
            {
                await _unitOfWork.CategoryRepository.Add(category);
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return Result<Category>.Error(new string[] { ex.Message });
            }
            return Result<Category>.Success(category);
        }

        public Result<IEnumerable<Category>> GetAll()
        {
            IEnumerable<Category> categories;
            try
            {
                categories = _unitOfWork.CategoryRepository.GetAll();
            }catch(Exception ex)
            {
                return Result<IEnumerable<Category>>.Error(new[] { ex.Message });
            }
            var result = Result<IEnumerable<Category>>.Success(categories);
            return result;
        }


        public async Task<Result<Category>> GetById(int id)
        {
            Category category;
            try
            {
                category = await _unitOfWork.CategoryRepository.GetById(id);
            }
            catch(Exception ex)
            {
                return Result<Category>.Error(new[] { ex.Message });
            }
            var result = Result<Category>.Success(category);
            return result;
        }
        public async Task<Result<bool>> Remove(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetById(id);
                if (category == null)
                    return Result<bool>.NotFound();
                _unitOfWork.CategoryRepository.Remove(category);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result<bool>.Error(new[] { ex.Message });
            }
            var result = Result<bool>.Success(true);
            return result;
        }

        public async Task<Result<bool>> Update(Category category)
        {
            try
            {
                var categoryTraking = await _unitOfWork.CategoryRepository.GetById(category.Id);
                if (categoryTraking == null)
                    return Result<bool>.NotFound();
                categoryTraking.Name = category.Name;
                _unitOfWork.CategoryRepository.Update(categoryTraking);
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex)
            {
               return Result<bool>.Error(new[] { ex.Message });
            }
            var result =  Result<bool>.Success(true);
            return result;
        }
    }
}
