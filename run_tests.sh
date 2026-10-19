#!/bin/bash
dotnet test --collect:"XPlat Code Coverage" --results-directory:./TestResults
reportgenerator -reports:"./TestResults/**/coverage.cobertura.xml" -targetdir:"./coveragereport" -reporttypes:Html
echo "Отчет готов: ./coveragereport/index.html"
