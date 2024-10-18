@API
Feature: API Parabank User Registration and Login

Scenario:1 User registers on Parabank with valid details
   Given Generate Test Registeration User Data
   When I register a new user with the following details
   Then the registration response should be successful
   And the response status code should be 200

Scenario:2 Login with the registered user
   Given I have the login endpoint for Parabank
   When I login using Registered username and password
   Then the login response should be successful
   And the response status code should be 200
