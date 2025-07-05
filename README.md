# LocalisationSheet

A full-stack localization management system built with React frontend and .NET backend.

## 🚀 Features

- **Multi-language Support**: Manage translations for multiple languages
- **Real-time Editing**: Inline editing of translation values
- **Search & Filter**: Find keys and translations quickly
- **Pagination**: Handle large datasets efficiently
- **RESTful API**: Clean API endpoints for all operations
- **Swagger Documentation**: Interactive API documentation
- **Docker Support**: Easy deployment with Docker Compose

## 🏗️ Architecture

### Frontend
- **React 18** with TypeScript
- **Vite** for fast development and building
- **Tailwind CSS** for styling
- **Axios** for API communication
- **React Query (@tanstack/react-query)** for server state management
- **Custom React hooks** for business logic and UI state

### Backend
- **.NET 8** Web API
- **Entity Framework Core** with PostgreSQL
- **AutoMapper** for object mapping
- **FluentValidation** for input validation
- **Swagger/OpenAPI** for documentation

## 📋 Prerequisites

- **.NET 8 SDK**
- **Node.js 20**
- **Docker & Docker Compose** (for containerized deployment)
- **PostgreSQL 14** (if running locally)

## 🛠️ Quick Start


```bash
# Clone the repository
git clone <repository-url>
cd LocalisationSheet

# Start all services
docker-compose up --build

# Access the application
# Frontend: http://localhost:5173
# Backend API: http://localhost:5000
# Swagger: http://localhost:5000/swagger
```
```

## 📚 API Endpoints

### Languages
- `GET /api/v1.0/languages` - Get all languages
- `GET /api/v1.0/languages/available` - Get available languages
- `POST /api/v1.0/languages` - Create new language
- `DELETE /api/v1.0/languages/{id}` - Delete language

### Keys
- `GET /api/v1.0/keys` - Get all keys
- `POST /api/v1.0/keys` - Create new key
- `PUT /api/v1.0/keys/{id}` - Update key
- `DELETE /api/v1.0/keys/{id}` - Delete key

### Translations
- `GET /api/v1.0/translations` - Get translations with pagination
- `PUT /api/v1.0/translations/{keyId}/{languageId}` - Upsert translation
- `DELETE /api/v1.0/translations/{keyId}/{languageId}` - Delete translation
