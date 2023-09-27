
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

## Implentation Decisions

## Known Issues
Ran into an issue when posting/creating a dino, the species enum values were not being serialized correctly.  If had more time, would resolve, but hitting the deadline so rather wrap up w/ what I have and document what I do not.

### Inmemory Database
I realized the specs called to use some flavor of sql, in memory sql and seeding would have expanded the timeline, rather I used simple databas w/ inmemory hard coded data. But this implementation is hidden behind a simple abstraction at the DB layer.

The data will reset each time the application is restarted

### IoC Registration
Went with manual registration vs auto-registration for no real reason other than did not see the need to use reflection to register things.

### Authorization
In a real scenerio we would add auth to each API endpoint, but well that may be overkill for this example.

### Technology/Packages Used
This does not have a persisted DB, because of the time constraint we are using json files and repository pattern to support db actions.  

- VS Code
- Packages Installed
    - .Net Runtime Tool 
    - C#
    - C# Dev Kit
    - CSharpier - Code Formatter


