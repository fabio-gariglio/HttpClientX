# HttpClientX
Making extensible, composable and testable the most famous .Net HttpClient.

## Preface

HttpClientX is **NOT** another .NET library to make HTTP requests. In fact, it recognises Microsoft's HttpClient as one of the best options for this task. HttpClientX aims to make HttpClient even easier to use by providing a fluent builder to compose the list of HTTP message handlers.

HttpClient allows to specify the main message handler to use to handle HTTP requests. This message handler can have another message handler as dependency and so on. In this way is possible to build a chain of message handlers to cover different aspects of handling an HTTP request. The last message handler in the chain should be of type `HttpClientHandler` as this handler sends the request over to the network.

Let's assume we want to log the HTTP requests and responses using NLog and use Polly as CircuitBreaker. These two capabilities could be implemented as HttpMessageHandler, composed as a chain and used to construct the HttpClient.
The code would look something like the following:

```cs
  var circuitBreakerPolicy = Polly.Policy
      .Handle<HttpRequestException>()
      .CircuitBreakerAsync(exceptionsAllowedBeforeBreaking: 10, durationOfBreak: TimeSpan.FromMinutes(1));

  var httpClientHandler = new HttpClientHandler();
  var nLogRequestResponseLoggerHandler = new NLogRequestResponseLoggerHandler(httpClientHandler);
  var circuitBreakerHandler = new CircuitBreakerHandler(circuitBreakerPolicy, nLogRequestResponseLoggerHandler);

  var httpClient = new HttpClient(circuitBreakerHandler);
```

With HttpClientX you could do the same by using the `HttpClientBuilder` to build an HttpClient. The message handlers can be specified through the usage of `.Use<T>()` and these will be executed in the same order as defined.
The code would look like the following:

```cs
  var circuitBreakerPolicy = Polly.Policy
      .Handle<HttpRequestException>()
      .CircuitBreakerAsync(exceptionsAllowedBeforeBreaking: 10, durationOfBreak: TimeSpan.FromMinutes(1));

  var httpClient = new HttpClientBuilder()
      .Use<CircuitBreakerHandler>(circuitBreakerPolicy)
      .Use<NLogRequestResponseLoggerHandler>()
      .Build();
```

## Getting Started

HttpClientX can be installed using the Nuget package manager or the `dotnet` CLI.

Nuget command line:

```bash
Install-Package HttpClientX
```

DotNet CLI:

```bash
dotnet add package HttpClientX
```

## Autofac

[Autofac](https://autofac.org/) is a well established Inversion of Control container for .Net. Is possible to use Autofac as HTTP message handler factory so that any service registered in the container can be injected as message handler dependency.

Autofac support comes as separate NuGet package that can be installed as follows:

```bash
Install-Package HttpClientX.Autofac
```

DotNet CLI:

```bash
dotnet add package HttpClientX.Autofac
```

To use Autofac capabilities you need to instrument `HttpClientBuilder` to use Autofac as HTTP message handler factory. The `.UseAutofac()` method requires an instance of Autofac `IComponentContext`.

```cs
var httpClient = new HttpClientBuilder()
  .UseAutofac(context) // Use Autofac as HTTP message handler factory
  .Build();
```

To let Autofac create an HTTP message handler this need to be registered as service.

Following is an example of how HttpClientX can be used in conjunction with Autofac in an registration module:

```cs
public class HostModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    // Registering the HTTP message handlers and their dependencies
    builder.RegisterType<NLogRequestResponseLoggerHandler>();
    builder.RegisterType<CircuitBreakerHandler>();
    
    var circuitBreakerPolicy = Polly.Policy
      .Handle<HttpRequestException>()
      .CircuitBreakerAsync(exceptionsAllowedBeforeBreaking: 10, durationOfBreak: TimeSpan.FromMinutes(1));
    
    builder.RegisterInstance(circuitBreakerPolicy);
    
    // Registering the factory to create an HttpClient
    builder.Register(context => BuildHttpClient(context))
      .As<HttpClient>()
      .SingleInstance();
  }
  
  private static HttpClient BuildHttpClient(IComponentContext context)
  {
    var httpClient = new HttpClientBuilder()
        .UseAutofac(context)
        .Use<CircuitBreakerHandler>(circuitBreakerPolicy)
        .Use<NLogRequestResponseLoggerHandler>()
        .Build(); 
        
    return httpClient;        
  }
}
```

**NOTE** HTTP message handler dependencies can be registered in to the container or specified as parameter of the `.Use<>()` method. 
