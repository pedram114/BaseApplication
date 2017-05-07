using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pedram.Core.Domain.Language;
using System.Collections;
using Pedram.Core.Domain.NotDbClasses.BreadCrumbs;
using Pedram.Core.Domain.NotDbClasses.Product;

namespace Pedram.Core.Domain.Users
    {
    public class UserContext : IUserContext
        {
        private Language.Language _MyLanguage;
        // private ICollection<Product.ProductGroup> _SelectedProduct;
        SelectedProducts _SelectedProduct;
        private string _SessionId;
        private string _PageTitle;
        private IList<BreadCrumbModel> _Breadcrumbs;
        private string _spliter="%%%%%%";
        public UserContext() {
            _MyLanguage = new Language.Language();

            }

        public Language.Language MyLanguage
            {
            get
                {
                return _MyLanguage;
                }

            set
                {
                _MyLanguage = value;
                }
            }
        //public ICollection<Product.ProductGroup> SelectedProduct
        //{
        //    get
        //    {
        //        return _SelectedProduct;
        //    }

        //    set
        //    {
        //        _SelectedProduct = value;
        //    }
        //}
        public string spliter {
            set {_spliter= "%%%%%%"; }
            get { return _spliter; }
        }
            public SelectedProducts SelectedProduct {
            get { return _SelectedProduct; }
            set { _SelectedProduct = value; }
            }
        public string SessionId
        {
            get
            {
                return _SessionId;
            }

            set
            {
                _SessionId = value;
            }
        }
   
        public string PageTitle
        {
            get { return _PageTitle; }
            set { _PageTitle = value; }
        }

        public IList<BreadCrumbModel> Breadcrumbs
        {
            get { return _Breadcrumbs; }
            set { _Breadcrumbs = value; }
        }
    }
    }
