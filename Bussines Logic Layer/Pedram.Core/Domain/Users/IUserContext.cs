using Pedram.Core.Domain.NotDbClasses.BreadCrumbs;
using Pedram.Core.Domain.NotDbClasses.Product;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Core.Domain.Users
    {
    public interface IUserContext
        {
        Language.Language MyLanguage { set; get; }
        //ICollection<Product.ProductGroup> SelectedProduct { set; get; }
        SelectedProducts SelectedProduct { set; get; }
        string PageTitle { set; get; }
        IList<BreadCrumbModel> Breadcrumbs { set; get; }
        string SessionId { set; get; }
        string spliter { set; get; }
    }
    }
