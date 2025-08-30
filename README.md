# SeleniumLoginTests

Este proyecto contiene pruebas automatizadas con **Selenium WebDriver** en .NET utilizando **NUnit**.

## 🚀 Requisitos

- .NET 9 SDK o superior
- Google Chrome
- ChromeDriver (se instala automáticamente con NuGet)
- Paquetes NuGet:
  - Selenium.WebDriver
  - Selenium.WebDriver.ChromeDriver
  - Microsoft.Extensions.Configuration
  - Microsoft.Extensions.Configuration.Json
  - NUnit
  - NUnit3TestAdapter
  - Microsoft.NET.Test.Sdk

## ⚙️ Configuración

1. Crear un archivo `appsettings.json` en la raíz del proyecto con la siguiente estructura (Las sigueintes credenciales son únicamente con fines de ejemplo):

```json
{
  "TestUser": {
    "Email": "usuario@test.com",
    "Password": "123456"
  },
  "BaseUrl": "https://dev.one21.app"
}
```

2. En el archivo `.csproj`, asegurarse de que `appsettings.json` se copie al directorio de salida:

```xml
<ItemGroup>
  <None Update="appsettings.json">
    <CopyToOutputDirectory>Always</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

## ▶️ Ejecución de pruebas

En la raíz del proyecto, ejecutar:

```bash
dotnet test
```

Esto abrirá Chrome y ejecutará las pruebas definidas.

## 📋 Casos de Prueba

1. **Login_Exitoso**: Valida que el login redirija a `/home`.
2. **Navegacion_Empleados**: Valida que la navegación a Empleados funcione.
3. **Navegacion_Configuracion**: Valida que la navegación a Configuración funcione.
4. **Validar_PageNotFound**: Valida que al acceder a `/dashboard` aparezca el mensaje *Page Not Found ⚠️* y que el botón *Back To Home* redirija correctamente. Actualmente existe un **bug** que redirige al login en lugar de `/home`.

## 🛡️ Notas de Seguridad

- **No subas credenciales reales al repositorio.**
- Usa credenciales ficticias en `appsettings.json` si es un trabajo académico.
- Si usas credenciales reales en local, mantén `appsettings.json` en el `.gitignore`.

