
# Developer Notes

## Building Projects
To build a given project follow the following
1. Open a terminal, `cd` into the root folder for the project
2. From the terminal, enter `dotnet build`

## Running the Web Project
1st Time you run the project you will need to execute the following command on the terminal
```
dotnet dev-certs https --trust
```

To launch the Web API projet, follow the following
1. Open a terminal, `cd` into the root of PrizePicks.Web
2. From the terminal, enter `dotnet run`

## Testing
To run unit tests follow the steps below
1. CD into the APITest folder
2. Run one of the following commands on the terminal

Single run
```
dotnet test
```

Run w/ watch (runs on each save)
```
dotnet watch test
```

## Formatting/Validating

This project has linting/formatting enabled on save, but that is limited.  The project also implemented Roslyn style valication (see [here](https://johnnyreilly.com/eslint-your-csharp-in-vs-code-with-roslyn-analyzers))

To run the validation execute the following from the terminal
cal
```
dotnet format style -v detailed --severity info --verify-no-changes
```

## Swagger Testing
You can view/test the API w/ swagger by following the steps below
1. Run the WebAPI project `dotnet run`
2. Open any browser and navigage to [Swagger Page](http://localhost:5191/swagger/index.html) `http://localhost:5191/swagger/index.html`

## Postman Testing
There is an included Postman collection.  For details on using the collection, [read this](PostmanTesting.md)