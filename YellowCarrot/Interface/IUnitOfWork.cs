﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Data;
using YellowCarrot.Repositories;

namespace YellowCarrot.Interface
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRecipeRepository Recipes { get; set; }
        public IIngredientRepository Ingredients { get; set; }
        public ITagRepository Tags { get; set; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Recipes = new RecipeRepository(_context);
            Tags = new TagRepository(_context);
            Ingredients = new IngredientRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();

        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }


    public interface IUnitOfWork : IDisposable
    {
        IIngredientRepository Ingredients { get; }
        IRecipeRepository Recipes { get; }
        ITagRepository Tags { get; }
        int Complete();
    }
}
