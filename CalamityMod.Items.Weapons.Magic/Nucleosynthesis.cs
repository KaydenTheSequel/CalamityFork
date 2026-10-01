using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "ElementalRay" })]
public class Nucleosynthesis : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 116;
		base.Item.height = 116;
		base.Item.damage = 70;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 18;
		base.Item.useTime = 4;
		base.Item.useAnimation = 16;
		base.Item.reuseDelay = 14;
		base.Item.useLimitPerAnimation = 4;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 7.5f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.UseSound = SoundID.Item60;
		base.Item.autoReuse = true;
		base.Item.shoot = 1;
		base.Item.shootSpeed = 6f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		float offsetAngle = (float)Math.PI * 2f * (float)player.itemAnimation / (float)player.itemAnimationMax;
		offsetAngle += (float)Math.PI / 4f + Main.rand.NextFloat(0f, 1.3f);
		float shootSpeed = 1f;
		if (player.itemAnimation == base.Item.useAnimation)
		{
			type = ModContent.ProjectileType<SolarElementalBeam>();
		}
		else if (player.itemAnimation == base.Item.useAnimation - base.Item.useTime)
		{
			type = ModContent.ProjectileType<NebulaElementalBeam>();
			offsetAngle -= 0.003926991f;
		}
		else if (player.itemAnimation == base.Item.useAnimation - base.Item.useTime * 2)
		{
			type = ModContent.ProjectileType<VortexElementalBeam>();
			shootSpeed = 2f;
		}
		else
		{
			if (player.itemAnimation != base.Item.useAnimation - base.Item.useTime * 3)
			{
				return false;
			}
			type = ModContent.ProjectileType<StardustElementalBeam>();
		}
		Vector2 spawnOffset = player.SafeDirectionTo(Main.MouseWorld, Vector2.UnitY).RotatedBy(offsetAngle) * (0f - Main.rand.NextFloat(40f, 96f));
		Vector2 shootDirection = (Main.MouseWorld - (position + spawnOffset)).SafeNormalize(Vector2.UnitX * (float)player.direction);
		int beam = Projectile.NewProjectile(source, position + spawnOffset, shootDirection * shootSpeed, type, damage, knockback, player.whoAmI);
		if (type == ModContent.ProjectileType<VortexElementalBeam>())
		{
			Main.projectile[beam].ai[0] = shootDirection.ToRotation();
			Main.projectile[beam].ai[1] = Main.rand.Next(100);
		}
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Photosynthesis>().AddIngredient(3467, 5).AddIngredient<LifeAlloy>(5)
			.AddIngredient(3457, 5)
			.AddTile(134)
			.Register();
	}
}
