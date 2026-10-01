using System;
using CalamityMod.Dusts;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "NullificationRifle" })]
public class NullificationPistol : ModItem, ILocalizedModType, IModType
{
	public bool shotType = true;

	public float mult;

	public static SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/Item/NullHit");

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 28;
		base.Item.damage = 190;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 18;
		base.Item.useAnimation = 18;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 0.1f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 8f;
		base.Item.shoot = ModContent.ProjectileType<NullShot>();
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			return 1.3f - (float)(1.0 - Math.Pow(Utils.GetLerpValue(0f, shotType ? 0.175f : 0.25f, mult, clamped: true), 2.5));
		}
		if (Main.zenithWorld)
		{
			return 3f * (shotType ? 0.7f : 1f);
		}
		if (!shotType)
		{
			return 1.5f - ((mult > 0.25f) ? mult : 0f);
		}
		return 1f - ((mult > 0.175f) ? mult : 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DudFire");
			style.Volume = 0.7f;
			style.Pitch = -0.5f + (shotType ? 0.5f : 0f);
			SoundEngine.PlaySound(in style, position);
			for (int i = 0; i < 18; i++)
			{
				Dust dust = Dust.NewDustPerfect(position, shotType ? ModContent.DustType<VoidDust>() : ModContent.DustType<LightDust>(), velocity.RotatedByRandom(100.0) * Main.rand.NextFloat(0.2f, 1f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.9f, 1.25f);
				dust.color = (shotType ? Color.White : (Main.rand.NextBool() ? Color.Orchid : Color.Turquoise));
			}
			shotType = !shotType;
			mult = 0f;
		}
		else if (shotType)
		{
			for (int j = 0; j < 3; j++)
			{
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<NullShot>(), damage / 3, 0f, player.whoAmI, 0f, 0f, j);
			}
			if (mult < 0.175f)
			{
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<NullFlash>(), damage, 0f, player.whoAmI);
			}
			if (mult < 0.35f)
			{
				mult += 0.013f;
			}
		}
		else
		{
			Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<NullShot>(), (int)((float)damage * 0.7f), 0f, player.whoAmI, 0f, 5f).penetrate = 1;
			if (mult < 0.25f)
			{
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<NullFlash>(), (int)((float)damage * 0.75f), 0f, player.whoAmI, 0f, 5f);
			}
			if (mult < 0.5f)
			{
				mult += 0.013f;
			}
		}
		if (player.altFunctionUse != 2)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/NullShot");
			style.Volume = 0.7f;
			style.Pitch = Main.rand.NextFloat(0f, 0.2f) + ((!shotType) ? 0.3f : 0f) - ((!shotType) ? ((mult > 0.175f) ? mult : 0f) : ((mult > 0.25f) ? mult : 0f));
			SoundEngine.PlaySound(in style, position);
			if (mult >= (shotType ? 0.175f : 0.25f))
			{
				for (int k = 0; k < 18; k++)
				{
					Dust dust2 = Dust.NewDustPerfect(position + velocity * 8f, ModContent.DustType<UnstableDust>(), velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.4f, 1f));
					dust2.noGravity = !Main.rand.NextBool(4);
					dust2.scale = Main.rand.NextFloat(1.2f, 1.8f);
					dust2.color = (Main.rand.NextBool() ? Color.White : (Main.rand.NextBool() ? Color.Orchid : Color.Turquoise));
					dust2.fadeIn = 7.5f;
				}
			}
		}
		return false;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float itemRotation = player.compositeFrontArm.rotation + (float)Math.PI / 2f * player.gravDir;
		float pullback = 7f;
		float animProgress = 0.5f - (float)player.itemTime / (float)player.itemTimeMax;
		(player.Center - player.Calamity().mouseWorld).ToRotation();
		_ = player.gravDir;
		if (animProgress < 0.4f)
		{
			pullback -= 2.75f * (float)Math.Pow((0.6f - animProgress) / 0.6f, 2.0);
		}
		Vector2 itemPosition = player.MountedCenter + itemRotation.ToRotationVector2() * pullback;
		Vector2 itemSize = default(Vector2);
		((Vector2)(ref itemSize))._002Ector(52f, 28f);
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
			rotation += ((player.altFunctionUse == 2) ? (-0.15f) : 0f) * (float)Math.Pow((0.6f - animProgress) / 0.6f, 2.0) * (float)player.direction;
		}
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rotation);
	}
}
