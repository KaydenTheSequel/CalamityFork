using System;
using CalamityMod.Cooldowns;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "PaintballBlaster" })]
public class SpeedBlaster : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle Shot = new SoundStyle("CalamityMod/Sounds/Item/Splatshot")
	{
		PitchVariance = 0.3f,
		Volume = 3.5f
	};

	public static readonly SoundStyle Dash = new SoundStyle("CalamityMod/Sounds/Item/SplatshotDash")
	{
		PitchVariance = 0.3f,
		Volume = 5f
	};

	public static readonly SoundStyle ShotBig = new SoundStyle("CalamityMod/Sounds/Item/SplatshotBig")
	{
		PitchVariance = 0.3f,
		Volume = 2f
	};

	public static readonly SoundStyle Empty = new SoundStyle("CalamityMod/Sounds/Item/DudFire")
	{
		PitchVariance = 0.3f,
		Volume = 0.7f
	};

	public static float DashShotDamageMult = 4.5f;

	public static int DashCooldown = 300;

	public static float FireRatePowerup = 1.15f;

	public float ColorValue;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DashCooldown / 60);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 30;
		base.Item.damage = 40;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 10);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2.25f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = Shot;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 20f;
		base.Item.shoot = ModContent.ProjectileType<SpeedBlasterShot>();
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (player.HasCooldown(SpeedBlasterBoost.ID) && player.altFunctionUse == 2)
		{
			SoundEngine.PlaySound(in Empty, player.Center);
			return false;
		}
		base.Item.UseSound = ((player.altFunctionUse == 2) ? ShotBig : Shot);
		return base.CanUseItem(player);
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-1f, -6f);
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (!player.HasCooldown(SpeedBlasterBoost.ID))
		{
			return 1f;
		}
		return FireRatePowerup;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 newPos = position + Utils.RotatedBy(new Vector2(38f, (float)player.direction * ((Math.Abs(velocity.SafeNormalize(Vector2.Zero).X) < 0.02f) ? (-2f) : (-8f))), (double)velocity.ToRotation(), default(Vector2));
		if (player.altFunctionUse == 2 && !player.HasCooldown(SpeedBlasterBoost.ID))
		{
			if (ColorValue >= 4f)
			{
				ColorValue = 0f;
			}
			else
			{
				ColorValue++;
			}
			Projectile.NewProjectile(source, newPos, velocity, type, (int)((float)damage * DashShotDamageMult), knockback, player.whoAmI, ColorValue, 3f);
			player.AddCooldown(SpeedBlasterBoost.ID, DashCooldown);
			player.Calamity().sBlasterDashActivated = true;
			if (player.velocity != Vector2.Zero)
			{
				Color ColorUsed = SpeedBlasterShot.GetColor(ColorValue);
				for (int i = 0; i <= 8; i++)
				{
					Vector2 sparkVel = player.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(MathHelper.ToRadians(45f)) * Main.rand.NextFloat(-28f, -36f);
					GeneralParticleHandler.SpawnParticle(new CritSpark(player.Center, sparkVel, Color.White, ColorUsed, 1.5f, 45, 0.5f, 2f));
				}
			}
			return false;
		}
		Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(player.HasCooldown(SpeedBlasterBoost.ID) ? 3f : 15f));
		float ShotMode = (player.HasCooldown(SpeedBlasterBoost.ID) ? 2f : 0f);
		Projectile.NewProjectile(source, newPos, newVel, type, damage, knockback, player.whoAmI, ColorValue, ShotMode);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3350).AddRecipeGroup("AnyMythrilBar", 5).AddTile(134)
			.Register();
	}
}
