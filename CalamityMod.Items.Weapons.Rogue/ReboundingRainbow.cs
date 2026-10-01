using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "AccretionDisk", "ElementalDisk" })]
public class ReboundingRainbow : RogueWeapon
{
	public static int stealthTimeMult = 2;

	public override float StealthVelocityMultiplier => 0.7f;

	public override float StealthDamageMultiplier => 0.75f;

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 38;
		base.Item.damage = 92;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.useAnimation = 16;
		base.Item.useStyle = 1;
		base.Item.useTime = 16;
		base.Item.knockBack = 9f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.shoot = ModContent.ProjectileType<ReboundingRainbowProj>();
		base.Item.shootSpeed = 15f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].Calamity().stealthStrike = true;
				Main.projectile[proj].timeLeft *= stealthTimeMult;
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SamsaraSlicer>().AddIngredient(3467, 5).AddIngredient<LifeAlloy>(5)
			.AddIngredient<MeldConstruct>(5)
			.AddTile(134)
			.Register();
	}
}
