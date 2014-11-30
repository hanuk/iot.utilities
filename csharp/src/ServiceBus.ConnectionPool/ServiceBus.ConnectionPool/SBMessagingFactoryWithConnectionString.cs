using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus.ConnectionPool
{
	public class SBMessagingFactoryWithConnectionString:PooledItemFactoryBase<MessagingFactory>
	{
		public SBMessagingFactoryWithConnectionString(string connectionString)
		{
			base._settings.ConnectionString = connectionString;
		}

		public override MessagingFactory Create()
		{
			ValidateMandatory("ConnectionString");
			return MessagingFactory.CreateFromConnectionString(_settings.ConnectionString);
		}
	}
}
;