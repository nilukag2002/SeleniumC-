Feature: CreateUser

@mytag
@mytag
Scenario: Create User Details
Given  I have the API url to create a user
When I call the API with post parameters for user creation
Then User details are created