namespace dotnet_rest_api.HATEOAS
{
    public class Link
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
        public Link(string href, string rel, string method)
        {
            this.rel = rel;
            this.href = href;
            this.method = method;
        }
    }
}