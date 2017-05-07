using Microsoft.AspNet.Identity.EntityFramework;
using Pedram.Core;
using Pedram.Core.Domain.Indexes;
using Pedram.Core.Domain.Language;
using Pedram.Core.Domain.Management;
using Pedram.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Data
    {
    public class PedramDbContext :IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
        ,IUnitOfWork
        {
        public PedramDbContext() : base( "name=PedramConnectionStrings" )
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }


        public DbSet<Language> Language { set; get; }

        public DbSet<LocalResource> LocalString { set; get; }
        public DbSet<RoleAccess> RoleAccesses { set; get; }

        public DbSet<Configuration> Configurations { set; get; }

        public DbSet<ProductIndex> ProductIndex { set; get; }
        public DbSet<SubProductIndex> SubProductIndex { set; get; }
        public DbSet<ProductGroupIndex> ProductGroupIndex { set; get; }



        //public DbSet<Address> Addresses { set; get; }
        //public DbSet<ApplicationUser> ApplicationUsers { set; get; }
        //public DbSet<CustomRole> CustomRoles { set; get; }
        //public DbSet<CustomUserClaim> CustomUserClaims { set; get; }
        //public DbSet<CustomUserLogin> CustomUserLogins { set; get; }
        //public DbSet<CustomUserRole> CustomUserRoles { set; get; }



        protected override void OnModelCreating( DbModelBuilder builder )
            {
            base.OnModelCreating( builder );

        //    builder.Entity<ApplicationUser>().ToTable( "Users" );
          //  builder.Entity<CustomRole>().ToTable( "Roles" );
          //  builder.Entity<CustomUserClaim>().ToTable( "UserClaims" );
           // builder.Entity<CustomUserRole>().ToTable( "UserRoles" );
         //   builder.Entity<CustomUserLogin>().ToTable( "UserLogins" );
            }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
            {
            return base.Set<TEntity>();
            }

        public int SaveAllChanges()
            {
            return base.SaveChanges();
            }

        public IEnumerable<TEntity> AddThisRange<TEntity>( IEnumerable<TEntity> entities ) where TEntity : class
            {
            return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange( entities );
            }

        public void MarkAsChanged<TEntity>( TEntity entity ) where TEntity : class
            {
            Entry( entity ).State = EntityState.Modified;
            }

        public IList<T> GetRows<T>( string sql, params object[] parameters ) where T : class
            {
            return Database.SqlQuery<T>( sql, parameters ).ToList();
            }

        public void ForceDatabaseInitialize()
            {
            this.Database.Initialize( force: true );
            }



        }
   

    }
