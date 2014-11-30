using System;
using Microsoft.ServiceBus.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceBus.ConnectionPool;
namespace ServiceBus.ConnectionPool.Tests
{
	[TestClass]
	public class QueueClientTests
	{

		/// <summary>
		/// Tests round robin load balancing using Service Bus connection string
		/// </summary>
		[TestMethod]
		public void TestRoundRobin()
		{
			string tp1_serviceBusConnectionString = "Endpoint=sb://hk-hack-hub-ns.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=JhDUJn307X1JEoPZb4aw0ZxYRz9cOdp4ZbnWS/Gm2Lc=;TransportType=Amqp";
			string connectionString = tp1_serviceBusConnectionString;
			SBMessagingFactoryWithConnectionString cst = new SBMessagingFactoryWithConnectionString(connectionString);
			ServiceBus.ConnectionPool.ConnectionPool<MessagingFactory> sbp = new ServiceBus.ConnectionPool.ConnectionPool<MessagingFactory>(cst, 10);
			sbp.Initialize();
			MessagingFactory mf1 = sbp.GetNext();
			MessagingFactory mf2 = sbp.GetNext();
			Assert.AreNotEqual(mf1, null);
			Assert.AreNotEqual(mf2, null);
			Assert.AreNotEqual(mf1.GetHashCode(), mf2.GetHashCode());
		}

		/// <summary>
		/// tests ServiceBus Queue send using SAS key 
		/// </summary>

		[TestMethod]
		public void TestTokenProviderWithQueue()
		{
			dynamic factorySettings = new DynamicDictionary();
			factorySettings.ServiceBusNameSpace = "MySBNamespace";
			factorySettings.SasKey = "MySASKeykdfkdfkdjfkdjfkdfkdflkdjf";
			factorySettings.SasPolicyName = "QueueSender";
			ServiceBus.ConnectionPool.SBMessagingFactoryWithTokenProvider fws = new ServiceBus.ConnectionPool.SBMessagingFactoryWithTokenProvider(factorySettings);
			ServiceBus.ConnectionPool.ConnectionPool<MessagingFactory> sbp = new ServiceBus.ConnectionPool.ConnectionPool<MessagingFactory>(fws, 10);
			sbp.Initialize();
			MessagingFactory mf1 = sbp.GetNext();
			string testMsg = "Hello";
			BrokeredMessage bm = new BrokeredMessage(testMsg);
			QueueClient qc = mf1.CreateQueueClient("myqueue");
			qc.Send(bm);
			BrokeredMessage bmr = qc.Receive();

			string receivedMsg = bmr.GetBody<string>();
			Assert.Equals(testMsg, receivedMsg);
		}

		[TestMethod]
		public void TestConnectionStringProviderWithQueue()
		{
			string serviceBusConnectionString = "Endpoint=sb://hk-hack-hub-ns.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=JhDUJn307X1JEoPZb4aw0ZxYRz9cOdp4ZbnWS/Gm2Lc=";
			string queueName = "myqueue";
			string connectionString = serviceBusConnectionString;
			SBMessagingFactoryWithConnectionString cst = new SBMessagingFactoryWithConnectionString(connectionString);
			ServiceBus.ConnectionPool.ConnectionPool<MessagingFactory> sbp = new ServiceBus.ConnectionPool.ConnectionPool<MessagingFactory>(cst, 10);
			sbp.Initialize();
			MessagingFactory mf1 = sbp.GetNext();
		
			string testMsg = "Hello";
			BrokeredMessage bm = new BrokeredMessage(testMsg);
			QueueClient qc = mf1.CreateQueueClient(queueName);
			qc.Send(bm);
			BrokeredMessage bmr = qc.Receive();

			string receivedMsg = bmr.GetBody<string>();
			Assert.Equals(testMsg, receivedMsg);
		}
	}
}
