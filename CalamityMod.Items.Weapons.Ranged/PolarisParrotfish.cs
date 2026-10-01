using System;
using System.Collections.Generic;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class PolarisParrotfish : ModItem, ILocalizedModType, IModType
{
	public int ShotNumber;

	public bool Happy;

	public float fireSpeed = 1f;

	public static readonly SoundStyle Shot = new SoundStyle("CalamityMod/Sounds/Item/PolarisShot")
	{
		Volume = 0.6f
	};

	public static readonly SoundStyle Squeak = new SoundStyle("CalamityMod/Sounds/Custom/CuteSqueak")
	{
		Volume = 0.75f
	};

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 34;
		base.Item.damage = 35;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 9;
		base.Item.useAnimation = 9;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 0.5f;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<PolarStar>();
		base.Item.shootSpeed = 10f;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.FindAndReplace("[GFB]", this.GetLocalizedValue(Main.zenithWorld ? "TooltipGFB" : "TooltipNormal"));
	}

	public override bool AltFunctionUse(Player player)
	{
		return Main.zenithWorld;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld && player.altFunctionUse == 2)
		{
			if (Happy && Main.rand.NextBool(50))
			{
				Happy = false;
				player.itemTime = 200;
				player.itemAnimation = 200;
				player.SetScreenshake(26f);
				player.AddBuff(163, 600);
				Main.NewText(CalamityUtils.GetTextValue("Misc.Polaris0"), byte.MaxValue, 0, 0);
				SoundEngine.PlaySound(Squeak with
				{
					Pitch = -1f
				}, player.Center);
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/CeaselessVoidDeathBuild");
				style.Pitch = 0.5f;
				SoundEngine.PlaySound(in style, player.Center);
				int theFuckening = ModContent.ProjectileType<AstralFlame>();
				int projDamage = 500;
				int totalProjectiles = 50;
				float radians = (float)Math.PI * 2f / (float)totalProjectiles;
				Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
				for (int k = 0; k < totalProjectiles; k++)
				{
					Vector2 projVelocity = spinningPoint.RotatedBy(radians * (float)k);
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center + projVelocity * 2000f, -projVelocity * 4f, theFuckening, projDamage, 0f, Main.myPlayer);
				}
			}
			else
			{
				SoundEngine.PlaySound(in Squeak, player.Center);
				CombatText.NewText(player.Hitbox, Color.Violet, "^-^");
				Happy = true;
				switch (Main.rand.Next(1, 6))
				{
				case 5:
					Main.NewText(CalamityUtils.GetTextValue("Misc.Polaris1"), 72, 209, 204);
					break;
				case 4:
					Main.NewText(CalamityUtils.GetTextValue("Misc.Polaris2"), 72, 209, 204);
					break;
				case 3:
					Main.NewText(CalamityUtils.GetTextValue("Misc.Polaris3"), 72, 209, 204);
					break;
				case 2:
					Main.NewText(CalamityUtils.GetTextValue("Misc.Polaris4"), 72, 209, 204);
					break;
				default:
					Main.NewText(CalamityUtils.GetTextValue("Misc.Polaris5"), 72, 209, 204);
					break;
				}
				for (int i = 0; i <= 6; i++)
				{
					Vector2 hVelocity = Utils.RotateRandom(new Vector2(0f, -4f), 0.45);
					hVelocity.X *= 0.66f;
					hVelocity *= Main.rand.NextFloat(1f, 2f);
					int heart = Gore.NewGore(player.GetSource_FromThis(), player.Center + velocity * 4f, hVelocity, 331, Main.rand.NextFloat(0.2f, 1.3f));
					Main.gore[heart].sticky = false;
					Gore obj = Main.gore[heart];
					obj.velocity *= 0.4f;
					Main.gore[heart].velocity.Y -= 0.85f;
				}
			}
		}
		else
		{
			if (Main.zenithWorld)
			{
				if (Happy && Main.rand.NextBool(100))
				{
					CombatText.NewText(player.Hitbox, Color.Violet, ">~<");
					Happy = false;
					SoundEngine.PlaySound(Squeak with
					{
						Pitch = -0.6f
					}, player.Center);
				}
				else
				{
					SoundEngine.PlaySound(in Shot, player.Center);
				}
			}
			else
			{
				SoundEngine.PlaySound(in Shot, player.Center);
			}
			for (int j = 0; j < ((!Happy) ? 1 : 3); j++)
			{
				Projectile.NewProjectile(source, position + velocity * 5f, velocity.RotatedByRandom(0.05f * (float)((j == 0) ? 1 : 6)), ModContent.ProjectileType<PolarStar>(), damage, knockback, player.whoAmI, 0f, ShotNumber);
			}
			if (ShotNumber >= 2)
			{
				ShotNumber = 0;
			}
			else
			{
				ShotNumber++;
			}
		}
		return false;
	}

	public override float UseSpeedMultiplier(Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		NPC target = player.Center.ClosestNPCAt(400f);
		fireSpeed = ((target == null) ? 1f : Utils.Remap(player.Center.Distance(target.Center), 100f, 400f, 2f, 1f));
		if (!Happy)
		{
			return fireSpeed;
		}
		return fireSpeed * 2f;
	}
}
