@UI
Feature: Registration and Login
  In order to use ParaBank's online services
  As a new user
  I want to be able to register, login, and verify validation errors for duplicate registrations
  And I want the site to work properly across different mobile resolutions

  @order1
  Scenario:1 Register a new user with valid details
    Given I am on the registration page
    When I fill out the registration form with valid details
      | FirstName | LastName | Address      | City     | State | ZipCode | PhoneNumber  | SSN         |
      | John      | Doe      | 1234 Main St | New York | NY    | 10001   | 555-123-4567 | 123-45-6789 |
    And I submit the registration form
    Then I should see a confirmation message for successful registration

    @order2
  Scenario:2 Login with the registered username
    Given I am on the login page
    When I login with the registered username and password
    Then I should be logged in successfully
    
    @order3
  Scenario:3 Attempt to register again with the same username
    Given I am on the registration page
    When I fill out the registration form with the same details
    And I submit the registration form
    Then I should see an error message for duplicate registration

    When I capture all network requests
    Then I save all captured network requests to a JSON file


 Scenario: Validate mobile resolution compatibility
    Given I am on the login page
    When I set the browser window size to "iPhone"
    Then the website should display correctly for "iPhone" resolution

    When I set the browser window size to "iPad"
    Then the website should display correctly for "iPad" resolution

    When I set the browser window size to "Android"
    Then the website should display correctly for "Android" resolution

    When I set the browser window size to "Pixel 4"
    Then the website should display correctly for "Pixel 4" resolution

    When I set the browser window size to "Pixel 5"
    Then the website should display correctly for "Pixel 5" resolution
