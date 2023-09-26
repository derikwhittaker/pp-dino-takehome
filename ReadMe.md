
## Table of Contents
* [Business Rules](#business-rules)
* [Implementation Decisions](#implentation-decisions)
* [Developer Notes](./docs/DeveloperNotes.md)
* [API/Route Notes](./docs/APINotes.md)
* [Testing with Postman](./docs/PostmanTesting.md)

## Business Rules

## Cages
* [x] Pull List of Cages [API](./docs/APINotes.md#get-all-cages)
* [x] Pull Cage by Id [API](./docs/APINotes.md#get-single-cage)
* [x] Create new Cage [API](./docs/APINotes.md#create-cage)
* [x] Update existing Cage [API](./docs/APINotes.md#edit-cage)
* [x] Powerdown a Cage [API](./docs/APINotes.md#power-down)
* [x] Powerup a Cage [API](./docs/APINotes.md#power-on)
* [x] Associate Dino to Cage [API](./docs/APINotes.md#associate-dinosaur-to-cage)
* [x] Unassociate Dino to Cage [API](./docs/APINotes.md#unassociate-dinosaur-to-cage)


## Dinosaur
* [x] Pull list of Dinosaurs [API](./docs/APINotes.md#get-all-dinosaurs)
* [x] Pull Dinosaurs by Id [API](./docs/APINotes.md#get-single-dinosaur)
* [x] Create a new Dinosaur [API](./docs/APINotes.md#create-dinosaur)
* [x] Update an exsting Dinosaur [API](./docs/APINotes.md#edit-dinosaur)

## Details
* All requests should respond with the correct HTTP status codes and a response, if necessary,
representing either the success or error conditions.
* Data should be persisted using some flavor of SQL.
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


### Technology/Packages Used
This does not have a persisted DB, because of the time constraint we are using json files and repository pattern to support db actions.  


