terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.0"
    }
  }
}

provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "rg" {
  name     = "rg-tournament-app"
  location = var.location
}

# App Service Plan (Free Tier)
resource "azurerm_service_plan" "asp" {
  name                = "asp-tournament-app"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  os_type             = "Windows"
  sku_name            = "F1"
}

# App Service for Backend API
resource "azurerm_windows_web_app" "api" {
  name                = "api-tournament-app-${var.environment}"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  service_plan_id     = azurerm_service_plan.asp.id

  site_config {
    always_on = false
    application_stack {
      dotnet_version = "v8.0"
    }
  }
}

# Static Web App for Frontend
resource "azurerm_static_web_app" "frontend" {
  name                = "swa-tournament-app-${var.environment}"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  sku_tier            = "Free"
  sku_size            = "Free"
}

# SQL Server
resource "azurerm_mssql_server" "sqlserver" {
  name                         = "sql-tournament-app-${var.environment}"
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  version                      = "12.0"
  administrator_login          = var.db_admin_user
  administrator_login_password = var.db_admin_password
}

# SQL Database
resource "azurerm_mssql_database" "sqldb" {
  name           = "TournamentDb"
  server_id      = azurerm_mssql_server.sqlserver.id
  collation      = "SQL_Latin1_General_CP1_CI_AS"
  max_size_gb    = 2
  sku_name       = "Basic"
}
