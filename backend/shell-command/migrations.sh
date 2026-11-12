dotnet ef migrations add Init \
    --output-dir ./Infrastructure/Database/Migrations \
    --project ./src/Modules/SavedMessage/SavedMessage.csproj \
    --startup-project ./src/Host/Host.csproj
