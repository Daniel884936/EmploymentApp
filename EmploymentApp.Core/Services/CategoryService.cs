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

        public async Task Add(Category category)
        {
            await _unitOfWork.CategoryRepository.Add(category);
        }
        public IEnumerable<Category> GetAll()
        {
            return _unitOfWork.CategoryRepository.GetAll();
        }
        public async Task<Category> GetById(int id)
        {
            return await _unitOfWork.CategoryRepository.GetById(id);
        }
        public async Task<bool> Remove(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);
            _unitOfWork.CategoryRepository.Remove(category);
            return true;
        }
        public async Task<bool> Update(Category category)
        {
            var categoryTraking= await _unitOfWork.CategoryRepository.GetById(category.Id);
            categoryTraking.Name = category.Name;
            _unitOfWork.CategoryRepository.Update(categoryTraking);
            return true;
        }
    }
}
