
# .Net Ninja Dojo Product Catalog API

This API has endpoints to query for and add products to data store.

# Technologies
* [ASP.NET Core 7](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 7](https://docs.microsoft.com/en-us/ef/core/)
* [Cosmos DB](https://learn.microsoft.com/en-us/azure/cosmos-db/)
* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://fluentvalidation.net/)
* [XUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq), [Moq.EntityFrameworkCore](https://www.nuget.org/packages/Moq.EntityFrameworkCore) & [NetArchTest](https://github.com/jbogard/Respawn)

# Run Locally

## Configure CosmosDb Emulator

In order to run locally we're using [CosmosDb Emulator](https://learn.microsoft.com/en-us/azure/cosmos-db/local-emulator?tabs=ssl-netstd21)

## Clone repository

```
  git clone https://raguileraglobant@dev.azure.com/raguileraglobant/ECommerce%20BE%20-%20Ninja%20Feb%202023/_git/ECommerce.Ninja.ProductCatalog
```

## Configure CosmosDB ConnectionString and Database name

We're using [Secret Manager Tool](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows#secret-manager) to store DB Connection configuration

```
	cd ECommerce.Ninja.ProductCatalog/ProductCatalog
	dotnet user-secrets init
	dotnet user-secrets set "CosmosConnectionString" "{yourLocalCosmosDBConnectionString}"
	dotnet user-secrets set "CosmosDataBase" "{yourLocalCosmosDB_DataBaseName}"
```

## Run the application

```
dotnet run --urls "http://localhost:5000"
```

Now you should be able to visit [http://localhost:5000/swagger/index.html]() to see swagger docs. 

# Contribute
We're using [git flow](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow) workflow.

Please follow these guidelines: 

- In order to contribute to the project you need to create a new branch based on development. Use a name for the branch that represents it's purpose e.g "update-readme"

- Create a PR from feature branch to development to integrate your changes.

- There must be at least two reviewers

- The PR must have a linked user story or task
