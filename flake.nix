{
  inputs.nixpkgs.url = "github:NixOS/nixpkgs/nixpkgs-unstable";
  inputs.flake-utils.url = "github:numtide/flake-utils";

  outputs = {
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
    in {
      devShells.default = pkgs.mkShell {
        packages = with pkgs; [
          dotnet-sdk
          csharpier

          openssl
          dotnet-ef
          docker_25
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
    });
}
