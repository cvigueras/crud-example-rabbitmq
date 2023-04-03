# crud-example-rabbitmq

# 1.- Initial setup:

Open power shell and run as administrator then run the following command

```bash
Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString(‘https://chocolatey.org/install.ps1'))
```

To install RabbitMQ, run the following command from the command line or from PowerShell:

```bash
choco install rabbitmq
```

Dependencies: 

RabbitMQ requires a 64-bit supported version of Erlang, for windows download ==> https://github.com/erlang/otp/releases/download/OTP-25.1.2/otp_win64_25.1.2.exe and install it.

Navigate to the sbin directory of the RabbitMQ Server installation directory.

C:\Program Files (x86)\RabbitMQ Server\rabbitmq_server-3.3.4\sbin (maybe could be in a different directory)
Run the following command to enable the plugin

```bash
rabbitmq-plugins.bat enable rabbitmq_management
```

Open your browser and type http://localhost:15672/

# Setup connection string to SQL Server:

Go to ==> Crud.Example.RabbitMQ/Presentation/Crud.Example.Presentation/appsettings.json and change the username and password for your local SQL Server instance:

ConnectionStrings": {
    "ConnStr": "Data Source=localhost\\SQLEXPRESS; Initial Catalog=Crud.Example; User Id=xxxx; Password=xxxxx"
  },

# 2.- Description:

The application has been created with N Layers, with the principle of single responsibility, with each of the Application layer services having an assigned role, and consists of an API, in this case, home delivery of food.

You have the following entities:

* Shop
* Dealer
* Order
* Food

Related to each other. The entities have been mapped in the Database, through the EntityFramework Core ORM.

For its correct operation, it is necessary to modify the file: appsettings.json, changing the connection credentials to the Database.

If the database does not exist, it will be created alone, with its data seed, if it exists it will not be created.

It has been implemented with claims and JWT, an authentication process.

The API with Swagger has been implemented.

A user must be previously registered with Swagger, for example, and then authenticated in the API.

The third-party DLL that has been used has been Automapper, to be able to map the entities with the DTOs, although this step does not make much sense right now, because it has not given me time to implement more Business logic in the domain layer, for this mapping to be really necessary.

I haven't had time to test, mainly because it didn't make much sense to do them since there is no logic in the domain layer to test either. For this reason, I have not been able to implement the Test with Mock data, and even with accurate data, since the Local connection to it is possible.

The messaging project was created to send Orders to Shops with RabbitMQ and an event would collect those messages to automatically notify the Shop that an order had been assigned to it. I have the server mounted in Local. I have used Domain Events to notify RabbitMQ about the removed items and expired items.

The IoC layer handles the Dependency Injection responsibility of the entire application. To decouple it from the Presentation layer, a project has been created for that.
