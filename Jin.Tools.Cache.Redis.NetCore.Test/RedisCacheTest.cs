using Jin.Tools.Cache.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Jin.Tools.Cache.Redis.NetCore.Test
{
    [TestClass]
    public class RedisCacheTest
    {
        public IConfiguration Configuration { get; }

        public RedisCacheTest()
        {
            Configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", false, true).Build();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var redisConnect = Configuration.GetSection("Redis:redisConnect").Value;
            var prefix = Configuration.GetSection("Redis:prefix").Value;
            ICache cache = new RedisCache(redisConnect, prefix);
            AddTest(cache);
            GetTest(cache);
            SetTest(cache);
            RemoveTest(cache);
            TryGetTest(cache);
        }
        void AddTest(ICache cache)
        {
            try
            {
                cache.Add("user", new User() { Id = 1, Name = "tyzshare" }, DateTime.Now.AddMinutes(2));
            }
            catch (Exception ex)
            {
                //重复插入报错
            }
        }

        void GetTest(ICache cache)
        {
            User user = new User();
            var result = cache.Get<User>("user");
        }

        void SetTest(ICache cache)
        {
            cache.Set("user", new User() { Id = 2, Name = "tyzshare2" }, DateTime.Now.AddMinutes(2));
        }

        void RemoveTest(ICache cache)
        {
            cache.Remove("user");
        }

        void TryGetTest(ICache cache)
        {
            User user = new User();
            var result = cache.TryGet("user", out user);
        }
    }

    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id
        {
            get; set;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get; set;
        }
    }
}
