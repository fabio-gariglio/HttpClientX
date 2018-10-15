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
