# Screen-SoundDB 🪑🎲

This project is inspired by a **course**. It teaches the basics of **Entity
Framework** and **Minimal API** with **C#**. It implements CRUD methods for
artists, genres, and songs. You can easily play with this using **Nix** ⛄.

## How to Build this thing?

Actualy, it has three projects:

1. ScreenSound
1. ScreenSoundCore
1. ScreenSoundAPI

ScreenSound is the **old** console line aplication (CLI). I plan on getting it
to work again, but right now, it is not stable. ScreenSoundCore is, as the name
implies, where the core functionality lives. It contains the models shared
between API and CLI. ScreenSoundAPI, in other hand, is the actual **API**. Build
this project using `dotnet`.

### Build and run with Nix ❄️

This is the easiest way. Search for **Nix** and install it on your machine.
Remeber: nix can be installed on windows through windows subsystem for Linux
(WSL). After installing it, check if `flake` and `nix-command` are enabled. If
not, enable them, nerd 🤓! (I know you can do it, rigth?)

With nix configured on your machine, just run:

```bash
nix run github:CaueAnjos/ScreenSound-DB#default
```

This should work just fine if you already have the Docker daemon set up. If it
doesn’t, go set up Docker on your machine. With Podman, this process would
potentially be simpler because it doesn't need a daemon. But, for now, this is
not automated 😢.

## Installation

The only official way to install it is through **Nix**. I know, its a bit clunky
for a c# project, but I promise: **Nix** is cool 😎. To install, simply add this
repo to your flake inputs and call the default package. Or, even simpler, run
the command below, and remember to setup the db with the previous build
instructions.

```bash
nix profile add github:CaueAnjos/ScreenSound-DB#default
```

With this, now you have the power of ScreenSound-DB on your palm! run it any
time with `ScreenSoundAPI` on your terminal.

To setup the db, run:

```bash
docker run --rm -d \
  --name ScreenSound-DB \
  -p 1433:1433 \
  -e ACCEPT_EULA=Y \
  -e MSSQL_SA_PASSWORD='[Senha123]' \
  mcr.microsoft.com/mssql/server:2022-latest
```

## Exploring API 🧭

Open `localhost:5000/scalar` in your browser and explore! Add music, artists,
and genres. Mess around with things. If anything breaks, it's a feature 🥶.

## Uninstalling

There is just two steps: stop ScreenSound-DB container. If you installed it into
your system, then there is two steps: stop ScreenSound-DB container and remove
ScreenSoundAPI from your system.

1. stop ScreenSound-DB

```bash
docker stop ScreenSound-DB
```

1. Remove ScreenSoundAPI

```bash
nix profile remove ScreenSound-DB
```
