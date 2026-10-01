using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class FriendlyLaserWallBeam : ModProjectile, ILocalizedModType, IModType
{
	public float time;

	public bool doneAttack;

	public int attackTime;

	public float laserFX;

	public float storedTime;

	public Color drawColor;

	public float sine;

	public float laserRot;

	private Vector2 beamStart;

	private Vector2 directionToTarget;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public new string LocalizationCategory => "Projectiles.Typeless";

	public ref float attackSpeed => ref base.Projectile.ai[0];

	public ref float laserType => ref base.Projectile.ai[1];

	public bool canDamage
	{
		get
		{
			if (doneAttack)
			{
				return laserFX >= 1f;
			}
			return false;
		}
	}

	public float laserLength => (laserType == 0f) ? 4000 : 2000;

	public Vector2 targetPos
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 1;
		base.Projectile.height = 1;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 6000;
		base.Projectile.scale = 2f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		if (laserFX > 0f)
		{
			laserFX = MathHelper.Lerp(laserFX, 0f, (time > 15f) ? 0.07f : 0.01f);
		}
		sine = (float)Math.Sin(time * 4f / (float)Math.PI);
		if (time == 0f)
		{
			laserRot = base.Projectile.velocity.ToRotation();
			beamStart = targetPos + Vector2.UnitX.RotatedBy(laserRot) * laserLength;
			directionToTarget = beamStart.DirectionTo(targetPos);
			if (attackSpeed == 0f)
			{
				attackSpeed = 3f;
			}
			if (attackSpeed < 0f)
			{
				attackSpeed = 0f - attackSpeed;
				time = attackTime;
			}
			base.Projectile.velocity = Vector2.Zero;
			laserFX = 1f;
			base.Projectile.ForceNetUpdate();
		}
		if (time >= (float)attackTime && !doneAttack)
		{
			SoundStyle attack = new SoundStyle("CalamityMod/Sounds/Custom/DoGLaserWallLightAttack");
			if (base.Projectile.scale > 3f)
			{
				attack = new SoundStyle("CalamityMod/Sounds/Custom/DoGLaserWallBigAttack");
			}
			SoundEngine.PlaySound(attack with
			{
				Volume = 0.4f,
				Pitch = 0f,
				MaxInstances = -1
			}, Vector2.Lerp(targetPos, Main.player[base.Projectile.owner].Center, (laserType == 0f) ? 0f : 0.7f));
			laserFX = 2.5f;
			doneAttack = true;
			storedTime = time;
			base.Projectile.ForceNetUpdate();
			if (Main.LocalPlayer.Distance(base.Projectile.Center) < 1600f)
			{
				Main.LocalPlayer.SetScreenshake(base.Projectile.ai[2]);
			}
		}
		float endTime = storedTime + 10f;
		if (time >= endTime && doneAttack)
		{
			base.Projectile.Kill();
			return;
		}
		if (doneAttack)
		{
			drawColor = Color.Lerp(Color.Magenta, Color.Cyan, (float)Math.Pow(Utils.GetLerpValue(endTime, storedTime, time, clamped: true), 2.0));
		}
		time += attackSpeed;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (canDamage)
		{
			return null;
		}
		return false;
	}

	public override bool CanHitPlayer(Player target)
	{
		if (canDamage)
		{
			return true;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 180);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		if (!canDamage)
		{
			return false;
		}
		float _ = float.NaN;
		Vector2 start = beamStart;
		Vector2 end = beamStart + directionToTarget * laserLength * 2f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 30f * base.Projectile.scale, ref _);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		if (laserFX == 0f)
		{
			return false;
		}
		Texture2D beam = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineThick", (AssetRequestMode)2).Value;
		Texture2D bBeam = ModContent.Request<Texture2D>("CalamityMod/Particles/LineThick", (AssetRequestMode)2).Value;
		Texture2D angleBeam = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowBlade", (AssetRequestMode)2).Value;
		Texture2D angleBeamInside = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowBladeNoBloom", (AssetRequestMode)2).Value;
		float opacity = (doneAttack ? 0.65f : 0.35f) * (float)Math.Pow(Math.Min(laserFX, 1f), 2.0);
		Color val = drawColor;
		((Color)(ref val)).A = 0;
		Color beamColor = val;
		if (CalamityClientConfig.Instance.Photosensitivity)
		{
			opacity = 0.2f;
		}
		if (laserType == 0f)
		{
			for (int t = 0; t < ((!doneAttack) ? 1 : 5); t++)
			{
				bool black = t > 0;
				Texture2D usedTex = (black ? bBeam : beam);
				float beamThickness = 0.03f * (black ? (0.8f - 0.15f * (float)t) : 1f) * ((laserFX <= 1f) ? ((float)Math.Pow(Math.Min(laserFX, 1f), 2.0)) : laserFX) * Utils.Remap(sine, -1f, 1f, 0.8f, 1.1f);
				float rot = beamStart.DirectionTo(targetPos).ToRotation() + (float)Math.PI / 2f;
				Main.EntitySpriteDraw(usedTex, beamStart - Main.screenPosition, null, (black ? (Color.Black * opacity) : (beamColor * opacity)) * (black ? (0.2f + 0.15f * (float)t) : 1f), rot, new Vector2((float)(beam.Width / 2), (float)beam.Height), new Vector2(beamThickness * base.Projectile.scale, laserLength / 975f * ((usedTex == beam) ? 1f : 0.8277f)), (SpriteEffects)0);
			}
		}
		else
		{
			opacity = 1f;
			if (CalamityClientConfig.Instance.Photosensitivity)
			{
				opacity = 0.2f;
			}
			for (int i = 0; i < ((!doneAttack) ? 1 : 5); i++)
			{
				bool notFirstDrawn = i > ((base.Projectile.scale > 3f) ? 1 : 0);
				float beamThickness2 = 0.008163265f * base.Projectile.scale * (notFirstDrawn ? (1f - 0.8f * (float)(i - 1) / 4f) : 1f) * ((laserFX <= 1f) ? ((float)Math.Pow(Math.Min(laserFX, 1f), 2.0)) : laserFX) * Utils.Remap(sine, -1f, 1f, 0.8f, 1.1f);
				Main.EntitySpriteDraw(notFirstDrawn ? angleBeamInside : angleBeam, beamStart - Main.screenPosition, null, (notFirstDrawn ? (Color.Black * opacity) : (beamColor * opacity)) * (notFirstDrawn ? (0.2f + 0.8f * (float)(i - 1) / 4f) : 1f), directionToTarget.ToRotation() + (float)Math.PI / 2f, new Vector2((float)(angleBeam.Width / 2), (float)angleBeam.Height), new Vector2(beamThickness2 * base.Projectile.scale, laserLength / 975f * 0.8277f), (SpriteEffects)0);
			}
		}
		return false;
	}

	public FriendlyLaserWallBeam()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		attackTime = 30;
		drawColor = Color.Magenta;
		beamStart = Vector2.Zero;
		directionToTarget = Vector2.Zero;
		base._002Ector();
	}
}
