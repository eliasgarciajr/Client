﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Client.Model.ViewModels
{
    public class UpdatePhoneRequest : IRequest<IActionResult>
    {
        public int Id { get; set; }
        public string NumberPhone { get; set; }
        public int ClientId { get; set; }
        public AddClientRequest Client { get; set; }
    }
}