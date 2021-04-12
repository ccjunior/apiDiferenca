using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacao.API.Model
{
    public class RedisConnection
    {
        private ConnectionMultiplexer _conexao;
        public RedisConnection(IConfiguration configuration)
        {
            
            
            
            _conexao = ConnectionMultiplexer.Connect(
                configuration.GetConnectionString("RedisServer"));
        }

        public string GetValueFromKey(string key)
        {
            var dbRedis = _conexao.GetDatabase();
            return dbRedis.StringGet(key);
        }

      
    }
}
