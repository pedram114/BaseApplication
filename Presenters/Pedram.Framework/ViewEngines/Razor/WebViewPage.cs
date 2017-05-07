using Pedram.Framework.Helpers;
using Pedram.Framework.Localization;
using Pedram.Framework.StartUpClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Pedram.Framework.ViewEngines.Razor
    {
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
        {
       
        public static ILanguageHelper _ILanguageHelper;
        private string ResourceName;
        Localizer _localizer;
        Localizer _Productlocalizer;
        public override void InitHelpers()
            {
            base.InitHelpers();
            _ILanguageHelper = SmObjectFactory.Container.GetInstance<ILanguageHelper>();
           
            }
        /// <summary>
        /// Get a localized resources
        /// </summary>
        public Localizer T
            {
            get
                {
                if (_localizer == null)
                    {
                    //null localizer
                    //_localizer = (format, args) => new LocalizedString((args == null || args.Length == 0) ? format : string.Format(format, args));

                    //default localizer
                    _localizer = ( format, args ) =>
                    {
                        var resFormat = _ILanguageHelper.GetResource( format );
                        if (string.IsNullOrEmpty( resFormat ))
                            {
                            return new LocalizedString( format );
                            }
                        return
                            new LocalizedString( (args == null || args.Length == 0)
                                                    ? resFormat
                                                    : string.Format( resFormat, args ) );
                    };
                    }
                return _localizer;
                }

            }

    
        public HelperResult RenderWrappedSection( string name, object wrapperHtmlAttributes )
            {
            Action<TextWriter> action = delegate ( TextWriter tw )
                {
                    var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes( wrapperHtmlAttributes );
                    var tagBuilder = new TagBuilder( "div" );
                    tagBuilder.MergeAttributes( htmlAttributes );

                    var section = RenderSection( name, false );
                    if (section != null)
                        {
                        tw.Write( tagBuilder.ToString( TagRenderMode.StartTag ) );
                        section.WriteTo( tw );
                        tw.Write( tagBuilder.ToString( TagRenderMode.EndTag ) );
                        }
                    };
            return new HelperResult( action );
            }

        public HelperResult RenderSection( string sectionName, Func<object, HelperResult> defaultContent )
            {
            return IsSectionDefined( sectionName ) ? RenderSection( sectionName ) : defaultContent( new object() );
            }

        public override string Layout
            {
            get
                {
                var layout = base.Layout;

                if (!string.IsNullOrEmpty( layout ))
                    {
                    var filename = Path.GetFileNameWithoutExtension( layout );
                    ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView( ViewContext.Controller.ControllerContext, filename);

                    if (viewResult.View != null && viewResult.View is RazorView)
                        {
                        layout = (viewResult.View as RazorView).ViewPath;
                        }
                    }

                return layout;
                }
            set
                {
                base.Layout = value;
                }
            }

       
        /// <summary>
        /// Gets a selected tab index (used in admin area to store selected tab index)
        /// </summary>
        /// <returns>Index</returns>
        public int GetSelectedTabIndex()
            {
            //keep this method synchornized with
            //"SetSelectedTabIndex" method of \Administration\Controllers\BaseNopController.cs
            int index = 0;
            string dataKey = "nop.selected-tab-index";
            if (ViewData[dataKey] is int)
                {
                index = (int)ViewData[dataKey];
                }
            if (TempData[dataKey] is int)
                {
                index = (int)TempData[dataKey];
                }

            //ensure it's not negative
            if (index < 0)
                index = 0;

            return index;
            }
        }

        public abstract class WebViewPage : WebViewPage<dynamic>
        {
        }
    }