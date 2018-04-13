using System.Linq.Expressions;

namespace CompiledFilters.Filters
{
    /// <summary>
    /// Implements a filter that evaluates to true if both of the other two <see cref="Filter{T}"/>s do.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    internal sealed class AndFilter<T> : Filter<T>
    {
        private readonly Filter<T> _lhs;
        private readonly Filter<T> _rhs;

        /// <summary>
        /// Creates a new instance of the <see cref="OrFilter{T}"/> class,
        /// that evaluates to true if both of the two given <see cref="Filter{T}"/>s does.
        /// </summary>
        /// <param name="lhs">The first filter to evaluate.</param>
        /// <param name="rhs">The second filter to evaluate.</param>
        public AndFilter(Filter<T> lhs, Filter<T> rhs)
        {
            _lhs = lhs;
            _rhs = rhs;
        }

        private protected override Expression GetFilterExpression()
            => Expression.And(
                GetFilterExpression(_lhs),
                GetFilterExpression(_rhs));
    }
}
