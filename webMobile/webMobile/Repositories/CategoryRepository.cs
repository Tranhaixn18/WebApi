using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webMobile.Models;
using webMobile.ModelView;

namespace webMobile.Repositories
{
    public interface ICategoryRepository
    {
       Task<PageResult<CategoriesRecord>> GetAll(int pageIndex,int pageSize);
       Task<CategoriesRecord> GetById(int id);
        Task Add(CategoriesRecord model);
        Task Update(int id,CategoriesRecord model);
        Task Delete(int id);
    }
    public class CategoryRepository : ICategoryRepository
    {
        private MyDbContext _db;
        public CategoryRepository(MyDbContext db)
        {
            _db = db;
        }
        public async Task Add(CategoriesRecord model)
        {
            _db.Categories.Add(model);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            CategoriesRecord record = await _db.Categories.FindAsync(id);
            _db.Categories.Remove(record);
            await _db.SaveChangesAsync();
        }

        public async Task<PageResult<CategoriesRecord>> GetAll(int pageIndex, int pageSize)
        {

            var totalRow = await _db.Categories.CountAsync();
            List<CategoriesRecord> data = await _db.Categories.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var pageResult = new PageResult<CategoriesRecord>
            {
                TotalRecords = totalRow,
                PageIndex=pageIndex,
                PageSize=pageSize,
                Items=data
            };
            return pageResult;
           
        }

        public async Task<CategoriesRecord> GetById(int id)
        {
            CategoriesRecord record = await _db.Categories.SingleOrDefaultAsync(tbl=>tbl.Id==id);
            return record;
        }

        public async Task Update(int id, CategoriesRecord model)
        {
            CategoriesRecord record = await  _db.Categories.SingleOrDefaultAsync(tbl => tbl.Id == id);
            record.Name = model.Name;
            await _db.SaveChangesAsync();
        }
    }
}
