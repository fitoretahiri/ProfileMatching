using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileMatching.Configurations;
using ProfileMatching.Models;
using ProfileMatching.Models.DTOs;
using ProfileMatching.ProfileMatchLayer.Documents;
using System.Security.Claims;
using System.Xml.Linq;

namespace ProfileMatching.ProfileMatchLayer.Users
{
    public class UserService : ControllerBase, IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        public UserService(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _dbContext = context;
        }
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }
        public async Task<ActionResult<IEnumerable<Recruiter>>> GetRecruiters()
        {
            var recruiters = await _userManager.Users.ToListAsync();
            return recruiters.OfType<Recruiter>().ToList();
        }
        public async Task<ActionResult<AppUser>> GetUserById(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return user;
            }
            public async Task<ActionResult<AppUser>> UpdateUser(string id, AppUser user)
            {
                // This method is returning concurrency error
                /*            
                *      if (id != user.Id)
                            {
                                return BadRequest();
                            }
                            var result = await _userManager.UpdateAsync(user);
                            if (!result.Succeeded)
                            {
                                return BadRequest(result.Errors);
                            }
                            return NoContent();
                */


        // This method returns null, however works in the AccountController getCurrentUser()
        // This should be the right way
        // var dbUser = await _getUser.GetCurrentUser();

                var dbUser = await _userManager.FindByIdAsync(id);

                if (dbUser == null) return BadRequest("User not found!");

                dbUser.Name = IsNullOrEmpty(user.Name) ? dbUser.Name : user.Name;
                dbUser.Surname = IsNullOrEmpty(user.Surname) ? dbUser.Surname : user.Surname;
                dbUser.UserName = IsNullOrEmpty(user.UserName) ? dbUser.UserName : user.UserName;

                await _dbContext.SaveChangesAsync();

                return Ok(dbUser);
            }

            private bool IsNullOrEmpty(string name)
            {
            return name == null || name == String.Empty;
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteUser(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return NoContent();
      }
    }
}