using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AddedRepository : IAddedRepository
    {
        private readonly DataContext _context;
        public AddedRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserAdd> GetUserAdd(int sourceUserId, int addedUserId)
        {
            return await _context.Added.FindAsync(sourceUserId, addedUserId);
        }

        public async Task<PagedList<AddDTO>> GetUserAdded(AddParams addParams)
        {
            var users = _context.Users.OrderBy(x => x.UserName).AsQueryable();
            var added = _context.Added.AsQueryable();

            if (addParams.Predicate == "added")
            {
                added = added.Where(add => add.SourceUserId == addParams.UserId);
                users = added.Select(add => add.AddedUser);
            }

            if (addParams.Predicate == "addedBy")
            {
                added = added.Where(add => add.AddedUserId == addParams.UserId);
                users = added.Select(add => add.SourceUser);
            }

            var addedUsers = users.Select(user => new AddDTO
            {
                Username = user.UserName,
                Alias = user.Alias,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
                Country = user.Country,
                Id = user.Id

            });

            return await PagedList<AddDTO>.CreateAsync(addedUsers, addParams.PageNumber, addParams.PageSize);
        }

        public async Task<AppUser> GetUserWithAdded(int userId)
        {
            return await _context.Users
                    .Include(x => x.AddedUsers)
                    .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}