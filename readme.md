<h1>fotostor.</h1>

<img src="/documentation/thumbnail_fotostorFrontFacingFeed.png">
  
<h1> General Description of Functionality </h1>
<p>fotostor. was conceived as a platform idea for artists to use a social media platform to help spread the reach of their work and to help monetise their work at the same time. Users can register on the platform and receive suggested posts through an algorithm that determines what's popular at the moment. Users can follow other users and get suggested posts based on posts they like on their feed, as well as enable the ability to sell clothing, prints, and licensing rights of their work to other users on the platform. Users can also purchase other artist's works, modify their profile picture and biography.
  
On the e-commerce side of the functionality, users can set their address to purchase clothing, licenses, and prints of the work they wish to purchase and place it in a shopping basket. Once the order has been processed through Stripe, the user receives an email with a receipt of what they've ordered, and if they've purchased a license, a link to the work in question.</p>

<h1> Functionality Showcase Videos </h1>

* [A short video (03:15) created for the Project Showcase before the end of the academic year. Demonstrates functionality.](https://drive.google.com/file/d/1zqKsFN8LpnWjonHwAugrjuA2MIOw-mNq/view?usp=sharing)
* [A long video (53:27) created as part of the final Project Upload. Goes into depth about the functionality, deployment pipelines, the stack, trials and tribulations during development and lessons learned.](https://drive.google.com/file/d/1lEoRs1YFrE5x-L3eb_Zr3ZtSnSVRxL-8/view?usp=sharing)


<h1> Built With </h1>

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

<h1> Documentation </h1>

* [Project Research Document](https://github.com/ryandeering/fotostor/blob/master/documentation/Research%20Document.pdf) - Prepared before a line of code was written. Details similar applications, stack and development risks.
* [Use Cases and Logical Architecture](https://github.com/ryandeering/fotostor/blob/master/documentation/Use%20Cases%20and%20Logical%20Architecture.pdf) - Details user use cases for the functionality of the project, and a graph of the logical architecture of the project.
* [Security & Durability Analysis](https://github.com/ryandeering/fotostor/tree/master/documentation) - OWASP Zap and k6 scalability testing reports are included within the repo.


<h1> Special Thanks </h1>

<p>I'd like to acknowledge and thank the following: Gary Clynch for being my project supervisor and giving me necessary feedback during development. My parents for putting up with me during the long hours. Jarid, Kay, João & Kama for providing sample artwork to help demonstrate the project. </p>


