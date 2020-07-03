namespace Charges.Models
{
    class Products
    {
        public int id { get; set; }
        public string name { get; set; }

        public Products(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }

}
