using System.Text.Json;

using TornOps.Models;

namespace TornOps.Services
{
    public class TornApiService
    {
        public async Task<UserDataModel> LoadFromMauiAssetAsync(string filename)
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(filename);
            var user = await JsonSerializer.DeserializeAsync<UserDataModel>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (user == null)
                throw new Exception("Deserialization failed.");

            return user;
        }
    }
}
