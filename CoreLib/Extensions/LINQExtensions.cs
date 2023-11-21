using System.Linq.Expressions;

namespace CoreLib.Extensions.LINQExtensions
{
    public static class LINQExtensions
    {
        //public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T,bool>> expression)
        // dunno why had bool condition but commented out
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, Expression<Func<T,bool>> expression)
        {
            if (expression == null)
            {
                return query;
            }
            return query.Where(expression);
        }
    }
}
