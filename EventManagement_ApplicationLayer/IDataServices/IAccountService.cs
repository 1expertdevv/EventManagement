using EventManagement_DomainLayer.ViewModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_ApplicationLayer.IDataServices
{
    public interface IAccountService
	{
		public Task<string> RegistrationAsync(RegisterVM user);
		public Task<string> LoginAsync(LoginVM login);

	}
}
