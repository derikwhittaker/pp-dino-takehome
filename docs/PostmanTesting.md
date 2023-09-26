
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