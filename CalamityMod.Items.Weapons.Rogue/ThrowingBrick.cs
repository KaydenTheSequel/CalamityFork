using CalamityMod.Projectiles.Rogue;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class ThrowingBrick : RogueWeapon
{
	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 20;
		base.Item.damage = 14;
		base.Item.shootSpeed = 15f;
		base.Item.shoot = ModContent.ProjectileType<Brick>();
		base.Item.useAnimation = (base.Item.useTime = 25);
		base.Item.useStyle = 1;
		base.Item.knockBack = 5f;
		base.Item.value = Item.sellPrice(0, 0, 0, 4);
		base.Item.rare = 0;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.UseSound = SoundID.Item1;
		base.Item.consumable = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 20f;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		ExtraArmAnimations.ThrowArmAnimationFast(player, base.Item);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 1f);
			if (p.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[p].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient(131).AddTile(18).Register();
	}
}
