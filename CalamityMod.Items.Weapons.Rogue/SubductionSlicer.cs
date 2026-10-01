using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "FlameScythe" })]
public class SubductionSlicer : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 48;
		base.Item.damage = 95;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.useStyle = 1;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.knockBack = 8.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.shoot = ModContent.ProjectileType<SubductionSlicerProj>();
		base.Item.shootSpeed = 16f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (proj.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[proj].Calamity().stealthStrike = player.Calamity().StealthStrikeAvailable();
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ScoriaBar>(9).AddTile(134).Register();
	}
}
