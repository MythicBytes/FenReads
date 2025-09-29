function fn() {
  var env = karate.env || 'local';
  karate.log('karate.env system property was:', env);
  
  var config = {
    env: env,
    apiBaseUrl: 'http://localhost:5001',
    defaultTimeout: 5000
  };

  // Configuration par environnement
  if (env == 'docker') {
    config.apiBaseUrl = java.lang.System.getenv('API_BASE_URL') || 'http://fenreads-api';
  } else if (env == 'staging') {
    config.apiBaseUrl = 'https://staging-api.fenreads.com';
  } else if (env == 'production') {
    config.apiBaseUrl = 'https://api.fenreads.com';
  }

  // Configuration des timeouts
  karate.configure('connectTimeout', 5000);
  karate.configure('readTimeout', 5000);
  
  // Configuration SSL (pour staging/production)
  if (env == 'staging' || env == 'production') {
    karate.configure('ssl', true);
  }

  // Headers par défaut
  karate.configure('headers', {
    'Content-Type': 'application/json',
    'Accept': 'application/json'
  });

  // Fonction helper pour l'authentification
  config.authenticate = function(username, password) {
    var result = karate.call('classpath:features/auth/login.feature', {
      username: username || 'testuser@example.com',
      password: password || 'Test123!'
    });
    return result.response.token;
  };

  // Données de test communes
  config.testData = {
    validUser: {
      email: 'testuser@example.com',
      password: 'Test123!',
      username: 'testuser'
    },
    invalidUser: {
      email: 'invalid@example.com',
      password: 'wrong'
    },
    testWork: {
      title: 'Test Manga',
      type: 'manga',
      status: 'ongoing',
      totalChapters: 100
    }
  };

  // Fonction pour générer des IDs uniques
  config.uuid = function() {
    return java.util.UUID.randomUUID() + '';
  };

  // Fonction pour obtenir le timestamp actuel
  config.timestamp = function() {
    return new Date().toISOString();
  };

  karate.log('Base URL:', config.apiBaseUrl);
  
  return config;
}