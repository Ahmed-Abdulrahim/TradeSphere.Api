namespace TradeSphere.Infrastructure.Specefication
{
    public class FeedBackSpecification : BaseSpecefication<FeedBack>
    {
        public FeedBackSpecification()
        {
            AddInculdes();
        }
        public FeedBackSpecification(int id) : base(r => r.Id == id)
        {
            AddInculdes();
        }
        public FeedBackSpecification(Expression<Func<FeedBack, bool>> criteria) : base(criteria)
        {
            AddInculdes();
        }

        void AddInculdes()
        {
            Includes.Add(o => o.AppUser);
            Includes.Add(o => o.Product);


        }

    }
}
