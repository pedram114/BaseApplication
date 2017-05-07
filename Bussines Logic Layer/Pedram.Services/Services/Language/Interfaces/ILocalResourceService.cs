using Pedram.Core.Domain.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Services.Services.Language.Interfaces
    {
    public interface ILocalResourceService
        {
        string GetResource( int LangId, string ResourceName );
        string GetResource( string LangName, string ResourceName );
        ICollection<LocalResource> GetAllResources( int LangId );
        ICollection<LocalResource> GetAllResources( string LangName );
        ICollection<LocalResource> GetAllResources();
        int AddLocalResurce(LocalResource model);
        ICollection<LocalResource> GetResource(string ResourceName);
        int DeleteLocalResurce(string Localresourcename);
        
        int UpdateLocalResource(LocalResource model);
        }
    }
