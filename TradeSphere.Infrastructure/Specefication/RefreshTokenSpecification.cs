namespace TradeSphere.Infrastructure.Specefication
{
    public class RefreshTokenSpecification : BaseSpecefication<RefreshToken>
    {
        public RefreshTokenSpecification()
        {
            AddInculdes();
        }
        public RefreshTokenSpecification(int id) : base(r => r.Id == id)
        {
            AddInculdes();
        }
        public RefreshTokenSpecification(Expression<Func<RefreshToken, bool>> criteria) : base(criteria)
        {
            AddInculdes();
        }

        void AddInculdes()
        {
            Includes.Add(r => r.AppUser);

        }
    }
}
