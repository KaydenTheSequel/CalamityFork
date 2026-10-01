using Terraria.ModLoader;

namespace CalamityMod;

public sealed class ExternalMods : ModSystem
{
	internal static Mod musicMod;

	internal static Mod vcmm;

	internal static Mod ancientsAwakened;

	internal static Mod biomeLava;

	internal static Mod bossChecklist;

	internal static Mod coloredDamageTypes;

	internal static Mod crouchMod;

	internal static Mod dialogueTweak;

	internal static Mod fargos;

	internal static Mod luminance;

	internal static Mod magicStorage;

	internal static Mod overhaul;

	internal static Mod redemption;

	internal static Mod remnants;

	internal static Mod soa;

	internal static Mod subworldLibrary;

	internal static Mod summonersAssociation;

	internal static Mod thorium;

	internal static Mod varia;

	internal static Mod wikithis;

	internal static bool MusicAvailable => musicMod != null;

	internal static bool VCMMAvailable => vcmm != null;

	public override void Load()
	{
		musicMod = null;
		ModLoader.TryGetMod("CalamityModMusic", out musicMod);
		vcmm = null;
		ModLoader.TryGetMod("UnCalamityModMusic", out vcmm);
		ancientsAwakened = null;
		ModLoader.TryGetMod("AAMod", out ancientsAwakened);
		biomeLava = null;
		ModLoader.TryGetMod("BiomeLava", out biomeLava);
		bossChecklist = null;
		ModLoader.TryGetMod("BossChecklist", out bossChecklist);
		coloredDamageTypes = null;
		ModLoader.TryGetMod("ColoredDamageTypes", out coloredDamageTypes);
		crouchMod = null;
		ModLoader.TryGetMod("CrouchMod", out crouchMod);
		dialogueTweak = null;
		ModLoader.TryGetMod("DialogueTweak", out dialogueTweak);
		fargos = null;
		ModLoader.TryGetMod("Fargowiltas", out fargos);
		luminance = null;
		ModLoader.TryGetMod("Luminance", out luminance);
		magicStorage = null;
		ModLoader.TryGetMod("MagicStorage", out magicStorage);
		overhaul = null;
		ModLoader.TryGetMod("TerrariaOverhaul", out overhaul);
		redemption = null;
		ModLoader.TryGetMod("Redemption", out redemption);
		remnants = null;
		ModLoader.TryGetMod("Remnants", out remnants);
		soa = null;
		ModLoader.TryGetMod("SacredTools", out soa);
		subworldLibrary = null;
		ModLoader.TryGetMod("SubworldLibrary", out subworldLibrary);
		summonersAssociation = null;
		ModLoader.TryGetMod("SummonersAssociation", out summonersAssociation);
		thorium = null;
		ModLoader.TryGetMod("ThoriumMod", out thorium);
		varia = null;
		ModLoader.TryGetMod("Varia", out varia);
		wikithis = null;
		ModLoader.TryGetMod("Wikithis", out wikithis);
	}

	public override void Unload()
	{
		musicMod = null;
		vcmm = null;
		ancientsAwakened = null;
		biomeLava = null;
		bossChecklist = null;
		coloredDamageTypes = null;
		crouchMod = null;
		dialogueTweak = null;
		fargos = null;
		luminance = null;
		magicStorage = null;
		overhaul = null;
		redemption = null;
		remnants = null;
		soa = null;
		subworldLibrary = null;
		summonersAssociation = null;
		thorium = null;
		varia = null;
		wikithis = null;
	}
}
