# ToDoListProject

## 安裝 Nuget 套件
- mongocsharpdriver
- MongoDB.FSharp
- Microsoft.AspNetCore
- FSharp.Json
- Swashbuckle.AspNetCore
- Microsoft.OpenApi
- ReportGenerator

## npm安裝(For angular)
- 在 Angular 專案中安裝
  - npm install @angular/material@7.2.0
  - npm install @angular/cdk@7.2.0
  - npm install hammerjs --save
  - npm install @types/hammerjs --save-dev

## API
- https://localhost:7241/swagger/index.html (透過VS執行)
- https://documenter.getpostman.com/view/17520209/UVsHT7xM

## Angular
- 預設開啟的port為 https://localhost:4200

## 計算 Code Coverage (dotnet core coverage)
- dotnet test --collect:"XPlat Code Coverage"
- reportgenerator -reports:./coverage.cobertura.xml -reporttypes:Html -targetdir:./