- [GraphQL:](#graphql)
  - [What is GraphQL?](#what-is-graphql)
  - [Core Concepts of Graph QL:](#core-concepts-of-graph-ql)
    - [Schema:](#schema)
    - [Types](#types)
    - [Resolvers](#resolvers)
  - [GraphQL in .Net:](#graphql-in-net)


<br>
<hr>
<br >


# GraphQL: 

## What is GraphQL?
GraphQL is a query and manipulation language for APIs
GraphQL is also the runtime for fulfilling requests.
GraphQL is not tied to any specific database or storage engine and is instead backed by  existing code and data.


## Core Concepts of Graph QL: 

### Schema: 
  <P> GraphQL server uses a schema to describe the shape of avialable data. This schema defins the hierarchy of types with fields that are populated from your back-end data stores. The schema also specifies exactly which queries and mutations are available for clients to execute. </p>
   
    - describes the API in full.
    - self-documenting 
    - comprised of "Types"
    - Must have a "Root Query Type"

### Types
- Scalar
  * Id
  * Int
  * String
  * Boolean
  * Float
- Objects
    ```
    type: car {
        id: ID!
        make: String!
        model: String!
    }
    note: ! refers to not null.
    ```
  
- Enumeration
-  Subscriptioin
-  Mutation
-  Query
- 

### Resolvers
Resolvers returns data for a given fields.
A resolver is a function that is responsible for populating the data for a single feild in your schema. It can populate data in any way you define, such as by fetching data from a back-end database or Third party.


## GraphQL in .Net: 

**NuGet package needed for graphql**
  - GraphQL.net (Open Source)
  - HotChocolate (Open Source)

  ```
  dotnet new web-n commanderGQL  # create blank project

  # Code to added necessary nuget package using .net CLI
    
  - Nuget package that are required of Hot Chocolate.

  dotnet add package HotChocolate.AspNetCore --version 12.5.0
  dotnet add package HotChocolate.Data.EntityFramework --version 12.5.0

  --
  dotnet add package GraphQL.Server.Ui.Voyager --version 5.2.0


  -Nuget package requried for EF core.

  dotnet add package Microsoft.EntityFrameworkCore.Design --version 5.0.9


  ----------------------
  # vs code extract: 
    
  code -r folderName # this will open folder from vscode.
  -------------------

  ```















