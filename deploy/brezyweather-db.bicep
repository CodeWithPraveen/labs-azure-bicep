@description('SQL Server used by the BrezyWeather app')
param sqlServerName string = 'sql-brezyweather-prod-${uniqueString(resourceGroup().id)}'

@description('SQL database used by the BrezyWeather app')
param sqlDatabaseName string = 'sqldb-brezyweather-prod'

@description('Admin user of the SQL Server')
param sqlAdminLogin string

@description('Password of the admin user of the SQL Server')
@secure()
param sqlAdminLoginPassword string

@description('Location for BrezyWeather resources')
param location string = resourceGroup().location

@description('SQL Server')
resource sqlServer 'Microsoft.Sql/servers@2021-02-01-preview' = {
  name: sqlServerName
  location: location
  properties: {
    administratorLogin: sqlAdminLogin
    administratorLoginPassword: sqlAdminLoginPassword
  }
}

@description('SQL Database')
resource sqlDatabase 'Microsoft.Sql/servers/databases@2021-02-01-preview' = {
  parent: sqlServer
  name: sqlDatabaseName
  location: location
  sku: {
    name: 'Basic'
  }
}

@description('Firewall rule to allow SQL Server access by Azure resources (App Service)')
resource allowAllWindowsAzureIps 'Microsoft.Sql/servers/firewallRules@2021-02-01-preview' = {
  parent: sqlServer
  name: 'AllowAllWindowsAzureIps'
  properties: {
    endIpAddress: '0.0.0.0'
    startIpAddress: '0.0.0.0'
  }
}
