﻿namespace TaskManager.Communication.Response.UserResponse
{
    public class UserProfileResponse
    {
        public Guid Id { get; set; }  
        public string Name { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty;
    }
}
