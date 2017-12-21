using System.Collections.Generic;

namespace OS.Domain.Queries
{
    public class Query
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public IList<SortCriterium> SortCriteria { get; set; } = new List<SortCriterium>();

        public IList<FilterCriterium> FilterCriteria { get; set; } = new List<FilterCriterium>();
    }
}