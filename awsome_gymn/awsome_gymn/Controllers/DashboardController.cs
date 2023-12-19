using awsome_gymn.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using System.Linq; // Add this using directive
using System.Collections.Generic;
using System.Threading.Tasks;

namespace awsome_gymn.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager; // Add RoleManager

        
        public DashboardController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // Initialize RoleManager here, within the constructor
            var roleStore = new RoleStore<IdentityRole>(db);
            roleManager = new RoleManager<IdentityRole>(roleStore);
        }

        // GET: Dashboard
        [Authorize]
        public ActionResult Dashboard()
        {
           // Query for classes and execute the query with ToList()
  var classes = db.Classes.ToList();

            // Query for users and execute the query with ToList()
            var users = db.Users.ToList();

            // Count the number of users with the "Admin" role
            var userCount = users.Count(u => userManager.IsInRole(u.Id, "user"));

            // Count the number of users with the "Admin" role
            var adminCount = users.Count(u => userManager.IsInRole(u.Id, "Admin"));

            // Query for trainers and execute the query with ToList()
            var trainers = db.Trainers.ToList();

            var model = new DashboardViewModel
            {
                ClassCount = classes.Count,
                AccountCount = users.Count,
                UserCount = userCount,
                AdminCount = adminCount,
                TrainerCount = trainers.Count
            };

            return View(model);
        }








        public ActionResult attendance()
        {
            return View();
        }

        [AdminAuthorize]
        // Action to list all users
        public ActionResult UserList()
        {
            var allUsers = userManager.Users.ToList();
            var usersWithUserRole = new List<ApplicationUser>();

            foreach (var user in allUsers)
            {
                if (userManager.IsInRole(user.Id, "User"))
                {
                    usersWithUserRole.Add(user);
                }
            }

            return View(usersWithUserRole);
        }
        [AdminAuthorize]
        public ActionResult EditUser(string id)
        {
            var user = userManager.FindById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = userManager.FindById(user.Id);
                if (existingUser == null)
                {
                    return HttpNotFound();
                }

                // Update user properties
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;

                // Save changes
                var result = userManager.Update(existingUser);

                if (result.Succeeded)
                {
                    // Redirect to user list or user details page
                    return RedirectToAction("UserList");
                }
                // Handle errors if the update fails
                ModelState.AddModelError("", "Failed to update user.");
            }
            return View(user);
        }

        [AdminAuthorize]
        public ActionResult DeleteUser(string id)
        {
            var user = userManager.FindById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(string id)
        {
            var user = userManager.FindById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var result = userManager.Delete(user);
            if (result.Succeeded)
            {
                // Redirect to the user list after successful deletion
                return RedirectToAction("UserList");
            }

            // Handle errors if the deletion fails
            ModelState.AddModelError("", "Failed to delete user.");
            return View(user);
        }
        public ActionResult UserDetails(string id)
        {
            var user = userManager.FindById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        public ActionResult CreateUser()
        {
            // Fetch a list of available roles
            var roles = roleManager.Roles.ToList();
            var model = new CreateUserWithRoleViewModel
            {
                Role = string.Empty, // Initialize Role property
                Roles = new SelectList(roles, "Name", "Name")
            };
            return View(model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser(CreateUserWithRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Add the user to the selected role
                    await userManager.AddToRoleAsync(user.Id, model.Role); // Change 'Roles' to 'Role'

                    // Redirect to the user list
                    return RedirectToAction("UserList");
                }
                AddErrors(result);
            }

            // If registration fails, or if the model is invalid, redisplay the registration form
            var roles = roleManager.Roles.ToList();
            model.Roles = new SelectList(roles, "Name", "Name");
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [AdminAuthorize]
        public ActionResult AdminList()
        {
            var allUsers = userManager.Users.ToList();
            var usersWithUserRole = new List<ApplicationUser>();

            foreach (var user in allUsers)
            {
                if (userManager.IsInRole(user.Id, "admin"))
                {
                    usersWithUserRole.Add(user);
                }
            }

            return View(usersWithUserRole);
        }



        // Rest of your actions...

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userManager.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}

