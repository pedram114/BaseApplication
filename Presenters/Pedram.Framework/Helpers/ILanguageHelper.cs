using Pedram.Core.Domain.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Framework.Helpers
    {
    public interface ILanguageHelper
        {
        string GetResource( int LangId, string ResourceName );
        string GetResource( string ResourceName );
        void ChangeLanguage( Language NewLanguage );
        Language GetCurrentLanguage(  );
 

        }
    }
