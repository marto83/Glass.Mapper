namespace Glass.Mapper.Caching
{
    public interface IObjectCache 
    {
        /// <summary>
        /// Check if an object exists in the cache
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool ContainsObject(ICacheKey key);

        /// <summary>
        /// Get object from the cache
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        object GetObject(ICacheKey key);

        /// <summary>
        /// Add and object with the given key to the cache
        /// </summary>
        /// <param name="args"></param>
        /// <param name="objectToAdd"></param>
        void AddObject(ICacheKey key, object objectToAdd);

        /// <summary>
        /// Removes all entries from the cache
        /// </summary>
        void ClearCache();
    }
}
