using System.IO;
using System.Text.Json;
using Npgsql;

namespace кулинар.DbService;

public static class DbConnect
{
    public static NpgsqlDataSource CreateDataSource()
    {
        var settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(settingsPath))
            throw new InvalidOperationException($"Не найден файл настроек: {settingsPath}");

        using var document = JsonDocument.Parse(File.ReadAllText(settingsPath));
        var connectionString = document.RootElement
            .GetProperty("ConnectionStrings")
            .GetProperty("Default")
            .GetString();

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("В appsettings.json не задана строка подключения PostgreSQL.");

        return NpgsqlDataSource.Create(connectionString);
    }
}