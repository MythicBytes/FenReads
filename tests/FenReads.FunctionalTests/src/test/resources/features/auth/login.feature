Feature: Authentication
  Tests d'authentification utilisateur

Background:
  * url apiBaseUrl

@auth @smoke
Scenario: Login avec des identifiants valides
  Given path '/api/auth/login'
  And request testData.validUser
  When method POST
  Then status 200
  And match response contains 
    """
    {
      token: '#string',
      expiresIn: '#number',
      user: {
        id: '#uuid',
        email: '#(testData.validUser.email)',
        username: '#string'
      }
    }
    """

@auth @negative
Scenario: Login avec des identifiants invalides
  Given path '/api/auth/login'
  And request testData.invalidUser
  When method POST
  Then status 401
  And match response contains { error: '#string' }

@auth
Scenario: Register un nouvel utilisateur
  * def uniqueEmail = 'user_' + uuid() + '@test.com'
  Given path '/api/auth/register'
  And request 
    """
    {
      email: '#(uniqueEmail)',
      password: 'Test123!',
      username: 'testuser_#(uuid())',
      confirmPassword: 'Test123!'
    }
    """
  When method POST
  Then status 201
  And match response contains { id: '#uuid', email: '#(uniqueEmail)' }

@auth @security
Scenario: Accès à une route protégée sans token
  Given path '/api/users/profile'
  When method GET
  Then status 401

@auth @security
Scenario: Accès à une route protégée avec token valide
  * def authToken = call read('login.feature@login-helper')
  Given path '/api/users/profile'
  And header Authorization = 'Bearer ' + authToken
  When method GET
  Then status 200
  And match response contains { id: '#uuid', email: '#string' }

@ignore @login-helper
Scenario: Helper pour obtenir un token
  Given path '/api/auth/login'
  And request testData.validUser
  When method POST
  Then status 200
  * def authToken = response.token