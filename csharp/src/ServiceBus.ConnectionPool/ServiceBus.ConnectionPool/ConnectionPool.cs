using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus.ConnectionPool
{
	public class ConnectionPool<T>
	{
		private List<PooledItem<T>> _factoryPool = new List<PooledItem<T>>();
		private PooledItemFactoryBase<T> _factoryStrategy;
		private int _poolSize;
		LoadBalancingStrategy<PooledItem<T>> _loadBalancerStrategy; 

		public ConnectionPool(PooledItemFactoryBase<T> factoryStrategy, int poolSize)
		{
			_poolSize = poolSize;
			_factoryStrategy = factoryStrategy;
			_loadBalancerStrategy = new LoadBalancingStrategy<PooledItem<T>>(_factoryPool);
		}
		public ConnectionPool(PooledItemFactoryBase<T> factoryStrategy, int poolSize, LoadBalancingStrategy<PooledItem<T>> loadbalancerStrategy)
		{
			_poolSize = poolSize;
			_factoryStrategy = factoryStrategy;
			_loadBalancerStrategy = loadbalancerStrategy;
		}
		public void Initialize()
		{
			Debug.Assert(_factoryStrategy != null);
			for (int i = 0; i < _poolSize; i++ )
			{
				_factoryPool.Add(new PooledItem<T>() { Item = _factoryStrategy.Create() });
			}
		}

		public T GetNext()
		{
			return _loadBalancerStrategy.GetItem().Item;
		}
	}
}
