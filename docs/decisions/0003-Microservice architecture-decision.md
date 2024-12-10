#Deciding to Use Microservices Architecture for AML system

## Status
Accepted

## Context
Our application has grown to include multiple features that can be independently scaled. These features include media service, inventory management, payment processing, and transaction service. We are considering whether to adopt a monolithic architecture or switch to microservices.

## Decision
We decided to adopt **Microservices Architecture** for the following reasons:

- **Scalability**: Microservices allow us to scale each feature independently based on demand.
- **Flexibility**: Different services can be written in different languages or frameworks, depending on the requirements of each service.
- **Resilience**: If one service fails, the others can continue running, which increases the application's overall availability.

## Consequences
- We will need to manage the complexity of multiple services, including inter-service communication, deployment, and monitoring.
- Microservices require infrastructure for service discovery, load balancing, and fault tolerance, adding additional overhead.
- Continuous integration and delivery will need to be implemented to handle the deployment of multiple services.

## Alternatives Considered
- **Monolithic Architecture**: While easier to develop initially, the monolithic approach was rejected due to challenges in scaling and managing the complexity as the application grows.
- **Serverless**: Considered but rejected due to limitations in long-running services and complexities in managing state.

