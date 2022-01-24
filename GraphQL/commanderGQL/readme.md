- [GraphQL:](#graphql)
  - [What is GraphQL?](#what-is-graphql)
  - [Core Concepts of Graph QL:](#core-concepts-of-graph-ql)
    - [Schema:](#schema)
    - [Types](#types)
    - [Resolvers](#resolvers)
  - [GraphQL in .Net:](#graphql-in-net)
  - [Code snippets for GraphQL:](#code-snippets-for-graphql)
    - [Use of DbContextPool and changes mades to query class](#use-of-dbcontextpool-and-changes-mades-to-query-class)
  - [Defininig Query](#defininig-query)
  - [Adding Graphql description in EF core Models.](#adding-graphql-description-in-ef-core-models)
  - [Adding Graphql description in types](#adding-graphql-description-in-types)
  - [Mutations](#mutations)
  - [GraphQl Queries:](#graphql-queries)
  - [Diagram using Voyager UI.](#diagram-using-voyager-ui)


<br>
<hr>
<br>


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
    ```graphql
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

### Resolvers
Resolvers returns data for a given fields.
A resolver is a function that is responsible for populating the data for a single feild in your schema. It can populate data in any way you define, such as by fetching data from a back-end database or Third party.


## GraphQL in .Net: 

**NuGet package needed for graphql**
  - GraphQL.net (Open Source)
  - HotChocolate (Open Source)

  ```console
  dotnet new web-n commanderGQL  # create blank project
  ```
  **Code to added necessary nuget package using .net CLI** <br>
  *Nuget package that are required of Hot Chocolate.*
  ```console  
  dotnet add package HotChocolate.AspNetCore --version 12.5.0
  dotnet add package HotChocolate.Data.EntityFramework --version 12.5.0

  dotnet add package GraphQL.Server.Ui.Voyager --version 5.2.0
  ```

  **Nuget package requried for EF core.**
  ```console
  dotnet add package Microsoft.EntityFrameworkCore.Design --version 5.0.9
  ```

  **vs code extract:**

  ```console
  code -r folderName # this will open folder from vscode.
  ```


## Code snippets for GraphQL: 
To use graphql in our project first we should register our graphql service in `startup.cs` file.
```csharp
  services.AddGraphQLServer()
```
Also in middleware section in `startup.cs` we use provide endpoints for our graphql using following code.
```csharp
   app.UseEndpoints(endpoints => { endpoints.MapGraphQL(); });
```
if we want to see the Voyager UI than add the following endpoints.
```csharp 
   app.UseGraphQLVoyager(new VoyagerOptions() { GraphQLEndPoint="/graphql" }, path: "/graphql-voyager")
```

Since Graphql is not bound to any language or framework, it is not adept at understanding the CLR classes. i.e c# POCO Classes. So in graphql we should expose our domain class using types.

### Use of DbContextPool and changes mades to query class

Adding our application context to the services
```csharp
        //this services provide error if we run multiples queries simualtanesouly.
        services.AddDbContext<ApplicationDbContext>(options => {
                options
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .UseSqlServer(Configuration.GetConnectionString("default"));
        });
```
The fault with this type of registering our DbContext is that we cannot run multiples queries simualtanesouly. To overcome this we will register our DbContext with `AddPooledDbContextFactory`. 

```csharp
 services.AddPooledDbContextFactory<ApplicationDbContext>(options => {
                options
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(Configuration.GetConnectionString("default"));
            });
```

## Defininig Query
To expose our data through the graphql API, we can do that using a QueryType. For doing this we should declare a class that expose our model and register that class in `startup.cs`

content of Query.cs
```csharp
 public class Query
    {

        //added to support DbcontextPool
        [UseDbContext(typeof(ApplicationDbContext))]
        // [UseProjection]  // used to get the foreign key refrenced tables data.
        public IQueryable<Platform> GetPlatforms([ScopedService] ApplicationDbContext context)
            //creating the scoped Dependency Imjection services
        {
            return context.Platforms;
        }

        
        [UseDbContext(typeof(ApplicationDbContext))]
        // [UseProjection]
        public IQueryable<Command> GetCommands([ScopedService] ApplicationDbContext context)
        {
            return context.Commands;
        }

    }
```

now to register our type class add the following code to `startup.cs` class, here we are adding code where we previously registered our graphql server. 
```csharp
   services
      .AddGraphQLServer()
      .AddQueryType<Query>()
```

whenever we add functionality to our `Query.cs` or our graphql file we need to registered those functionality to our services. for example if I add projection, sorting, and filtering functionality to graphql,  our code looks something like this.

```csharp
    ....
        [UseDbContext(typeof(ApplicationDbContext))]
        [UseFiltering]
        [UseSorting]
        [UseProjection]  // used to get the foreign key refrenced tables data.
        public IQueryable<Platform> GetPlatforms([ScopedService] ApplicationDbContext context)
            //creating the scoped Dependency Imjection services
        {
            return context.Platforms;
        }

    .....
```

and our `startup.cs` looks something like this:
```csharp
  services
        .AddGraphQLServer()
        .AddQueryType<Query>()
        .AddProjections()  //use to add UseProjection in qurey.cs
        .AddFiltering()
        .AddSorting()
```

## Adding Graphql description in EF core Models.
We can also add descriiption to our EF core model class that we are exposing and provide details about that class or the column(property) present in that class and their purpose. It is fairly easy to do so, all we need to do is add `` attributes to our class and properties.

```csharp
    [GraphQLDescription(@"Represent the command and howto perform those command.")]
    public class Command
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string HowTo { get; set; }
        [Required]
        [GraphQLDescription("Command to run on command line interface.")]
        public string CommandText { get; set; }
        [Required]
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
```
## Adding Graphql description in types
In our above code we see that both our DataAnnotation and GraphQl description are in single file which may clutter our ef data model. 
so next way of doing that will be creating types. In graphql types are used to specify the fields of the domain classes you would like to expose. To create a type in graphql we should create a class that extends from `ObjectGraphType<T>` where T is our ef model class.

```csharp
 public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Field(p => p.LicenseKey).Ignore();

            descriptor
                .Field(p => p.Commands)
                .ResolveWith<PlatformResolver>(c => c.GetCommands(default, default))
                .UseDbContext<ApplicationDbContext>();
        }
    }

    public class PlatformResolver
    {
        public IQueryable<Command> GetCommands([Parent] Platform platform, [ScopedService] ApplicationDbContext context)
        {
            return context.Commands.Where(x => x.PlatformId == platform.Id);
        }
    }
```
Also, In graphql types we can choose the fields that we are going to expose in addition to adding description, add our own resolver and others things. And just like our functionality we should register our type in `startup.cs` file. 

```csharp
 services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<PlatformType>()
    .AddType<CommandTypes>()
    .AddFiltering()
    .AddSorting()
```

## Mutations 

Mutation is way of adding, updating and delete data in graphql. To add mutation capability to our graphql, we add a class that have methods to add records to our database and then we register that to `startup.cs`.

```csharp
services
  .AddGraphQLServer()
  .AddQueryType<Query>()
  .AddType<PlatformType>()
  .AddType<CommandTypes>()
  // .AddType<PlTypes>()
  // .AddProjections()  //use to add UseProjection in qurey.cs
  .AddFiltering()
  .AddSorting()
  .AddMutationType<Mutations>()
  ;
```

## GraphQl Queries: 
All queries are send to Server using Http POST method.

**Getting platforms**
```graphql 
query {
	platforms {
		id
		name
	}
}
```


**Getting related class as well (it uses Project functionality)**
```graphql
query{
	platforms{
		name
		commands {
			howTo
			commandText
		}
	}
}
```

**parallel queries** (fails in case of Dbcontext only data service, we should use pooled context)
```graphql
query {
	a: platforms {
		id
		name
	}
	b: platforms {
		id
		name
	}
	c: platforms {
		id
		name
	}
}
```

**Mutations**
```graphql
mutation {
	addCommand(input: { howto: "preform directory Listing", commandtext: "ls -la", platformId: 4}) {
		command {
			howTo, 
			commandText,
			platform{
				id
			}
		}
	}
}
```


## Diagram using Voyager UI.
![image](./Screenshot%202022-01-24%20150945.png)