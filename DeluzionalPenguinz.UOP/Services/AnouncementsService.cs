using DeluzionalPenguinz.Models.Identity;
using System.Net.Http.Json;

namespace DeluzionalPenguinz.UOP.Services
{
    public class AnouncementsService
    {
        HttpClient httpClient;

        public AnouncementsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> UpdateAnouncement(AnouncementDTO anouncement)
        {
            var result = await httpClient.PutAsJsonAsync("api/Anouncements/UpdateAnAnouncement", anouncement);

            result.EnsureSuccessStatusCode();

            BooleanResponse response  = await result.Content.ReadFromJsonAsync<BooleanResponse>();

            return response.Success;
        }
        public async Task<bool> AddAnouncement(AnouncementDTO anouncement)
        {
            var result = await httpClient.PostAsJsonAsync("api/Anouncements/AddNewAnouncement", anouncement);

            result.EnsureSuccessStatusCode();

            BooleanResponse response = await result.Content.ReadFromJsonAsync<BooleanResponse>();

            return response.Success;
        }
        public async Task<bool> DeleteAnouncement(int anouncementId)
        {
            var result = await httpClient.DeleteAsync($"api/Anouncements/DeleteAnouncement/{anouncementId}");

            result.EnsureSuccessStatusCode();

            BooleanResponse response = await result.Content.ReadFromJsonAsync<BooleanResponse>();

            return response.Success;
        }
        public async Task<IEnumerable<AnouncementDTO>> GetAllAnouncements()
        {
            var result = await httpClient.GetAsync("api/Anouncements/GetAllAnouncements");

            result.EnsureSuccessStatusCode();

            var response = await result.Content.ReadFromJsonAsync<IEnumerable<AnouncementDTO>>();

            return response;
        }


    }
}
