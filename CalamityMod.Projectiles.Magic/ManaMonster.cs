using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ManaMonster : ModProjectile, ILocalizedModType, IModType
{
	public const int NPCAttackTime = 50;

	public const int PlayerAttackRedirectTime = 45;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Time => ref base.Projectile.ai[0];

	public Player Target => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 52);
		base.Projectile.friendly = true;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.Opacity = 0f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		if (Time < 50f)
		{
			base.Projectile.Opacity = Utils.GetLerpValue(0f, 30f, Time, clamped: true);
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 27f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.05f;
			}
		}
		else
		{
			if (Time == 50f)
			{
				SoundEngine.PlaySound(Utils.SelectRandom<SoundStyle>(Main.rand, SoundID.Zombie39, SoundID.Zombie40, SoundID.Zombie41), Target.Center);
				SoundEngine.PlaySound(in SoundID.DD2_DrakinShot, Target.Center);
				CreateTransitionBurstDust();
			}
			if (Time < 95f)
			{
				float idealMovementDirection = base.Projectile.AngleTo(Target.Center);
				float angularTurnSpeed = 0.09f;
				float newSpeed = MathHelper.Lerp(((Vector2)(ref base.Projectile.velocity)).Length(), 22f, 0.1f);
				base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(idealMovementDirection, angularTurnSpeed).ToRotationVector2() * newSpeed;
			}
			else if (((Vector2)(ref base.Projectile.velocity)).Length() < 35f)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 1.04f;
			}
			base.Projectile.Opacity = Utils.GetLerpValue(0f, 15f, base.Projectile.timeLeft, clamped: true);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		Time++;
	}

	public void CreateTransitionBurstDust()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 75; i++)
			{
				Dust brimstone = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Square(-25f, 25f), 235);
				brimstone.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2f, 3.1f) * MathHelper.Lerp(1f, 2.175f, (float)i / 75f);
				brimstone.velocity = Vector2.Lerp(brimstone.velocity, -Vector2.UnitY * ((Vector2)(ref brimstone.velocity)).Length(), 0.5f);
				brimstone.scale = MathHelper.Lerp(1f, 1.875f, (float)i / 75f) * Main.rand.NextFloat(0.8f, 1f);
				brimstone.fadeIn = 0.4f;
				brimstone.noGravity = true;
			}
		}
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		modifiers.SourceDamage *= 0f;
		if (Main.masterMode)
		{
			modifiers.SourceDamage.Flat += 540f;
		}
		else if (Main.expertMode)
		{
			modifiers.SourceDamage.Flat += 450f;
		}
		else
		{
			modifiers.SourceDamage.Flat += 360f;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 180);
	}

	public override bool? CanDamage()
	{
		if (!(base.Projectile.Opacity >= 1f))
		{
			return false;
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
