using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class CrystalPiercer : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 62;
		base.Item.height = 62;
		base.Item.damage = 92;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 22);
		base.Item.useStyle = 1;
		base.Item.knockBack = 6f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<CrystalPiercerProjectile>();
		base.Item.shootSpeed = 20f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int stealth = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
				Main.projectile[stealth].aiStyle = -1;
				Main.projectile[stealth].tileCollide = false;
				Main.projectile[stealth].usesIDStaticNPCImmunity = false;
				Main.projectile[stealth].usesLocalNPCImmunity = true;
				Main.projectile[stealth].localNPCHitCooldown = 10;
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryonicBar>(8).AddTile(134).Register();
	}
}
