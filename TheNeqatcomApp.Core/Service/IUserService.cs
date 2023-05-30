using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;

namespace TheNeqatcomApp.Core.Service
{
    public interface IUserService
    {
        List<Gpuser> GetAllUsers();
        Gpuser GetUserById(int id);
        void CreateUser(Gpuser user);
        void UpdateUser(Gpuser user);
        void DeleteUser(int id);
        string Auth(Gpuser login);
        void updatePassword(Gpuser gpuser);
        List<Followers> GetAllGpfollower(int lendId);
        void addfollower(int lendId, int loaneId);
        void DeleteFollower(int lendId, int loaneId);
    }
}
