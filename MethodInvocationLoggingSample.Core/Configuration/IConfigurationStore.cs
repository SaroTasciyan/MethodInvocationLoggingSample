using System;

namespace MethodInvocationLoggingSample.Core.Configuration
{
    public interface IConfigurationStore
    {
        string GetConnectionString(string key);
        T GetCasting<T>(string key);
        T Get<T>(string key) where T : IConvertible;
        bool TryGetCasting<T>(string key, out T value);
        bool TryGet<T>(string key, out T value) where T : IConvertible;
    }
}