using CalamityMod.Events;
using CalamityMod.Rarities;
using CalamityMod.World;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class SulphurousSeaWorldSideChanger : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 0;
	}

	public override void SetDefaults()
	{
		base.Item.width = 54;
		base.Item.height = 46;
		base.Item.useTime = 16;
		base.Item.useAnimation = 16;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.value = 0;
		base.Item.autoReuse = false;
		base.Item.useStyle = 4;
		base.Item.UseSound = SoundID.Item111;
	}

	public override bool? UseItem(Player player)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		CalamityNetcode.SyncWorld();
		string key = "Mods.CalamityMod.Misc.SulphurSwitchLeft";
		if (Abyss.AtLeftSideOfWorld)
		{
			Abyss.AtLeftSideOfWorld = false;
			key = "Mods.CalamityMod.Misc.SulphurSwitchRight";
		}
		else
		{
			Abyss.AtLeftSideOfWorld = true;
		}
		CalamityUtils.BroadcastLocalizedText(key, AcidRainEvent.TextColor);
		return true;
	}
}
