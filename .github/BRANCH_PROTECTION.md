# Configuration des Branch Protection Rules

## Configuration à appliquer sur GitHub

### Pour la branche `main`

1. Aller dans **Settings → Branches**
2. Cliquer sur **Add rule**
3. Pattern : `main`
4. Cocher :
   - ✅ **Require a pull request before merging**
     - ✅ Require approvals (1)
     - ✅ Dismiss stale pull request approvals when new commits are pushed
     - ✅ Require review from CODEOWNERS
   - ✅ **Require status checks to pass before merging**
     - ✅ Require branches to be up to date before merging
     - Status checks requis :
       - `Build and Test`
       - `Code Quality Check`
       - `PR Status Check`
   - ✅ **Require conversation resolution before merging**
   - ✅ **Require linear history**
   - ✅ **Include administrators**
   - ✅ **Restrict who can push to matching branches**
     - Ajouter uniquement les mainteneurs

### Pour la branche `develop`

1. Pattern : `develop`
2. Cocher :
   - ✅ **Require a pull request before merging**
     - ✅ Require approvals (1)
   - ✅ **Require status checks to pass before merging**
     - Status checks requis :
       - `Build and Test`
       - `Code Quality Check`
       - `PR Status Check`
   - ✅ **Require conversation resolution before merging**
   - ✅ **Require branches to be up to date before merging**

## Workflow Git Flow

### Création d'une feature
```bash
git checkout develop
git pull origin develop
git checkout -b feature/ma-feature
# Développement...
git push -u origin feature/ma-feature
```

### Création d'une PR
1. Push sur la branche feature
2. GitHub Actions lance automatiquement les tests
3. Créer la PR vers `develop`
4. Les checks doivent être verts avant merge
5. Au moins 1 review requise

### Statuts requis
- ✅ **Build and Test** : Compilation et tous les tests passent
- ✅ **Code Quality Check** : Format du code et analyse de sécurité
- ✅ **PR Status Check** : Validation globale

## Configuration locale recommandée

```bash
# Configurer les hooks pre-push
git config core.hooksPath .githooks

# Vérifier le format avant commit
dotnet format --verify-no-changes

# Lancer les tests localement
dotnet test
```