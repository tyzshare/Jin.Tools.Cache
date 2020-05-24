using System;

namespace Jin.Tools.Cache
{

    /// <summary>
    /// 缓存对象定义接口
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 添加对象到缓存,如果已存在或添加失败则抛出异常
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expiry">过期时间，支持本地时间，内部实现将自动转UTC时间</param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/>或<paramref name="value"/> 参数为null</exception>
        /// <exception cref="ArgumentException"><paramref name="key"/> 已经存在值</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="expiry"/> 为DateTime.MinValue</exception>
        void Add<T>(string key, T value, DateTime expiry);


        /// <summary>
        /// 设置对象的值到指定的key，如果不存在则添加一个；如果存在，则覆盖旧值
        /// </summary>
        /// <typeparam name="T">缓存值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expiry">过期时间</param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/>或<paramref name="value"/> 参数为null</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="expiry"/> 为DateTime.MinValue</exception>
        void Set<T>(string key, T value, DateTime expiry);


        /// <summary>
        /// 移除指定key的缓存
        /// </summary>
        /// <param name="key">要移除的缓存key</param>
        void Remove(string key);


        /// <summary>
        /// 获取缓存，如果不存在，则返回null，如果<typeparamref name="T"/>不能为空，则抛出异常
        /// </summary>
        /// <typeparam name="T">要获取的值类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <returns>返回获取的缓存值</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/>为null</exception>
        /// <exception cref="NullReferenceException">获取缓存值为空，且<typeparamref name="T"/>为值类型，无法强制转转</exception>
        /// <exception cref="InvalidCastException">获取的缓存值无法转换为<typeparamref name="T"/></exception>
        T Get<T>(string key);


        /// <summary>
        /// 获取缓存，如果未找到，则使用acquire设置缓存并返回新值
        /// </summary>
        /// <typeparam name="T">缓存值类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="acquire">获取新缓存值的方法</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="refreshForce">是否直接刷新新值，如果为true，则使用acquire获取新值刷新缓存并返回；如果为false，则缓存未找到值时，则使用acquire获取新值刷新缓存并返回</param>
        /// <returns>返回缓存值</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/>或<paramref name="acquire"/>的返回值 为null</exception>
        T Get<T>(string key, Func<T> acquire, DateTime expiry, bool refreshForce = false);


        /// <summary>
        /// 获取缓存，如果存在则返回true，并输出值；如果不存在，则返回false, value将赋值为default(T)
        /// </summary>
        /// <typeparam name="T">要获取的值的类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">如果存在则返回true，并输出值；如果不存在，则返回false, value将赋值为default(T)</param>
        /// <returns>如果缓存值存在且类型转换成功，则返回true，否则返回false</returns>
        bool TryGet<T>(string key, out T value);

    }
}
