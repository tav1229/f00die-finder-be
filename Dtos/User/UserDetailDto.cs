﻿using f00die_finder_be.Common;

namespace f00die_finder_be.Dtos.User
{
    public class UserDetailDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public Role Role { get; set; }

    }
}
