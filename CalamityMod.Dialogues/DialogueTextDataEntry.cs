using CalamityMod.UI.DialogueDisplay;
using Terraria.ModLoader;

namespace CalamityMod.Dialogues;

internal record DialogueTextDataEntry(Mod ProviderMod, string FilePath, string DialogueKey, DialogueTextData Data);
