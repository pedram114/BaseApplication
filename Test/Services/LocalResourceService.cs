using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using Pedram.Services.Services.Language.Interfaces;
using Pedram.Framework;
using System.Data.Entity;

namespace Test.Services
    {
    [TestClass]
    public class LocalResourceService
        {
        ILanguageService _ILanguageService;
        ILocalResourceService _ILocalResourceService;
        public LocalResourceService( ILanguageService ILanguageService , ILocalResourceService ILocalResourceService )
            {
            _ILanguageService = ILanguageService;
            _ILocalResourceService = ILocalResourceService;
            }
        [TestMethod]
        public void GetResource()
            {
            Database.SetInitializer( new MigrateDatabaseToLatestVersion<Pedram.Data.PedramDbContext, Pedram.Data.Migrations.Configuration>() );

          
            var data = _ILocalResourceService.GetAllResources( "English" );
            data = _ILocalResourceService.GetAllResources( 1 );
            var data1 = _ILocalResourceService.GetResource( 1, "Hello" );
            var data2 = _ILocalResourceService.GetResource( 1, "Hello" );



            }
        }
    }
