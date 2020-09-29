using System.Collections.Generic;
using Models;

namespace WebAppClient.ViewModels
{
    public class UsersVM
    {
        public UsersVM()
        {
            lstUser = new List<User>();
        }
        public IEnumerable<User> lstUser { get; set; }

        public string TextSearch { get; set; }
    }
}
