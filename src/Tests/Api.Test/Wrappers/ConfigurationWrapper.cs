using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Api.Test.Wrappers;

public interface IConfigurationWrapper
{
    string GetConnectionString(string name);
}

public class ConfigurationWrapper(IConfiguration configuration) : IConfigurationWrapper
{
    public string GetConnectionString(string name)
    {
        return configuration.GetConnectionString(name) ?? string.Empty;
    }
}


