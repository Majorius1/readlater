using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using ReadLater.Entities;
using ReadLater.Repository;

namespace ReadLater.Services {
	public class BookmarkService : IBookmarkService
    {
        protected IUnitOfWork _unitOfWork;

        public BookmarkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            bookmark.CreateDate = DateTime.Now;
			bookmark.AuthorId = HttpContext.Current.User.Identity.GetUserId();
            _unitOfWork.Repository<Bookmark>().Insert(bookmark);
            _unitOfWork.Save();
            return bookmark;
        }

		public void DeleteBookmark(Bookmark bookmark) {
			_unitOfWork.Repository<Bookmark>().Delete(bookmark);
			_unitOfWork.Save();
		}

		public List<Bookmark> GetAllUsersBookmarks() {
			return _unitOfWork.Repository<Bookmark>().Query()
					.OrderBy(l => l.OrderByDescending(b => b.UsageCount))
					.Get()
					.ToList();
		}

		public Bookmark GetBookmark(int id) {
			return _unitOfWork.Repository<Bookmark>().Query()
				.Filter(bookmark => bookmark.ID == id)
				.Get().FirstOrDefault();
		}

		public List<Bookmark> GetBookmarks(string category = null)
        {
			var bookmarksRepositoryQuery = _unitOfWork.Repository<Bookmark>().Query();
			var userId = HttpContext.Current.User.Identity.GetUserId();
			if (string.IsNullOrEmpty(category))
            {
                return bookmarksRepositoryQuery
					.Filter(bookmark => bookmark.AuthorId.Equals(userId))
					.OrderBy(l => l.OrderByDescending(b => b.CreateDate))
                    .Get()
                    .ToList();
            }
            else
            {
                return bookmarksRepositoryQuery
					.Filter(bookmark => bookmark.AuthorId.Equals(userId) 
						&& bookmark.Category != null && bookmark.Category.Name == category)
                    .Get()
                    .ToList();
            }
        }

		public void IncrementBookmarkUsage(Bookmark bookmark) {
			bookmark.UsageCount++;
			UpdateBookmark(bookmark);
		}

		public void UpdateBookmark(Bookmark bookmark) {
			var bookmarkRepository = _unitOfWork.Repository<Bookmark>();
			bookmarkRepository.Update(bookmark);
			_unitOfWork.Save();
		}
	}
}
