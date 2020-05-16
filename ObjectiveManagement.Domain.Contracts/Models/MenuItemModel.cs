namespace ObjectiveManagement.Domain.Contracts.Models
{
    public class MenuItemModel
    {
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public bool Children { get; set; }
    }
}
