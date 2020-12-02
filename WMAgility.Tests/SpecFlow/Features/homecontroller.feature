Feature: User can view pages
       In order to get information
       As a user
       I want to access the public pages

Scenario: Homepage
       When I go to page '/'
       Then the http result should be OK

Scenario: Privacy
       When I go to page '/Home/Privacy'
       Then the http result should be OK