Feature: Booker CRUD Operations
  
Scenario: End-to-end CRUD test for a booking
  Given I authenticate as admin
  When I create a new booking
  Then the booking should be created successfully
  When I get the booking details
  Then the details should match the created booking
  When I update the booking
  Then the booking should be updated successfully
  When I delete the booking
  Then the booking should no longer exist