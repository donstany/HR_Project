using CDN.Core3.Data.Contracts;
using CDN.Core3.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOWebFramework.Infrastructure.Data
{
    public class CdnDbContext : DbContext, ICdnContext
    {
        public CdnDbContext(DbContextOptions<CdnDbContext> options)
                        : base(options)
        {
        }

        public DbSet<CdnFile> CdnFiles { get; set; }
        public DbSet<CdnFileContent> CdnFileContents { get; set; }
    }
}
