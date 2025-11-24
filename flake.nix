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

      packages.default = let
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

      apps.default = self.apps.${system}.dockerSupport;

      apps.dockerSupport = {
        type = "app";
        program = "${pkgs.writeShellScriptBin "run" ''
          set -e

          cleanup() {
            echo "Stopping database container..."
            ${docker}/bin/docker stop ScreenSound-DB 2>/dev/null || true
          }
          trap cleanup EXIT

          echo "Starting SQL Server container..."
          ${docker}/bin/docker run --rm -d \
            --name ScreenSound-DB \
            -p 1433:1433 \
            -e ACCEPT_EULA=Y \
            -e MSSQL_SA_PASSWORD='[Senha123]' \
            mcr.microsoft.com/mssql/server:2022-latest

          sleep 10
          echo "SQL Server is ready!"
          echo "Starting API..."
          ${self.packages.${system}.default}/bin/ScreenSoundAPI
        ''}/bin/run";
      };
    });
}
