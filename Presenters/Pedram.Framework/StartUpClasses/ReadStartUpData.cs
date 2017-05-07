using Pedram.Core.Domain.Language;
using Pedram.Framework.Cache;
using Pedram.Framework.Helpers;
using Pedram.Services.Services.Language.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;

namespace Pedram.Framework.StartUpClasses
    {
    public class ReadStartUpData
        {
        public static void ReadAllLanguages() {
            ILanguageService _ILanguageService = SmObjectFactory.Container.GetInstance<ILanguageService>();

            ICollection<Language> AllLanguages = new List<Language>();
            if (Directory.Exists( AppDomain.CurrentDomain.BaseDirectory + "App_Data/Languages" ))
                {
                string []Files = Directory.GetFiles( AppDomain.CurrentDomain.BaseDirectory + "App_Data/Languages" );                
                foreach (var item in Files)
                    {
                    Language Lang = new Language();

                    XmlTextReader reader = new XmlTextReader( item );
                    while (reader.Read())
                        {
                        switch (reader.NodeType)
                            {
                            case XmlNodeType.Element: // The node is an element.
                                switch (reader.Name)
                                    {
                                    case "Language":
                                        Lang.LocalResources = new List<LocalResource>();
                                        Lang.DefaultEnglishName = reader.GetAttribute( "DefaultEnglishName" );
                                        Lang.LangName = reader.GetAttribute( "CustomName" );
                                        Lang.LangCalture = reader.GetAttribute( "Calture" );
                                        Lang.LangIconAddress = reader.GetAttribute("LangIconAddress");
                                        Lang.RightToLeft = Boolean.Parse(reader.GetAttribute( "RightToLeft" ));  
                                        Lang.DefaultLang = Boolean.Parse(reader.GetAttribute("DefaultLang"));
                                        break;
                                    case "LocalString":
                                        if (Lang != null)
                                            {
                                            Lang.LocalResources.Add( new LocalResource() {LocalName= reader.GetAttribute( "LocalName" ),
                                                                                      LocalValue= reader.GetAttribute( "LocalValue" )
                                                } );

                                            }
                                        break;
                                    }
                                break;
                            case XmlNodeType.Text: //Display the text in each element.
                                break;
                            case XmlNodeType.EndElement: //Display the end of the element.
                                break;
                            }
                        }
                    AllLanguages.Add( Lang );
                    }
                }


            foreach (var item in AllLanguages)
                {
                if (item.LangName.ToLower() == "english")
                    item.DefaultLang = true;
                if (!_ILanguageService.LanguageExist(item))
                {
                    _ILanguageService.AddLanguage(item);
                   // InsertMenus(item);
                }
                else
                    _ILanguageService.UpdateLanguage(item);
                }
            

            

      
            }
        public static void ReadAllLocalResources() {
            ILocalResourceService _ILocalResourceService = SmObjectFactory.Container.GetInstance<ILocalResourceService>();
            LocalResourcesCache.LocalResource = _ILocalResourceService.GetAllResources();


        }
     
        public static void SetDefaultLanguage() {
        
            HttpCookie cookie = HttpContext.Current.Request.Cookies["DefaultLan"];
            if (cookie == null)
                {
                var _ILanguageService = SmObjectFactory.Container.GetInstance<ILanguageService>();
                var Language = _ILanguageService.GetDefaultLanguage();
                SmObjectFactory.Container.GetInstance<ILanguageHelper>().ChangeLanguage( Language );
                }


            }
        //public static void InsertMenus(Language item) {
        //    ILanguageService _ILanguageService = SmObjectFactory.Container.GetInstance<ILanguageService>();
        //    IProductService _IProductService = SmObjectFactory.Container.GetInstance<IProductService>();
        //    if (item.LangName == "English") {
        //            ICollection<ProductGroup> EngProducts = new List<ProductGroup>();
        //            ICollection<ProductAttachment> EngProductAttach = new List<ProductAttachment>();
        //            //Roll up
        //            EngProductAttach.Add(new ProductAttachment() { Name= "MOTORS",
        //                Product =new List<Product>() {
        //                    new Product() { ProductName="Central" },
        //                    new Product() {ProductName="Side" },
        //                    new Product() {ProductName="Tubular" }

        //            } });
        //            EngProductAttach.Add(new ProductAttachment() { Name = "SLAT" ,
        //                 Product = new List<Product>() {
        //                    new Product() { ProductName="ALUMINIUM" },
        //                    new Product() {ProductName="POLYCARBONATE" },
        //                    new Product() {ProductName="FOAMY" },
        //                    new Product() {ProductName="FULL PUNCH" }

        //            }
        //            });
        //            EngProductAttach.Add(new ProductAttachment() { Name = "ATTACHMENTS",
        //                 Product=new List<Product>() {
        //                    new Product() { ProductName="SHAFT" },
        //                    new Product() {ProductName="Control circuit" },
        //                    new Product() {ProductName="Brackets" },
        //                    new Product() {ProductName="Photocell" },
        //                    new Product() {ProductName="FLASHER" },
        //                    new Product() {ProductName="End Cap" },
        //                    new Product() {ProductName="Remote" },
        //                    new Product() {ProductName="Selector" }

        //            }
        //            });
        //            EngProducts.Add(new ProductGroup() { Name="Roll Up", ProductAttachment= EngProductAttach ,GroupID=1 });
        //            //Roll up

        //            //SECTIONAL
        //            EngProducts.Add(new ProductGroup() { Name = "SECTIONAL" , GroupID = 2 });
        //            //SECTIONAL

        //            //GATE
        //            ICollection<ProductAttachment> EngProductAttach1 = new List<ProductAttachment>();
        //            EngProductAttach1.Add(new ProductAttachment() { Name = "بازویی" ,
        //                Product = new List<Product>() {
        //                    new Product() { ProductName="FAAC 412" },
        //                    new Product() {ProductName="FAAC 402 CBC" },
        //                    new Product() {ProductName="FAAC 402 SBS" },
        //                    new Product() {ProductName="FAAC 422 CBAC" },
        //                    new Product() {ProductName="FAAC 400 SB" },
        //                    new Product() {ProductName="FAAC 400 SBS" },
        //                    new Product() {ProductName="FAAC 400 CBAC" },
        //                    new Product() {ProductName="DEJ 400" },
        //                    new Product() {ProductName="DEJ 600" },
        //                    new Product() {ProductName="G-BAT 300" },
        //                    new Product() {ProductName="G-BAT 400" }


        //            }
        //            });
        //            EngProductAttach1.Add(new ProductAttachment() { Name = "ریلی",
        //             Product = new List<Product>() {
        //                    new Product() { ProductName="FAAC C720" },
        //                    new Product() {ProductName="FAAC C721" },
        //                    new Product() {ProductName="FAAC C740" },
        //                    new Product() {ProductName="FAAC 741" },
        //                    new Product() {ProductName="FAAC 746" },
        //                    new Product() {ProductName="FAAC 844" },
        //                    new Product() {ProductName="FAAC 844 MC 3PH" },
        //                    new Product() {ProductName="FALCON M5" },
        //                    new Product() {ProductName="FALCON M8" },
        //                    new Product() {ProductName="FALCON M14" },
        //                    new Product() {ProductName="FORESEE 500" }


        //            }
        //            });
        //            EngProductAttach1.Add(new ProductAttachment() { Name = "زیر سطحی",
        //            Product=new List<Product>() {
        //                new Product() { ProductName="AAC 770" }
        //            } });
        //            EngProductAttach1.Add(new ProductAttachment() { Name = "پا ملخی",
        //                Product = new List<Product>() {
        //                new Product() { ProductName="FAAC 391" }
        //            }
        //            });
        //            EngProducts.Add(new ProductGroup() {Name = "GATE", ProductAttachment = EngProductAttach1, GroupID = 3 });
        //            //GATE
        //            //GLASS SLIDING
        //            ICollection<ProductAttachment> EngProductAttach2 = new List<ProductAttachment>();
        //            EngProductAttach2.Add(new ProductAttachment() { Name = "SLIDING" ,
        //                Product = new List<Product>() {
        //                    new Product() { ProductName="FAAC A100" },
        //                    new Product() {ProductName="FAAC A140" },
        //                    new Product() {ProductName="D/JEK" }
        //            }
        //            });
        //            EngProductAttach2.Add(new ProductAttachment() { Name = "TELESCOP" ,
        //                Product = new List<Product>() {
        //                    new Product() { ProductName="FAAC A140T" },
        //                    new Product() {ProductName="D/JEK" }                            
        //            }
        //            });
        //            EngProductAttach2.Add(new ProductAttachment() { Name = "خمیده" ,
        //                Product = new List<Product>() {
        //                    new Product() { ProductName="RECORD" }
        //            }
        //            });                    
        //            EngProducts.Add(new ProductGroup() { Name = "GLASS SLIDING", ProductAttachment = EngProductAttach2, GroupID = 4 });
        //            //GLASS SLIDING
        //            //BARRIERS
        //            EngProducts.Add(new ProductGroup() { Name = "BARRIERS" , GroupID = 5 });
        //            //BARRIERS
        //            //BMS
        //            ICollection<ProductAttachment> EngProductAttach3 = new List<ProductAttachment>();
        //            EngProductAttach3.Add(new ProductAttachment() { Name = "TOCH PANEL" });
        //            EngProductAttach3.Add(new ProductAttachment() { Name = "SMS MODULE" });
        //            EngProductAttach3.Add(new ProductAttachment() { Name = "REMOTE" });
        //            EngProductAttach3.Add(new ProductAttachment() { Name = "KEYS" });
        //            EngProductAttach3.Add(new ProductAttachment() { Name = "SOUND SYSTEM" });
        //            EngProducts.Add(new ProductGroup() { Name = "BMS", ProductAttachment = EngProductAttach3 , GroupID = 6});
        //        //BMS
        //        //CAMRAS
        //        ICollection<ProductAttachment> EngProductAttach4 = new List<ProductAttachment>();
        //        EngProductAttach4.Add(new ProductAttachment() { Name = "SONY" ,
        //            Product = new List<Product>() {
        //                    new Product() { ProductName="SPEED DOME" },
        //                    new Product() {ProductName="BOX" },
        //                    new Product() {ProductName="BULET" },
        //                    new Product() {ProductName="DOME" },
        //                    new Product() {ProductName="SPY" }
        //            }
        //        });
        //        EngProductAttach4.Add(new ProductAttachment() { Name = "SAMSUNG",
        //            Product = new List<Product>() {
        //                    new Product() { ProductName="SPEED DOME" },
        //                    new Product() {ProductName="BOX" },
        //                    new Product() {ProductName="BULET" },
        //                    new Product() {ProductName="DOME" },
        //                    new Product() {ProductName="SPY" }
        //            }
        //        });
        //        EngProductAttach4.Add(new ProductAttachment() { Name = "SPY TECH",
        //            Product = new List<Product>() {
        //                    new Product() { ProductName="SPEED DOME" },
        //                    new Product() {ProductName="BOX" },
        //                    new Product() {ProductName="BULET" },
        //                    new Product() {ProductName="DOME" },
        //                    new Product() {ProductName="SPY" }
        //            }
        //        });
        //        EngProductAttach4.Add(new ProductAttachment() { Name = "CNB",
        //            Product = new List<Product>() {
        //                    new Product() { ProductName="SPEED DOME" },
        //                    new Product() {ProductName="BOX" },
        //                    new Product() {ProductName="BULET" },
        //                    new Product() {ProductName="DOME" },
        //                    new Product() {ProductName="SPY" }
        //            }
        //        });
        //        EngProductAttach4.Add(new ProductAttachment() { Name = "Z-VIEW",
        //            Product = new List<Product>() {
        //                    new Product() { ProductName="SPEED DOME" },
        //                    new Product() {ProductName="BOX" },
        //                    new Product() {ProductName="BULET" },
        //                    new Product() {ProductName="DOME" },
        //                    new Product() {ProductName="SPY" }
        //            }
        //        });
        //        EngProducts.Add(new ProductGroup() { Name = "CAMMERA" , ProductAttachment= EngProductAttach4, GroupID = 7 });
        //        //CAMRAS
        //        foreach (var item1 in EngProducts)
        //        {
        //           _IProductService.AddProductGroup(item1);
                
        //        }
        //        var model = _IProductService.GetAllProductGroup();
        //        var resources = new List<LocalResource>();
                
        //        foreach (var item1 in model)
        //        {
        //            resources.Add(new LocalResource() { LocalName = "Pedram.Product.ProductGroup"+item1.ID, LocalValue = item1.Name });
        //            foreach (var item2 in item1.ProductAttachment)
        //            {
        //                resources.Add(new LocalResource() { LocalName = "Pedram.Product.ProductAttachment" + item2.ID, LocalValue = item2.Name });
        //                foreach (var item3 in item2.Product)
        //                {
        //                    resources.Add(new LocalResource() { LocalName = "Pedram.Product.Product" + item3.ID, LocalValue = item3.ProductName });

        //                }

        //            }

        //        }



        //    }






        //    else if (item.LangName == "فارسی")
        //        {
        //            ICollection<ProductGroup> EngProducts = new List<ProductGroup>();
        //            ICollection<ProductAttachment> EngProductAttach = new List<ProductAttachment>();
        //            //Roll up
        //            EngProductAttach.Add(new ProductAttachment()
        //            {
        //                Name = "موتور",
        //                Product = new List<Product>() {
        //                    new Product() { ProductName="سنترال" },
        //                    new Product() {ProductName="ساید" },
        //                    new Product() {ProductName="تیوبلار" }
        //                }
        //            });
        //            EngProductAttach.Add(new ProductAttachment() { Name = "تیغه",
        //                Product = new List<Product>() {
        //                    new Product() { ProductName="آلومینیوم" },
        //                    new Product() {ProductName="پلی کربنات" },
        //                    new Product() {ProductName="فوم دار" },
        //                    new Product() {ProductName="فول پانچ" }

        //            }
        //            });
        //            EngProductAttach.Add(new ProductAttachment() { Name = "ملحقات",
        //                Product = new List<Product>() {
        //                    new Product() { ProductName="شفت" },
        //                    new Product() {ProductName="مدار فرمان" },
        //                    new Product() {ProductName="براکت" },
        //                    new Product() {ProductName="چشمی" },
        //                    new Product() {ProductName="فلاشر" },
        //                    new Product() {ProductName="اندکپ یاتاقان" },
        //                    new Product() {ProductName="ریموت" },
        //                    new Product() {ProductName="سلکتور" }

        //            }
        //            });
        //            EngProducts.Add(new ProductGroup() { Language = item, Name = "کرکره", ProductAttachment = EngProductAttach , GroupID = 1 });
        //            //Roll up

        //            //SECTIONAL
        //            EngProducts.Add(new ProductGroup() { Language = item, Name = "سکشنال" , GroupID =2  });
        //            //SECTIONAL

        //            //GATE
        //            ICollection<ProductAttachment> EngProductAttach1 = new List<ProductAttachment>();
        //            EngProductAttach1.Add(new ProductAttachment() { Name = "بازویی",
        //                Product = new List<Product>() {
        //                    new Product() { ProductName="FAAC 412" },
        //                    new Product() {ProductName="FAAC 402 CBC" },
        //                    new Product() {ProductName="FAAC 402 SBS" },
        //                    new Product() {ProductName="FAAC 422 CBAC" },
        //                    new Product() {ProductName="FAAC 400 SB" },
        //                    new Product() {ProductName="FAAC 400 SBS" },
        //                    new Product() {ProductName="FAAC 400 CBAC" },
        //                    new Product() {ProductName="DEJ 400" },
        //                    new Product() {ProductName="DEJ 600" },
        //                    new Product() {ProductName="G-BAT 300" },
        //                    new Product() {ProductName="G-BAT 400" }


        //            }
        //            });
        //            EngProductAttach1.Add(new ProductAttachment() { Name = "ریلی",
        //                Product = new List<Product>() {
        //                    new Product() { ProductName="FAAC C720" },
        //                    new Product() {ProductName="FAAC C721" },
        //                    new Product() {ProductName="FAAC C740" },
        //                    new Product() {ProductName="FAAC 741" },
        //                    new Product() {ProductName="FAAC 746" },
        //                    new Product() {ProductName="FAAC 844" },
        //                    new Product() {ProductName="FAAC 844 MC 3PH" },
        //                    new Product() {ProductName="FALCON M5" },
        //                    new Product() {ProductName="FALCON M8" },
        //                    new Product() {ProductName="FALCON M14" },
        //                    new Product() {ProductName="FORESEE 500" }


        //            }
        //            });
        //            EngProductAttach1.Add(new ProductAttachment() { Name = "زیر سطحی",
        //                Product = new List<Product>() {
        //                new Product() { ProductName="AAC 770" }
        //            }
        //            });
        //            EngProductAttach1.Add(new ProductAttachment() { Name = "پا ملخی",
        //                Product = new List<Product>() {
        //                new Product() { ProductName="FAAC 391" }
        //            }
        //            });
        //            EngProducts.Add(new ProductGroup() { Language = item, Name = "جک پارکینگی", ProductAttachment = EngProductAttach1 , GroupID = 3});
        //            //GATE
        //            //GLASS SLIDING
        //            ICollection<ProductAttachment> EngProductAttach2 = new List<ProductAttachment>();
        //            EngProductAttach2.Add(new ProductAttachment() { Name = "اسلایدینگ",
        //                Product = new List<Product>() {
        //                    new Product() { ProductName="FAAC A100" },
        //                    new Product() {ProductName="FAAC A140" },
        //                    new Product() {ProductName="D/JEK" }
        //            }
        //            });
        //            EngProductAttach2.Add(new ProductAttachment() { Name = "تلسکوپی",
        //                Product = new List<Product>() {
        //                    new Product() { ProductName="FAAC A140T" },
        //                    new Product() {ProductName="D/JEK" }
        //            }
        //            });
        //            EngProductAttach2.Add(new ProductAttachment() { Name = "خمیده",
        //                Product = new List<Product>() {
        //                    new Product() { ProductName="RECORD" }
        //            }
        //            });
        //            EngProducts.Add(new ProductGroup() { Language = item, Name = "درب شیشه ای", ProductAttachment = EngProductAttach2 , GroupID = 4});
        //            //GLASS SLIDING
        //            //BARRIERS
        //            EngProducts.Add(new ProductGroup() { Language = item, Name = "راهبند" , GroupID = 5});
        //            //BARRIERS
        //            //BMS
        //            ICollection<ProductAttachment> EngProductAttach3 = new List<ProductAttachment>();
        //            EngProductAttach3.Add(new ProductAttachment() { Name = "پنل لمسی" });
        //            EngProductAttach3.Add(new ProductAttachment() { Name = "ماژول اس ام اس" });
        //            EngProductAttach3.Add(new ProductAttachment() { Name = "کنترل" });
        //            EngProductAttach3.Add(new ProductAttachment() { Name = "کلید ها" });
        //            EngProductAttach3.Add(new ProductAttachment() { Name = "سیستم صدا" });
        //            EngProducts.Add(new ProductGroup() { Language = item, Name = "خانه هوشمند", ProductAttachment = EngProductAttach3 , GroupID = 6});
        //        //BMS
        //        //CAMRAS
        //        ICollection<ProductAttachment> EngProductAttach4 = new List<ProductAttachment>();
        //        EngProductAttach4.Add(new ProductAttachment()
        //        {
        //            Name = "سونی",
        //            Product = new List<Product>() {
        //                    new Product() { ProductName="اسپید دام" },
        //                    new Product() {ProductName="صنعتی" },
        //                    new Product() {ProductName="بولت" },
        //                    new Product() {ProductName="دام" },
        //                    new Product() {ProductName="جاسوسی" }
        //            }
        //        });
        //        EngProductAttach4.Add(new ProductAttachment()
        //        {
        //            Name = "سامسونگ",
        //            Product = new List<Product>() {
        //                    new Product() { ProductName="اسپید دام" },
        //                    new Product() {ProductName="صنعتی" },
        //                    new Product() {ProductName="بولت" },
        //                    new Product() {ProductName="دام" },
        //                    new Product() {ProductName="جاسوسی" }
        //            }
        //        });
        //        EngProductAttach4.Add(new ProductAttachment()
        //        {
        //            Name = "اسپای تک",
        //            Product = new List<Product>() {
        //                    new Product() { ProductName="اسپید دام" },
        //                    new Product() {ProductName="صنعتی" },
        //                    new Product() {ProductName="بولت" },
        //                    new Product() {ProductName="دام" },
        //                    new Product() {ProductName="جاسوسی" }
        //            }
        //        });
        //        EngProductAttach4.Add(new ProductAttachment()
        //        {
        //            Name = "سی ان بی",
        //            Product = new List<Product>() {
        //                    new Product() { ProductName="اسپید دام" },
        //                    new Product() {ProductName="صنعتی" },
        //                    new Product() {ProductName="بولت" },
        //                    new Product() {ProductName="دام" },
        //                    new Product() {ProductName="جاسوسی" }
        //            }
        //        });
        //        EngProductAttach4.Add(new ProductAttachment()
        //        {
        //            Name = "زد-ویو",
        //            Product = new List<Product>() {
        //                    new Product() { ProductName="اسپید دام" },
        //                    new Product() {ProductName="صنعتی" },
        //                    new Product() {ProductName="بولت" },
        //                    new Product() {ProductName="دام" },
        //                    new Product() {ProductName="جاسوسی" }
        //            }
        //        });
        //        EngProducts.Add(new ProductGroup() { Language = item, Name = "دوربین" , ProductAttachment= EngProductAttach4, GroupID = 7 });
        //            //CAMRAS
        //            item.ProductGroup = EngProducts;
        //            int Res = _ILanguageService.UpdateLanguage(item);


        //        }
            

        //}
    }
    }
