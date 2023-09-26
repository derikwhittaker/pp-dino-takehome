# API Routes

## Overview
Each of the routes are below.  There is a postman collection in this project, each route has a corrisponding postment action that can be run/invoked and is documented via the `Postman Collection` value under the route.

## Cage
### Create Cage
* Verb: POST
* Route: /api/cages
* Postman Collection: `Cages - Create`
* Status: Completed

### Edit Cage
* Verb: PUT
* Route: /api/cages/{cageId}
* Postman Collection: `Cages - Update`
* Status: Completed

## Get All Cages
* Verb: GET
* Route: /api/cages
* Postman Collection: `Cages - Get All`
* Status: Completed

## Get Single Cage
* Verb: GET
* Route: /api/cages/{cageId}
* Postman Collection: `Cages - Get Single`
* Status: Completed

## Power Down
* Verb: PUT
* Route: /api/cages/{cageId}/powerdown
* Postman Collection: `Cages - Powerdown - Single`
* Status: Completed

## Power On
* Verb: PUT
* Route: /api/cages/{cageId}/poweron
* Postman Collection: `Cages - Powerup - Single`
* Status: Completed

## Associate Dinosaur to Cage
* Verb: PUT
* Route: /api/cages/{cageId}/associatedinosaur/{dinosaurId}
* Postman Collection: `Cages - Associate Dino - *`
* Status: Completed

## Unassociate Dinosaur to Cage
* Verb: PUT
* Route: /api/cages/{cageId}/unassociatedinosaur/{dinosaurId}
* Postman Collection: `Cages - Unssociate Dino`
* Status: Completed

## Dinosaurs
### Create Dinosaur
* Verb: POST
* Route: /api/dinosaurs
* Status: Scaffoleded

### Edit Dinosaur
* Verb: PUT
* Route: /api/dinosaurs
* Status: Scaffoleded

## Get All Dinosaurs
* Verb: GET
* Route: /api/dinosaurs
* Status: Scaffoleded

## Get Single Dinosaur
* Verb: GET
* Route: /api/dinosaurs/{dinosaurId}
* Status: Scaffoleded
