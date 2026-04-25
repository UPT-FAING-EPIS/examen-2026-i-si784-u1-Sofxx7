output "backend_url" {
  value = azurerm_windows_web_app.api.default_hostname
}

output "frontend_url" {
  value = azurerm_static_web_app.frontend.default_host_name
}
