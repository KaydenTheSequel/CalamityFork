using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class VividClarity : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/VividClarityShoot")
	{
		Volume = 0.3f
	};

	public static readonly SoundStyle BeamSound = new SoundStyle("CalamityMod/Sounds/Item/VividClarityBeamAppear");

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 140;
		base.Item.height = 140;
		base.Item.damage = 133;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 42;
		base.Item.useTime = 4;
		base.Item.useAnimation = 20;
		base.Item.reuseDelay = base.Item.useAnimation;
		base.Item.useLimitPerAnimation = 5;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 7.5f;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.UseSound = UseSound;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<VividBeam>();
		base.Item.shootSpeed = 6f;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo projSource, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		Vector2 playerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float speed = base.Item.shootSpeed;
		float xPos = (float)Main.mouseX + Main.screenPosition.X - playerPos.X;
		float yPos = (float)Main.mouseY + Main.screenPosition.Y - playerPos.Y;
		float f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
		float sourceVariationLow = 20f;
		float sourceVariationHigh = 60f;
		Vector2 source = playerPos + f.ToRotationVector2() * MathHelper.Lerp(sourceVariationLow, sourceVariationHigh, Main.rand.NextFloat());
		for (int i = 0; i < 50; i++)
		{
			source = playerPos + f.ToRotationVector2() * MathHelper.Lerp(sourceVariationLow, sourceVariationHigh, Main.rand.NextFloat());
			if (Collision.CanHit(playerPos, 0, 0, source + (source - playerPos).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
			{
				break;
			}
			f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
		}
		Vector2 velocityReal = Main.MouseWorld - source;
		Vector2 velocityVariation = Utils.SafeNormalize(new Vector2(xPos, yPos), Vector2.UnitY) * speed;
		velocityReal = velocityReal.SafeNormalize(velocityVariation) * speed;
		velocityReal = Vector2.Lerp(velocityReal, velocityVariation, 0.25f);
		Projectile.NewProjectile(projSource, source, velocityReal, type, damage, knockback, player.whoAmI, 0f, Main.rand.Next(3));
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/VividClarityGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Nucleosynthesis>().AddIngredient<PhantasmalFury>().AddIngredient<ShadowboltStaff>()
			.AddIngredient<UltraLiquidator>()
			.AddIngredient<MiracleMatter>()
			.AddTile<DraedonsForge>()
			.Register();
	}
}
