using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using API.Entities;
using API.DTOs;
using API.Helpers;

namespace API.Controllers
{
    [Authorize]
    public class AddedController : BaseApiController
    {

        private readonly IUserRepository _userRepository;
        private readonly IAddedRepository _addedRepository;
        public AddedController(IUserRepository userRepository, IAddedRepository addedRepository)
        {
            _addedRepository = addedRepository;
            _userRepository = userRepository;

        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddAdd(string username)
        {
            var sourceUserId = User.GetUserId();
            var addedUser = await _userRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _addedRepository.GetUserWithAdded(sourceUserId);

            if (addedUser == null) return NotFound();

            if (sourceUser.UserName == username) return BadRequest("You cannot Add yourself");

            var userAdd = await _addedRepository.GetUserAdd(sourceUserId, addedUser.Id);

            if (userAdd != null) return BadRequest("You already added this user");

            userAdd = new UserAdd
            {
                SourceUserId = sourceUserId,
                AddedUserId = addedUser.Id
            };

            sourceUser.AddedUsers.Add(userAdd);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to add user");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddDTO>>> GetUserAdded([FromQuery] AddParams addParams)
        {
            addParams.UserId = User.GetUserId();
            var users = await _addedRepository.GetUserAdded(addParams);
            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(users);
        }
    }
}