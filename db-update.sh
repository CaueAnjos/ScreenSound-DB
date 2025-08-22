#!/bim/bash

echo "Starting to update the database..."

docker compose start
dotnet ef database update --project ScreenSoundCore

echo "Done! Your database is now up to date."
