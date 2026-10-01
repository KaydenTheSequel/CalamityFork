using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Shoes })]
public class AngelTreads : ModItem, ILocalizedModType, IModType
{
	public static float RunningSpeed = 7.5f;

	public static float MoveSpeedBoost = 0.12f;

	public static float FlightTimeBoost = 0.1f;

	public static int LavaImmunityTime = 420;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBoost.ToPercent(), FlightTimeBoost.ToPercent(), LavaImmunityTime.FramesToSeconds());

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 36;
		base.Item.value = CalamityGlobalItem.RarityLightPurpleBuyPrice;
		base.Item.rare = 6;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().angelTreads = true;
		player.accRunSpeed = RunningSpeed;
		player.rocketBoots = (player.vanityRocketBoots = 3);
		player.moveSpeed += MoveSpeedBoost;
		player.iceSkate = true;
		player.waterWalk = true;
		player.fireWalk = true;
		player.lavaMax += LavaImmunityTime;
		player.lavaRose = true;
	}

	public override void UpdateVanity(Player player)
	{
		player.vanityRocketBoots = 3;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(5000).AddIngredient<HarpyRing>().AddIngredient<EssenceofSunlight>(5)
			.AddIngredient(547)
			.AddIngredient(548)
			.AddIngredient(549)
			.AddTile(134)
			.Register();
	}
}
