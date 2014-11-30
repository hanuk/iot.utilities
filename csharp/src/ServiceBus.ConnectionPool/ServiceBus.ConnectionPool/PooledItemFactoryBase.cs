using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus.ConnectionPool
{
	public abstract class PooledItemFactoryBase<T>
	{
		protected dynamic _settings;
		protected PooledItemFactoryBase() {
			_settings = new DynamicDictionary();
		}
		protected PooledItemFactoryBase(dynamic settings)
		{

			_settings = settings;
		}
		abstract public T Create();
		protected virtual void ValidateMandatory(string fieldName)
		{
			ValidateEmpty();
			ValidateFieldPresence(fieldName);
			ValidateFieldNull(fieldName);
		}
		protected virtual void ValidateMandatory(List<string> fieldNames )
		{
			ValidateEmpty();

			foreach (string fieldName in fieldNames)
			{
				ValidateFieldPresence(fieldName);
				ValidateFieldNull(fieldName);
			}
		}
		protected void ValidateFieldPresence(string fieldName)
		{
			if (!_settings.InnerDictionary.ContainsKey(fieldName))
			{
				throw new ApplicationException(string.Format("{0} not found", fieldName));
			}
		}
		protected void ValidateEmpty()
		{
			if (_settings.InnerDictionary.Count == 0)
			{
				throw new ApplicationException("Empty settings");
			}
		}
		protected void ValidateFieldNull(string fieldName)
		{
			object val = null; 
			bool result = _settings.InnerDictionary.TryGetValue(fieldName, out val);
			if (!result || val == null)
				throw new ApplicationException(string.Format("{0} has empty value", fieldName));
		}
	}
}
