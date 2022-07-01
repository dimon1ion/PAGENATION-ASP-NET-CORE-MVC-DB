using System.Collections;

namespace MW_Pagenation_MVC_DB.ModelView
{
    public class PageView
    {
        public IEnumerable Users { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
