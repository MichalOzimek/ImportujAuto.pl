using ImportCars.Data;
using ImportCars.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace ImportCars.Areas.Admin;

public static class Helper
{
    public static async Task<List<Images>> SavePhotosAsync(int imageId, List<IFormFile> photos, string imageType, string entityIdPropertyName, Context _context)
    {
        List<Images> savedPhotos = new List<Images>();
        if (photos != null)
        {
            string dictionaryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", imageType);

            // Sprawdź, czy katalog istnieje, jeśli nie, utwórz go
            if (!Directory.Exists(dictionaryPath))
            {
                Directory.CreateDirectory(dictionaryPath);
            }

            foreach (var formFile in photos)
            {
                Random random = new Random();
                string randomString = new string(Enumerable.Range(0, 10).Select(i => (char)('a' + random.Next(26))).ToArray());
                string? formFileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName?.Trim('"');
                string filePath = $"{imageType}_{imageId}_image_{randomString}{Path.GetExtension(formFileName)}";
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", imageType, filePath);

                using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                // Ustawianie właściwości dynamicznie na podstawie przekazanej nazwy właściwości
                var propertyInfo = typeof(Images).GetProperty(entityIdPropertyName);
                if (propertyInfo != null)
                {
                    Images photo = new Images($"~/img/{imageType}/{filePath}");
                    propertyInfo.SetValue(photo, imageId);

                    _context.Images.Add(photo);
                    await _context.SaveChangesAsync();

                    savedPhotos.Add(photo);
                }
                else
                {
                    throw new InvalidCastException("Error while saving photo path to database");
                }
            }
        }
        return savedPhotos;
    }

    public static async Task RemovePhotosAsync<T>(DbContext context, string selectedPhotosIds, string imageType, Expression<Func<T, int?>> entityIdProperty)
    where T : class
    {
        if (!string.IsNullOrEmpty(selectedPhotosIds))
        {
            var selectedIdArray = selectedPhotosIds.Split(',').Select(x => int.Parse(x)).ToArray();
            foreach (var itemId in selectedIdArray)
            {
                var photos = await context.Set<T>().FindAsync(itemId);
                if (photos != null)
                {
                    var entityId = entityIdProperty.Compile()(photos);
                    var pathProperty = typeof(T).GetProperty("Path");

                    if (pathProperty != null)
                    {
                        string? filePath = pathProperty?.GetValue(photos)?.ToString();
                        FileInfo file = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot") + filePath?.Replace("~", ""));
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }

                    context.Set<T>().Remove(photos);
                }
            }
            await context.SaveChangesAsync();
        }
    }


}
