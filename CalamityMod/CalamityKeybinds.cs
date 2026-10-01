using Terraria.ModLoader;

namespace CalamityMod;

public class CalamityKeybinds : ModSystem
{
	public static ModKeybind AccessoryParryHotKey { get; private set; }

	public static ModKeybind AdrenalineHotKey { get; private set; }

	public static ModKeybind AmmoCycleHotkey { get; private set; }

	public static ModKeybind AngelicAllianceHotKey { get; private set; }

	public static ModKeybind ArmorSetBonusHotKey { get; private set; }

	public static ModKeybind AscendantInsigniaHotKey { get; private set; }

	public static ModKeybind BoosterDashHotKey { get; private set; }

	public static ModKeybind DashHotkey { get; private set; }

	public static ModKeybind ExoChairSlowdownHotkey { get; private set; }

	public static ModKeybind GodSlayerDashHotKey { get; private set; }

	public static ModKeybind GravistarSabatonHotkey { get; private set; }

	public static ModKeybind NormalityRelocatorHotKey { get; private set; }

	public static ModKeybind RageHotKey { get; private set; }

	public static ModKeybind SpectralVeilHotKey { get; private set; }

	public static ModKeybind TransformerHotKey { get; private set; }

	public static ModKeybind SwitchGravityHotkey { get; private set; }

	public static ModKeybind ExpandDebuffInfo { get; private set; }

	public static ModKeybind ThePointerLock { get; private set; }

	public override void Load()
	{
		AccessoryParryHotKey = KeybindLoader.RegisterKeybind(base.Mod, "ActivateAccessoryParry", "N");
		AdrenalineHotKey = KeybindLoader.RegisterKeybind(base.Mod, "AdrenalineMode", "B");
		AmmoCycleHotkey = KeybindLoader.RegisterKeybind(base.Mod, "AmmoCycle", "Mouse3");
		AngelicAllianceHotKey = KeybindLoader.RegisterKeybind(base.Mod, "AngelicAllianceBlessing", "G");
		ArmorSetBonusHotKey = KeybindLoader.RegisterKeybind(base.Mod, "ArmorSetBonus", "Y");
		AscendantInsigniaHotKey = KeybindLoader.RegisterKeybind(base.Mod, "AscendantInsigniaHotKey", "K");
		BoosterDashHotKey = KeybindLoader.RegisterKeybind(base.Mod, "BoosterDash", "Q");
		DashHotkey = KeybindLoader.RegisterKeybind(base.Mod, "DashDoubleTapOverride", "F");
		ExoChairSlowdownHotkey = KeybindLoader.RegisterKeybind(base.Mod, "ExoChairSlowDown", "RightShift");
		GodSlayerDashHotKey = KeybindLoader.RegisterKeybind(base.Mod, "GodSlayerDash", "H");
		GravistarSabatonHotkey = KeybindLoader.RegisterKeybind(base.Mod, "GravistarSabatonHotkey", "X");
		NormalityRelocatorHotKey = KeybindLoader.RegisterKeybind(base.Mod, "NormalityRelocator", "Z");
		RageHotKey = KeybindLoader.RegisterKeybind(base.Mod, "RageMode", "V");
		SpectralVeilHotKey = KeybindLoader.RegisterKeybind(base.Mod, "SpectralVeilTeleport", "Z");
		TransformerHotKey = KeybindLoader.RegisterKeybind(base.Mod, "TransformerHotKey", "K");
		SwitchGravityHotkey = KeybindLoader.RegisterKeybind(base.Mod, "GravitySwapOverride", "T");
		ExpandDebuffInfo = KeybindLoader.RegisterKeybind(base.Mod, "ExpandDebuffInfo", "LeftControl");
		ThePointerLock = KeybindLoader.RegisterKeybind(base.Mod, "ThePointerLock", "N");
	}

	public override void Unload()
	{
		AccessoryParryHotKey = null;
		AdrenalineHotKey = null;
		AmmoCycleHotkey = null;
		AngelicAllianceHotKey = null;
		ArmorSetBonusHotKey = null;
		AscendantInsigniaHotKey = null;
		BoosterDashHotKey = null;
		DashHotkey = null;
		ExoChairSlowdownHotkey = null;
		GodSlayerDashHotKey = null;
		GravistarSabatonHotkey = null;
		NormalityRelocatorHotKey = null;
		RageHotKey = null;
		SpectralVeilHotKey = null;
		TransformerHotKey = null;
		ExpandDebuffInfo = null;
	}
}
