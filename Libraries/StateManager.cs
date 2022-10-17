using Dapr.Client;
using Libraries.Interfaces;

namespace Libraries;

public class StateManager<T> : IStateManager<T>
{
    private readonly DaprClient _daprClient;
    public StateManager(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }
    public async Task<T> GetDataFromStateStore(string stateStoreName, string key)
    {
        var data = await _daprClient.GetStateAsync<T>(stateStoreName, GetKey(key));
        return data;
    }
    public async Task SaveDataToStateStore(string stateStoreName, string key, string value)
    {
        await _daprClient.SaveStateAsync(stateStoreName, GetKey(key), value);
        return;
    }
    private string GetKey(string key)
    {
        return $"{nameof(T)}_{key}";
    }
}
