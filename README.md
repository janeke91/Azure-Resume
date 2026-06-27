# Azure-Resume

My personal resume hosted on Azure. It tracks how many people have visited the page using a serverless backend and a NoSQL database.

## How it works

The resume is a static website hosted on Azure Blob Storage. When someone opens the page, JavaScript calls an Azure Function that reads a visitor counter from Cosmos DB, increases it, and returns the updated number. The counter is then displayed on the page.

## Tech Stack

- Frontend — HTML, CSS, JavaScript
- Hosting — Azure Blob Storage (Static Website)
- Backend — Azure Functions (C#, .NET 8)
- Database — Azure Cosmos DB
- CI/CD — GitHub Actions(separate pipelines for frontend and backend)

## Architechture

<img width="1364" height="814" alt="architecture" src="https://github.com/user-attachments/assets/81fb5ecc-81c9-475f-b379-833a572598e6" />

 ## CI/CD

Both pipelines run automatically on every push to main, meaning no manual deployment needed.

Frontend: uploads static files to Azure Blob Storage using az storage blob upload-batch. Authenticates via Storage Account Key (AZURE_STORAGE_KEY).

Backend: builds the .NET 8 project and deploys the Function App using Azure/functions-action. Authenticates via GitHub OIDC with a managed identity.

## Notes

- The backend uses the Cosmos DB SDK directly (ReadItemAsync / ReplaceItemAsync) instead of output bindings, which have compatibility issues with .NET 8 Isolated Worker.
- CORS is configured on the Function App to allow requests from the Blob Storage domain.

## Challenges

- The CosmosDB output binding does not work with .NET 8 Isolated Worker, so I had to use the Cosmos SDK directly to read and write the counter.
- Had to recreate the Cosmos DB account due to a partition key misconfiguration.
- CORS needed to be set at the Function App level, not in code.
- Tracked down a 500 error using Application Insights logs.

## Screenshots
<img width="2558" height="1308" alt="website" src="https://github.com/user-attachments/assets/ca94a231-e5f9-4217-8eb0-ef8126d4651d" />
##
<img width="1168" height="358" alt="cosmosDB" src="https://github.com/user-attachments/assets/10fce007-ac1a-44c9-b554-0ee6aa4f2b5d" />
##
<img width="1526" height="392" alt="fucntion-app" src="https://github.com/user-attachments/assets/e319a689-5c28-440a-b9a5-2fa4ac1b7cb7" />
