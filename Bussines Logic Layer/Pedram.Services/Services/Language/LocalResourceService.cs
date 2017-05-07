using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pedram.Core.Domain.Language;
using Pedram.Core;
using System.Data.Entity;
using Pedram.Services.Services.Language.Interfaces;
using StructureMap;

namespace Pedram.Services.Services.Language
    {
    public class LocalResourceService : ILocalResourceService
        {
        private IUnitOfWork _IUOW;
        private IDbSet< Pedram.Core.Domain.Language.Language > _Language;
        private IDbSet< Pedram.Core.Domain.Language.LocalResource > _LocalResource;
        private ILanguageService _ILanguageService;
        public LocalResourceService( IUnitOfWork IUOW, ILanguageService ILanguageService ) {
            _IUOW = IUOW;
            _Language = _IUOW.Set<Core.Domain.Language.Language>();
            _LocalResource = _IUOW.Set<LocalResource>();
            _ILanguageService = ILanguageService;
       
        }
        public ICollection<LocalResource> GetAllResources( int LangId )
            {
            var Lang = _ILanguageService.GetLanguage( LangId );
            if (Lang != null) {
                var query = from q in _LocalResource
                            where q.Language.LanguageId == LangId
                            select q;
            return query.ToList();
                }
            return null;
            }
        public ICollection<LocalResource> GetAllResources( string LangName )
            {
            var Lang = _ILanguageService.GetLanguage( LangName );
            if (Lang != null)
                {
                var query = from q in _LocalResource
                            where q.Language.LangName == LangName
                            select q;
                return query.ToList();
                }
            return null;
            }
        public ICollection<LocalResource> GetAllResources()
        {
                var query = from q in _LocalResource
                            select q;
                return query.ToList();
           
        }

        public string GetResource( int LangId, string ResourceName )
            {
            var Lang = _ILanguageService.GetLanguage( LangId );
            if (Lang != null)
                {
                var query = from q in _LocalResource
                            where q.LocalName == ResourceName && q.Language.LanguageId==LangId
                            select q;
                return query.Select( x => x.LocalValue ).ToList().FirstOrDefault().ToString();
                          
                        
                }
            return null;

            }
      

        public ICollection<LocalResource> GetResource(string ResourceName)
        {
            var query = from q in _LocalResource
                        where q.LocalName == ResourceName 
                        select q;
            return query.ToList();       
        }
      
       
        public int AddLocalResurce(LocalResource model) {
            try
            {
                _LocalResource.Add(model);
                _IUOW.SaveAllChanges();
                return 1;
            }
            catch {
                return 0;
            }
        }
       
        public int UpdateLocalResource(LocalResource model) {
            _IUOW.SaveAllChanges();
            return 1;
        }
        public int DeleteLocalResurce(string Localresourcename)
        {
            try
            {
                foreach (var item in GetResource(Localresourcename))
                {
                    _LocalResource.Remove(item);
                    _IUOW.SaveAllChanges();
                }
               
                return 1;
            }
            catch
            {
                return 0;
            }
        }
       
        string ILocalResourceService.GetResource( string LangName, string ResourceName )
            {
            var Lang = _ILanguageService.GetLanguage( LangName );
            if (Lang != null)
                {
                var query = from q in _LocalResource
                            where q.LocalName == ResourceName
                            select q;
                return query.Select( x => x.LocalValue ).ToString();


                }
            return null;

            }


        }
    }
