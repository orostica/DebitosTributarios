using Microsoft.Data.Sqlite;

namespace DebitosTributarios.Infrastructure.Settings;

public class SqliteDatabaseSettings
{
    private SqliteDatabaseSettings(string connectionString, string databasePath)
    {
        ConnectionString = connectionString;
        DatabasePath = databasePath;
    }

    public string ConnectionString { get; }

    public string DatabasePath { get; }

    public static SqliteDatabaseSettings Create(string rawConnectionString, string contentRootPath)
    {
        var builder = new SqliteConnectionStringBuilder(rawConnectionString);

        if (!Path.IsPathRooted(builder.DataSource))
        {
            builder.DataSource = Path.GetFullPath(Path.Combine(contentRootPath, builder.DataSource));
        }

        return new SqliteDatabaseSettings(builder.ConnectionString, builder.DataSource);
    }
}
