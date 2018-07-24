using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Net.Core.BusinessLayer.Interfaces
{
    public interface ICacheManager
    {
        T Get<T>(string cacheKey, Func<T> funcAcquireData, TimeSpan? duration = null)
            where T : class;

        Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> funcAcquireData, TimeSpan? duration = null)
            where T : class;

        void Remove(string cacheKey);
    }
}
