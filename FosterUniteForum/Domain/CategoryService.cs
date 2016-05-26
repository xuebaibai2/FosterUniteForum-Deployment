using FosterUniteForum.Data.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain
{
    public class CategoryService : BasicService
    {
        private Category category;
        private List<Category> categoryList;
        public CategoryService()
        {
            category = new Category();
            categoryList = new List<Category>();
        }
        public CategoryService(ForumDbContext context) : base(context) { }

        public bool AddCategory(int boardID, string categoryName, int sortOrder)
        {
            Category tempCategory = this.GetCategory(categoryName, boardID);
            if (tempCategory == null)
            {
                category.BoardId = boardID;
                category.Name = categoryName;
                category.SortOrder = sortOrder;
                context.Category.Add(category);
                context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public void UpdateCategory(Category category)
        {
            this.category = context.Category.Where(m => m.Id == category.Id).SingleOrDefault();
            this.category.Name = category.Name;
            this.category.SortOrder = category.SortOrder;
            context.SaveChangesAsync();
        }

        public int GetCategoryID(string categoryName, int boardID)
        {
            return context.Category
                .Where(m => m.Name.Equals(categoryName))
                .Where(m => m.BoardId == boardID)
                .SingleOrDefault()
                .Id;
        }

        public Category GetCategoryByID(int categoryID)
        {
            return context.Category.Where(m => m.Id == categoryID).SingleOrDefault();
        }

        public List<Category> GetCategoryList(int boardID)
        {
            return context.Category.Where(m => m.BoardId == boardID).ToList();
        }

        public Category GetCategory(string categoryName, int boardID)
        {
            return context.Category
                .Where(m => m.Name.Equals(categoryName))
                .Where(m => m.BoardId == boardID)
                .SingleOrDefault();
        }

        public void DeleteCategory(string categoryName, int boardID)
        {
            int categoryID = this.GetCategoryID(categoryName, boardID);
            context.Category.Remove(context.Category.Where(m => m.Id == categoryID).SingleOrDefault());
            context.SaveChangesAsync();
        }
        public void DeleteAllCategories(int boardID)
        {
            context.Category.RemoveRange(context.Category.Where(m => m.BoardId == boardID));
            context.SaveChangesAsync();
        }

    }
}
