﻿using Skeleton.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Skeleton.Data.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DrawDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommmitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
