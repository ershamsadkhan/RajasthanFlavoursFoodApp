﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingService.DTO.User
{
    public class UserDto
    {
        public long Userid { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string PrimaryAddress { get; set; }
		public string UserPhoneNumber { get; set; }
		public string UserEmailAddress { get; set; }
		public bool IsAdmin { get; set; }
		public String RegisterDate { get; set; }

	}
}