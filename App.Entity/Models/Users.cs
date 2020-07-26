﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace App.Entity.Models
{
    public partial class Users
    {
        public Users()
        {
            Articles = new HashSet<Articles>();
            Comments = new HashSet<Comments>();
        }

        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string UserName { get; set; }
        public string UserMail { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Articles> Articles { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
    }
}