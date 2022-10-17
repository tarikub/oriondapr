namespace Libraries.Interfaces;

public interface IStateManager<T>
{
    Task<T> GetDataFromStateStore(string stateStoreName, string key);
    Task SaveDataToStateStore(string stateStoreName, string key, string value);
}