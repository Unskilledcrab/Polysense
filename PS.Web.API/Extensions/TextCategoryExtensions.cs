using PS.Shared.Models;
using PS.Web.API.Data;
using System.Threading.Tasks;

namespace PS.Web.API.Extensions
{
    public static class TextCategoryExtensions
    {
        public static Task UpdateOrCreate(this PolysenseContext dbContext, TextCategory model)
        {
            return dbContext.UpdateOrCreate(dbContext.TextCategory, model, m => m.Id == model.Id || m.Name == model.Name);
        }
    }
}