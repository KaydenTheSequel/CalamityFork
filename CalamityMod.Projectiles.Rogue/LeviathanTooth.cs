using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

[PierceResistException(false)]
public class LeviathanTooth : ModProjectile, ILocalizedModType, IModType
{
	public NPC chosenTarget;

	public bool stuckInTarget;

	public bool stuckInGround;

	public bool canDamage;

	public bool canStick;

	public Vector2 vibrate;

	public int stuckTimer;

	public Vector2 placementCenter;

	private float placementDistance;

	private Vector2 placementVelocity;

	public Vector2 storedVelocity;

	public bool collideWithTiles;

	public bool toothDirection;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 3;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_0758: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color newColor = ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Lerp(Color.Red, Color.White, 0.85f));
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.5f * Utils.GetLerpValue(255f, 0f, base.Projectile.alpha, clamped: true));
		if (time == 0f)
		{
			stuckTimer = Main.rand.Next(100, 121);
			if (base.Projectile.ai[1] < 4f)
			{
				toothDirection = Main.rand.NextBool();
			}
		}
		if (!stuckInTarget && !stuckInGround)
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 5f && time > 30f && base.Projectile.numHits == 0)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.97f;
			}
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			if ((time % 2f == 0f || !canStick) && time > 5f)
			{
				Vector2 position = base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 3f + Main.rand.NextVector2Circular(6f, 6f);
				int type = ((!ChildSafety.Disabled) ? 16 : 5);
				Vector2? velocity = (-base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 4f).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.1f, 0.8f);
				newColor = default(Color);
				Dust.NewDustPerfect(position, type, velocity, 100, newColor, Main.rand.NextFloat(0.8f, 1.4f) * (canStick ? 1f : 1.3f)).noGravity = true;
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(canStick ? 0.3f : 0.1f) * Main.rand.NextFloat(5f, 8f), "CalamityMod/Particles/LargeBloom", affectedByGravity: false, 8, Main.rand.NextFloat(0.055f, 0.07f), ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Lerp(Color.DarkRed, Color.Black, Main.rand.NextFloat(0f, 0.5f))) * 0.7f, new Vector2(0.7f, 1f), useAddativeBlend: false, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.8f));
			}
			if (time > 90f && canStick)
			{
				base.Projectile.velocity.Y += 0.03f;
				if (base.Projectile.velocity.Y > 0f)
				{
					base.Projectile.velocity.X *= 0.99f;
				}
			}
		}
		else if (stuckInTarget)
		{
			float power = 5f * Utils.GetLerpValue(120f, 0f, stuckTimer, clamped: true);
			vibrate = Main.rand.NextVector2Circular(power, power);
			base.Projectile.rotation = storedVelocity.SafeNormalize(Vector2.UnitX).ToRotation() + (float)Math.PI / 2f;
			Vector2 impaleVel = storedVelocity * 0.5f * Utils.GetLerpValue(120f, 0f, stuckTimer, clamped: true);
			placementCenter = chosenTarget.Center + placementVelocity * placementDistance + impaleVel;
			base.Projectile.Center = placementCenter;
			stuckTimer--;
			if (chosenTarget.life <= 0 || chosenTarget == null)
			{
				stuckTimer = 0;
			}
			if (stuckTimer == 0)
			{
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					base.Projectile.localNPCImmunity[i] = 0;
				}
				canDamage = true;
				vibrate = Vector2.Zero;
				canStick = false;
				collideWithTiles = true;
				base.Projectile.velocity = storedVelocity;
				base.Projectile.extraUpdates = 4;
				stuckInTarget = false;
				for (int j = 0; j <= 7; j++)
				{
					Vector2 center2 = base.Projectile.Center;
					int type2 = ((!ChildSafety.Disabled) ? 16 : 5);
					Vector2? velocity2 = (storedVelocity * 2.5f).RotatedByRandom(0.7) * Main.rand.NextFloat(0.1f, 0.8f);
					newColor = default(Color);
					Dust.NewDustPerfect(center2, type2, velocity2, 0, newColor, Main.rand.NextFloat(0.9f, 1.8f)).noGravity = false;
				}
				for (int k = 0; k <= 3; k++)
				{
					GeneralParticleHandler.SpawnParticle(new AltSparkParticle(base.Projectile.Center, (storedVelocity * 4.5f).RotatedByRandom(0.7) * Main.rand.NextFloat(0.1f, 0.8f) + new Vector2(0f, -2f), affectedByGravity: true, 20, 0.5f, ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.DarkRed) * 0.7f));
				}
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfSmallHit", 3);
				style.Volume = 0.5f;
				style.Pitch = Main.rand.NextFloat(-0.2f, -0.3f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
		}
		if (stuckInGround)
		{
			base.Projectile.rotation = storedVelocity.ToRotation() + (float)Math.PI / 2f;
		}
		if (collideWithTiles && Collision.SolidCollision(base.Projectile.Center, 4, 4))
		{
			canDamage = false;
			base.Projectile.timeLeft = 180;
			stuckInGround = true;
			storedVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
			base.Projectile.velocity = Vector2.Zero;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/RavagerRockPillarHit1");
			style.Volume = 0.25f;
			style.Pitch = Main.rand.NextFloat(-0.3f, -0.6f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			collideWithTiles = false;
		}
		base.Projectile.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 0f, 60f, 255f, 0f);
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= ((target == chosenTarget) ? 1f : (canStick ? 0.2f : 0.5f));
	}

	public override bool? CanDamage()
	{
		if (!canDamage)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		if (target == chosenTarget)
		{
			target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
		}
		if (!stuckInTarget && canStick)
		{
			if (base.Projectile.timeLeft < 400)
			{
				base.Projectile.timeLeft = 400;
			}
			collideWithTiles = false;
			canDamage = false;
			placementDistance = 0f - Vector2.Distance(target.Center, base.Projectile.Center);
			placementVelocity = (target.Center - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
			placementCenter = placementVelocity * (placementDistance * 0.01f);
			chosenTarget = target;
			stuckInTarget = true;
			storedVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 8f;
			base.Projectile.velocity = Vector2.Zero;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		if (base.Projectile.ai[1] == 2f)
		{
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/LeviathanTooth2", (AssetRequestMode)2).Value;
		}
		if (base.Projectile.ai[1] == 3f)
		{
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/LeviathanTooth3", (AssetRequestMode)2).Value;
		}
		if (base.Projectile.ai[1] == 4f)
		{
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/GreenWater", (AssetRequestMode)2).Value;
		}
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition + vibrate, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)(toothDirection ? 1 : 0));
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 18f, targetHitbox);
	}

	public LeviathanTooth()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		canDamage = true;
		canStick = true;
		vibrate = Vector2.Zero;
		stuckTimer = 180;
		collideWithTiles = true;
		base._002Ector();
	}
}
