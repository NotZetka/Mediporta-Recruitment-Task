## Recruitment Task for Mediporta Job Application

### Technologies Used:
- .NET 8
- ASP.NET
- EntityFramework Core
- MSSQL
- Docker
- Swagger
- XUnit

### How to Run Docker:
To run Docker using command prompt, execute the following command:

```bash
docker-compose -f "docker-compose.yml" -f "docker-compose.override.yml" -f "obj\Docker\docker-compose.vs.debug.g.yml" -p mediportadocker --ansi never up -d
```

Alternatively, you can use Docker Compose in Visual Studio.

### Task Description:
Prepare a REST API in .NET 8 and C#, internally based on a list of tags provided by the StackOverflow API (https://api.stackexchange.com/docs). Project assumptions include:

- Fetch a minimum of 1000 tags from the SO API into a local database or another persistent cache.
- The fetching can occur either at startup or upon the first request, either entirely or gradually for only missing data.
- Calculate the percentage share of tags in the entire fetched population (source field count, appropriately converted).
- Provide tags through paginated APIs with sorting options by name and share in both directions.
- Provide an API method to force re-fetching of tags from SO.
- Provide the OpenAPI definition for the prepared API methods.
- Include logging, error handling, and runtime service configuration.
- Prepare a few selected internal unit tests for service implementation.
- Prepare a few selected integration tests based on the provided API.
- Utilize containerization to ensure repeatable building and running of the project.
- Publish the solution in a GitHub repository.
- The entire solution should run with just the command "docker compose up".

### How to Run:
1. Clone this repository.
2. Navigate to the project directory.
3. Execute the Docker Compose command mentioned above.
