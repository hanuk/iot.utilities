using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.ConnectionPool
{
	/// <summary>
	/// object that stores the information about pooled item. For now only the item. 
	/// The object will have to be extended to add the usage state for affecting
	/// load balancing strategies. 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PooledItem<T>
	{
		T _pooledItem;
		public T Item
		{
			get { return _pooledItem; }
			set { _pooledItem = value; }
		}
	}
}
