# Azure-Resume

My personal resume hosted on Azure. It tracks how many people have visited the page using a serverless backend and a NoSQL database.

## How it works

The resume is a static website hosted on Azure Blob Storage. When someone opens the page, JavaScript calls an Azure Function that reads a visitor counter from Cosmos DB, increases it, and returns the updated number. The counter is then displayed on the page.

## Tech Stack

- Frontend — HTML, CSS, JavaScript
- Hosting — Azure Blob Storage (Static Website)
- Backend — Azure Functions (C#, .NET 8)
- Database — Azure Cosmos DB
- CI/CD — GitHub Actions
