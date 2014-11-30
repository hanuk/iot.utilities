ServiceBus.ConnectionPool
=======================

# License

Microsoft Developer Experience & Evangelism

Copyright (c) Microsoft Corporation. All rights reserved.

THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

The example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious. No association with any real company, organization, product, domain name, email address, logo, person, places, or events is intended or should be inferred.



#Background
Azure ServiceBus MessagingFactory encapsulates TCP connection and is a key to service bus client scalability. Several clients can be created from the same MessagingFactory instance. All the clients created from the same MessagingFactory will use the same TCP connection. SeviceBus.ConnectionPool is a simple connection pool that by default uses round robin strategy for dispensing MessageFactory instance.

#Extending load balancing strategy
Create a new strategy by inheriting from LoadBalacingStrategy and imnplementing GetItem().
Additional.

#Prerequisites
- Microsoft.ServiceBus.dll: install Nuget package "Microsoft Azure Service Bus", version 2.5.2.0

#Testing
ServiceBus.ConnectionPool requires an existing ServiceBus namespace, a queue and an Event Hub. ServieBus.ConnectionPool.Tests project includes a few tests which requires various connection strings to be filled. 

#Work to be done
Thread safety of the connection pool has not been tested extensively. Will write a multi-threaded test client for this purpose.