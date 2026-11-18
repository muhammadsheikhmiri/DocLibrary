using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectChaptereight.BlogWeb.DataBaseLayer;
using ProjectChaptereight.BlogWeb.DataBaseLayer.Entities;

namespace ProjectChaptereight.BlogWeb.DataBaseLayer.Context
{
    public class BolgContext : DbContext
    {
        //اینجا از  Base استفاده کردیم  که بره  سراغ سازنده والد
        public BolgContext(DbContextOptions<BolgContext> options) : base(options)
        {



        }
        //تعریف تیبل ها 
        public DbSet<Category> categories { get;set;}
    public DbSet<Post> posts {get;set;}
  public DbSet<PostComment>postComments {get;set;}
  public DbSet<User> users {get;set;}


        //این کد همه جداول را میگیرد و به روابط کاری ندارد 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
