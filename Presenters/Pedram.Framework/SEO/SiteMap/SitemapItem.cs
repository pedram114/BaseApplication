using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Framework.SEO.SiteMap
{
    public class SitemapItem : ISitemapItem
    {
        public SitemapItem(string url, DateTime? lastModified, ChangeFrequency? changeFrequency, float? priority,string text)
        {
            this.url = url;
            this.lastModified = lastModified;
            this.changeFrequency = changeFrequency;
            this.priority = priority;
            this.text = text;
        }

        private string url;
        private DateTime? lastModified;
        private ChangeFrequency? changeFrequency;
        private float? priority;
        private string text;
        
        public string Url
        {
            get { return this.url; }
        }

        public string Text
        {
            get { return this.text; }
        }
        public DateTime? LastModified
        {
            get { return this.lastModified; }
        }

        public ChangeFrequency? ChangeFrequency
        {
            get { return this.changeFrequency; }
        }

        public float? Priority
        {
            get { return this.priority; }
        }
        public IList<ISitemapItem> SitemapItems { set; get; }
    }
}
