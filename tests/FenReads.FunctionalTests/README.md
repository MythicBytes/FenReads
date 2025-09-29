# FenReads Functional Tests avec Karate

Ce dossier contient les tests fonctionnels utilisant Karate Framework via Docker.

## Structure

```
FenReads.FunctionalTests/
├── README.md
├── Dockerfile                   # Image Docker pour Karate
├── docker-compose.yml          # Stack de test complète
├── src/
│   └── test/
│       └── resources/
│           ├── karate-config.js     # Configuration globale
│           ├── logback.xml          # Configuration des logs
│           └── features/
│               ├── auth/            # Tests d'authentification
│               ├── works/           # Tests CRUD des œuvres
│               ├── reading/         # Tests de suivi de lecture
│               └── performance/     # Tests de performance
└── reports/                    # Rapports générés
```

## Exécution des tests avec Docker

### Prérequis
- Docker & Docker Compose

### Lancer tous les tests
```bash
cd tests/FenReads.FunctionalTests
docker-compose up --build
```

### Tests spécifiques par tag
```bash
docker-compose run --rm karate-tests --tags @auth
docker-compose run --rm karate-tests --tags @works
docker-compose run --rm karate-tests --tags @performance
```

### Tests parallèles
```bash
docker-compose run --rm -e KARATE_THREADS=5 karate-tests
```

### Avec environnement spécifique
```bash
docker-compose run --rm -e KARATE_ENV=staging karate-tests
docker-compose run --rm -e KARATE_ENV=production karate-tests
```

## Environnements

Les environnements sont configurés dans `karate-config.js` :
- `local` : http://localhost:5000
- `docker` : http://fenreads-api:80
- `staging` : URL de staging
- `production` : URL de production

## Rapports

Les rapports Karate sont générés dans :
- `target/karate-reports/` - Rapports HTML détaillés
- `target/cucumber-html-reports/` - Rapports Cucumber
- `target/gatling/` - Rapports de performance (si Gatling est activé)

## Tests de Performance avec Gatling

Pour exécuter les tests de performance :

```bash
mvn clean test-compile gatling:test
```

## Structure d'un test Karate

```gherkin
Feature: Gestion des œuvres

Background:
  * def baseUrl = karate.env == 'local' ? 'http://localhost:5000' : 'http://fenreads-api'
  * def authToken = call read('classpath:auth/login.feature')
  * header Authorization = 'Bearer ' + authToken.response.token

@works @smoke
Scenario: Créer une nouvelle œuvre
  Given url baseUrl + '/api/works'
  And request { title: 'Test Manga', type: 'manga', status: 'ongoing' }
  When method POST
  Then status 201
  And match response contains { id: '#uuid', title: 'Test Manga' }

@works @performance
Scenario: Test de charge - Listing des œuvres
  Given url baseUrl + '/api/works'
  And param page = 1
  And param size = 50
  When method GET
  Then status 200
  And match response.items == '#[50]'
  And assert responseTime < 1000
```

## CI/CD Integration

Pour GitLab CI :
```yaml
functional-tests:
  stage: test
  image: maven:3-openjdk-17
  services:
    - docker:dind
  script:
    - docker-compose -f tests/FenReads.FunctionalTests/docker-compose.test.yml up -d
    - mvn test -Dkarate.env=docker
    - docker-compose down
  artifacts:
    reports:
      junit:
        - target/karate-reports/*.xml
    paths:
      - target/karate-reports/
```

## Debugging

### Mode debug
```bash
mvn test -Dkarate.options="--tags @debug" -Dkarate.log=DEBUG
```

### UI Mode (Karate UI)
```bash
java -jar karate.jar -i feature.feature
```