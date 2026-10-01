using System;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using CalamityMod.Projectiles;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class PearlGod : ModItem, ILocalizedModType, IModType
{
	public bool swapType;

	public bool healVisual;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 80;
		base.Item.height = 46;
		base.Item.damage = 160;
		base.Item.scale = 0.75f;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 9;
		base.Item.useAnimation = 9;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/GunShotSmall")
		{
			Volume = 0.65f
		};
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 14f;
		base.Item.shoot = 10;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		float itemRotation = player.compositeFrontArm.rotation + (float)Math.PI / 2f * player.gravDir;
		Vector2 itemPosition = player.MountedCenter + itemRotation.ToRotationVector2() * 7f;
		if (Main.zenithWorld)
		{
			Projectile.NewProjectile(source, itemPosition + velocity.RotatedBy(-0.6 * (double)player.direction) + velocity * 1.35f, velocity, ModContent.ProjectileType<ShockblastRound>(), damage * 3, knockback * 5f, player.whoAmI, 0f, 10f);
			return false;
		}
		if (!swapType)
		{
			for (int k = 0; k < 2; k++)
			{
				Color color = (Color)(Main.rand.Next(1, 4) switch
				{
					2 => Color.LightPink, 
					1 => Color.LightBlue, 
					_ => Color.Khaki, 
				});
				GeneralParticleHandler.SpawnParticle(new SparkParticle(itemPosition + velocity.RotatedBy(-0.6 * (double)player.direction) + velocity * 1.35f, velocity.RotatedByRandom(0.25) * Main.rand.NextFloat(0.2f, 1.5f), affectedByGravity: false, Main.rand.Next(20, 26), Main.rand.NextFloat(0.4f, 0.65f), color));
			}
			for (int i = 0; i < 6; i++)
			{
				Color color2 = (Color)(Main.rand.Next(1, 4) switch
				{
					2 => Color.LightPink, 
					1 => Color.LightBlue, 
					_ => Color.Khaki, 
				});
				GeneralParticleHandler.SpawnParticle(new PearlParticle(itemPosition + velocity.RotatedBy(-0.6 * (double)player.direction) + velocity * 1.35f, velocity.RotatedByRandom(0.25) * Main.rand.NextFloat(0.2f, 1f), affectedByGravity: false, Main.rand.Next(40, 46), Main.rand.NextFloat(0.6f, 0.75f), color2, 0.95f, Main.rand.NextFloat(1f, -1f), hitTiles: true));
			}
			int shotColor = Main.rand.Next(1, 4);
			for (int j = 0; j < 3; j++)
			{
				CalamityGlobalProjectile cgp = Projectile.NewProjectileDirect(source, itemPosition + velocity.RotatedBy(-0.6 * (double)player.direction) + velocity * 1.65f, velocity.RotatedBy(j switch
				{
					1 => 0.025f, 
					0 => 0f, 
					_ => -0.025f, 
				}), type, damage, knockback, player.whoAmI).Calamity();
				if (shotColor == 1)
				{
					cgp.pearlBullet1 = true;
				}
				if (shotColor == 2)
				{
					cgp.pearlBullet2 = true;
				}
				if (shotColor == 3)
				{
					cgp.pearlBullet3 = true;
				}
				shotColor = ((shotColor >= 3) ? 1 : (shotColor + 1));
			}
		}
		if (swapType)
		{
			for (int l = 0; l < 8; l++)
			{
				Color color3 = (Color)(Main.rand.Next(1, 4) switch
				{
					2 => Color.LightPink, 
					1 => Color.LightBlue, 
					_ => Color.Khaki, 
				});
				GeneralParticleHandler.SpawnParticle(new SparkParticle(itemPosition + velocity.RotatedBy(-0.6 * (double)player.direction) + velocity * 1.35f, velocity.RotatedByRandom(0.25) * Main.rand.NextFloat(0.2f, 1.5f), affectedByGravity: false, Main.rand.Next(20, 26), Main.rand.NextFloat(0.4f, 0.65f), color3));
			}
			for (int m = 0; m < 13; m++)
			{
				Color color4 = (Color)(Main.rand.Next(1, 4) switch
				{
					2 => Color.LightPink, 
					1 => Color.LightBlue, 
					_ => Color.Khaki, 
				});
				Dust dust = Dust.NewDustPerfect(itemPosition + velocity.RotatedBy(-0.6 * (double)player.direction) + velocity * 1.35f, 278, velocity.RotatedByRandom(0.25) * Main.rand.NextFloat(0.1f, 0.9f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.3f, 0.45f);
				dust.color = color4;
			}
			Projectile lifeShot = Projectile.NewProjectileDirect(source, itemPosition + velocity.RotatedBy(-0.6 * (double)player.direction) - velocity * 0.65f, velocity, type, damage, knockback, player.whoAmI);
			if (!healVisual)
			{
				lifeShot.Calamity().betterLifeBullet1 = true;
			}
			if (healVisual)
			{
				lifeShot.Calamity().betterLifeBullet2 = true;
			}
			healVisual = !healVisual;
		}
		swapType = !swapType;
		return false;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float itemRotation = player.compositeFrontArm.rotation + (float)Math.PI / 2f * player.gravDir;
		Vector2 itemPosition = player.MountedCenter + itemRotation.ToRotationVector2() * 7f;
		Vector2 itemSize = default(Vector2);
		((Vector2)(ref itemSize))._002Ector(80f, 46f);
		Vector2 itemOrigin = default(Vector2);
		((Vector2)(ref itemOrigin))._002Ector(-24f, 4f);
		CalamityUtils.CleanHoldStyle(player, itemRotation, itemPosition, itemSize, itemOrigin);
		base.UseStyle(player, heldItemFrame);
	}

	public override void UseItemFrame(Player player)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float animProgress = 0.5f - (float)player.itemTime / (float)player.itemTimeMax;
		float rotation = (player.Center - player.Calamity().mouseWorld).ToRotation() * player.gravDir + (float)Math.PI / 2f;
		if (animProgress < 0.4f)
		{
			rotation += -0.03f * (float)Math.Pow((0.6f - animProgress) / 0.6f, 2.0) * (float)player.direction;
		}
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rotation);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Arietes41>().AddIngredient<LifeAlloy>(5).AddIngredient<RuinousSoul>(2)
			.AddIngredient(4412)
			.AddTile(134)
			.Register();
	}
}
