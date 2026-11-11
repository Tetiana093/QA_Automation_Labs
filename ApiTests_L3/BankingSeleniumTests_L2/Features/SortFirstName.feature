Feature: Manager Customers Sorting
  In order to manage bank clients efficiently
  As a Bank Manager
  I want to be able to sort customers by their First Name

Scenario: Sort customers by First Name descending
  Given I navigate to the banking application homepage
  When I click on the Bank Manager Login button
  And I go to the Customers tab
  And I click on the First Name column header
  Then the customer list should be sorted by First Name