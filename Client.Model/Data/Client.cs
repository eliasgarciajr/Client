using Client.Model.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Client.Model.Data
{
    public class AClient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateBirth { get; set; }
        public EEducationType EEducationType { get; set; }
        public string Password { get; set; }
        public IList<Phone> Phones { get; set; } = new List<Phone>();

    }
}