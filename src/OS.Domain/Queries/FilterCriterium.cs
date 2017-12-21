namespace OS.Domain.Queries
{
    public class FilterCriterium : Criterium
    {
        public CriteriumOperator Operator { get; set; }

        public object Value { get; set; }
    }
}