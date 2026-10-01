using System;
using System.IO;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PolarisGaze : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 direction;

	public const float maxShred = 650f;

	public Projectile Wheel;

	public bool Dashing;

	public Vector2 DashStart;

	public Particle[] Rings;

	public Particle PolarStar;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/GalaxiaExtra";

	public ref float Shred => ref base.Projectile.ai[0];

	public ref float HitChargeCooldown => ref base.Projectile.ai[1];

	public float ShredRatio => MathHelper.Clamp(Shred / 325f, 0f, 1f);

	public Player Owner => Main.player[base.Projectile.owner];

	public float Bounce(float x)
	{
		if (!(x <= 50f))
		{
			if (!(x <= 65f))
			{
				return 1f;
			}
			return 1f + 0.15f * (float)Math.Sin((x - 50f) / 15f * (float)Math.PI);
		}
		return x / 50f;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 70);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = FourSeasonsGalaxia.PolarisAttunement_LocalIFrames;
	}

	public override bool? CanDamage()
	{
		return Shred >= (float)FourSeasonsGalaxia.PolarisAttunement_LocalIFrames;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 145f * base.Projectile.scale;
		float bladeWidth = 86f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center, Owner.Center + direction * bladeLength, bladeWidth, ref collisionPoint);
	}

	public override void AI()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_063c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0897: Unknown result type (might be due to invalid IL or missing references)
		//IL_0813: Unknown result type (might be due to invalid IL or missing references)
		//IL_0818: Unknown result type (might be due to invalid IL or missing references)
		//IL_0822: Unknown result type (might be due to invalid IL or missing references)
		//IL_082c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0831: Unknown result type (might be due to invalid IL or missing references)
		//IL_0842: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ad: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			SoundEngine.PlaySound(in SoundID.Item90, base.Projectile.Center);
			initialized = true;
			Projectile[] projectile = Main.projectile;
			foreach (Projectile proj in projectile)
			{
				if (proj.active && proj.type == ModContent.ProjectileType<PolarisGazeStar>() && proj.owner == Owner.whoAmI)
				{
					Wheel = proj;
					Dashing = true;
					DashStart = Owner.Center;
					Wheel.timeLeft = 60;
					Owner.GiveUniversalIFrames(FourSeasonsGalaxia.PolarisAttunement_SlashIFrames);
					break;
				}
			}
		}
		if (Owner.CantUseHoldout())
		{
			base.Projectile.Kill();
			return;
		}
		float bladeLength = 120f * base.Projectile.scale;
		for (int j = 0; j < 3; j++)
		{
			if (Rings[j] == null)
			{
				Rings[j] = new ConstellationRingVFX(Owner.Center + direction * (25f + bladeLength * 0.33f * (float)j), Color.DarkOrchid, direction.ToRotation(), base.Projectile.scale * 0.25f * (float)j, new Vector2(0.5f, 1f), 1f, 3 + j, 2f, 7f, important: true);
				GeneralParticleHandler.SpawnParticle(Rings[j]);
				continue;
			}
			Rings[j].Time = 0;
			Rings[j].Position = Owner.Center + Vector2.Lerp(Vector2.Zero, (0.9f + 0.1f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 10f - (float)j * 0.5f)) * direction * (25f + bladeLength * 0.33f * (float)(j + 1)), Bounce(MathHelper.Clamp(Shred - (float)(30 * j), 0f, 650f)));
			Rings[j].Rotation = direction.ToRotation();
			Rings[j].Scale = base.Projectile.scale * 0.25f * (float)(j + 1);
			(Rings[j] as ConstellationRingVFX).Opacity = 0.5f + 0.5f * ShredRatio;
		}
		if (PolarStar == null)
		{
			PolarStar = new GenericSparkle(Owner.Center + direction, Vector2.Zero, Color.White, Color.DodgerBlue, base.Projectile.scale, 2, 0.05f, 5f, needed: true);
			GeneralParticleHandler.SpawnParticle(PolarStar);
		}
		else
		{
			PolarStar.Time = 0;
			PolarStar.Position = Owner.Center + direction * 46f * base.Projectile.scale;
			PolarStar.Color = (((double)ShredRatio > 0.75) ? Color.Lerp(Color.White, Color.CornflowerBlue, Utils.Remap(ShredRatio, 0.75f, 0.85f, 0f, 1f)) : Color.White);
			PolarStar.Rotation += (1f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 4f)) * (((double)ShredRatio > 0.85) ? 0.03f : 0.02f);
			PolarStar.Scale = base.Projectile.scale * 2f;
		}
		if (Main.rand.NextBool())
		{
			Vector2 smokeSpeed = direction.RotatedByRandom(0.2356194704771042) * Main.rand.NextFloat(10f, 30f) * (ShredRatio * 0.5f + 1f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, smokeSpeed + Owner.velocity, Color.Lerp(Color.Purple, Color.Indigo, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f)), 30, Main.rand.NextFloat(0.5f, 1f) * (ShredRatio * 0.5f + 1f), 0.6f));
			if (Main.rand.NextBool(3) && (double)ShredRatio > 0.85)
			{
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, smokeSpeed + Owner.velocity, Main.hslToRgb(0.55f, 1f, 0.5f + 0.2f * ShredRatio), 20, Main.rand.NextFloat(0.4f, 0.7f) * (ShredRatio * 0.5f + 1f), 0.6f, 0f, glowing: true, 0.01f));
			}
		}
		if (Shred >= 650f)
		{
			Shred = 650f;
		}
		if (Shred < 0f)
		{
			Shred = 0f;
		}
		direction = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
		((Vector2)(ref direction)).Normalize();
		base.Projectile.rotation = direction.ToRotation();
		base.Projectile.Center = Owner.Center + direction * 60f;
		base.Projectile.localNPCHitCooldown = FourSeasonsGalaxia.PolarisAttunement_LocalIFrames - (int)MathHelper.Lerp(0f, (float)(FourSeasonsGalaxia.PolarisAttunement_LocalIFrames - FourSeasonsGalaxia.PolarisAttunement_LocalIFramesCharged), ShredRatio);
		base.Projectile.scale = 1f + ShredRatio * 1.5f;
		if ((Wheel == null || !Wheel.active) && Dashing)
		{
			Dashing = false;
			Player owner = Owner;
			owner.velocity *= 0.1f;
			SoundEngine.PlaySound(in CommonCalamitySounds.MeatySlashSound, base.Projectile.Center);
			if (Owner.whoAmI == Main.myPlayer && Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.Center - DashStart / 2f, Vector2.Zero, ModContent.ProjectileType<PolarisGazeDash>(), (int)((float)base.Projectile.damage * FourSeasonsGalaxia.PolarisAttunement_SlashDamageBoost), 0f, Owner.whoAmI).ModProjectile is PolarisGazeDash dash)
			{
				dash.DashStart = DashStart;
				dash.DashEnd = Owner.Center;
			}
		}
		Owner.Calamity().LungingDown = false;
		if (Dashing)
		{
			Owner.Calamity().LungingDown = true;
			Owner.fallStart = (int)(Owner.position.Y / 16f);
			Owner.velocity = Owner.SafeDirectionTo(Wheel.Center, Vector2.Zero) * 60f;
			if (Owner.Distance(Wheel.Center) < 60f)
			{
				Wheel.active = false;
			}
		}
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign(direction.X));
		Owner.itemRotation = direction.ToRotation();
		if (Owner.direction != 1)
		{
			Owner.itemRotation -= (float)Math.PI;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Shred += FourSeasonsGalaxia.PolarisAttunement_ShredChargeupGain;
		HitChargeCooldown--;
		base.Projectile.timeLeft = 2;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float maxMultiplier = (float)FourSeasonsGalaxia.PolarisAttunement_FullChargeDamage / (float)FourSeasonsGalaxia.PolarisAttunement_BaseDamage;
		float damageMultiplier = MathHelper.Lerp(1f, maxMultiplier, ShredRatio);
		float damageReduction = (float)base.Projectile.localNPCHitCooldown / (float)FourSeasonsGalaxia.PolarisAttunement_LocalIFrames;
		modifiers.SourceDamage *= damageMultiplier * damageReduction;
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		float maxMultiplier = (float)FourSeasonsGalaxia.PolarisAttunement_FullChargeDamage / (float)FourSeasonsGalaxia.PolarisAttunement_BaseDamage;
		float damageMultiplier = MathHelper.Lerp(1f, maxMultiplier, ShredRatio);
		float damageReduction = (float)base.Projectile.localNPCHitCooldown / (float)FourSeasonsGalaxia.PolarisAttunement_LocalIFrames;
		modifiers.SourceDamage *= damageMultiplier * damageReduction;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		ShredTarget();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		ShredTarget();
	}

	private void ShredTarget()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == Owner.whoAmI)
		{
			Owner.fallStart = (int)(Owner.position.Y / 16f);
			if (HitChargeCooldown <= 0f)
			{
				SoundEngine.PlaySound(in SoundID.NPCHit30, base.Projectile.Center);
				Shred += 80f;
				Owner.GiveUniversalIFrames(FourSeasonsGalaxia.PolarisAttunement_ShredIFrames);
				HitChargeCooldown = 20f;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCHit43, base.Projectile.Center);
		if ((double)ShredRatio > 0.85 && Owner.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.ClampedMouseWorld(), Vector2.Zero, ModContent.ProjectileType<PolarisGazeStar>(), (int)((float)base.Projectile.damage * FourSeasonsGalaxia.PolarisAttunement_ShotDamageBoost), base.Projectile.knockBack, Owner.whoAmI, Shred);
		}
		if (Dashing)
		{
			Player owner = Owner;
			owner.velocity *= 0.1f;
		}
		Owner.Calamity().LungingDown = false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sword = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/GalaxiaExtra", (AssetRequestMode)2).Value;
		float drawRotation = direction.ToRotation() + (float)Math.PI / 4f;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)sword.Height);
		Vector2 drawOffset = Owner.Center + direction * 10f - Main.screenPosition;
		Main.EntitySpriteDraw(sword, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(initialized);
		writer.WriteVector2(direction);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		initialized = reader.ReadBoolean();
		direction = reader.ReadVector2();
	}

	public PolarisGaze()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		direction = Vector2.Zero;
		Rings = new Particle[3];
		base._002Ector();
	}
}
