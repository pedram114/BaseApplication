using Pedram.Services.Services.Language.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pedram.Core.Domain.Language;
using Pedram.Core;
using System.Data.Entity;

namespace Pedram.Services.Services.Language
    {
    public class LanguageService : ILanguageService
        {
        IUnitOfWork _IUOW;
        IDbSet<Pedram.Core.Domain.Language.Language> _Language;
        IDbSet<Pedram.Core.Domain.Language.LocalResource> _LocalResource;
        IDbSet<Pedram.Core.Domain.Language.Menu> _Menu;
        public LanguageService( IUnitOfWork IUOW ) {
            _IUOW = IUOW;
            _Language = _IUOW.Set<Pedram.Core.Domain.Language.Language>();
            _LocalResource= _IUOW.Set<Pedram.Core.Domain.Language.LocalResource>();
            _Menu = _IUOW.Set<Pedram.Core.Domain.Language.Menu>();
            
        }
        public int AddLanguage( Core.Domain.Language.Language Lan )
            {
            _Language.Add( Lan );
            return _IUOW.SaveAllChanges();
             
            }
        public int UpdateLanguage( Core.Domain.Language.Language Lan )
            {

            var da = (from q in _Language
                      where q.LangName == Lan.LangName
                      select q).First();
            da.LocalResources.Clear();
            da.LocalResources = Lan.LocalResources;
           return _IUOW.SaveAllChanges();
           
            }
        public ICollection<Pedram.Core.Domain.Language.Language> GetAllLanguages()
            {
            try
                {
                var query = from q in _Language
                            select q;
                return query.ToList();
                }
            catch (Exception e) {
                return null;
                }
      
            }

        public bool LanguageExist( Pedram.Core.Domain.Language.Language Lan )
            {
            try
                {
                var query = from q in _Language
                            where q.LangName == Lan.LangName
                            select q;
                if (query.Count() > 0)
                    return true;
                }
            catch (Exception e) {
                return false;               
                }
            return false;
            }

        public Pedram.Core.Domain.Language.Language GetLanguage(string LangName) {
            var query = from q in _Language
                        where q.LangName == LangName
                        select q;
            return query.ToList().FirstOrDefault();
            }

        public Pedram.Core.Domain.Language.Language GetLanguage( int LangId )
            {
            var query = from q in _Language
                        where q.LanguageId == LangId
                        select q;
            return query.ToList().FirstOrDefault();
            }

        public Pedram.Core.Domain.Language.Language GetDefaultLanguage( )
            {
            var query = from q in _Language
                        where q.DefaultLang==true
                        select q;
            return query.ToList().FirstOrDefault();
            }
        public ICollection<Menu> GetAllMenus(){
            var query = from q in _Menu
                        select q;
            return query.ToList();


        }
   
    }
}
