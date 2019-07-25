# DotNetCoreRabbitMQ
The basic service of this application is to send mail.

# Configuration
In the main root, you will see the appsettings.json.
{
  "RabbitMQ": {
    "HostUrl": "xx.xx.xx.xx",
    "UserName": "guest",
    "Password": "guest",
    "Queue": "MailQueue"
  }
}

Set your RabbitMQ configuration here:
RabbitMQ Host URL,
RabbitMQ User (optional),
RabbitMQ Password (optional),
RabbitMQ Queues MailQueue (optional)

in MS.Logic/MailLogic/MailSender.cs , you have to set your mail client settings.
  client.Host = "xx.xx.xx.xx";
  string smtpUserName = "xx@xx";
  string smtpPassword = "xx";
  client.Port = 587;
  
For error logs and mail logs, you have to configure your database in the MS.Data/EFDatabase/NotificationApplicationContext.cs

optionsBuilder.UseSqlServer("Server=xx.xx.xx.xx;Database=NotificationApplication;User ID=sa;Password=xx;");

Here is the EntityFramework for .NET Core, EntityFrameworkCore.

# Docker

MSSQL For Linux
For the docker efcore, run this command -> docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<pass>' -p 1433:1433 -d microsoft/mssql-server-linux 
Here is going to run your container for mssql server for linux. Then you have to set your database and tables as a model which you can find in the Model folder.
  
MS.Consumer's Dockerfile
Run this command in the right path, -> docker build -t <tagname> .

Then check your images with this command -> docker images

You will see your console application's image. Then run this command for the container -> docker run -d -i --name <container's name> <tagname(which you give the name of your image)>

Enjoy!
