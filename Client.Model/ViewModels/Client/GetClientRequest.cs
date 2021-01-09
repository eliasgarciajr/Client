using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Client.Model.ViewModels
{
	public class GetClientRequest : IRequest<IActionResult>
	{
		public int Id { get; set; }
	}
	
}