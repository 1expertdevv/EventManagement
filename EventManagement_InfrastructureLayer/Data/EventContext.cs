using EventManagement_DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_InfrastructureLayer.Data
{
	public class EventContext: IdentityDbContext<ApplicationUser>
	{
		public EventContext(DbContextOptions<EventContext> option):base(option)
		{
		}

		public DbSet<Event> Events { get; set; }
		public DbSet<RegisterEvent> RegisterEvents { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
	}
}
