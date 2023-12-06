﻿namespace CNPM_BE.Models
{
    public class HouseholdMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HouseholdId { get; set; }
        public bool IsActive { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public string Nation { get; set; }
    }
}
