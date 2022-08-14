using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using webMobile.Models;
using webMobile.ModelView;

namespace webMobile.Repositories
{
    public interface IUsersRepository
    {
        Task<PageResult<UsersRecord>> GetAll(int pageIndex, int pageSize);
        Task<UsersRecord> GetById(int id);
        Task<int> Create(UserCreateRequest model);
        Task<int> Update(int id,UserCreateRequest model);
        Task<int> Delete(int id);
        
    }
    public class UsersRepository : IUsersRepository
    {
        private readonly MyDbContext _db;
        private readonly IStorageRepository _storageRepository;
        private const string USER_CONTENT_FOLDER_NAME = "users";
        public UsersRepository(MyDbContext db, IStorageRepository storageRepository)
        {
            _db = db;
            _storageRepository = storageRepository;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFIleName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName= $"{Guid.NewGuid()}{Path.GetExtension(originalFIleName)}";
            await _storageRepository.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
        public async Task<int> Create(UserCreateRequest model)
        {
            var user = new UsersRecord
            {
                FullName = model.FullName,
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                Status = 1
            };
            if(model.Avata != null)
            {
                user.Avata = await this.SaveFile(model.Avata);
            }
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user.ID;
        }

        public async Task<int> Delete(int id)
        {
            var result = await _db.Users.FindAsync(id);
            var image = result.Avata;
            await _storageRepository.DeleteFileAsync(image);
            _db.Users.Remove(result);
            await _db.SaveChangesAsync();
            return result.ID;
        }

        public async Task<PageResult<UsersRecord>> GetAll(int pageIndex, int pageSize)
        {
            var result = from u in _db.Users select new { u };
            var totalRow = await _db.Users.CountAsync();
            var data = await result.Skip((pageIndex - 1) * pageSize).Take(pageSize). 
                        Select(x=> new UsersRecord() {
                            ID=x.u.ID,
                            FullName=x.u.FullName,
                            UserName=x.u.UserName,
                            Avata=x.u.Avata,
                            Email=x.u.Email

                        }).ToListAsync();
            var pageResult = new PageResult<UsersRecord>
            {
                TotalRecords = totalRow,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = data
            };
            return pageResult;
        }

        public async Task<UsersRecord> GetById(int id)
        {
            var result = await _db.Users.FindAsync(id);
            return result;
        }

        public async Task<int> Update(int id,UserCreateRequest model)
        {
            var user = await _db.Users.FindAsync(id);
            user.FullName = model.FullName;
            user.UserName = model.UserName;
            user.Password = model.Password;
            user.Email = model.Email;
            if(model.Avata != null)
            {
                user.Avata = await this.SaveFile(model.Avata);
            }
            await _db.SaveChangesAsync();
            return user.ID;
        }
    }
}
