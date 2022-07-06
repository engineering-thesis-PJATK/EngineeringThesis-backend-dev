using System.Threading.Tasks;

namespace OneBan_TMS.Validators
{
    public interface IBaseFilter<T>
    {
        abstract Task<FilterResult> IsValid(T entity);
        abstract Task<FilterResult> IsValid(T entity, int entityId);
    }
}