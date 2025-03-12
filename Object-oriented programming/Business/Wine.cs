namespace Object_oriented_programming.Business
{
    public class Wine : Drink
    {
        private const string Category = "Vino";

        public Wine(int quantity) : base(quantity)
        {
        }

        public override string GetCategory() => Category;
    }
}