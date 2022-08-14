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
    public interface IProductRepository
    {
        Task<PageResult<ProductsRecord>> GetAll(int pageIndex, int pageSize);
        Task<ProductsRecord> GetById(int id);
        Task<int> Add(ProductCreateRequest model);
        Task<int> Update(int id, ProductUpdateRepuest model);
        Task<int> Delete(int id);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _db;
        private readonly IStorageRepository _storageRepository;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public ProductRepository(MyDbContext db,IStorageRepository storageRepository)
        {
            _db = db;
            _storageRepository = storageRepository;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageRepository.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        } 
        public async Task<int> Add(ProductCreateRequest request)
        {
            var product = new ProductsRecord
            {
                Name = request.Name,
                CategoryID = request.CategoryID,
                Decription = request.Decription,
                Content = request.Content,
                Hot = request.Hot,
                Price = request.Price,
                Discount = request.Discount,
                Amount = request.Amount
            };
            if(request.ThumbnailImage != null)
            {
                //product.Photo = await SaveFile(request.ThumbnailImage);  trường hợp product có 1 ảnh(có trường photo)
                //product có nhiều ảnh và không sử dụng field photo trong bảng product nữa
                product.ProductImages = new List<ProductImageRecord>()
                {
                   new ProductImageRecord()
                   {
                       Caption = "Thumbnail image",
                    DateCreated = DateTime.Now,
                    FileSize = request.ThumbnailImage.Length,
                    ImagePath = await this.SaveFile(request.ThumbnailImage),
                    IsDefault = true,
                    SortOrder = 1
                   }
                };
            }
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product.ID;
        }
        
        public async Task<int> Delete(int id)
        {
            var result = await _db.Products.FindAsync(id);
            var images = _db.ProductImageRecords.Where(tbl => tbl.ProductId == id);
            foreach(var image in images)
            {
                await _storageRepository.DeleteFileAsync(image.ImagePath);
            }
            _db.Products.Remove(result);
            await _db.SaveChangesAsync();
            return result.ID;
        }

        public async Task<PageResult<ProductsRecord>> GetAll(int pageIndex, int pageSize)
        {
            var result = from p in _db.Products
                         join pi in _db.ProductImageRecords
                         on p.ID equals pi.ProductId
                         select new { p, pi };
            var totalRow = await _db.Products.CountAsync();
            var data = await result.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(x=>new ProductsRecord() {
                    ID = x.p.ID,
                    Name = x.p.Name,
                    CategoryID = x.p.CategoryID,
                    Decription = x.p.Decription,
                    Discount = x.p.Discount,
                    Hot = x.p.Hot,
                    Price = x.p.Price,
                    Amount = x.p.Amount,
                   Photo=x.pi.ImagePath
                }).ToListAsync();
            var pageResult = new PageResult<ProductsRecord>
            {
                TotalRecords = totalRow,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = data
            };
            return pageResult;
        }

        public async Task<ProductsRecord> GetById(int id)
        {
            var result = await _db.Products.FindAsync(id);
            return result;
        }

        public async Task<int> Update(int id, ProductUpdateRepuest model)
        {
            var result = await _db.Products.FindAsync(id);
            result.Name = model.Name;
           
            result.Content = model.Content;
            result.Decription = model.Decription;
         
            result.Hot = model.Hot;
         
            if(model.ThumbnailImage != null)
            {
                //result.Photo = await SaveFile(model.ThumbnailImage);
                //update theo danh sach ảnh
                var thumbnailImage = await _db.ProductImageRecords.FirstOrDefaultAsync(tbl => tbl.ProductId == model.Id);
                if(thumbnailImage != null)
                {
                    thumbnailImage.FileSize = model.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(model.ThumbnailImage);
                    _db.ProductImageRecords.Update(thumbnailImage);
                }
            }
            await _db.SaveChangesAsync();
            return result.ID;
        }
    }
}
