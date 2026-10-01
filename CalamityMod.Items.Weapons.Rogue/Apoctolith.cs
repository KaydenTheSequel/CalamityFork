using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Apoctolith : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 64;
		base.Item.damage = 185;
		base.Item.shootSpeed = 18f;
		base.Item.shoot = ModContent.ProjectileType<ApoctolithProj>();
		base.Item.useAnimation = (base.Item.useTime = 26);
		base.Item.useStyle = 1;
		base.Item.knockBack = 10f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.Item1;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.autoReuse = true;
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
			int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (p.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[p].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/ApoctolithGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ThrowingBrick>(100).AddIngredient<Voidstone>(50).AddIngredient<Lumenyl>(20)
			.AddTile(134)
			.Register();
	}
}
