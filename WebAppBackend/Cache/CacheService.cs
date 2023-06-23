using Newtonsoft.Json;
using StackExchange.Redis;

namespace WebAppBackend.Cache
{
    public class CacheService : ICacheService
    {
        private IDatabase _db;

        public CacheService()
        {
            ConfiguracaoRedis();
        }

        private void ConfiguracaoRedis()
        {
            _db = ConnectionHelper.Connection.GetDatabase();
        }

        public T GetData<T>(string key)
        {
            var value = _db.StringGet(key);
            if(!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }

        public object RemoverData(string key)
        {
            bool _existe = _db.KeyExists(key);
            if (_existe)
            {
                return _db.KeyDelete(key);
            }

            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset horaExpiracao)
        {
            TimeSpan expiracao = horaExpiracao.DateTime.Subtract(DateTime.Now);
            var definir = _db.StringSet(key, JsonConvert.SerializeObject(value), expiracao);
            return definir;
        }
    }
}
