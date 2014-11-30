using System;
using System.Text;
using Microsoft.ServiceBus.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceBus.ConnectionPool;

namespace ServiceBus.ConnectionPool.Tests
{
	[TestClass]
	public class EventHubTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			dynamic factorySettings = new ServiceBus.ConnectionPool.DynamicDictionary();
			factorySettings.ConnectionString = "your_connection_string";
			SBMessagingFactoryWithConnectionString fws = new SBMessagingFactoryWithConnectionString(factorySettings.ConnectionString);
			ConnectionPool<MessagingFactory> sbp = new ConnectionPool<MessagingFactory>(fws, 10);
			sbp.Initialize();
			MessagingFactory mf1 = sbp.GetNext();
			EventHubClient ehc = mf1.CreateEventHubClient("your_eventhub_name");
			EventData ed = new EventData(Encoding.UTF8.GetBytes("Hello"));

			try
			{
				ehc.Send(ed);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
