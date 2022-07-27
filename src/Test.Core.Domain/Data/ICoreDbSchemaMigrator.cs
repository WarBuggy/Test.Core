using System.Threading.Tasks;

namespace Test.Core.Data;

public interface ICoreDbSchemaMigrator
{
    Task MigrateAsync();
}
