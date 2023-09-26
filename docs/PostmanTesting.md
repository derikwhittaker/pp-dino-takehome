
# Testing With Postman

## I created an entire suite of Postman collections for testing.


## Associate Dino to Cage Validation

### Valid
<!-- Request: `Cages - Associate Dino - Valid`
Notes: This provide valid Cage `1` and Dino `6`
Expected Response: HTTP 200 -->

### InValid Cage Id
Request: `Cages - Associate Dino - Fail - Invalid Cage id`
Notes: This provide valid Cage `999` (which is invalid) and Dino `1`
Expected Response: HTTP 404 Not Found

### InValid Dino Id
Request: `Cages - Associate Dino - Fail - Invalid Dino id`
Notes: This provide valid Cage `1` and Dino `999` (which is invalid) 
Expected Response: HTTP 404 Not Found

### InValid Dino for Cage
Request: `Cages - Associate Dino - Fail - Invalid Dino for Cage`
Notes: This provide valid Cage `1` and Dino `1` (which is a herbavor) 
Expected Response: HTTP 400 Bad Request


### InValid Cage at Capacity
Request: `Cages - Associate Dino - Fail - Cage at Capacity`
Notes: This provide valid Cage `3` and Dino `5` -- Cage defaults 1 dino and capacity of 1
Expected Response: HTTP 400 Bad Request


## Unassociate Dino to Cage Validation


### Valid Cage Id
Request: `Cages - Unassociate Dino - Valid`
Notes: To test this, run the associate before you run this.
Expected Response: HTTP 200



## Dinosaur Actions

### Pull all Dinosaurs
Requrest: `Dinosaurs - Get - All`
Notes: none
Expected Response: HTTP 200

### Pull Dinosaur by Id
Requrest: `Dinosaurs - Get - Single`
Notes: Will return requested dino
Expected Response: HTTP 200

### Pull Dinosaur by Id - Invalid Id
Requrest: `Dinosaurs - Get - Single - Invalid Id`
Notes: Will return requested dino
Expected Response: HTTP 404 Not Found

### Create Dinosaur - Valid
Requrest: `Dinosaurs - Create`
Notes: Will return new dino
Expected Response: HTTP 200

### Create Dinosaur - Invalid - Missing Name
Requrest: `Dinosaurs - Create - Invalid - Missing Name`
Notes: Will throw error
Expected Response: HTTP 400 Bad Request

### Update Dinosaur - Valid
Requrest: `Dinosaurs - Updated`
Notes: Will return updated Dino
Expected Response: HTTP 200

### Update Dinosaur - Invalid - Missing Name
Requrest: `Dinosaurs - Updated - Invalid 0 Missing Name`
Notes: Will throw error
Expected Response: HTTP 400 Bad Request