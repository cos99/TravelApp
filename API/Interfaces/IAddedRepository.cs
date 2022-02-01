using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IAddedRepository
    {
        Task<UserAdd> GetUserAdd(int sourceUserId, int addedUserId);
        Task<AppUser> GetUserWithAdded(int userId);
        Task<PagedList<AddDTO>> GetUserAdded(AddParams addParams);
    }
}