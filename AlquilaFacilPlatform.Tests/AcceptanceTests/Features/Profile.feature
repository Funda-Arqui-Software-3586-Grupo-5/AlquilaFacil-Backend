Feature: Profile Management
    In order to manage user profiles effectively
    As an administrator or user
    I want to be able to create and update profiles

    Background:
        Given a profile with the following details:
            | Name         | FatherName | MotherName | DateOfBirth | DocumentNumber | Phone       | UserId |
            | initialName  | initialFn  | initialMn  | 1990-01-01  | 1234567890    | 9876543210  | 1      |

    Scenario: Create profile with initial details
        When the administrator creates the profile
        Then the profile should be created successfully

    Scenario: Update profile details
        When the administrator updates the profile with the following details:
            | Name    | FatherName | MotherName | DateOfBirth | DocumentNumber | Phone       | UserId |
            | newName | newFn      | newMn     | 2000-02-02  | 9876543210    | 1234567890  | 1      |
        Then the profile details should be updated successfully

    #Scenario: Update profile with invalid data
    #   When the administrator updates the profile with invalid data:
    #        | Name | FatherName | MotherName | DateOfBirth | DocumentNumber | Phone       | UserId |
    #        | ""   | newFn      | newMn     | 2000-02-02  | 9876543210    | 1234567890  | 1      |
    #    Then an error should occur "Invalid profile data"

    Scenario: Update profile with missing required fields
        When the administrator updates the profile with missing required fields:
            | Name    | FatherName | DateOfBirth | UserId |
            | newName | newFn      | 2000-02-02  | 1      |
        Then the profile details should be updated successfully
