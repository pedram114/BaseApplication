using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pedram.Core.Domain.Language;
using Pedram.Framework;
using Pedram.Services.Services.Language.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Services
    {
    [TestClass]
    public class LanguageService
        {
        [TestMethod]
        public void GetLanguage() {
            Database.SetInitializer( new MigrateDatabaseToLatestVersion<Pedram.Data.PedramDbContext, Pedram.Data.Migrations.Configuration>() );

          ////  InitialCodes.StartInitialStructureMap();
          //  ILanguageService Lang = ObjectFactory.GetInstance<ILanguageService>();

          //  var data = Lang.GetLanguage( 1 );
          //  var data1 = Lang.GetLanguage( "فارسی" );
          //  var data2 = Lang.LanguageExist( new Language() { LangName = "English" } );
            


            }

        }
    }
