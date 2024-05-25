﻿using f00die_finder_be.Dtos.User;
using f00die_finder_be.Filters;
using f00die_finder_be.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByIdAsync(Guid userId)
        {
            var result = await _userService.GetUserByIdAsync(userId);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateDto userUpdateDto)
        {
            var result = await _userService.UpdateAsync(userUpdateDto);
            return Ok(result);
        }

        [HttpGet("my-info")]
        public async Task<IActionResult> GetMyInfoAsync()
        {
            var result = await _userService.GetMyInfoAsync();
            return Ok(result);
        }
    }
}