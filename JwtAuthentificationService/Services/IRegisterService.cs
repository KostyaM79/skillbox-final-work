using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtAuthenticationService.Models;
using NotebookDatabase.Models;

namespace JwtAuthenticationService.Services
{
    public interface IRegisterService
    {
        Task<string> Register(RegisterModel model);

        Task<UserModel> RegisterAdmin(RegisterModel model);

        UserModel[] GetUsers();

        void Delete(string id);
    }
}
