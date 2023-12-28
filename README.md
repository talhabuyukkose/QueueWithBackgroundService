# Queue With BackgroundService

Effective queue system with .Net Framework

## Project Description
This project aims to implement an effective queue mechanism in a .NET Web API without using external queue technologies like RabbitMQ. The project utilizes .NET features such as BackgroundService and IHostedService, along with the Channel class, to achieve the desired queue functionality.

## Getting Started
Clone the project to your local machine and install the necessary dependencies.

```
git clone https://github.com/talhabuyukkose/QueueWithBackgroundService.git
cd your-project
dotnet restore
```

## The goal of the project
How to develop an effective queue system with .Net framework without Queue systems.

Here are some examples of how to use the project:
Both endpoints work in the same queue. The purpose is to show how to work the same queue with different endpoints


## Sources
About System.Threading.Channels:  https://learn.microsoft.com/en-us/dotnet/core/extensions/channels
About Background tasks with hosted services : https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services
