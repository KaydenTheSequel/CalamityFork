using System;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class NanoblackTesselation : ModProjectile, ILocalizedModType, IModType
{
	internal static Asset<Texture2D> Glow;

	private const int SpriteWidth = 52;

	private const int Lifetime = 60;

	private const int VanishTime = 12;

	internal const int MinDelay = 15;

	internal const int MaxDelay = 45;

	private const float TargetingRange = 600f;

	private const float FiringRange = 2000f;

	private const float StartingRotationIncrement = 0.65999997f;

	private const float DriftSpeed = 0.8f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	private Player Owner => Main.player[base.Projectile.owner];

	internal ref float AttackDelay => ref base.Projectile.ai[0];

	internal ref float CurrentSpin => ref base.Projectile.ai[1];

	private bool IsVanishing => base.Projectile.timeLeft < 12;

	public override void Load()
	{
		Glow = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
	}

	public override void SetStaticDefaults()
	{
		base.DrawOffsetX = -10;
		base.DrawOriginOffsetY = 0;
		base.DrawOriginOffsetX = 0f;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.scale = 0.5f;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 60;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 60)
		{
			FrameOneEffects();
		}
		if (Owner.whoAmI == Main.myPlayer)
		{
			Owner.Calamity().mouseWorldListener = true;
		}
		bool shouldShutdown = false;
		if (!IsVanishing)
		{
			shouldShutdown = AttemptAttackThisFrame();
		}
		if (shouldShutdown)
		{
			base.Projectile.timeLeft = 11;
			float orbScale = 1.5f;
			Color orbColor = NanoblackReaper.TesselationParticleColor;
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 15, orbScale, orbColor));
		}
		ProcessSpin();
		if (!IsVanishing && ((Vector2)(ref base.Projectile.velocity)).LengthSquared() > 0.64000005f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.75f;
		}
		if (IsVanishing)
		{
			base.Projectile.scale *= 0.88f;
		}
	}

	private void FrameOneEffects()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		if (AttackDelay < 15f)
		{
			AttackDelay = 15f;
		}
		else if (AttackDelay > 45f)
		{
			AttackDelay = 45f;
		}
		CurrentSpin = 0.65999997f;
		float sparkSpeed = 2f;
		float baseRot = (float)Math.PI / 2f;
		float scale = 0.018f;
		int lifetime = 15;
		Color color = NanoblackReaper.TesselationParticleColor;
		Vector2 squashStretch = default(Vector2);
		((Vector2)(ref squashStretch))._002Ector(1f, 0.3f);
		for (int i = 0; i < 6; i++)
		{
			float rot = baseRot + (float)i * ((float)Math.PI / 3f);
			Vector2 sparkVel = sparkSpeed * rot.ToRotationVector2();
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, sparkVel, affectedByGravity: false, lifetime, scale, color, squashStretch, quickShrink: true));
		}
	}

	private void ProcessSpin()
	{
		float rotationIncrement = CurrentSpin * (float)base.Projectile.spriteDirection;
		base.Projectile.rotation += rotationIncrement;
		CurrentSpin *= 0.93f;
	}

	private bool AttemptAttackThisFrame()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		if (AttackDelay > 0f)
		{
			AttackDelay--;
			return false;
		}
		bool inFiringRange = false;
		NPC target = Owner.ClampedMouseWorld().ClosestNPCAt(600f, ignoreTiles: true, bossPriority: true);
		if (target == null || !target.active)
		{
			target = base.Projectile.Center.ClosestNPCAt(600f, ignoreTiles: true, bossPriority: true);
		}
		if (target != null && target.active)
		{
			inFiringRange = target.DistanceSQ(base.Projectile.Center) < 4000000f;
		}
		if (!inFiringRange)
		{
			return true;
		}
		PerformAttack(target);
		return true;
	}

	private void PerformAttack(NPC target)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		float xInterp = Main.rand.NextFloat();
		float yInterp = Main.rand.NextFloat();
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		Vector2 c = target.Center;
		float dartboardScale = 0.4f;
		Vector2 val = c - dartboardScale * target.Size;
		Vector2 bottomRight = c + dartboardScale * target.Size;
		float dartboardX = MathHelper.Lerp(val.X, bottomRight.X, xInterp);
		float dartboardY = MathHelper.Lerp(val.Y, bottomRight.Y, yInterp);
		Vector2 strikeDest = default(Vector2);
		((Vector2)(ref strikeDest))._002Ector(dartboardX, dartboardY);
		Vector2 offset = strikeDest - c;
		if (base.Projectile.Calamity().stealthStrike)
		{
			int carveID = ModContent.ProjectileType<NanoblackPiercingStrike>();
			int carveDamage = base.Projectile.damage;
			float carveKB = 0f;
			int carveIdx = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, carveID, carveDamage, carveKB, base.Projectile.owner, 0f, dartboardX, dartboardY);
			if (carveIdx.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[carveIdx].ArmorPenetration += NanoblackReaper.LightspeedCarveArmorPenetration;
			}
			return;
		}
		int zpeID = ModContent.ProjectileType<NanoblackStrike>();
		int zpeDamage = base.Projectile.damage;
		float zpeKB = 0f;
		int zpeIdx = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), strikeDest, Vector2.Zero, zpeID, zpeDamage, zpeKB, base.Projectile.owner, target.whoAmI, offset.X, offset.Y);
		if (zpeIdx.WithinBounds(Main.maxProjectiles))
		{
			Projectile obj = Main.projectile[zpeIdx];
			obj.ArmorPenetration += NanoblackReaper.ZeroPointArmorPenetration;
			obj.direction = (obj.spriteDirection = base.Projectile.spriteDirection);
		}
		Vector2 lineVel = 3f * base.Projectile.velocity;
		float xScale = 0.009f;
		float xShrink = 0.88f;
		Color lineColor = NanoblackReaper.ZeroPointLineColor;
		GeneralParticleHandler.SpawnParticle(new StaticGlowLine(base.Projectile.Center, strikeDest, lineVel, 7, xScale, xShrink, lineColor));
		int numOrbs = 3;
		float orbScale = 1.5f;
		Vector2 orbVel = lineVel;
		for (int i = 0; i < numOrbs; i++)
		{
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, orbVel, affectedByGravity: false, 15, orbScale, lineColor));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		float fWidthOverTwo = 26f;
		float fHeightOverTwo = (float)base.Projectile.height / 2f;
		SpriteEffects eff = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			eff = (SpriteEffects)1;
		}
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(fWidthOverTwo, fHeightOverTwo);
		Main.EntitySpriteDraw(Glow.Value, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, origin, base.Projectile.scale, eff);
	}
}
