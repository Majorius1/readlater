using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReadLater.Entities {
	public class ApplicationUser : IdentityUser, IObjectState {
		public virtual ICollection<Category> Categories { get; set; }

		public virtual ICollection<Bookmark> Bookmarks { get; set; }

		[NotMapped]
		public ObjectState ObjectState { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager) {
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}
	}
}
