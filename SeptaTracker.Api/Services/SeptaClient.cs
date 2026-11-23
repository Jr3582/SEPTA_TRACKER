using SeptaTracker.Api.Models;

public class SeptaClient {
    private readonly HttpClient _client;
    public SeptaClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<SeptaTrainDto>> GetTrainsAsync()
    {
        var res = await _client.GetFromJsonAsync<List<SeptaTrainDto>>("TrainView/index.php");
        if(res==null) return [];
        return res;
    }
    
}