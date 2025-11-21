#!/bin/bash

project=./src/ScreenSoundCore/ScreenSoundCore.csproj

echo -n "Do you want to setup db for ScreenSound? [y/n]: "
read -r setup

if [[ $setup == 'y' ]] || [[ $setup == 'Y' ]]; then
    echo "Starting Docker Compose services..."
    docker compose up -d

    echo "Running database migrations..."
    dotnet ef database update --project $project

    echo "Done! Your database is now up to date."
fi
