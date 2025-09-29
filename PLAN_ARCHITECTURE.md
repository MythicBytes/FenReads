# FenReads - Plan d'Architecture et Fonctionnalités

## 1. FONCTIONNALITÉS PRINCIPALES

### 1.1 Gestion de Collection
- **Catalogue de mangas/BDs/Novels/Webcomics**
  - Import de fichiers (PDF, CBZ, CBR, EPUB, TXT, images)
  - Support spécifique pour light novels, web novels et webcomics
  - Support du format vertical (webtoon/manhua)
  - Métadonnées enrichies (voir section 1.9)
  - Couvertures et vignettes automatiques
  - Catégorisation par type (Manga, BD, Light Novel, Web Novel, Webcomic)
  - Tags personnalisés et genres multiples
  - Recherche et filtres avancés

### 1.2 Système de Lecture
- **Lecteur intégré pour mangas/BDs**
  - Support multi-formats (PDF, CBZ, CBR, images)
  - Navigation page par page ou double page
  - Mode plein écran
  - Zoom et ajustement automatique
- **Lecteur intégré pour webcomics**
  - Défilement vertical continu (webtoon)
  - Chargement progressif des images
  - Support du format long strip
  - Adaptation automatique à la largeur d'écran
- **Lecteur intégré pour novels**
  - Support EPUB, TXT, HTML
  - Mode lecture confortable avec thèmes
  - Réglage de la taille de police et interligne
  - Mode nuit/jour
- **Support multi-plateformes**
  - Interface responsive (mobile/tablette/desktop)
  - PWA pour installation sur smartphones
  - Export vers liseuses (Kobo Libra Colour, Kindle)
  - Synchronisation cloud de la progression
- **Fonctionnalités communes**
  - Marque-pages multiples
  - Reprise de lecture automatique
  - Mode hors-ligne avec téléchargement

### 1.3 Suivi de Lecture
- **Progression personnelle**
  - État de lecture (non lu, en cours, lu, abandonné)
  - Pourcentage de progression par tome
  - Historique de lecture avec dates
  - Statistiques de lecture (temps, pages, volumes)
  - Liste de souhaits et favoris

### 1.4 Système Multi-utilisateurs
- **Gestion des utilisateurs**
  - Authentification et autorisation
  - Profils utilisateurs
  - Rôles (admin, lecteur, invité)
  - Partage de collection avec permissions
  - Synchronisation entre appareils

### 1.5 Scraping et Import
- **Scraping de métadonnées**
  - Nautiljon (prioritaire pour mangas/animes FR)
  - MyAnimeList API
  - AniList API
  - MangaDex metadata
  - NovelUpdates (pour light/web novels)
  - Babelio (pour BDs françaises)
  - Google Books API
  - Import batch avec détection automatique
  - Mise à jour périodique des métadonnées
- **Scraping d'œuvres complètes**
  - Mangas-origines.fr (mangas FR)
  - Chireads.com (novels CN)
  - Sites de webcomics (Webtoon, Tapas, etc.)
  - Téléchargement par chapitres/volumes
  - Gestion de la pagination et navigation
  - Respect des limites de rate et delays
  - Conversion automatique en CBZ/EPUB
  - Détection de mises à jour de chapitres
  - File d'attente avec priorités

### 1.6 Analyse et Gestion de Fichiers
- **Scanner automatique**
  - Surveillance de répertoires sur le NAS
  - Détection automatique de nouveaux fichiers
  - Extraction de métadonnées depuis noms de fichiers
  - Organisation automatique par série/tome
  - Import en masse avec détection de doublons
  - Synchronisation bidirectionnelle NAS <-> DB
- **Gestion des fichiers**
  - Renommage automatique selon patterns configurables
  - Déplacement et organisation dans l'arborescence
  - Détection et suppression de doublons
  - Nettoyage des noms de fichiers
  - Historique des modifications avec rollback possible

### 1.7 Administration du Scraper
- **Module d'administration dans le client**
  - Dashboard avec statut des jobs en cours
  - Configuration des sources de scraping
  - Gestion des règles d'extraction
  - Planification des tâches (cron expressions)
  - Logs et historique des exécutions
  - File d'attente des jobs avec priorités
  - Retry automatique en cas d'échec
  - Statistiques de scraping (succès/échecs)
  - Accès restreint par rôles utilisateur (admin uniquement)
  - Communication via API backend → API scraper

### 1.8 Système de Backup et Récupération
- **Backup local sur NAS (géré par l'application)**
  - Dossier dédié `/volume1/fenreads_backups/` pour rollback
  - Conservation 90 jours des fichiers originaux avant modification
  - Structure : `/backups/YYYY-MM/operation_uuid/original_files/`
  - Nettoyage automatique des backups > 90 jours via container dédié
  - Métadonnées d'opération stockées en JSON
- **Backup cloud externe (informatif - géré hors application)**
  - Instance Duplicati existante sur le NAS
  - Configuration manuelle d'une tâche de backup pour :
    - Source : `/volume1/manga`, `/volume1/bd`, `/volume1/fenreads_backups`
    - Destination : Bucket S3 Infomaniak
    - Planification : Sauvegarde différentielle quotidienne
    - Rétention suggérée : 7 daily, 4 weekly, 12 monthly
- **Stratégie de récupération**
  1. Rollback immédiat depuis backup NAS (< 90 jours)
  2. Restauration Duplicati pour archives (> 90 jours) si configuré
  3. Logs d'audit pour traçabilité complète

### 1.9 Support Mobile et Liseuses
- **Application mobile (PWA)**
  - Installation native sur iOS/Android
  - Mode hors-ligne avec synchronisation
  - Interface tactile optimisée
  - Gestes de navigation (swipe, pinch-to-zoom)
  - Téléchargement sélectif pour économie d'espace
  - Notifications push pour nouveaux chapitres
- **Export vers liseuses via KCC (Kindle Comic Converter)**
  - Support Kobo Libra Colour avec profil couleur dédié
  - Support Kindle, Kobo, Sony Reader
  - Conversion CBZ/CBR → EPUB/MOBI/KEPUB optimisé
  - Optimisation automatique des images (résolution, niveaux de gris/couleur)
  - Détection et suppression des marges blanches
  - Rotation et split des doubles pages
  - Profils prédéfinis par modèle de liseuse
  - Export direct par USB ou envoi par email
  - CLI KCC intégrée dans Docker pour conversion batch
- **Optimisations mobiles**
  - Compression adaptative des images
  - Préchargement intelligent
  - Cache local avec gestion d'espace
  - Mode économie de données

### 1.10 Gestion des Métadonnées
- **Métadonnées communes**
  - Titre original et alternatifs
  - Auteur(s) et illustrateur(s)
  - Éditeur(s) (JP/FR/US)
  - Date de parution (originale et traductions)
  - Statut (En cours, Terminé, Abandonné, Licencié)
  - Nombre de volumes/chapitres
  - Synopsis détaillé
  - Genres et tags
  - Note moyenne et popularité
  - Liens vers sources externes
- **Métadonnées spécifiques Manga/BD**
  - Démographie (Shonen, Seinen, Josei, etc.)
  - Magazine de prépublication
  - Adaptations (anime, drama, etc.)
- **Métadonnées spécifiques Novels**
  - Type (Light Novel, Web Novel, Visual Novel)
  - Nombre de mots/pages
  - Traducteur/équipe de traduction
  - Statut de traduction
  - Site de publication original
- **Enrichissement automatique**
  - Matching intelligent par titre/auteur
  - Fusion de données multi-sources
  - Détection de doublons et variantes
  - Suggestion de métadonnées manquantes


## 2. ARCHITECTURE TECHNIQUE

### 2.1 Stack Technologique
- **Backend**
  - .NET 9
  - ASP.NET Core Web API
  - Entity Framework Core
  - PostgreSQL
  - Redis (cache)
  - Hangfire (jobs)

- **Frontend**
  - React 18+ avec TypeScript
  - Redux Toolkit (state management)
  - Material-UI ou Ant Design
  - PDF.js pour lecture PDF
  - ePub.js pour lecture EPUB
  - SignalR pour temps réel
  - PWA avec Workbox
  - Responsive design (mobile-first)

- **Services**
  - Docker & Docker Compose
  - Stockage direct sur NAS (volumes Docker)
  - Scanner de répertoires (analyse et import automatique)
  - Une seule base PostgreSQL partagée

- **Tests**
  - xUnit (framework de test)
  - Shouldly (assertions)
  - NSubstitute (mocks)
  - Testcontainers (tests d'intégration avec Docker)
  - Karate (tests fonctionnels API via Docker)

- **CI/CD**
  - GitHub Actions
  - Build et tests automatiques sur branches feature/*, bugfix/*, hotfix/*
  - Tests requis pour validation des Pull Requests
  - Protection des branches main et develop
  - Déploiement manuel sur NAS via SSH

### 2.2 Architecture Logicielle (Simplifiée)
```
┌─────────────────────────────────────────────────────────┐
│                Frontend (React) - Port 3000             │
│              (Incluant module admin scraper)            │
│                  ↓ (API calls only)                     │
├─────────────────────────────────────────────────────────┤
│             FenReads API (.NET) - Port 5000             │
│         (Monolithique avec modules intégrés)            │
│  ┌────────────────────────────────────────────────────┐│
│  │ • Authentication & Authorization                    ││
│  │ • Reader & Catalog Management                       ││
│  │ • File Management & Organization                    ││
│  │ • Directory Scanner & Watcher                       ││
│  │ • Scraper Proxy (relaye vers API scraper)          ││
│  └────────────────────────────────────────────────────┘│
│                  ↓ (HTTP API calls)                     │
├─────────────────────────────────────────────────────────┤
│     FenReads.Scraper (Service API) - Port 5001          │
│              (API REST uniquement)                      │
│  ┌────────────────────────────────────────────────────┐│
│  │ • Job Management API                                ││
│  │ • Source Configuration API                          ││
│  │ • Scraping Execution                                ││
│  │ • Returns results via API responses                 ││
│  └────────────────────────────────────────────────────┘│
├─────────────────────────────────────────────────────────┤
│              Data Layer (EF Core + Repos)               │
│           (Accessible uniquement par FenReads.Api)      │
├─────────────────────────────────────────────────────────┤
│  ┌──────────┐  ┌──────────┐  ┌──────────┐             │
│  │PostgreSQL│  │Redis     │  │NAS       │              │
│  │          │  │(Cache)   │  │Storage   │              │
│  └──────────┘  └──────────┘  └──────────┘             │
└─────────────────────────────────────────────────────────┘
```

**Flux de communication:**
- Client React → API principale uniquement
- API principale → API Scraper pour les opérations de scraping
- API principale → Base de données pour stocker les résultats
- Scraper → Retourne les résultats à l'API principale via HTTP

## 3. PRINCIPES ARCHITECTURAUX ET PATTERNS

### 3.1 Domain-Driven Design (DDD)

#### BaseEntity et héritage
```csharp
public abstract class BaseEntity : IAuditableEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    
    // Support des événements domaine
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
}
```

**Justifications :**
- **Identité uniforme** : Toutes les entités ont un Guid comme identifiant unique
- **Traçabilité** : Les champs d'audit permettent de suivre qui modifie quoi et quand
- **Event-driven** : Support natif des événements domaine pour découplage
- **DRY** : Évite la duplication du code d'infrastructure dans chaque entité

#### IAuditableEntity
**Objectif :** Séparer la préoccupation d'audit du reste de la logique métier

**Bénéfices :**
- Permet d'implémenter des intercepteurs EF Core pour mise à jour automatique
- Facilite les tests unitaires en isolant cette préoccupation
- Rend le système d'audit optionnel et configurable

#### ValueObject
```csharp
public abstract class ValueObject
{
    protected abstract IEnumerable<object?> GetEqualityComponents();
    // Égalité basée sur les valeurs, pas l'identité
}
```

**Concept DDD fondamental :**
- Les Value Objects sont définis par leurs valeurs, pas leur identité
- Immutables par design
- Exemples : Email, FilePath, DateRange, Money, Address

**Cas d'usage dans FenReads :**
- `FilePath` : Encapsule la logique de validation des chemins NAS
- `ChapterNumber` : Gère les formats complexes (1, 1.5, 1a, etc.)
- `ReadingPosition` : Position dans un chapitre (page + scroll)
- `ScrapingConfiguration` : Paramètres de scraping immutables

#### IDomainEvent
```csharp
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
```

**Architecture événementielle :**
- Découple les différents bounded contexts
- Permet l'implémentation de patterns avancés (Event Sourcing, CQRS)
- Facilite l'audit et la traçabilité

**Événements domaine planifiés :**
- `WorkAddedEvent` : Nouveau manga/BD ajouté → déclenche scraping métadonnées
- `ChapterCompletedEvent` : Chapitre terminé → met à jour les statistiques
- `FileOperationCompletedEvent` : Opération fichier terminée → met à jour l'index
- `NewChapterAvailableEvent` : Nouveau chapitre détecté → notification utilisateurs
- `DuplicateDetectedEvent` : Doublon trouvé → proposition de nettoyage

### 3.2 Clean Architecture

#### Séparation en couches
```
┌─────────────────────────────────────────┐
│           Presentation (API/UI)         │  → Controllers, ViewModels
├─────────────────────────────────────────┤
│           Application                    │  → Use Cases, Services, DTOs
├─────────────────────────────────────────┤
│           Domain                        │  → Entities, Value Objects, Events
├─────────────────────────────────────────┤
│           Infrastructure                │  → EF Core, External APIs, File I/O
└─────────────────────────────────────────┘
```

**Règles de dépendance :**
- Domain ne dépend de rien
- Application dépend uniquement du Domain
- Infrastructure dépend de Domain et Application
- Presentation dépend de toutes les couches

**Bénéfices pour FenReads :**
- **Testabilité** : Domain et Application testables sans dépendances externes
- **Flexibilité** : Facile de changer de base de données ou API externe
- **Maintenabilité** : Logique métier isolée et facile à comprendre
- **Évolutivité** : Ajout de nouvelles fonctionnalités sans impact sur l'existant

### 3.3 Patterns utilisés

#### Repository Pattern
- Abstraction de l'accès aux données
- Facilite les tests avec des repositories en mémoire
- Permet de changer de système de persistance

#### Unit of Work
- Gestion des transactions
- Cohérence des données
- Optimisation des accès base de données

#### Specification Pattern
- Requêtes réutilisables et composables
- Logique de filtrage dans le domaine
- Exemple : `UnreadChaptersSpecification`, `DuplicateFilesSpecification`

#### Mediator Pattern (via MediatR)
- Découple les contrôleurs de la logique métier
- Gestion centralisée des cross-cutting concerns
- Pipeline behaviors pour validation, logging, etc.

### 3.4 Justifications spécifiques à FenReads

#### Pourquoi cette complexité ?

**1. Gestion de fichiers sur NAS**
- Les FileOperations génèrent des événements pour traçabilité
- ValueObjects pour chemins validés et sécurisés
- Pattern Repository pour abstraction du système de fichiers

**2. Multi-utilisateurs**
- Audit automatique via IAuditableEntity
- Événements pour synchronisation temps réel
- Isolation des contextes utilisateur

**3. Scraping complexe**
- Événements pour orchestration asynchrone
- Specifications pour filtrage intelligent
- Repository pattern pour cache de résultats

**4. Évolution future**
- Architecture prête pour microservices
- Support natif pour Event Sourcing si besoin
- CQRS facilement implémentable

### 3.5 Compromis et simplifications possibles

Pour un MVP ou prototype :
- Supprimer IDomainEvent si pas de temps réel requis
- Fusionner Application et Domain pour petits projets
- Utiliser des DTOs simples au lieu de ValueObjects
- Repository générique au lieu de repositories spécifiques

Cependant, pour FenReads avec ses besoins de :
- Gestion de fichiers critiques sur NAS
- Scraping avec multiples sources
- Multi-utilisateurs avec synchronisation
- Évolution vers des fonctionnalités avancées

→ L'architecture proposée est justifiée et évitera la dette technique.

## 4. ORGANISATION DES RÉPERTOIRES

```
FenReads/
├── src/
│   ├── FenReads.Api/                    # API principale
│   │   ├── Controllers/
│   │   ├── Middleware/
│   │   ├── Filters/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── FenReads.Application/            # Logique métier
│   │   ├── Services/
│   │   │   ├── CatalogService.cs
│   │   │   ├── ReaderService.cs
│   │   │   ├── UserService.cs
│   │   │   └── ScrapingService.cs
│   │   ├── DTOs/
│   │   ├── Mappings/
│   │   ├── Validators/
│   │   └── Interfaces/
│   │
│   ├── FenReads.Domain/                 # Entités et logique domaine
│   │   ├── Entities/
│   │   │   ├── Manga.cs
│   │   │   ├── User.cs
│   │   │   ├── ReadingProgress.cs
│   │   │   └── Collection.cs
│   │   ├── ValueObjects/
│   │   ├── Enums/
│   │   └── Interfaces/
│   │
│   ├── FenReads.Infrastructure/         # Accès données et services externes
│   │   ├── Data/
│   │   │   ├── ApplicationDbContext.cs
│   │   │   ├── Migrations/
│   │   │   └── Configurations/
│   │   ├── Repositories/
│   │   ├── Services/
│   │   │   ├── NasStorageService.cs
│   │   │   ├── DirectoryWatcherService.cs
│   │   │   ├── FileAnalyzerService.cs
│   │   │   ├── CacheService.cs
│   │   │   └── EmailService.cs
│   │   └── External/
│   │       ├── Metadata/
│   │       │   ├── NautiljonScraper.cs
│   │       │   ├── MyAnimeListClient.cs
│   │       │   ├── AniListClient.cs
│   │       │   ├── NovelUpdatesClient.cs
│   │       │   ├── BabelioClient.cs
│   │       │   └── GoogleBooksClient.cs
│   │       ├── Content/
│   │       │   ├── MangasOriginesScraper.cs
│   │       │   ├── ChiReadsScraper.cs
│   │       │   ├── WebtoonScraper.cs
│   │       │   └── ContentDownloader.cs
│   │       └── Converters/
│   │           ├── KccService.cs        # Wrapper pour KCC CLI
│   │           ├── CbzConverter.cs
│   │           └── EpubConverter.cs
│   │
│   ├── FenReads.Scraper/                # Service de scraping API uniquement
│   │   ├── Controllers/
│   │   │   ├── JobsController.cs
│   │   │   ├── SourcesController.cs
│   │   │   └── StatusController.cs
│   │   ├── Jobs/
│   │   ├── Scrapers/
│   │   ├── Models/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── FenReads.FileManager/            # Service de gestion fichiers
│   │   ├── Services/
│   │   │   ├── FileOrganizerService.cs
│   │   │   ├── DuplicateDetector.cs
│   │   │   ├── FileRenamer.cs
│   │   │   └── FileHistoryService.cs
│   │   └── Models/
│   │
│   ├── FenReads.Shared/                 # Code partagé
│   │   ├── Constants/
│   │   ├── Extensions/
│   │   ├── Helpers/
│   │   └── Models/
│   │
│   └── tests/                            # Tests unitaires et intégration
│       ├── FenReads.Domain.Tests/       # Tests unitaires domaine
│       ├── FenReads.Application.Tests/  # Tests unitaires application
│       ├── FenReads.Infrastructure.Tests/ # Tests unitaires infrastructure
│       ├── FenReads.Api.IntegrationTests/ # Tests d'intégration API
│       ├── FenReads.Infrastructure.IntegrationTests/ # Tests d'intégration infra
│       └── FenReads.FunctionalTests/    # Tests fonctionnels Karate
│
├── client/                               # Application React
│   ├── public/
│   ├── src/
│   │   ├── components/
│   │   │   ├── reader/
│   │   │   ├── catalog/
│   │   │   ├── fileManager/
│   │   │   ├── scraperAdmin/           # Module admin scraper
│   │   │   └── shared/
│   │   ├── pages/
│   │   ├── services/
│   │   ├── store/
│   │   ├── hooks/
│   │   ├── utils/
│   │   └── types/
│   ├── package.json
│   └── tsconfig.json
│
├── docker/
│   ├── Dockerfile.api
│   ├── Dockerfile.scraper
│   ├── Dockerfile.client
│   ├── Dockerfile.kcc          # Image avec KCC et dépendances Python
│   └── docker-compose.yml
│
├── scripts/
│   ├── init-db.sql
│   ├── backup.sh
│   └── deploy.sh
│
├── docs/
│   ├── API.md
│   ├── DEPLOYMENT.md
│   └── SCRAPING_CONFIG.md
│
├── .gitignore
├── .editorconfig
├── README.md
└── FenReads.sln
```

## 4. MODÈLES DE DONNÉES PRINCIPAUX

### Œuvre (Base commune)
```csharp
public abstract class Work
{
    public Guid Id { get; set; }
    public WorkType Type { get; set; } // Manga, BD, LightNovel, WebNovel, Webcomic
    public string Title { get; set; }
    public List<string> AlternativeTitles { get; set; }
    public string OriginalTitle { get; set; }
    public string Synopsis { get; set; }
    public WorkStatus Status { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string CoverPath { get; set; }
    public List<string> FilePaths { get; set; }
    public FileFormat Format { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Genre> Genres { get; set; }
    public WorkMetadata Metadata { get; set; }
    public SourceInfo Source { get; set; } // Info sur l'origine (scraping ou local)
}

public class Manga : Work
{
    public string Author { get; set; }
    public string Illustrator { get; set; }
    public string Series { get; set; }
    public int? VolumeNumber { get; set; }
    public int? ChapterCount { get; set; }
    public Demographic Demographic { get; set; }
    public string Magazine { get; set; }
    public List<Adaptation> Adaptations { get; set; }
}

public class Novel : Work
{
    public string Author { get; set; }
    public NovelType NovelType { get; set; } // Light, Web, Visual
    public int? WordCount { get; set; }
    public int? PageCount { get; set; }
    public string Translator { get; set; }
    public TranslationStatus TranslationStatus { get; set; }
    public string OriginalPublisher { get; set; }
}

public class Webcomic : Work
{
    public string Author { get; set; }
    public string Artist { get; set; }
    public WebcomicFormat ComicFormat { get; set; } // Webtoon, Strip, Page
    public int? EpisodeCount { get; set; }
    public string Platform { get; set; } // Webtoon, Tapas, etc.
    public bool IsVerticalScroll { get; set; }
}

public class WorkMetadata
{
    public Dictionary<string, string> ExternalIds { get; set; } // MAL, AniList, Nautiljon, etc.
    public List<Publisher> Publishers { get; set; }
    public decimal? Rating { get; set; }
    public int? Popularity { get; set; }
    public DateTime LastMetadataUpdate { get; set; }
    public List<string> Sources { get; set; }
}

public class SourceInfo
{
    public string SourceType { get; set; } // Local, MangasOrigines, ChiReads, etc.
    public string SourceUrl { get; set; }
    public DateTime LastScrapeDate { get; set; }
    public bool AutoUpdate { get; set; }
    public List<Chapter> AvailableChapters { get; set; }
}
```

### Progression de Lecture
```csharp
public class ReadingProgress
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkId { get; set; }
    public WorkType WorkType { get; set; }
    
    // Pour Manga/BD
    public int? CurrentPage { get; set; }
    public int? TotalPages { get; set; }
    
    // Pour Novels
    public int? CurrentChapter { get; set; }
    public float? ReadingPercentage { get; set; }
    public string LastPosition { get; set; } // Pour EPUB
    
    public ReadingStatus Status { get; set; }
    public DateTime LastReadDate { get; set; }
    public TimeSpan ReadingTime { get; set; }
    public List<Bookmark> Bookmarks { get; set; }
}
```

## 5. STRUCTURE ACTUELLE DU PROJET

### Projets créés
```
FenReads/
├── src/
│   ├── FenReads.Api/                    # API Web ASP.NET Core
│   ├── FenReads.Domain/                 # Entités et logique domaine
│   ├── FenReads.Application/            # Services et logique métier
│   └── FenReads.Infrastructure/         # Accès données (EF Core + PostgreSQL)
│
├── tests/
│   ├── FenReads.Domain.Tests/           # Tests unitaires domaine (xUnit + Shouldly + NSubstitute)
│   ├── FenReads.Application.Tests/      # Tests unitaires application
│   ├── FenReads.Infrastructure.Tests/   # Tests unitaires infrastructure
│   ├── FenReads.Api.IntegrationTests/   # Tests d'intégration API (Testcontainers)
│   ├── FenReads.Infrastructure.IntegrationTests/ # Tests d'intégration infra
│   └── FenReads.FunctionalTests.Runner/ # Runner Karate via Testcontainers
│
├── .github/
│   └── workflows/
│       └── ci.yml                       # Pipeline CI GitHub Actions
│
└── FenReads.sln                         # Solution .NET

```

### Packages NuGet installés
- **Infrastructure** : EF Core 9.0, Npgsql.EntityFrameworkCore.PostgreSQL
- **API** : Swashbuckle.AspNetCore (Swagger)
- **Tests** : xUnit, Shouldly, NSubstitute, Testcontainers (PostgreSQL, Redis)

### État actuel
- ✅ Architecture Clean Architecture/DDD mise en place
- ✅ Projets de tests configurés avec frameworks de test
- ✅ Pipeline CI/CD GitHub Actions configuré
- ✅ Support Docker et Testcontainers
- ✅ Git Flow configuré (main/develop + features)
- ⏳ Modèles de domaine à créer
- ⏳ Dockerfiles à configurer

## 6. PLAN DE DÉVELOPPEMENT

### Phase 1 - Core (2-3 semaines) ✅ En cours
1. ✅ Setup projet et architecture
2. ⏳ Modèles de données et migrations
3. ⏳ API de base (CRUD manga/utilisateurs)
4. ⏳ Authentification JWT
5. ⏳ Upload et stockage fichiers

### Phase 2 - Lecture (2 semaines)
1. Lecteur PDF/images
2. Navigation et zoom
3. Sauvegarde progression
4. Marque-pages

### Phase 3 - Catalogue (2 semaines)
1. Import batch
2. Extraction métadonnées
3. Recherche et filtres
4. Génération vignettes

### Phase 4 - Scraping & File Management (2 semaines)
1. Intégration APIs externes
2. Interface admin du scraper
3. Service de gestion fichiers
4. Détection/suppression doublons
5. Renommage automatique
6. Jobs planifiés
7. Intégration KCC pour export liseuses

### Phase 5 - Optimisation (1 semaine)
1. Cache Redis
2. Pagination optimisée
3. Compression images
4. PWA support

## 7. SÉCURITÉ

- Authentification JWT avec refresh tokens
- Autorisation basée sur les rôles
- Validation stricte des entrées
- Protection CSRF
- Rate limiting API
- Chiffrement des données sensibles
- Logs d'audit
- Backup automatique

## 8. CONFIGURATION DOCKER POUR NAS

### Docker Compose
```yaml
version: '3.8'
services:
  fenreads-api:
    build: ./docker/Dockerfile.api
    volumes:
      - /volume1/manga:/app/storage/manga:rw  # Lecture-écriture pour gestion fichiers
      - /volume1/bd:/app/storage/bd:rw
      - fenreads-data:/app/data
    environment:
      - NAS_MANGA_PATH=/app/storage/manga
      - NAS_BD_PATH=/app/storage/bd
      - ConnectionStrings__DefaultConnection=Host=db;Database=fenreads;Username=fenreads;Password=password
      - SCRAPER_API_URL=http://fenreads-scraper:80
      - SCRAPER_API_KEY=your-secure-api-key
    ports:
      - "5000:80"
    depends_on:
      - db
      - redis
      - fenreads-scraper
    networks:
      - fenreads-network

  fenreads-scraper:
    build: ./docker/Dockerfile.scraper
    ports:
      - "5001:80"  # API REST uniquement (pas d'interface web)
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - API_KEY=your-secure-api-key  # Clé API pour sécuriser la communication
    networks:
      - fenreads-network

  kcc-converter:
    build: ./docker/Dockerfile.kcc
    volumes:
      - /volume1/manga:/app/input:ro
      - /volume1/exports:/app/output:rw
      - kcc-profiles:/app/profiles
    environment:
      - KCC_PROFILE=KoboLibraColour  # Profil par défaut
    networks:
      - fenreads-network

  backup-cleaner:
    image: alpine:latest
    volumes:
      - /volume1/fenreads_backups:/backups:rw
    command: >
      sh -c "while true; do
        find /backups -type d -mtime +90 -exec rm -rf {} + 2>/dev/null;
        sleep 86400;
      done"
    restart: unless-stopped
    networks:
      - fenreads-network

  db:
    image: postgres:15
    volumes:
      - postgres-data:/var/lib/postgresql/data
    environment:
      - POSTGRES_DB=fenreads
      - POSTGRES_USER=fenreads
      - POSTGRES_PASSWORD=password
    networks:
      - fenreads-network

  redis:
    image: redis:7-alpine
    volumes:
      - redis-data:/data
    networks:
      - fenreads-network

  client:
    build: ./docker/Dockerfile.client
    ports:
      - "3000:80"
    environment:
      - REACT_APP_API_URL=http://localhost:5000
    depends_on:
      - fenreads-api
    networks:
      - fenreads-network

networks:
  fenreads-network:
    driver: bridge

volumes:
  postgres-data:
  redis-data:
  fenreads-data:
  kcc-profiles:
```

## 9. SERVICES DE GESTION DE FICHIERS

### Service de Scanner
```csharp
public interface IDirectoryScanner
{
    Task<ScanResult> ScanDirectoryAsync(string path);
    Task<List<MangaFile>> DetectNewFilesAsync();
    Task SyncWithDatabaseAsync();
    void StartWatching(string[] directories);
}
```

### Service de Gestion de Fichiers avec Backup
```csharp
public interface IFileManager
{
    Task<FileOperation> RenameFileAsync(string path, string newName);
    Task<FileOperation> MoveFileAsync(string source, string destination);
    Task<FileOperation> OrganizeBySeriesAsync(List<MangaFile> files);
    Task<List<Duplicate>> FindDuplicatesAsync(string directory);
    Task<bool> DeleteDuplicatesAsync(List<Duplicate> duplicates, DuplicateStrategy strategy);
    Task<bool> RollbackOperationAsync(Guid operationId);
}

public class NasBackupService : IBackupService
{
    private const string BACKUP_ROOT = "/volume1/fenreads_backups";
    private const int RETENTION_DAYS = 90;
    
    public async Task<string> BackupFilesAsync(FileOperation operation, List<string> files)
    {
        var backupPath = $"{BACKUP_ROOT}/{DateTime.Now:yyyy-MM}/{operation.Id}";
        
        // Copier les fichiers originaux vers le backup
        foreach (var file in files)
        {
            var destPath = Path.Combine(backupPath, "original_files", Path.GetFileName(file));
            await File.CopyAsync(file, destPath);
        }
        
        // Sauvegarder les métadonnées
        var metadata = new {
            OperationId = operation.Id,
            OperationType = operation.Type,
            Timestamp = DateTime.Now,
            Files = files,
            ExpiresAt = DateTime.Now.AddDays(RETENTION_DAYS)
        };
        
        await File.WriteAllTextAsync(
            Path.Combine(backupPath, "metadata.json"),
            JsonSerializer.Serialize(metadata)
        );
        
        return backupPath;
    }
    
    public async Task<bool> RestoreFromBackupAsync(Guid operationId)
    {
        // Recherche du backup
        var backupPath = FindBackupPath(operationId);
        if (!Directory.Exists(backupPath)) return false;
        
        // Restauration des fichiers
        var metadata = await LoadMetadata(backupPath);
        foreach (var originalFile in metadata.Files)
        {
            var backupFile = Path.Combine(backupPath, "original_files", Path.GetFileName(originalFile));
            await File.MoveAsync(backupFile, originalFile, overwrite: true);
        }
        
        return true;
    }
}

public enum DuplicateStrategy
{
    KeepBestQuality,
    KeepNewest,
    KeepOldest,
    Manual
}
```

### Service KCC pour conversion liseuses
```csharp
public interface IKccConverterService
{
    Task<ConversionResult> ConvertForDeviceAsync(
        string inputPath, 
        string outputPath, 
        KccProfile profile);
    
    Task<byte[]> ConvertToKepubAsync(string cbzPath, KoboModel model);
    Task<byte[]> ConvertToMobiAsync(string cbzPath, KindleModel model);
    List<KccProfile> GetAvailableProfiles();
    Task<bool> CreateCustomProfileAsync(KccProfile profile);
}

public class KccProfile
{
    public string Name { get; set; }
    public string Device { get; set; } // KoboLibraColour, KindlePaperwhite, etc.
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsColor { get; set; }
    public bool Manga { get; set; } // Right to left
    public bool SplitDoublePages { get; set; }
    public bool RemoveMargins { get; set; }
    public int Quality { get; set; } // 1-100
}
```

### APIs de Communication Backend/Scraper
```csharp
// Dans FenReads.Api - Proxy vers le scraper
public class ScraperProxyController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IScraperResultService _resultService;
    
    [HttpGet("/api/scraper/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetStatus()
        => await ForwardToScraper("/api/status");
    
    [HttpPost("/api/scraper/jobs/start")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> StartJob([FromBody] JobRequest request)
        => await ForwardToScraper("/api/jobs/start", request);
    
    [HttpGet("/api/scraper/sources")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetSources()
        => await ForwardToScraper("/api/sources");
    
    [HttpPost("/api/scraper/execute")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ExecuteScraping([FromBody] ScrapingRequest request)
    {
        // Appelle le scraper
        var result = await ForwardToScraper("/api/execute", request);
        
        // Stocke les résultats dans la DB
        await _resultService.SaveResults(result);
        
        return Ok(result);
    }
}

// Dans FenReads.Scraper - API REST uniquement
[ApiController]
[Route("api")]
public class ScraperController : ControllerBase
{
    [HttpGet("status")]
    public IActionResult GetStatus();
    
    [HttpPost("jobs/start")]
    public IActionResult StartJob([FromBody] JobRequest request);
    
    [HttpPost("execute")]
    public async Task<ScrapingResult> ExecuteScraping([FromBody] ScrapingRequest request);
    
    [HttpGet("sources")]
    public IActionResult GetConfiguredSources();
}
```

### Pattern de Nommage Supportés
- `[Série] - Tome XX.cbz`
- `Série_TXX_Chapitre_XXX.pdf`
- `Série/Tome XX/001.jpg`
- Détection automatique basée sur regex configurables

## 10. DÉPLOIEMENT SUR NAS

### Prérequis NAS
- Docker et Docker Compose installés
- Minimum 4GB RAM disponible
- Accès SSH activé
- Ports requis:
  - 5000: API principale
  - 5001: Interface admin du scraper
  - 3000: Frontend React
- Permissions lecture/écriture sur les dossiers manga/BD
- Instance Duplicati existante (configuration manuelle du backup)

### Installation
```bash
# 1. Cloner le repository sur le NAS
git clone https://github.com/user/fenreads.git
cd fenreads

# 2. Configurer les variables d'environnement
cp .env.example .env
# Éditer .env avec les chemins NAS corrects

# 3. Build et démarrage
docker-compose up -d

# 4. Initialiser la base de données
docker-compose exec fenreads-api dotnet ef database update

# 5. Premier scan des répertoires
docker-compose exec fenreads-scanner dotnet run --scan-now
```

### Backup et récupération
- **Application** : Backup automatique 90j dans `/volume1/fenreads_backups/`
- **Base de données** : Backup PostgreSQL via script cron
- **Duplicati externe** : Configurer manuellement une tâche pour :
  - Sources : `/volume1/manga`, `/volume1/bd`, `/volume1/fenreads_backups`
  - Destination : Infomaniak S3
- Les fichiers manga/BD originaux restent sur le NAS