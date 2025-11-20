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

### Build with Nix ❄️

This is the easiest way. Search for **Nix** and install it on your machine.
Remeber: nix can be installed on windows through windows subsystem for Linux
(WSL). After installing it, check if `flake` and `nix-command` are enabled. If
not, enable them, nerd 🤓! (I know you can do it, rigth?)

With nix configured on your machine, just run:

```bash
nix shell \
  nixpkgs#docker_25 \
  nixpkgs#dotnet-ef \
  nixpkgs#bash \
  nixpkgs#curl \
  --command bash -c '
    curl -fsSL https://raw.githubusercontent.com/CaueAnjos/ScreenSound-DB/v0.0.0/db-setup.bash | bash && \
    nix run github:CaueAnjos/ScreenSound-DB#default
  '
```

This should work just fine if you already have the Docker daemon set up. If it
doesn’t, go set up Docker on your machine. With Podman, this process would
potentially be simpler because it doesn't need a daemon. But, for now, this is
not automated 😢.
