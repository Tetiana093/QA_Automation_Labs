Feature: Public API Testing
  
Scenario: Get a random cat fact
  When I request a random cat fact
  Then the status code should be 200 OK
  And the response should contain a text fact