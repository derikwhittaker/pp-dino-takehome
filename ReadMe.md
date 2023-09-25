
## Business Rules

* All requests should respond with the correct HTTP status codes and a response, if necessary,
representing either the success or error conditions.
* *Data should be persisted using some flavor of SQL.
* Each dinosaur must have a name.
* Each dinosaur is considered an herbivore or a carnivore, depending on its species.
* Carnivore can only be in a cage with other dinosaurs of the same species.
* Each dinosaur must have a species (See enumerated list below, feel free to add others).
* Herbivore cannot be in the same cage as Carnivore.
* Use Carnivore dinosaurs like Tyrannosaurus, Velociraptor, Spinosaurus and Megalosaurus.
* Use Herbivore like Brachiosaurus, Stegosaurus, Ankylosaurus and Triceratops.


## Implentation Decisions

### Inmemory Database
I realized the specs called to use some flavor of sql, in memory sql and seeding would have expanded the timeline, rather I used simple databas w/ inmemory hard coded data. But this implementation is hidden behind a simple abstraction at the DB layer.

The data will reset each time the application is restarted

## Routes

### Cage
#### Create
* Verb: POST
* Route: /api/cages
* Status: Scaffoleded

#### Edit
* Verb: PUT
* Route: /api/cages/{cageId}
* Status: Scaffoleded

### Get All
* Verb: GET
* Route: /api/cages
* Status: Scaffoleded

### Get Single
* Verb: GET
* Route: /api/cages/{cageId}
* Status: Scaffoleded

### Power Down
* Verb: PUT
* Route: /api/cages/{cageId}/powerdown
* Status: Scaffoleded

### Power On
* Verb: PUT
* Route: /api/cages/{cageId}/poweron
* Status: Scaffoleded

### Dinosaurs
#### Create
* Verb: POST
* Route: /api/dinosaurs
* Status: Scaffoleded

#### Edit
* Verb: PUT
* Route: /api/dinosaurs
* Status: Scaffoleded

### Get All
* Verb: GET
* Route: /api/dinosaurs
* Status: Scaffoleded

### Get Single
* Verb: GET
* Route: /api/dinosaurs/{dinosaurId}
* Status: Scaffoleded




## Developer Notes

### Building Projects
To build a given project follow the following
1. Open a terminal, `cd` into the root folder for the project
2. From the terminal, enter `dotnet build`

### Running the Web Project
1st Time you run the project you will need to execute the following command on the terminal
```
dotnet dev-certs https --trust
```

To launch the Web API projet, follow the following
1. Open a terminal, `cd` into the root of PrizePicks.Web
2. From the terminal, enter `dotnet run`



### Technology/Packages Used
This does not have a persisted DB, because of the time constraint we are using json files and repository pattern to support db actions.  



## Developer 



### Formatting/Validating

This project has linting/formatting enabled on save, but that is limited.  The project also implemented Roslyn style valication (see [here](https://johnnyreilly.com/eslint-your-csharp-in-vs-code-with-roslyn-analyzers))

To run the validation execute the following from the terminal
cal
```
dotnet format style -v detailed --severity info --verify-no-changes
```