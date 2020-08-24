using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using ReadLater.Entities;
using ReadLater.Repository;

namespace ReadLater.Services {
	public class CategoryService : ICategoryService
    {
        protected IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Category CreateCategory(Category category)
        {
			category.AuthorId = HttpContext.Current.User.Identity.GetUserId();
            _unitOfWork.Repository<Category>().Insert(category);
            _unitOfWork.Save();
            return category;
        }

        public void UpdateCategory(Category category)
        {
            _unitOfWork.Repository<Category>().Update(category);
            _unitOfWork.Save();
        }

        public List<Category> GetCategories()
        {
			var userId = HttpContext.Current.User.Identity.GetUserId();
			return _unitOfWork.Repository<Category>().Query()
				.Filter(category => category.AuthorId.Equals(userId))
				.Get().ToList();
        }

        public Category GetCategory(int Id)
        {
			var userId = HttpContext.Current.User.Identity.GetUserId();
			return _unitOfWork.Repository<Category>().Query()
                                                    .Filter(c => c.ID == Id && c.AuthorId.Equals(userId))
                                                    .Get()
                                                    .FirstOrDefault();
        }

        public Category GetCategory(string Name)
        {
			var userId = HttpContext.Current.User.Identity.GetUserId();
			return _unitOfWork.Repository<Category>().Query()
                                        .Filter(c => c.Name == Name && c.AuthorId.Equals(userId))
                                        .Get()
                                        .FirstOrDefault();
        }

        public void DeleteCategory(Category category)
        {
            _unitOfWork.Repository<Category>().Delete(category);
            _unitOfWork.Save();
        }
    }
}
