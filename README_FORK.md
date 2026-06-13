# p5rpc.rpc (dynamic) — fork & build guide

This is a fork-ready copy of **AnimatedSwine37/p5rpc.rpc**: a Reloaded-II mod that gives
**Persona 5 Royal** dynamic Discord Rich Presence (location/event-aware), the same idea as
SVRichPresence does for Stardew Valley.

It already builds via GitHub Actions out of the box. Below is the exact path from
"unzip" to "downloadable mod".

## 1. What changed vs the original (so you know what you're shipping)
- **Discord App ID is now configurable** (`Config.cs` → "Discord Application ID").
  Default keeps the original mod's app + images so it works immediately. Set your own
  only if you want custom images (see `LOCATIONS_GUIDE.md`).
- **Sequence-aware fallback** (`Mod.cs` → `GetSequenceFallback`): undocumented areas no
  longer just say "Roaming somewhere". They now show "In a battle", "Watching a cutscene",
  "Time passes by...", "In a conversation", etc., based on the live game sequence.
  This is verified against `p5rpc.lib.interfaces` `SequenceType`.
- **CI triggers on `main` AND `master`** (`/.github/workflows/reloaded.yml`).
  The original repo's default branch is `master`; without this a fresh fork won't auto-build on push.
- `ModConfig.json` `ProjectUrl` set to a placeholder for your fork.

Field/event location data was **not fabricated** — see `LOCATIONS_GUIDE.md` for why and how to grow it.

## 2. Build it (the part you asked about)
1. Create a GitHub repo (or fork https://github.com/AnimatedSwine37/p5rpc.rpc) and push
   the contents of this zip to it.
2. Edit `ModConfig.json` → `"ProjectUrl"` to your repo URL (optional but recommended).
3. Push to `main` or `master`. Go to the **Actions** tab → the
   "Build and Publish Reloaded Mod" workflow runs automatically
   (or click **Run workflow** for a manual run).
4. When it finishes, open the run → **Artifacts** → download **GitHub Release**.
   That zip is the installable Reloaded-II mod.
   (A proper GitHub *Release* is only published when you push a **git tag**, e.g. `v1.0.0`.)

That's it — no `.sln`/template wiring needed; this is a complete, real Reloaded-II project.

## 3. Install & run (to test it)
1. Install **Reloaded-II**, add Persona 5 Royal (`p5r.exe`).
2. Install dependencies in Reloaded: **p5rpc.lib** and **Reloaded SharedLib Hooks**
   (Reloaded will offer to download these automatically — they're listed in `ModConfig.json`).
3. Drop the built mod into your Reloaded mods folder, enable it, launch the game with the
   Discord desktop app running. Presence appears on your profile.

## 4. Requirements to build locally (optional)
- .NET 7 SDK (Windows). The mod targets `net7.0-windows`.
- Just run `./Publish.ps1` in PowerShell; CI does the same.

See `LOCATIONS_GUIDE.md` for making presence richer (more locations + custom images).
