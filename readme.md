<h1>fotostor.</h1>

<img src="/documentation/thumbnail_fotostorFrontFacingFeed.png">
  
<h1> General Description </h1>
<p>fotostor. was conceived as a platform idea for artists to use a social media platform to help spread the reach of their work and to help monetise their work at the same time. Users can register on the platform and receive suggested posts through an algorithm that determines what's popular at the moment. Users can follow other users and get suggested posts based on posts they like on their feed, as well as enable the ability to sell clothing, prints, and licensing rights of their work to other users on the platform. Users can also purchase other artist's works, modify their profile picture and biography.
  
On the e-commerce side of the functionality, users can set their address to purchase clothing, licenses, and prints of the work they wish to purchase and place it in a shopping basket. Once the order has been processed through Stripe, the user receives an email with a receipt of what they've ordered, and if they've purchased a license, a link to the work in question.</p>

<h1>Built With </h1>

* [Blazor WebAssembly](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) - The web framework used
* [ASP.NET 5 Core Web Api](https://dotnet.microsoft.com/apps/aspnet/apis) - REST API
* [IdentityServer4](https://identityserver4.readthedocs.io/en/latest/) - Authorisation framework to secure API
* [Selenium IDE](https://www.selenium.dev/selenium-ide/) - Integrated web testing
* [Azure Pipelines](https://azure.microsoft.com/en-us/services/devops/pipelines/) - Cloud hosted CI/CD pipeline defined by YAML
* [xUnit](https://xunit.net/) - Unit testing framework for ASP.NET Core
* GenFu & Moq - Libraries to generate realistic testing data used in conjunction with xUnit
* [Stripe.NET](https://github.com/stripe/stripe-dotnet) - Official .NET Stripe library, used for transactions
* [ImageSharp](https://github.com/SixLabors/ImageSharp) - Image processing library
* [SendGrid](https://github.com/sendgrid/sendgrid-csharp) - Email library to send proof of transaction
* [Google Cloud Storage API for .NET](https://www.nuget.org/packages/Google.Cloud.Storage.V1/) - For uploading of images to a cloud-hosted bucket



