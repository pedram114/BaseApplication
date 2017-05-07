using Pedram.Framework.Helpers;
using Pedram.Framework.StartUpClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Framework.CustomizedAttributes
    {
    public class PedramDisplayAttribute : DisplayNameAttribute
        {
        private string _ResourceName;

        public PedramDisplayAttribute(string ResourceName )
        {
            _ResourceName = ResourceName;
        }
        public override string DisplayName
            {
            get
                {
                var data= SmObjectFactory.Container.GetInstance<ILanguageHelper>().GetResource( _ResourceName );

                return data == null ? "free space" : data;

                }
            }

        }
    }
