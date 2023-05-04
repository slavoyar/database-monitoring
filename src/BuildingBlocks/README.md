# Getting Started with EventBusRabbitMq

This project was borrowed from [MSDN eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers).

For a better undestanding check out [simple implemintation](https://github.com/Batiskaaaf/EventBusRabbitMQ).

## For EventBus to run

You need either: 

1. Run RabbitMq on your local machine

    [windows](https://community.chocolatey.org/packages/rabbitmq)

2. Run RabbitMq in a [docker](https://hub.docker.com/_/rabbitmq)

## Getting started

1. Add references to the EventBus and EventBusRabbitMQ libraries.

1. Register EventBusRabbitMq (see [simple implemination](https://github.com/Batiskaaaf/EventBusRabbitMQ/tree/main/Tests/EventBusTestProj) Program.cs line 39).

    2.1 HostName for ConnectionFactory in our case is localhost.
    
    2.2 ClientName for EventBusRabbitMQ the name of your service.
        ex. (PingService, Aggregator, Authorization)

## Publishing an event

1. Create a record "CustomEvent" (not a class) inherited from "BaseEvent". (see  [simple implemination](https://github.com/Batiskaaaf/EventBusRabbitMQ/tree/main/Tests/EventBusTestProj) CustomEvets)

    1.1 The "CustomEvent" record is stored in the folder of your service, there is no need to change the BuildingBlocks folder.

2. Using DI, get the IEventBus service. (see [simple implemination](https://github.com/Batiskaaaf/EventBusRabbitMQ/tree/main/Tests/EventBusTestProj) SendController.cs line 19)

3. Publish your event. (see [simple implemination](https://github.com/Batiskaaaf/EventBusRabbitMQ/tree/main/Tests/EventBusTestProj) SendController.cs line 32)

All subscribed services received our event.

## Subscribing on event

1. Create "CustomEventHandler<T>" inherited from IIntegrationEventHandler<T>
    where T is the event we want to listen to. Implement Handle as needed. (see [simple implemination](https://github.com/Batiskaaaf/EventBusRabbitMQ/tree/main/Tests/EventBusTestProj) CustomEventsHandler)

2. Register "CustomEventHandler" in DI. (see [simple implemination](https://github.com/Batiskaaaf/EventBusRabbitMQ/tree/main/Tests/EventBusTestProj) Program.cs line 63)

3. Configure subscriptions. (see [simple implemination](https://github.com/Batiskaaaf/EventBusRabbitMQ/tree/main/Tests/EventBusTestProj) Program.cs line 71)

    3.1 The Subscribe<T, TH> method where T is the event we want to listen to, and Th is the handler that will handle it.

Now we are subscribed to the event.

## Learn More

You can learn more about RabbitMQ on [RabbitMQ official tutorials](https://www.rabbitmq.com/getstarted.html).