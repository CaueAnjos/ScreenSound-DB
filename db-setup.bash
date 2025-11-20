#!/bin/bash

repo=https://github.com/CaueAnjos/ScreenSound-DB.git
project=./src/ScreenSoundCore/ScreenSoundCore.csproj

echo -n "Do you want to setup db for ScreenSound? [y/n]: "
read -r setup

if [[ $setup == 'y' ]] || [[ $setup == 'Y' ]]; then
    echo "Cloning repository..."
    git clone $repo --depth 1
    cd ScreenSound-DB || exit

    echo "Starting Docker Compose services..."
    docker compose up -d

    echo "Running database migrations..."
    dotnet ef database update --project $project

    echo "Done! Your database is now up to date."

    cd ..
fi
