using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WpfRegistration.Domain.Services
{
    public interface IDataService<T>
    {
        Task<List<T>> Getall();

        Task<T> Get(Guid Id);

        Task<T> Create(T entity);

        Task<T> Update(Guid Id, T entity);

        Task<bool> Delete(Guid Id);
    }
}
