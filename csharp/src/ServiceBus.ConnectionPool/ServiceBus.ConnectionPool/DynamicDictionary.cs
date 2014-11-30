using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.ConnectionPool
{
	/// <summary>
	/// Adopted from MSDN samples
	/// </summary>
	public class DynamicDictionary : DynamicObject
	{
		Dictionary<string, object> _innerDictionary = new Dictionary<string, object>();

		public Dictionary<string,object> InnerDictionary { 
			get 
			{
				return _innerDictionary;
			}
		}
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			string name = binder.Name;
			_innerDictionary.TryGetValue(name, out result);
			//ignore the return value as we don't want RuntimeBinderException if an item is not found
			return true;
		}
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			_innerDictionary[binder.Name] = value;
			return true;
		}
	}
}
