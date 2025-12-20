namespace TradeSphere.Infrastructure.Specefication
{
    public class CategorySpecification : BaseSpecefication<Category>
    {
        public CategorySpecification()
        {
            AddInculdes();
        }
        public CategorySpecification(int id) : base(r => r.Id == id)
        {
            AddInculdes();
        }
        public CategorySpecification(Expression<Func<Category, bool>> criteria) : base(criteria)
        {
            AddInculdes();
        }

        void AddInculdes()
        {
            Includes.Add(c => c.Products);

        }
    }
}
