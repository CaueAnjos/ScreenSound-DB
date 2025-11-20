# Screen-SoundDB 🪑🎲

this project is inspired from a course. It teaches the basics of **Entity
Framework** and **Minimal API** with **C#**. It implements CRUD methods for
artists, genres and musics. You can esealy play with this using **nix** ⛄.

## How to Build this thing?

Actialy, it has three projects:

1. ScreenSound
1. ScreenSoundCore
1. ScreenSoundAPI

ScreenSound is the **old** console line aplication (CLI). I have plans on
getting this thing work again, but, right now, it is not stabel. ScreenSoundCore
is, as the name implies, where the core functuonality stays. It contains the
models that are shared between API and CLI. ScreenSoundAPI, in other hand, is
the actual **API**. Build this project with `dotnet`.

### Build with Nix ❄️

This is the eseast way. Search for **nix** and install it in your machime.
Remeber: nix can be installed on windows, but with windows subsystem for Linux
(WSL). Finished intalling, check if `flake` and `nix-command` are enabled. If
not so, enable it nerd 🤓! (I know you can do so, rigth?)

With nix configured in your machime, just run:

```bash
nix shell nixpkgs#docker_25 nixpkgs#dotnet-ef nixpkgs#bash nixpkgs#curl
curl -s https://github.com/CaueAnjos/ScreenSound-DB/blob/v0.0.0/db-setup.bash | bash
nix run github:CaueAnjos/ScreenSound-DB#default
```

This should work just fine if you allready have docker deamon setup. If this
does not work for you, go set docker on your machime. With podman, this process
would be, potentionaly, simpler, becouse it doesn't need a deamon. But, for now,
this is not automated 😢.
