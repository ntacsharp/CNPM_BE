﻿namespace CNPM_BE.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
