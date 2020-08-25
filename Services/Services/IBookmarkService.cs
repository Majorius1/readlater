using System.Collections.Generic;
using ReadLater.Entities;

namespace ReadLater.Services {
	public interface IBookmarkService
    {
        Bookmark CreateBookmark(Bookmark bookmark);
		void UpdateBookmark(Bookmark bookmark);
		void DeleteBookmark(Bookmark bookmark);
        List<Bookmark> GetBookmarks(string category = null);
		List<Bookmark> GetAllUsersBookmarks();
		Bookmark GetBookmark(int id);
		void IncrementBookmarkUsage(Bookmark bookmark);
    }
}