# Web app using slot auto scale application

## Tecnologies
- .Net 6.0
- Webapp service azure
- Sql Server
- App domain
- App Service Logs

## Repository
[Git](https://github.com/robertath/webapp-autoscale-dotnet-6-GoodsStore)


## Steps

### 1. Create webapp

	Infra\creates-azure.ps1

### 2. App Service Logs

```
Go to Azure portal
App Service created > Monitoring > App Service Logs
Set on to all that you need and send to storage if want
```

### 3. Deploying from Azure portal
```
Go to Azure portal
App service > Deployment > Deployment Center
Select `GitHub`
Select `App Service build service` 
Select Repository: `webapp-autoscale-dotnet-6-GoodsStore` (git repository)
Select Branch: `main`
```

### 4. Scale slots
```
Go to Azure portal
App service > Scale up (vertical)
App service > Scale out (horizontal, add new slots)
	- Manual scale
	- Custom Autoscale
		- Schedule
		- By metrics




