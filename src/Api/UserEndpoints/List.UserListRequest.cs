﻿using System;

namespace Api.UserEndpoints
{
    public class UserListRequest
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
    }
}
