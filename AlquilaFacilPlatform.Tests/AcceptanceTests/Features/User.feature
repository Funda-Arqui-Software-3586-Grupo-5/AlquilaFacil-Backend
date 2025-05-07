Feature: User Management
    In order to manage user information effectively
    As an administrator
    I want to be able to update user details

    Scenario: Update username
        Given a user with username "initialUsername" and password hash "passwordHash"
        When the administrator updates the username to "newUsername"
        Then the username should be "newUsername"

    Scenario: Update password hash
        Given a user with username "username" and password hash "initialHash"
        When the administrator updates the password hash to "newHash"
        Then the password hash should be "newHash"
