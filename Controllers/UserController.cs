﻿using f00die_finder_be.Common;
using f00die_finder_be.Dtos.User;
using f00die_finder_be.Filters;
using f00die_finder_be.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [AuthorizeFilterAttribute([Role.Admin])]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByIdAsync(Guid userId)
        {
            var result = await _userService.GetUserByIdAsync(userId);
            return Ok(result);
        }

        [AuthorizeFilterAttribute]
        [HttpPut]
        public async Task<IActionResult> UpdateMyInfoAsync([FromBody] UserUpdateDto userUpdateDto)
        {
            var result = await _userService.UpdateMyInfoAsync(userUpdateDto);
            return Ok(result);
        }

        [AuthorizeFilterAttribute]
        [HttpGet("my-info")]
        public async Task<IActionResult> GetMyInfoAsync()
        {
            var result = await _userService.GetMyInfoAsync();
            return Ok(result);
        }

        [AuthorizeFilterAttribute([Role.Admin])]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsersAsync([FromQuery] FilterUserAdminDto? filter, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var result = await _userService.GetUsersGetRestaurantsAdminAsync(filter, pageSize, pageNumber);
            return Ok(result);
        }

        [AuthorizeFilterAttribute([Role.Admin])]
        [HttpPut("status/{userId}")]
        public async Task<IActionResult> ChangeUserStatusAdminAsync(Guid userId, [FromQuery] UserStatus status)
        {
            var result = await _userService.ChangeUserStatusAdminAsync(userId, status);
            return Ok(result);
        }
    }
}
