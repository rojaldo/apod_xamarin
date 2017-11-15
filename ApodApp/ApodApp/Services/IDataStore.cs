using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApodApp
{
	public interface IDataStore<T>
	{
		Task<bool> AddItemAsync(T item);
		Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
		Task<T> GetItemAsync(string id);
		Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
	}
}
