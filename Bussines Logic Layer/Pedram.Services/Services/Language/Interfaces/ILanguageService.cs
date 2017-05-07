using Pedram.Core.Domain.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Services.Services.Language.Interfaces
    {
    public interface ILanguageService
        {
        int AddLanguage( Pedram.Core.Domain.Language.Language Lan );
        bool LanguageExist( Pedram.Core.Domain.Language.Language Lan );
        ICollection< Pedram.Core.Domain.Language.Language > GetAllLanguages( );
        Pedram.Core.Domain.Language.Language GetLanguage(string LangName );
        Pedram.Core.Domain.Language.Language GetLanguage( int LangId );
        int UpdateLanguage( Core.Domain.Language.Language Lan );
        Pedram.Core.Domain.Language.Language GetDefaultLanguage();
        ICollection<Menu> GetAllMenus();
        
        }
    }
