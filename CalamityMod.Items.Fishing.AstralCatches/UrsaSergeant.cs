using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.AstralCatches;

public class UrsaSergeant : ModItem, ILocalizedModType, IModType
{
	public static int CooldownReducedPerKill = 180;

	public static int MaxCooldown = 300;

	public static int BaseSwipeDamage = 325;

	public new string LocalizationCategory => "Items.Fishing";

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.Calamity();
		modPlayer.ursaSergeant = true;
		if (!hideVisual)
		{
			modPlayer.ursaSergeantVisual = true;
		}
		else
		{
			modPlayer.ursaSergeantVisual = false;
		}
	}
}
