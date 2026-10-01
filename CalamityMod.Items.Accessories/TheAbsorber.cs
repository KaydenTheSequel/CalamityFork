using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class TheAbsorber : ModItem, ILocalizedModType, IModType
{
	public static float MoveSpeedBoost = 0.12f;

	public static float JumpSpeedBoost = 0.6f;

	public static int AuraLifetime = 1800;

	public static int AuraRegenBoost = 4;

	public static float AuraDamageBoost = 0.08f;

	public static float AuraDamageReductionBoost = 0.05f;

	public static float DamageTakenHealedPercent = 0.05f;

	public new string LocalizationCategory => "Items.Accessories";

	public static int ThornsDamage => 200.ScaleWithDifficulty();

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBoost.ToPercent(), AuraLifetime.FramesToSeconds(), AuraRegenBoost.ToRegenPerSecond(), AuraDamageBoost.ToPercent(), AuraDamageReductionBoost.ToPercent(), DamageTakenHealedPercent.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 24;
		base.Item.defense = 6;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		player.noKnockback = true;
		calamityPlayer.absorber = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<GrandGelatin>().AddIngredient<Baroclaw>().AddIngredient<GiantTortoiseShell>()
			.AddIngredient<MolluskHusk>(5)
			.AddIngredient<MeldConstruct>(6)
			.AddTile(412)
			.Register();
	}
}
