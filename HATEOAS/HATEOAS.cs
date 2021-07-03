using System.Collections.Generic;

namespace dotnet_rest_api.HATEOAS
{
    public class HATEOAS
    {
        private string url;
        private string protocol = "https://";

        public List<Link> actions = new List<Link>();

        public HATEOAS(string url)
        {
            this.url = url;
        }

        public HATEOAS(string url, string protocol)
        {
            this.protocol = protocol;
            this.url = url;
        }

        public void AddAction(string rel, string method)
        {
            actions.Add(new Link(this.protocol + this.url, rel, method));
        }

        public Link[] GetActions(string suffix)
        {
            Link[] tempLink = actions.ToArray();
            foreach (var link in tempLink)
            {
                link.href = $"{link.href}/{suffix}";
            }
            return tempLink;
        }
    }
}