# ERecrutement — Gestion des offres et candidatures

Application web de recrutement développée en C# avec ASP.NET Core MVC.
Elle propose deux espaces, candidat et recruteur, pour consulter
des offres d’emploi, enregistrer des candidatures et gérer des offres.

Les données sont stockées dans SQL Server avec Entity Framework Core.
L’authentification et les rôles reposent sur ASP.NET Core Identity.

## Fonctionnalités présentes dans le code

### Comptes utilisateurs

- Inscription avec choix du rôle Candidat ou Recruteur.
- Connexion et déconnexion.
- Restrictions d’accès aux contrôleurs selon le rôle.
- Navigation adaptée au rôle de l’utilisateur connecté.

### Espace candidat

- Consultation des offres d’emploi.
- Enregistrement d’une candidature à une offre.
- Consultation de l’historique personnel des candidatures.
- Annulation d’une candidature.

Ces actions nécessitent un profil candidat associé au compte.

### Espace recruteur

- Création d’une offre.
- Consultation des offres associées au recruteur connecté.
- Modification et suppression d’offres.

Les offres comprennent les informations suivantes :

- Poste.
- Secteur.
- Profil recherché.
- Type de contrat.
- Rémunération.

La création d’une offre nécessite un profil recruteur associé au compte.

## Technologies utilisées

| Technologie | Rôle |
|---|---|
| C# / .NET 8 | Langage et plateforme |
| ASP.NET Core MVC | Contrôleurs et vues de l’application |
| Razor et Razor Pages | Interfaces et pages de gestion des comptes |
| ASP.NET Core Identity | Authentification et rôles |
| Entity Framework Core 8 | Accès aux données et migrations |
| SQL Server | Base de données configurée |
| HTML, CSS et Bootstrap | Présentation de l’interface |

Le package SQLite est également référencé dans le projet,
mais la configuration active utilise SQL Server.

## Organisation

| Dossier ou fichier | Description |
|---|---|
| `Controllers/` | Actions liées aux offres, candidats et recruteurs |
| `Models/` | Entités de l’application |
| `Views/` | Vues Razor |
| `Areas/Identity/` | Pages de gestion des comptes |
| `Data/` | Contexte Entity Framework Core |
| `Migrations/` | Évolution du schéma de la base |
| `wwwroot/` | Ressources statiques |
| `Program.cs` | Services, authentification, rôles et routage |

## Modèle de données

- **ApplicationUser** : compte utilisateur géré par Identity.
- **Candidat** : informations du candidat et lien vers son compte.
- **Recruteur** : informations du recruteur et de son entreprise.
- **Offre** : offre d’emploi associée à un recruteur.
- **Candidature** : association entre un candidat et une offre,
  avec date de candidature.

Un recruteur peut publier plusieurs offres.
Un candidat peut avoir plusieurs candidatures.
Chaque candidature concerne une offre.

## Installation

### Prérequis

- SDK .NET 8.
- Une instance SQL Server accessible.
- Visual Studio avec les outils de développement ASP.NET,
  ou un éditeur compatible avec .NET.
- Outils Entity Framework Core pour appliquer les migrations.

### 1. Cloner le dépôt

```bash
git clone https://github.com/salma-jnt/ERecrutement.git
cd ERecrutement
```

### 2. Restaurer les dépendances

```bash
dotnet restore
```

### 3. Configurer la base de données

Dans le projet `ERecrutement`, adapter la chaîne
`ConnectionStrings:DefaultConnection` à votre instance SQL Server.

Ne pas publier de mots de passe dans le dépôt.
Pour les identifiants locaux, utiliser une configuration
non versionnée, des variables d’environnement ou les secrets utilisateur .NET.

### 4. Appliquer les migrations

Dans Visual Studio :

1. Ouvrir `ERecrutement.sln`.
2. Définir `ERecrutement` comme projet de démarrage.
3. Ouvrir la console du Gestionnaire de package.
4. Sélectionner `ERecrutement` comme projet par défaut.
5. Exécuter :

```powershell
Update-Database
```

La base doit être initialisée avant le lancement :
l’application cherche à créer les rôles au démarrage.

### 5. Lancer l’application

Depuis le dossier contenant la solution :

```bash
dotnet run --project ERecrutement/ERecrutement.csproj
```

Ouvrir l’adresse indiquée dans le terminal.

## Compétences mobilisées

- Développement web avec ASP.NET Core MVC.
- Modélisation relationnelle.
- Persistance avec Entity Framework Core.
- Gestion des migrations de base de données.
- Authentification et autorisation par rôles.
- Création de formulaires et de vues Razor.

## Réalisation

Salma Janati-Idrissi.
