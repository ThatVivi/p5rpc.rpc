# Making presence richer: locations, events & images

The mod maps the game's internal `Major_Minor` IDs to nice text/images via three files in
`p5rpc.rpc/`:

- `fields.json`  — overworld/dungeon locations (`FLD_GET_MAJOR` / `FLD_GET_MINOR`)
- `events.json`  — story/conversation events (Sequencer `EventInfo`)
- `imageText.json` — hover-text shown for each image key

## The honest part: the location map is NOT fully known
P5R has hundreds of `Major_Minor` field IDs and thousands of event IDs. **Nobody has
published a complete map.** The upstream repo ships only ~14 fields + 2 events, and the
only existing fork is identical (no community data exists yet, verified June 2026).

I deliberately **did not invent IDs** — a wrong ID would mislabel your presence (show the
wrong room), which is worse than the generic fallback. Instead, the new sequence-aware
fallback guarantees every screen still shows something sensible (battle / cutscene /
calendar / conversation / roaming) even before a single new ID is added.

## How to discover real IDs (the only correct way)
1. In Reloaded-II, open this mod's config → enable **Debug Mode**.
2. Open the Reloaded **console/log**. As you play, it prints lines like:
   - `In field 5_2 (undocumented)`   ← a field you can document
   - `In event 712_0 (undocumented)` ← an event you can document
   - `Current sequence: Current BATTLE, Last FIELD`
3. Note the `Major_Minor` for each place you care about (Leblanc attic, each Palace floor,
   Mementos, confidant spots, Velvet Room, etc.), then add entries.

### `fields.json` entry format
```json
{
  "Major": 5,
  "Minor": 2,
  "Name": "Leblanc Attic",
  "Description": "Relaxing in the attic",
  "State": "Planning the next heist",   // optional extra line
  "ImageKey": "yongen-jaya",            // optional; MUST be an uploaded asset (see below)
  "InMetaverse": false                  // true adds party + level cycling to the state line
}
```
Lookup is exact `Major`+`Minor`. Fields not listed fall back to the sequence text.

### `events.json` entry format
```json
{ "Major": 712, "Minor": 0, "Name": "Airsoft shop", "Description": "Shopping with Ryuji", "State": "Untouchable", "ImageKey": null }
```
If an event has no `ImageKey`, it reuses the previous field's image.

## Images: the real blocker for custom art
Discord Rich Presence images are **not** the PNGs in the `images/` folder. Those are just
source art. Discord only shows images that are uploaded as **Art Assets** to a Discord
**Application**, and referenced by their asset **key name**.

The default app ID (`1032265834111975424`) is the original author's. It already hosts:
`logo`, `shibuya`, `aoyama-itchome`, `yongen-jaya`. You can reuse those keys freely.

**To add NEW images you must use your own Discord app:**
1. https://discord.com/developers/applications → New Application (this name shows as
   "Playing <name>").
2. **Rich Presence → Art Assets** → upload PNGs. The **asset name** you give each upload
   is the string you put in `ImageKey` (e.g. upload as `kichijoji` → `"ImageKey": "kichijoji"`).
3. Copy the **Application ID** and paste it into the mod config → **Discord Application ID**
   (or change the default in `Config.cs`).
4. Add a matching `imageText.json` entry: `"kichijoji": "Kichijoji"`.

Until an image key exists on the active app, that location shows a broken/blank image — so
new field entries should either reuse the 4 existing keys, omit `ImageKey`, or wait until
you've set up your own app.

## Contributing back
This is exactly how the upstream data set is meant to grow — collect IDs with Debug Mode,
add entries, open a PR. Paste me your debug log dump and I'll turn it into clean
`fields.json` / `events.json` entries.
