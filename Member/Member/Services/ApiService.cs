using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Member.Models.Member;
using Newtonsoft.Json;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private string _token;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> AuthenticateAsync(string baseUrl)
    {
        var loginData = new { id = "exam", password = "exam" };
        var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{baseUrl}/api/Authenticate/Login", content);

        if (response.IsSuccessStatusCode)
        {
            var tokenResponse = await response.Content.ReadAsStringAsync();
            _token = tokenResponse; // JsonConvert.DeserializeObject<TokenResponse>(tokenResponse).Token;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token.Trim('"'));
            return true;
        }

        return false;
    }

    public async Task<string> CallApiAsync(string url, HttpMethod method, object data)
    {
        var request = new HttpRequestMessage(method, url);

        if (data != null)
        {
            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return content;
        }

        throw new HttpRequestException($"API request failed: {response.StatusCode}");
    }
}

public class TokenResponse
{
    public string Token { get; set; }
}
