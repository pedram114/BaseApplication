using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Script.Serialization;


namespace Pedram.Framework.Theme
    {
    public static class MvcTheme
        {

        public static string ThemeName { get; }
        public static string ThemePath { get; set; }
        private const string AppSettingName = "MvcTheme";
        private const string ThemeBundleFileName = "ThemeBundle.json";

        static MvcTheme()
            {
            ThemePath = "~/Themes";
            ThemeName = WebConfigurationManager.AppSettings[AppSettingName];
            }

        public static void Themeable( this VirtualPathProviderViewEngine engine )
            {
            if (string.IsNullOrEmpty( ThemeName ))
                return;

            var themeFolder = HttpContext.Current.Server.MapPath( string.Format( "{0}/{1}/", ThemePath, ThemeName ) );
            if (!Directory.Exists( themeFolder ))
                throw new DirectoryNotFoundException( string.Format( "Theme folder not exists: {0}/{1}}", ThemePath,
                    ThemeName ) );

            var newViewLocations = new[]
            {
                string.Format("{0}/{1}/Views/{2}/{3}.cshtml", ThemePath, ThemeName, "{1}", "{0}"),
                string.Format("{0}/{1}/Views/Shared/{2}.cshtml", ThemePath, ThemeName, "{0}"),

                // vb.net :
                // string.Format("{0}/{1}/Views/{2}/{3}.vbhtml", ThemePath, ThemeName, "{1}", "{0}"),
                // string.Format("{0}/{1}/Views/Shared/{2}.vbhtml", ThemePath, ThemeName, "{0}"),

            };
            engine.ViewLocationFormats = newViewLocations;
            engine.PartialViewLocationFormats = newViewLocations;
            }

        public static void RegisterThemeBundels( BundleCollection bundles )
            {
            if (ThemeName == null)
                return;

            var list = ReadThemeBundles();

            foreach (var themeBundle in list)
                {
                switch (themeBundle.BundleType)
                    {
                    case BundleType.Script:
                        bundles.Add( new ScriptBundle( themeBundle.VirtualPath ).Include(
                            themeBundle.Urls ) );
                        break;
                    case BundleType.Style:
                        bundles.Add( new StyleBundle( themeBundle.VirtualPath ).Include(
                            themeBundle.Urls ) );
                        break;
                    default:
                        throw new ArgumentOutOfRangeException( nameof( themeBundle.BundleType ) );
                    }
                }
            }

        public static List<ThemeBundle> ReadThemeBundles()
            {
            try
                {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var jsonaddress =
                    System.Web.HttpContext.Current.Server.MapPath( string.Format( "{0}/{1}/{2}", ThemePath, ThemeName, ThemeBundleFileName ) );
                var json = System.IO.File.ReadAllText( jsonaddress );
                var list = jss.Deserialize<List<ThemeBundle>>( json );

                return list;
                }
            catch (Exception ex)
                {
                throw new Exception( string.Format( "Cannot read {0}. see more error in inner exception.", ThemeBundleFileName ), ex );
                }
            }
        }

    public class ThemeBundle
        {
        public BundleType BundleType { get; set; }
        public string VirtualPath { get; set; }
        public string[] Urls { get; set; }
        }

    public enum BundleType
        {
        Style, Script
        }
    }
