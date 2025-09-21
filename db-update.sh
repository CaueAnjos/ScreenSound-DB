#!/bin/bash

project=./src/ScreenSoundCore/ScreenSoundCore.csproj

echo -n "Do you want to reset db for update?[y/n]: "
read -r reset

if [[ $reset == 'y' ]] || [[ $reset == 'Y' ]]; then
    echo "Reseting db"
    docker compose down
    docker compose up
fi

echo "Starting to update the database..."

docker compose start
dotnet ef database update --project $project

echo "Done! Your database is now up to date."
