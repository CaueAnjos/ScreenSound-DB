{
  inputs.nixpkgs.url = "github:NixOS/nixpkgs/nixpkgs-unstable";
  inputs.flake-utils.url = "github:numtide/flake-utils";

  outputs = {
    self,
    flake-utils,
    nixpkgs,
    ...
  }:
    flake-utils.lib.eachDefaultSystem (system: let
      pkgs = nixpkgs.legacyPackages.${system};
      dotnet-sdk = with pkgs.dotnetCorePackages;
        combinePackages [
          sdk_9_0
          sdk_8_0
        ];
      docker = pkgs.docker_25;

      container-cmd = {pkg}: let
        pname = pkg.pname;
        exe = "${pkg}/bin/${pname}";
      in ''
        cleanup() {
          echo "Stopping ${pname} database container..."
          ${exe} stop ScreenSound-DB 2>/dev/null || true
        }
        trap cleanup EXIT

        echo "Starting ${pname} SQL Server container..."
        ${exe} run --rm -d \
          --name ScreenSound-DB \
          -p 1433:1433 \
          -e ACCEPT_EULA=Y \
          -e MSSQL_SA_PASSWORD='[Senha123]' \
          mcr.microsoft.com/mssql/server:2022-latest
      '';
    in {
      devShells.default = pkgs.mkShell {
        packages = with pkgs; [
          dotnet-sdk
          csharpier

          openssl
          dotnet-ef
          docker
        ];

        env = {
          LD_LIBRARY_PATH = "${pkgs.openssl.out}/lib:$LD_LIBRARY_PATH";
        };
      };

      packages.api = let
        dotnet-sdk = pkgs.dotnetCorePackages.sdk_9_0;
        dotnet-runtime = pkgs.dotnetCorePackages.aspnetcore_9_0;
      in
        pkgs.buildDotnetModule
        rec {
          pname = "ScreenSoundAPI";
          version = "0.0.0";
          src = ./.;
          projectFile = "src/ScreenSoundAPI/ScreenSoundAPI.csproj";
          inherit dotnet-sdk;
          inherit dotnet-runtime;
          nugetDeps = ./deps.json;

          makeWrapperArgs = [
            "--set"
            "DOTNET_CONTENTROOT"
            "${placeholder "out"}/lib/${pname}"
          ];
        };

      packages.default = self.packages.${system}.dockerSupport;

      packages.dockerSupport = pkgs.writeShellApplication {
        name = "screensound-api";
        runtimeInputs = [docker];
        text = ''
          set -e

          ${container-cmd {
            pkg = docker;
          }}

          sleep 10
          echo "SQL Server is ready!"
          echo "Starting API..."
          ${self.packages.${system}.api}/bin/ScreenSoundAPI
        '';
      };

      packages.podmanSupport = pkgs.writeShellApplication {
        name = "run";
        runtimeInputs = [pkgs.podman];
        text = ''
          set -e

          ${container-cmd {
            pkg = pkgs.podman;
          }}

          sleep 10
          echo "SQL Server is ready!"
          echo "Starting API..."
          ${self.packages.${system}.api}/bin/ScreenSoundAPI
        '';
      };
    });
}
