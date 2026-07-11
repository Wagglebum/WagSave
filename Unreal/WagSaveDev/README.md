# WagSaveDev — plugin dev/test host

A throwaway Unreal host project (UE **5.8**) used only to **develop and test the WagSave plugin**. The
plugin itself lives in its own repo and is **symlinked** into this project's `Plugins/` folder, so edits
in the plugin repo appear here with no copying.

Only the project skeleton is committed (`Config/`, `Source/`, `.uproject`). Generated output
(`Binaries/`, `Intermediate/`, `Saved/`, `Content/`) and the plugin symlink are gitignored — see
`.gitignore`.

## One-time setup: link the plugin

The plugin repo (`wagsave-unreal`) is a plugin at its own root, so symlink that repo directly as
`Plugins/WagSave`:

```bash
ln -sfn /Users/allensnow/Source/wagsave-unreal \
        "/Users/allensnow/Source/wagglebum/WagSave/Unreal/WagSaveDev/Plugins/WagSave"
```

Verify it resolves:

```bash
ls -la "/Users/allensnow/Source/wagglebum/WagSave/Unreal/WagSaveDev/Plugins/WagSave/WagSave.uplugin"
```

That's it — open `WagSaveDev.uproject` (rebuild when prompted) and the WagSave plugin is active.

## Run the test suite

From the **plugin repo**, the runner builds this host + runs the Automation Specs headless:

```bash
ENGINE_DIR="/Users/Shared/Epic Games/UE_5.8" \
UPROJECT="/Users/allensnow/Source/wagglebum/WagSave/Unreal/WagSaveDev/WagSaveDev.uproject" \
TEST_FILTER="WagSave" \
bash /Users/allensnow/Source/wagsave-unreal/Scripts/run-tests.sh
```

It prints a `Results: N passed, M failed` tally (currently 57 passing).

## Optional: the online-cloud companion plugin

The optional `WagSaveOnlineCloud` companion lives at `wagsave-unreal/Extras/WagSaveOnlineCloud`. It is
**not** linked in by default. If you want it here, **copy** it into `Plugins/` — do **not** symlink it
from `Extras/`:

```bash
cp -R /Users/allensnow/Source/wagsave-unreal/Extras/WagSaveOnlineCloud \
      "/Users/allensnow/Source/wagglebum/WagSave/Unreal/WagSaveDev/Plugins/WagSaveOnlineCloud"
```

Why copy, not symlink: the built module's load path to the core WagSave module is baked relative to a
normal sibling-`Plugins/` layout. A plugin symlinked from `Extras/` resolves `@loader_path` to the wrong
place and fails to load at runtime ("module could not be loaded"). It also needs the **OnlineSubsystem**
plugin enabled (its `.uplugin` declares that dependency).

## Notes

- **RiderLink is disabled.** It won't build on the current Xcode-27-beta / Clang-21 toolchain (its `RD`
  module fails to compile), so it's disabled for this project (`.uproject` Plugins) and at the engine
  level (`EnabledByDefault: false` in the engine's `RiderLink.uplugin`, backed up to `.bak`). Re-enable
  when off the beta toolchain or once Rider ships compatible libraries.
- After adding/removing plugin **source files**, a stale UBT cache can skip them — clear
  `Intermediate/Build/Mac/arm64/WagSaveDevEditor/Development/Makefile.bin` (and the module's `Intermediate`
  folder) and rebuild. The test runner already clears the plugin's `UnrealEditor.modules` manifest.
