using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus.ConnectionPool
{
	public class SBMessagingFactoryWithTokenProvider:PooledItemFactoryBase<MessagingFactory>
	{
		public SBMessagingFactoryWithTokenProvider(dynamic settings)
			:base((DynamicDictionary)settings)
		{

		}
		public override MessagingFactory Create()
		{
			List<string> names = new List<string>() { "ServiceBusNameSpace", "SasPolicyName", "SasKey" };
			ValidateMandatory(names);
			Uri serviceBusUri = ServiceBusEnvironment.CreateServiceUri("sb", _settings.ServiceBusNameSpace, string.Empty);
			TokenProvider tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(_settings.SasPolicyName, _settings.SasKey);
			return MessagingFactory.Create(serviceBusUri, tokenProvider);
		}
	}
}
