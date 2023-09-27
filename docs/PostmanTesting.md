
HEY DERIK.... FINISH THIS DOCUMENT


# Testing With Postman

## I created an entire suite of Postman collections for testing.

## Get/Edit Cages

### Fetch all Cages
- Request: `Cages - Get All`
- Notes: No input needed
- Expected Response: HTTP 200

### Fetch Single Cage
- Request: `Cages - Get Single`
- Notes: Provide Id of 1 to 10 as part of the URL and will pass
- Expected Response: HTTP 200


### Fetch Single Cage - Bad Id
- Request: `Cages - Get Single - Failure`
- Notes: Provide Id of greater tha 100
- Expected Response: HTTP 400 Not Found

### Create a new Cage
- Request: `Cages - Create`
- Notes: Provide the body as below, must leave out the ID.  If you omit the capacity it will default to 0, not allowing dinosaurs
- Expected Response: HTTP 200 
- Minimal Body as Json
```
{
    "Capacity": "10",
    "PowerStatus" : 1
}
```

### Update an existing Cage
- Request: `Cages - Update`
- Notes: Provide the body as below, must have a valid ID. 
- Expected Response: HTTP 200 
- Minimal Body as Json
```
{
    "Id": 1,
    "Capacity": "10",
    "PowerStatus" : 1
}
```

### Update an existing Cage - Failure
- Request: `Cages - Update - Failure`
- Notes: Provide an ID that does not exist
- Expected Response: HTTP 404 Not Found
- Minimal Body as Json
```
{
    "Id": 1111,
    "Capacity": "10",
    "PowerStatus" : 1
}
```

## Power Up/Down a Cage

### Powerdown an existing Cage
- Request: `Cages - Powerdown - Single`
- Notes: Provide an ID that exist (2 is valid) and does not have Dinosaurs
- Expected Response: HTTP 200 

### Powerdown an existing Cage - Failure
- Request: `Cages - Powerdown - Single - Failure`
- Notes: Provide an ID that exist and has Dinosaurs - 1
- Expected Response: HTTP 400 Bad Request 

### Powerdown an existing Cage - Failure -- Cage does not exist
- Request: `Cages - Powerdown - Single - Failure`
- Notes: Update the Id in the URL something that does not exist (222 works)
- Expected Response: HTTP 404 Key Not Found

### Powerup an existing Cage
- Request: `Cages - Powerup - Single`
- Notes: Must powerdown a cage first (see ablove), then invoke this w/ the same Id to power it up.
- Expected Response: HTTP 200 

### Powerup an existing Cage - Bad Id
- Request: `Cages - Powerup - Single - Bad Id`
- Notes: Provide a key in the URL that does not exist (222 works)
- Expected Response: HTTP 404 Key Not Found


## Associate Dino to Cage Validation

### Valid
<!-- - Request: `Cages - Associate Dino - Valid`
- Notes: This provide valid Cage `1` and Dino `6`
- Expected Response: HTTP 200 -->

### InValid Cage Id
- Request: `Cages - Associate Dino - Fail - Invalid Cage id`
- Notes: This provide valid Cage `999` (which is invalid) and Dino `1`
- Expected Response: HTTP 404 Not Found

### InValid Dino Id
- Request: `Cages - Associate Dino - Fail - Invalid Dino id`
- Notes: This provide valid Cage `1` and Dino `999` (which is invalid) 
- Expected Response: HTTP 404 Not Found

### InValid Dino for Cage
- Request: `Cages - Associate Dino - Fail - Invalid Dino for Cage`
- Notes: This provide valid Cage `1` and Dino `1` (which is a herbavor) 
- Expected Response: HTTP 400 Bad Request


### InValid Cage at Capacity
- Request: `Cages - Associate Dino - Fail - Cage at Capacity`
- Notes: This provide valid Cage `3` and Dino `5` -- Cage defaults 1 dino and capacity of 1
- Expected Response: HTTP 400 Bad Request


## Unassociate Dino to Cage Validation

### Unassociate Dino From Cage
- Request: `Cages - Unassociate Dino - Valid`
- Notes: To test this, run the associate before you run this.
- Expected Response: HTTP 200


### Unassociate Dino From Cage - Bad Dino Id
- Request: `Cages - Unassociate Dino - Invalid - Bad Dino`
- Notes: Update the dino value in the url (last data point) to something that does not exist
- Expected Response: HTTP 404 Not Found


### Unassociate Dino From Cage - Bad Cage Id
- Request: `Cages - Unassociate Dino - Invalid - Bad Cage`
- Notes: Update the cage value in the url (first data point) to something that does not exist
- Expected Response: HTTP 404 Not Found



## Dinosaur Actions

### Pull all Dinosaurs
Requrest: `Dinosaurs - Get - All`
- Notes: none
- Expected Response: HTTP 200

### Pull Dinosaur by Id
Requrest: `Dinosaurs - Get - Single`
- Notes: Will return requested dino
- Expected Response: HTTP 200

### Pull Dinosaur by Id - Invalid Id
Requrest: `Dinosaurs - Get - Single - Invalid Id`
- Notes: Will return requested dino
- Expected Response: HTTP 404 Not Found

### Create Dinosaur - Valid
Requrest: `Dinosaurs - Create`
- Notes: Will return new dino
- Expected Response: HTTP 200

### Create Dinosaur - Invalid - Missing Name
Requrest: `Dinosaurs - Create - Invalid - Missing Name`
- Notes: Will throw error
- Expected Response: HTTP 400 Bad Request

### Update Dinosaur - Valid
Requrest: `Dinosaurs - Updated`
- Notes: Will return updated Dino
- Expected Response: HTTP 200

### Update Dinosaur - Invalid - Missing Name
Requrest: `Dinosaurs - Updated - Invalid 0 Missing Name`
- Notes: Will throw error
- Expected Response: HTTP 400 Bad Request