using Asp.Net.Core.BusinessLayer.Interfaces;
using Asp.Net.Core.Transverse.Logger.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Net.Core.BusinessLayer.Services
{
    public class CacheManager : ICacheManager
    {
        private readonly IGenericLogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _memoryCache;

        private const string APPLICATION = "Asp.net Core";
        private static readonly int ENTITE_CACHE_DEFAULT_TIME_IN_SECONDS = 3600; //1 hour
        private TimeSpan _defaultExpirationTime = new TimeSpan(0, 0, ENTITE_CACHE_DEFAULT_TIME_IN_SECONDS);


        public CacheManager(
            IGenericLogger<CacheManager> logger,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Current Contexte cache

        public T Get<T>(string cacheKey, Func<T> funcAcquireData, TimeSpan? duration = null)
            where T : class
        {
            return GetCache(BuildCacheKey(cacheKey), funcAcquireData, duration);
        }
        public Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> funcAcquireData, TimeSpan? duration = null)
            where T : class
        {
            return GetCache(BuildCacheKey(cacheKey), funcAcquireData, duration);
        }

        public void Remove(string cacheKey)
        {
            RemoveCache(BuildCacheKey(cacheKey));
        }

        #endregion

        #region Méthodes privées

        private string BuildCacheKey(string cacheKey)
        {
            return $"{APPLICATION}_{cacheKey}";
        }

        private async Task<T> GetCacheAsync<T>(string cacheKey, Func<Task<T>> funcAcquireData, TimeSpan? duration = null)
           where T : class
        {
            //On récupère le cache ou null si la valeur n'est pas définie
            T data = GetCache<T>(cacheKey);

            bool hasCache = data != null;

            // Si la valeur n'existe pas dans le cache
            if (!hasCache)
            {
                // on génére les données en appelant la méthode initiale
                data = await funcAcquireData.Invoke();
                // puis on stocke le résultat dans le cache
                SetCache<T>(cacheKey, data, duration);
            }

            return data;
        }

        private T GetCache<T>(string cacheKey, Func<T> funcAcquireData, TimeSpan? duration = null)
            where T : class
        {
            //On récupère le cache ou null si la valeur n'est pas définie
            T data = GetCache<T>(cacheKey);

            bool hasCache = data != null;

            // Si la valeur n'existe pas dans le cache
            if (!hasCache)
            {
                // on génére les données en appelant la méthode initiale
                data = funcAcquireData.Invoke();
                // puis on stocke le résultat dans le cache
                SetCache<T>(cacheKey, data, duration);
            }

            return data;
        }

        private T GetCache<T>(string cacheKey) where T : class
        {
            _logger.Debug("Récupération du cache portant la clef {cacheKey}", cacheKey);

            T result = _memoryCache.Get<T>(cacheKey);

            return result;
        }

        private void SetCache<T>(string cacheKey, T data, TimeSpan? duration = null) where T : class
        {
            _logger.Debug("Mise à jour du cache portant la clef {cacheKey}", cacheKey);

            TimeSpan time = _defaultExpirationTime;
            if (duration.HasValue)
            {
                time = duration.Value;
            }

            MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                // Set the absolute expiration
                .SetAbsoluteExpiration(DateTime.Now.Add(time))
                // Pin to cache.
                .SetPriority(CacheItemPriority.Normal)
                // Add eviction callback
                .RegisterPostEvictionCallback(callback: EvictionCallback, state: this);

            //Ajoute ou met à jour le cache
            _memoryCache.Set(cacheKey, data, cacheEntryOptions);
        }

        private static void EvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            string strReason = null;
            switch (reason)
            {
                case EvictionReason.Removed:
                    strReason = "Supprimé";
                    break;
                case EvictionReason.Replaced:
                    strReason = "Remplacé";
                    break;
                case EvictionReason.Expired:
                    strReason = "Expiré";
                    break;
                case EvictionReason.TokenExpired:
                    strReason = "Le token a expiré";
                    break;
                case EvictionReason.Capacity:
                    strReason = "La capacité a débordé";
                    break;
                case EvictionReason.None:
                default:
                    strReason = "Aucune";
                    break;
            }
            var message = $"L'entrée du cache a été expulsé. Raison: {strReason}.";
            ((CacheManager)state).LogDebug(message);
        }

        internal void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        private void RemoveCache(string cacheKey)
        {

            _memoryCache.Remove(cacheKey);
            _logger.Debug("Suppression du cache portant la clef {cacheKey}", cacheKey);
        }

        #endregion

    }
}
