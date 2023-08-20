@description('Pricing tier; default set to Standard')
param skuName string = 'S1'

@description('Admin user of the SQL Server')
param sqlAdminLogin string

@description('Password of the admin user of the SQL Server')
@secure()
param sqlAdminLoginPassword string

@description('Location for all resources.')
param location string = resourceGroup().location

@description('App Service Plan for hosting BrezyWeather app')
var appServicePlanName = 'asp-brezyweather-prod'

@description('App Service for hosting BrezyWeather app')
var websiteName = 'app-brezyweather-prod-${uniqueString(resourceGroup().id)}'

@description('Docker image of the containerized BrezyWeather app')
var dockerImageName = 'codewithpraveen/labs-appservice-azuresql:1.0'

@description('Name of the connection string used in the app service to connect to SQL Server')
param connectionStringName string = 'BrezyWeatherDbConn'

@description('SQL Server used by the BrezyWeather app')
param sqlServerName string = 'sql-brezyweather-prod'

@description('SQL database used by the BrezyWeather app')
var sqlDatabaseName = 'sqldb-brezyweather-prod'

@description('Azure App Service Plan')
resource appServicePlan 'Microsoft.Web/serverfarms@2020-06-01' = {
  name: appServicePlanName
  location: location
  properties: {
    reserved: true
  }
  sku: {
    name: skuName
  }
  kind: 'linux'
}

@description('Azure App Service')
resource website 'Microsoft.Web/sites@2020-12-01' = {
  name: websiteName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: 'DOCKER|${dockerImageName}'
    }
  }
}

// Forming the connecting string. 
// Using environment() function to build the Fully Qualified Domain Name (FQDN).
var connectionString = 'Server=tcp:${sqlServerName}${environment().suffixes.sqlServerHostname},1433;Initial Catalog=${sqlDatabaseName};Persist Security Info=False;User ID=${sqlAdminLogin};Password=${sqlAdminLoginPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'

@description('Connection string used by the App Service to connect to SQL Database')
resource webSiteConnectionStrings 'Microsoft.Web/sites/config@2020-12-01' = {
  parent: website
  name: 'connectionstrings'
  properties: {
    '${connectionStringName}': {
      value: connectionString
      type: 'SQLAzure'
    }
  }
}
