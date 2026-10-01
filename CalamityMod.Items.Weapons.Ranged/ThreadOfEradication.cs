using System;
using System.Collections.Generic;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "Deathwind" })]
public class ThreadOfEradication : ModItem, ILocalizedModType, IModType
{
	private int storedDMG = 1;

	private float storedKB = 1f;

	private int storedDir = 1;

	private float storedRot = -5f;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		tooltips.FindAndReplaceAll("ff00ff", DevourerofGodsHead.SpecialMoveColor.Hex3());
	}

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 82;
		base.Item.damage = 4375;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 45;
		base.Item.useAnimation = 45;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<FriendlyLaserWallBeam>();
		base.Item.shootSpeed = 20f;
		base.Item.useAmmo = AmmoID.Arrow;
		base.Item.channel = true;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/ThreadOfEradicationGlow", (AssetRequestMode)2).Value);
	}

	public override void UseItemFrame(Player player)
	{
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		if (player.direction == -1)
		{
			player.itemRotation -= (float)Math.PI;
		}
		if (storedRot == -5f)
		{
			storedRot = player.itemRotation;
		}
		bool flip = false;
		if (storedDir != player.direction)
		{
			flip = true;
		}
		if (player.itemTime < 60)
		{
			player.itemRotation = player.DirectionTo(player.Calamity().mouseWorld).ToRotation();
			if (player.itemTime == 1)
			{
				if (player.channel)
				{
					player.itemTime = 180;
					player.itemAnimation = 180;
				}
				else
				{
					if (Main.LocalPlayer.Distance(player.Center) < 1600f)
					{
						Main.LocalPlayer.SetScreenshake(2f);
					}
					if (Main.myPlayer == player.whoAmI)
					{
						Projectile.NewProjectile(base.Item.GetSource_FromThis(), player.Center + Vector2.UnitX.RotatedBy(player.itemRotation) * 2016f, Vector2.UnitX.RotatedBy(player.itemRotation), ModContent.ProjectileType<FriendlyLaserWallBeam>(), storedDMG, storedKB, player.whoAmI, -1f, 1f);
					}
				}
			}
		}
		else
		{
			player.itemRotation += MathHelper.Clamp(MathHelper.WrapAngle(player.DirectionTo(player.Calamity().mouseWorld).ToRotation() - player.itemRotation), -0.045f, 0.045f);
			if (player.itemTime == 60)
			{
				if (Main.LocalPlayer.Distance(player.Center) < 1600f)
				{
					Main.LocalPlayer.SetScreenshake(5f);
				}
				if (Main.myPlayer == player.whoAmI)
				{
					int p = Projectile.NewProjectile(base.Item.GetSource_FromThis(), player.Center + Vector2.UnitX.RotatedBy(player.itemRotation) * 2016f, Vector2.UnitX.RotatedBy(player.itemRotation), ModContent.ProjectileType<FriendlyLaserWallBeam>(), storedDMG * 4, storedKB, player.whoAmI, -0.25f, 1f);
					if (Main.projectile.IndexInRange(p))
					{
						Main.projectile[p].scale = 4f;
					}
				}
				player.itemTime = 0;
				player.itemAnimation = 0;
			}
		}
		if (player.dashDelay != -1)
		{
			player.direction = player.itemRotation.ToRotationVector2().X.DirectionalSign();
		}
		storedDir = player.direction;
		storedRot = player.itemRotation;
		if (player.direction == -1)
		{
			player.itemRotation += (float)Math.PI;
		}
		if (flip)
		{
			player.itemRotation -= (float)Math.PI;
			player.itemRotation *= -1f;
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		storedDMG = damage;
		storedKB = knockback;
		return false;
	}
}
