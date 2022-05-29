namespace Learn.BlazorServerApp.Data
{
    public interface IDA
    {
        string ConnectionStringName { get; set; }

        Task<List<T>> LoadData<T, U>(string sql, U parameters);
        Task SaveData<T>(string sql, T parameters);
        Task<T> Find<T, U>(string sql, U parameters);
        Task Delete<T>(string sql, T parameters);
    }
}