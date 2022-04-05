using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SandBox.Data;
using SandBox.Models;

namespace SandBox.Controllers
{
    public class RolController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<AppRole> _roleManager;

        public RolController(
            ApplicationDbContext context,
            RoleManager<AppRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public async Task<IActionResult> Details(string id)
        {
            // var role = await _roleManager.FindByIdAsync(id);
            var role = await _context.Roles
                .Include(r => r.Policies)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        public IActionResult Create()
        {
            var policies = _context.Policies.ToList();
            ViewData["Policies"] = new MultiSelectList(policies, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppRole role)
        {
            var policiesIds = HttpContext.Request.Form["Policies"];
            if (ModelState.IsValid)
            {
                foreach (var policyId in policiesIds)
                {
                    var policy = _context.Policies.Find(int.Parse(policyId));
                    if (policy is not null) role.Policies.Add(policy);
                }
                await _roleManager.CreateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            var policies = _context.Policies.ToList();
            ViewData["Policies"] = new MultiSelectList(policies, "Id", "Nombre");
            return View(role);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _context.Roles
                .Include(r => r.Policies)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (role is null)
            {
                return NotFound();
            }
            var selectedPolicies = role.Policies.Select(p => p.Id.ToString()).ToArray();
            var policies = _context.Policies.ToList();
            ViewData["Policies"] = new MultiSelectList(policies, "Id", "Nombre", selectedPolicies);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, AppRole role)
        {
            var policiesIds = HttpContext.Request.Form["Policies"];
            if (ModelState.IsValid)
            {
                //var result = await _roleManager.UpdateAsync(role);
                //var policies2 = _context.Roles.First(r => r.Id == role.Id).Policies;
                 _context.Roles.Update(role);
                await _context.Entry(role).Collection(r => r.Policies).LoadAsync();
                role.Policies?.Clear();
                //if(policies2 is not null) _context.RemoveRange(policies2);
                var result2 = await _context.SaveChangesAsync();
                foreach (var policyId in policiesIds)
                {
                    var policy = await _context.Policies.FindAsync(int.Parse(policyId));
                    if (policy is not null) role.Policies?.Add(policy);
                }

                //await _context.DisposeAsync();

                await _roleManager.UpdateAsync(role);
                
                //await _roleManager.UpdateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            var selectedPolicies = role.Policies.Select(p => p.Id.ToString()).ToArray();
            var policies = _context.Policies.ToList();
            ViewData["Policies"] = new MultiSelectList(policies, "Id", "Nombre", selectedPolicies);
            return View(role);
        }
    }
}
