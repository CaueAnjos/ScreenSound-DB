#!/bin/bash

repo=https://github.com/CaueAnjos/ScreenSound-DB.git
project=./src/ScreenSoundCore/ScreenSoundCore.csproj

echo -n "Do you want to setup db for ScreenSound?[y/n]: "
read -r setup

if [[ $setup == 'y' ]] || [[ $setup == 'Y' ]]; then
    echo "cloning, momentaraly, the repo CaueAnjos/ScreenSound-DB"
    git clone $repo --depth 1
    echo "setting up db"
    docker compose ScreenSound-DB/compose.yaml up -d
    dotnet ef database update --project $project
    echo "Done! Your database is now up to date."
fi
