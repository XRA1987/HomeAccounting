namespace HomeAccounting.Domain.Entities
{
    public class Category
    {
        public Category()
        {
            Sources = new HashSet<Source>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Source> Sources { get; set; }
    }
}
