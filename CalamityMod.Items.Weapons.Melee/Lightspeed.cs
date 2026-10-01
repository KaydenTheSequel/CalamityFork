using CalamityMod.Cooldowns;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "ElementalShortsword", "ElementalShiv" })]
public class Lightspeed : ModItem, ILocalizedModType, IModType
{
	public static int MaxEnergy = 100;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 74;
		base.Item.height = 94;
		base.Item.useStyle = 5;
		base.Item.damage = 220;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.shootSpeed = 10f;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.shoot = ModContent.ProjectileType<LightspeedHoldout>();
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.SetDefaults();
	}

	public override bool AltFunctionUse(Player player)
	{
		return player.Calamity().elementalMastery >= 100;
	}

	public override void HoldItem(Player player)
	{
		if (player.Calamity().cooldowns.TryGetValue(ElementalMastery.ID, out var cooldown))
		{
			cooldown.timeLeft = player.Calamity().elementalMastery;
		}
		else
		{
			player.AddCooldown(ElementalMastery.ID, 0);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(4923).AddIngredient<Lucrecia>().AddIngredient(3467, 5)
			.AddIngredient<LifeAlloy>(5)
			.AddIngredient(3458, 5)
			.AddTile(134)
			.Register();
	}
}
