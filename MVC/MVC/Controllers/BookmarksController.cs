using ReadLater.Entities;
using ReadLater.Services;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace MVC.Controllers {
	[Authorize]
	public class BookmarksController : Controller {
		private readonly ICategoryService _categoryService;
		private readonly IBookmarkService _bookmarkService;

		public BookmarksController(ICategoryService categoryService, IBookmarkService bookmarkService) {
			_categoryService = categoryService;
			_bookmarkService = bookmarkService;
		}

		public ActionResult Index() {
			List<Bookmark> model = _bookmarkService.GetBookmarks();
			return View(model);
		}

		public ActionResult Details(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Bookmark bookmark = _bookmarkService.GetBookmark(id.Value);
			if (bookmark == null) {
				return HttpNotFound();
			}

			return View(bookmark);

		}

		private void InitializaCategoriesSelectList() {
			var categories = _categoryService.GetCategories();
			var categoriesSelectList = new SelectList(categories, "ID", "Name");
			ViewBag.CategoriesSelectList = categoriesSelectList;
		}

		[HttpGet]
		public ActionResult Create() {
			InitializaCategoriesSelectList();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Bookmark bookmark) {
			if (ModelState.IsValid) {
				_bookmarkService.CreateBookmark(bookmark);
				return RedirectToAction("Index");
			}

			return View(bookmark);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateCategoryWithoutReload(Category category) {
			if (ModelState.IsValid) {
				_categoryService.CreateCategory(category);
				var categories = _categoryService.GetCategories();
				return PartialView("CategoriesDropdown", new SelectList(categories, "ID", "Name"));
			}

			return View(category);
		}

		public ActionResult Edit(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Bookmark bookmark = _bookmarkService.GetBookmark(id.Value);
			if (bookmark == null) {
				return HttpNotFound();
			}

			InitializaCategoriesSelectList();
			return View(bookmark);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Bookmark bookmark) {
			if (ModelState.IsValid) {
				_bookmarkService.UpdateBookmark(bookmark);
				return RedirectToAction("Index");
			}

			return View(bookmark);
		}

		public ActionResult Delete(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Bookmark bookmark = _bookmarkService.GetBookmark(id.Value);
			if (bookmark == null) {
				return HttpNotFound();
			}

			return View(bookmark);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id) {
			Bookmark bookmark = _bookmarkService.GetBookmark(id);
			_bookmarkService.DeleteBookmark(bookmark);
			return RedirectToAction("Index");
		}

		[HttpPost]
		[AllowAnonymous]
		public ActionResult IncrementBookmarkUsage(int id) {
			var bookmark = _bookmarkService.GetBookmark(id);
			if (bookmark == null) {
				return HttpNotFound();
			}

			_bookmarkService.IncrementBookmarkUsage(bookmark);
			return Redirect(bookmark.URL);
		}
	}
}