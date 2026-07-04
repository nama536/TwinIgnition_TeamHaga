{
  description = "Environment for Slang shader development";
  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";
    neovim.url = "git+file:///home/yellow14/Documents/GitHub/NixConfiguration?dir=modules/neovim";
  };
  outputs =
    {
      self,
      nixpkgs,
      neovim,
    }:
    let
      system = "x86_64-linux";
      pkgs = import nixpkgs {
        inherit system;
        config.allowUnfree = true;
      };

      dotnetSdk = pkgs.dotnet-sdk_10;

      wrappedCsharpLs = pkgs.symlinkJoin {
        name = "csharp_ls-wrapped";
        paths = [ pkgs.csharp-ls ];
        nativeBuildInputs = [ pkgs.makeWrapper ];
        postBuild = ''
          					wrapProgram $out/bin/csharp-ls \
          					--set DOTNET_ROOT "${dotnetSdk}/share/dotnet" \
          					--prefix PATH : "${dotnetSdk}/bin"
          					'';
      };
    in
    {
      devShells.${system}.default = pkgs.mkShell {
        nativeBuildInputs = with pkgs; [
          vscode
          unityhub
          dotnetSdk
          spirv-cross
          shader-slang
          clang-tools
          wrappedCsharpLs
          neovim.packages.${system}.default # Drops your exact wrapped Neovim straight in!
        ];

        shellHook = ''
          echo "⚡ [Nix Flake] Slang development environment activated!"
          echo "Available tools: $(slangc -version | head -n 1)"
        '';
      };
    };
}
