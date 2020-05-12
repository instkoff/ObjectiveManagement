namespace ObjectiveManagement.Domain.Contracts.Models
{
    public class MenuItemModel
    {
        public MenuItemModel()
        {
            a_attr = new Link();
        }
        public string Id { get; set; }
        public string parent { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public Link a_attr { get; }

        public class Link
        {
            public string href { get; set; }
        }
    }
}
