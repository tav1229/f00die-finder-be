namespace f00die_finder_be.Dtos.SemanticSearch
{
    public class SemanticSearchResult
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int IdType { get; set; }
        public double Score { get; set; }
    }
}
