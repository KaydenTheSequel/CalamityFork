using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class BloomStone : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 54;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer calamityPlayer = player.Calamity();
		Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.25f, 0.4f, 0.2f);
		calamityPlayer.healingPotionMultiplier += 0.5f;
		calamityPlayer.bloomStone = true;
		calamityPlayer.bloomStoneHookVisuals = true;
	}

	public override void UpdateVanity(Player player)
	{
		player.Calamity().bloomStoneHookVisuals = true;
	}
}
