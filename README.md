# GQL Workshop

This repo holds the source code to a sample project created to teach my co-workers about GraphQL backends in dotnet core.

The master branch holds the final version of the code. Intermediate steps can be found in the following branches:

- **`0-db-model-and-rest-api`**  
  Starting point. Simple DB model and REST api.
- **`1-install-and-first-query`**  
  Installs and configures [graphql-dotnet/conventions][1] and set up the first queries.
- **`2-mutation-and-refactor`**  
  Adds a mutation, then refactors it using the [MediatR][2] library.
- **`3-nested-query-and-dataloader`**  
  Adds a _nested_ property to a top-level type, then refactors it to use a data loader, to prevent N+1 problems.

[1]: https://github.com/graphql-dotnet/conventions
[2]: https://github.com/jbogard/MediatR
