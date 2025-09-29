# Git Flow Configuration pour FenReads

## Branches principales
- **main** : Branche de production (versions stables)
- **develop** : Branche de développement (intégration continue)

## Branches de support

### Feature (feature/*)
Pour développer de nouvelles fonctionnalités
```bash
# Créer une feature
git checkout -b feature/nom-feature develop

# Finaliser une feature
git checkout develop
git merge --no-ff feature/nom-feature
git branch -d feature/nom-feature
git push origin develop
```

### Release (release/*)
Pour préparer une nouvelle version de production
```bash
# Créer une release
git checkout -b release/1.0.0 develop

# Finaliser une release
git checkout main
git merge --no-ff release/1.0.0
git tag -a v1.0.0 -m "Version 1.0.0"
git checkout develop
git merge --no-ff release/1.0.0
git branch -d release/1.0.0
git push origin main develop --tags
```

### Hotfix (hotfix/*)
Pour corriger des bugs critiques en production
```bash
# Créer un hotfix
git checkout -b hotfix/1.0.1 main

# Finaliser un hotfix
git checkout main
git merge --no-ff hotfix/1.0.1
git tag -a v1.0.1 -m "Hotfix 1.0.1"
git checkout develop
git merge --no-ff hotfix/1.0.1
git branch -d hotfix/1.0.1
git push origin main develop --tags
```

### Bugfix (bugfix/*)
Pour corriger des bugs non-critiques dans develop
```bash
# Créer un bugfix
git checkout -b bugfix/nom-bug develop

# Finaliser un bugfix
git checkout develop
git merge --no-ff bugfix/nom-bug
git branch -d bugfix/nom-bug
git push origin develop
```

## Conventions de nommage

- **Features** : `feature/short-description`
- **Releases** : `release/x.y.z` (semantic versioning)
- **Hotfixes** : `hotfix/x.y.z`
- **Bugfixes** : `bugfix/short-description`
- **Tags** : `v1.0.0`

## Workflow standard

1. Toutes les nouvelles fonctionnalités partent de `develop`
2. Les features sont mergées dans `develop`
3. Quand `develop` est prêt, créer une `release`
4. La release est mergée dans `main` ET `develop`
5. Les hotfixes partent de `main` et sont mergés dans `main` ET `develop`

## Commandes utiles sans git-flow installé

```bash
# Voir toutes les branches
git branch -a

# Nettoyer les branches locales mergées
git branch --merged | grep -v "\*\|main\|develop" | xargs -n 1 git branch -d

# Voir le graphe des branches
git log --graph --pretty=oneline --abbrev-commit --all
```