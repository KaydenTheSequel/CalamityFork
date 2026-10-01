using CalamityMod.Cooldowns;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Melee;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Lucrecia : BaseSwordHoldoutItem, ILocalizedModType, IModType
{
	public static int MaxEnergy = 100;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override int ProjectileType => ModContent.ProjectileType<LucreciaHoldout>();

	public override void SetDefaults()
	{
		base.Item.width = 54;
		base.Item.height = 54;
		base.Item.useStyle = 5;
		base.Item.damage = 150;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 34);
		base.Item.shootSpeed = 10f;
		base.Item.knockBack = 8.25f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.SetDefaults();
	}

	public override bool AltFunctionUse(Player player)
	{
		return player.Calamity().darklightEnergy >= MaxEnergy;
	}

	public override void HoldItem(Player player)
	{
		if (player.Calamity().cooldowns.TryGetValue(DarklightEnergy.ID, out var cooldown))
		{
			cooldown.timeLeft = player.Calamity().darklightEnergy;
		}
		else
		{
			player.AddCooldown(DarklightEnergy.ID, 0);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<LifeAlloy>(5).AddIngredient(75, 10).AddIngredient(520, 5)
			.AddIngredient(521, 5)
			.AddTile(134)
			.Register();
	}
}
