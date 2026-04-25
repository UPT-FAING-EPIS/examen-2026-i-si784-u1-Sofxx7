# 🏆 Tournament App — Plataforma de Torneos Deportivos en Línea

[![Build Backend](https://github.com/UPT-FAING-EPIS/examen-2026-i-si784-u1-Sofxx7/actions/workflows/ci-backend.yml/badge.svg)](https://github.com/UPT-FAING-EPIS/examen-2026-i-si784-u1-Sofxx7/actions)
[![SonarCloud](https://sonarcloud.io/api/project_badges/measure?project=tournament-app&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=tournament-app)

Plataforma web para organizar, gestionar y participar en torneos deportivos en línea. Permite la inscripción de equipos/jugadores, generación automática de brackets de eliminatorias, programación de partidos y seguimiento de resultados en tiempo real.

## 🚀 Tecnologías

| Capa | Tecnología |
|------|-----------|
| Backend | .NET 8, Entity Framework Core, JWT |
| Frontend | React 18 + Vite + TypeScript |
| Base de Datos | SQL Server 2022 |
| IaC | Terraform + Azure |
| CI/CD | GitHub Actions |
| Code Scanning | SonarCloud, Semgrep, Snyk |

## 📁 Estructura del Proyecto

```
├── backend/                  # API .NET 8 (Clean Architecture)
│   ├── src/
│   │   ├── TournamentApp.API/
│   │   ├── TournamentApp.Domain/
│   │   ├── TournamentApp.Application/
│   │   └── TournamentApp.Infrastructure/
│   └── tests/
│       ├── TournamentApp.UnitTests/
│       └── TournamentApp.IntegrationTests/
├── frontend/                 # React + Vite
├── infrastructure/           # Terraform (Azure)
├── docs/                     # Documentación
├── .github/workflows/        # GitHub Actions
└── docker-compose.yml        # SQL Server local
```

## 🛠️ Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 18+](https://nodejs.org/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Terraform](https://www.terraform.io/downloads) (para despliegue)

## ⚡ Inicio Rápido

### 1. Levantar Base de Datos
```bash
docker-compose up -d
```

### 2. Ejecutar Backend
```bash
cd backend/src/TournamentApp.API
dotnet run
```
La API estará disponible en `https://localhost:5001` y Swagger en `https://localhost:5001/swagger`.

### 3. Ejecutar Frontend
```bash
cd frontend
npm install
npm run dev
```
El frontend estará disponible en `http://localhost:5173`.

## 📋 Endpoints Principales

| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | `/api/auth/register` | Registrar usuario |
| POST | `/api/auth/login` | Iniciar sesión |
| GET | `/api/tournaments` | Listar torneos |
| POST | `/api/tournaments` | Crear torneo |
| GET | `/api/tournaments/{id}` | Detalle de torneo |
| POST | `/api/teams` | Registrar equipo |
| GET | `/api/matches?tournamentId={id}` | Partidos por torneo |
| PUT | `/api/matches/{id}/result` | Registrar resultado |

## 🧪 Tests
```bash
cd backend
dotnet test --collect:"XPlat Code Coverage"
```

## 🌐 URLs de Producción

- **Aplicación**: _Pendiente_
- **API Swagger**: _Pendiente_
- **Repositorio**: [GitHub](https://github.com/UPT-FAING-EPIS/examen-2026-i-si784-u1-Sofxx7)

## 📄 Licencia

Este proyecto es parte del examen de la asignatura SI784 — Universidad Privada de Tacna.
