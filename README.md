# README #

This README would normally document whatever steps are necessary to get your application up and running.

### What is this repository for? ###

* This is for code review repository with sample code.
* Focusing C#,ASP.NET, MVC, Unit Testing, CSS & Jquery
* Non relational database project (MongoDB)
* Version 1.0.0.

### Steps to install and configure the prerequisites

	01.	Mongo DB installation
		•	Determine which MongoDB version you need (64 bit may be).
		•	Download the preferred installation package from following link. 
		•	https://www.mongodb.com/download-center?jmp=nav#communityWwrwr (Windows Server 2008 R2 64-bit and later, with SSL support x64) and install it on your local machine. 

	02.	Visual studio 2015
		•	Download the visual studio 2015 from Microsoft and install it with the c# and web development components. You can download it from https://www.visualstudio.com/downloads/

# Steps configure the MongoDB connection

	01.	Installation of MongoDB double clicking on downloaded .msi file.
	
	02.	Setup the Mongo DB Environment
		•	Open an administrator command prompt. (Press the Win key, type cmd.exe, and press Ctrl + Shift + Enter to run the Command Prompt as Administrator.)
		•	MongoDB requires a data directory to store all data. MongoDB’s default data directory path is the absolute path \data\db on the drive from which you start MongoDB. Create this folder by running the following command in a Command Prompt: md \data\db

	03.	Start MongoDB
		•	To start MongoDB, run mongod.exe. For example, from the Command Prompt: "C:\Program Files\MongoDB\Server\3.4\bin\mongod.exe"
		•	This starts the main MongoDB database process. The waiting for connections message in the console output indicates that the mongod.exe process is running successfully.

	04.	Connect MongoDB
		•	To connect to MongoDB through the mongo.exe shell, open another Command Prompt: "C:\Program Files\MongoDB\Server\3.4\bin\mongo.exe"

	05.	Configure a Windows Service for MongoDB Community Edition
		•	Open an Administrator command prompt.
		•	Create directories.
		•	Create directories for your database and log files:
			?	mkdir c:\data\db 
			?	mkdir c:\data\log
		•	Create a configuration file.
		•	Create a configuration file. The file must set systemLog.path. Include additional configuration options as appropriate.
		•	For example, create a file at C:\Program Files\MongoDB\Server\3.4\mongod.cfg that specifies both systemLog.path and storage.dbPath

				systemLog:
				destination: file
				path: c:\data\log\mongod.log
				storage:
				dbPath: c:\data\db

				(***--Create a mongodb.log file manually in c:\data\log\mongodb.log if required when running the service***)


		•	Install the MongoDB service
		•	Run all of the following commands in Command Prompt with “Administrative Privileges”.
		•	Install the MongoDB service by starting mongod.exe with the --install option and the -config option to specify the previously created configuration file.
				?	"C:\Program Files\MongoDB\Server\3.4\bin\mongod.exe" --config "C:\Program Files\MongoDB\Server\3.4\mongod.cfg" –install

		•	Start the MongoDB service.
		•	net start MongoDB

		•	To connect to MongoDB through the mongo.exe shell, open another Command Prompt: "C:\Program Files\MongoDB\Server\3.4\bin\mongo.exe"


	06.	Use Robomongo
		•	Download robomongo and install it (https://robomongo.org/download)
		•	Start the Robomongo after installation (by clicking on Robomongo 0.9.0 icon on desktop) 
		•	Create new connection using 
			?	Name: SomeConnectionName
			?	Address: localhost
			?	Port: 27017 (Next to the address)

# Steps to prepare the source code to build properly.

	01.	Clone or download the source code.
	02.	Keep downloaded path not to be too much long. (Should be Like C:\ Eranga_Ananda_SoftwareEngineer_NET)
	03.	Run visual studio 2015 as Administrator (Write click on visual studio icon and Select Run as administrator)
	04.	Open the sample solution from the extracted folder (C:\ Eranga_Ananda_SoftwareEngineer_NET )
	05.	Restore all the missing NuGet packages by using Manage NuGet packages (Write click on package missing project and select Manage NuGet Packages… and restore if needed)
	06.	Open the Robomongo and create a database BooksDB collection Books and Import the provided Books.json file to Books collection
	07.	Check the MongoDB connection string to following app.config and Web.config files In solution folder structure 

		<connectionStrings>
			<add name="MongoDb.ConnectionString"connectionString="mongodb://localhost:27017/BooksDB" />
		</connectionStrings>

		01.Presentation -> WebApp -> Web.config
		01.Presentation -> WebApi -> Web.config 
		01.Presentation -> WebApi.Tests -> app.config
		01.Presentation -> WebApp.Tests -> app.config
		02.Service -> Service.Tests -> app.config 
		03.Repository -> Core.Repository.Tests -> app.config -> in this config use a deferent database only for Repository testing purpose witch called BooksDBRepositoryTest

### Contribution guidelines ###

* Writing tests
* Code review
* Non relational data base