# APJ.NET
Le projet est composé de 5 sous-projets:
  1. [**APJ-Generator**](//github.com/johnatan1999/APJ.NET-v2/tree/main/Sources/APJ-Generator/APJ-Generator)  
    Générateur de code backend à partir d'un template donné
    - [Mapping des modèles](https://github.com/johnatan1999/APJ.NET-v2/blob/main/Sources/APJ-Generator/APJ-Generator/Actions/Generator/MapGenerator.cs) (Database Frist)
    - [Générateur de controlleur](https://github.com/johnatan1999/APJ.NET-v2/blob/main/Sources/APJ-Generator/APJ-Generator/Actions/Generator/ServiceGenerator.cs)
    - [Générateur de service](https://github.com/johnatan1999/APJ.NET-v2/blob/main/Sources/APJ-Generator/APJ-Generator/Actions/Generator/ServiceGenerator.cs)
    **Code source**
    ![image](https://user-images.githubusercontent.com/50406127/186083203-45e2d99b-50fe-4238-a4c8-fe3a4bab5cce.png)
    [**Template**](https://github.com/johnatan1999/APJ.NET-v2/tree/main/Sources/APJ-Generator/APJ-Generator/Templates)
    ![image](https://user-images.githubusercontent.com/50406127/186083425-c333f953-36d1-4769-a4eb-d93b038b6366.png)

  2. [**APJ.NET**](https://github.com/johnatan1999/APJ.NET-v2/tree/main/Sources/APJ.NET/APJ.NET)
    Moteur du framework côté backend:
      - Gestion de rôle
      - Gestion des utilisateurs
      - Gestion des historiques utilisateur
  3. [**DAO-G**](https://github.com/johnatan1999/APJ.NET-v2/tree/main/Sources/APJ.NET/DAO-G)
    Module d'accès aux données:
      - [Connection avec différente type de base de données](https://github.com/johnatan1999/APJ.NET-v2/blob/main/Sources/APJ.NET/DAO-G/Connection/ConnectionFactory.cs)
      - [Requêtage dynamique](https://github.com/johnatan1999/APJ.NET-v2/blob/main/Sources/APJ.NET/DAO-G/Util/QueryInitializer.cs)
  4. [**apj-react-generator**](https://github.com/johnatan1999/APJ.NET-v2/tree/main/Sources/apj-react-generator)
    Générateur de code frontend (CRUD de base)
      - [Générateur des services](https://github.com/johnatan1999/APJ.NET-v2/blob/main/Sources/apj-react-generator/src/generator/generator.js) (appel des API)
      - [Générateur des pages](https://github.com/johnatan1999/APJ.NET-v2/blob/main/Sources/apj-react-generator/src/generator/generator.js) (Saisie, Modification, Liste, Recherche, Supression)
    **Code source**
    ![image](https://user-images.githubusercontent.com/50406127/186083879-d9dc4534-8f6e-407d-941b-f78de670f7f6.png)

  5. [**apj-react**](https://github.com/johnatan1999/APJ.NET-v2/tree/main/Sources/apj-react)
    Moteur du framework côté frontend  
    Cette partie est composée principalement des composants utilisés, des pages, des services pour l' appel des API et des configurations  
    ![image](https://user-images.githubusercontent.com/50406127/186084822-086a5de0-95ad-4cf1-9746-f98a29d8c0a8.png)

