using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus.ConnectionPool
{
	public class LoadBalancingStrategy<T>
	{
		protected List<T> _pooledItemList;
		private int _currentPoolIndex;
		public LoadBalancingStrategy(List<T> pooledItemList)
		{
			Debug.Assert(pooledItemList != null);
			this._pooledItemList = pooledItemList;
			_currentPoolIndex = 0;
		}

		public virtual T GetItem()
		{
			Debug.Assert(_pooledItemList.Count != 0);
			T loadBalancingItem = _pooledItemList[_currentPoolIndex];
			Interlocked.Increment(ref _currentPoolIndex);
			if (_currentPoolIndex == _pooledItemList.Count)
			{
				Interlocked.Exchange(ref _currentPoolIndex, 0);
			}

			return loadBalancingItem;
		}
	}
}
