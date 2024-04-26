using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class UsersController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: Users/Home
        public async Task<ActionResult> Index(string q, string roleId,bool? studentStatusId, int page = 1)
        {
            var studentStatus = new Dictionary<string, bool?> { { "أختر", null } , { "تم اكمال التسجيل", true }, { "لم يتم اكمال التسجيل", false }};
            var roles = _roleManager.Roles.Select(x => x.Name).AsEnumerable();
            var userName = _userManager.GetUserName(User);
            var users = _userManager.Users;
            if(!string.IsNullOrWhiteSpace(roleId))
            {
                users = (await _userManager.GetUsersInRoleAsync(roleId)).AsQueryable();
            }
            users = users
                .Where(x => x.UserName != userName);
                //.WhereIf(x => x.UserName.Contains(q), !q.IsNullOrWhiteSpace());
            //List<string> userIds = new List<string>();
            if(!string.IsNullOrWhiteSpace(q))
            {
                users = users.Where(x => x.UserName!.Contains(q));
            }


            ViewData["roleId"] = new SelectList(roles, roleId);
            ViewBag.StudentStatus = new SelectList(studentStatus, "Value", "Key", studentStatusId);
            return View(await users.OrderBy(x => x.Id).ToListAsync());
        }

        public ActionResult Create()
        {
            var user = new ApplicationUser();
            user.AllRoles = _roleManager.Roles.ToList();
            return View(user);
        }

        [HttpPost]
        public ActionResult Create(ApplicationUser user)
        {
            if (user.RolesIds != null)
            {
                var result = _userManager.CreateAsync(user, user.Password!).Result;

                if (result.Succeeded)
                {
                    result = _userManager.AddToRolesAsync(user, user.RolesIds.ToArray()).Result;
                    return RedirectToAction("Index");

                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            else
            {
                ModelState.AddModelError("", "أختر صلاحية واحدة على الأقل");
            }
            user.AllRoles = _roleManager.Roles.ToList();

            ModelState.AddModelError("", "حدث خطأ, لم يتم الحفظ.");

            return View(user);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            user.AllRoles = _roleManager.Roles.ToList();
            user.RolesIds = await _userManager.GetRolesAsync(user);

            if (user == null)
                return NotFound();

            return View("NotExist", user);

        }
        [HttpPost]
        public async Task<ActionResult> Edit(ApplicationUser uiUsr)
        {
            var user = await _userManager.FindByIdAsync(uiUsr.Id);
            user!.UserName = uiUsr.UserName;
            user.PersonName = uiUsr.PersonName;
            user.PhoneNumber = uiUsr.PhoneNumber;
            user.Email = uiUsr.Email;
            user.Job = uiUsr.Job;
            if (uiUsr.RolesIds != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    user.AllRoles = _roleManager.Roles.Where(x => x.Id != "01").ToList();
                    await _userManager.RemoveFromRolesAsync(user, roles);
                    await _userManager.AddToRolesAsync(user, uiUsr.RolesIds.ToArray());
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            else
            {
                ModelState.AddModelError("", "أختر صلاحية واحدة على الأقل");
            }
            user.AllRoles = _roleManager.Roles.ToList();
            user.RolesIds = await _userManager.GetRolesAsync(user);

            return View(user);
        }

        public ActionResult Details(string id)
        {
            var user = _userManager.FindByIdAsync(id);
            ViewBag.Roles = _userManager.GetRolesAsync(user.Result);
            if (user == null)
                return NotFound();
            return View(user);
        }

        public async Task<ActionResult> Reset(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            ViewBag.Roles = await _userManager.GetRolesAsync(user);
            if (user == null)
                return NotFound();
            return View(user);
        }

        [HttpPost]
        [ActionName("Reset")]
        public async Task<ActionResult> ResetConfirm(string id,string password)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View(user);
        }

        //[Authorize(Roles = AppSettings.SuperadminRole)]
        public async Task<ActionResult> ChangeEnable(string id, bool isEnable)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsEnabled = isEnable;
            try
            {
                var result = await _userManager.UpdateAsync(user);
                //if (isEnable == false)
                    await _userManager.UpdateSecurityStampAsync(user);
                if (result == IdentityResult.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [ActionName("Delete")]
        //[HttpPost]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            try
            {
                await _userManager.DeleteAsync(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }


    }
}
