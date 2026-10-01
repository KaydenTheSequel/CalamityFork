using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

[PierceResistException(false)]
public class LeviathanToothStealth : ModProjectile, ILocalizedModType, IModType
{
	public bool canDamage;

	public bool isTopJaw;

	public float topJawRot;

	public float bottomJawRot;

	public int jawSlamTime = 25;

	public bool spawnedDust;

	public int jawLength = 750;

	public int hitTimer;

	public override string Texture => "CalamityMod/Projectiles/Rogue/LeviathanTooth";

	public new string LocalizationCategory => "Projectiles.Rogue";

	public ref float time => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

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
		base.Projectile.extraUpdates = 0;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8;
		base.Projectile.ArmorPenetration = 25;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override void AI()
	{
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		if (hitTimer > 0)
		{
			hitTimer--;
		}
		if (time == 0f)
		{
			topJawRot = -(float)Math.PI / 4f;
			bottomJawRot = (float)Math.PI / 4f;
		}
		topJawRot *= 0.95f;
		bottomJawRot *= 0.95f;
		float openingRot = 0.5f;
		if (time <= 10f)
		{
			float lerpValue = 1f - (float)Math.Pow(Utils.GetLerpValue(10f, 0f, time, clamped: true), 2.0);
			topJawRot = MathHelper.Lerp(-(float)Math.PI / 4f, -(float)Math.PI / 4f - openingRot, lerpValue);
			bottomJawRot = MathHelper.Lerp((float)Math.PI / 4f, (float)Math.PI / 4f + openingRot, lerpValue);
		}
		else
		{
			float lerpValue2 = (float)Math.Pow(Utils.GetLerpValue(11f, jawSlamTime, time, clamped: true), 4.0);
			topJawRot = MathHelper.Lerp(-(float)Math.PI / 4f - openingRot, 0f, lerpValue2);
			bottomJawRot = MathHelper.Lerp((float)Math.PI / 4f + openingRot, 0f, lerpValue2);
		}
		if (time < (float)jawSlamTime * 1.5f)
		{
			Owner.direction = base.Projectile.direction;
			if (time < (float)jawSlamTime)
			{
				base.Projectile.Center = Owner.Center;
				base.Projectile.velocity = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld) * 0.01f;
			}
			Owner.itemTime = (Owner.itemAnimation = 2);
			Vector2 toMouse = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
			Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, toMouse.ToRotation() + topJawRot * (float)Owner.direction + (float)Math.PI / 2f * (float)(-Owner.direction) + ((Owner.direction == -1) ? ((float)Math.PI) : 0f));
			Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, toMouse.ToRotation() + bottomJawRot * (float)Owner.direction + (float)Math.PI / 2f * (float)(-Owner.direction) + ((Owner.direction == -1) ? ((float)Math.PI) : 0f));
		}
		if (time == (float)jawSlamTime)
		{
			Owner.SetScreenshake(5f);
			Owner.Calamity().ConsumeStealthByAttacking();
			SoundStyle crunch = new SoundStyle("CalamityMod/Sounds/NPCKilled/PerfSmallDeath");
			for (int i = 0; i < 3; i++)
			{
				SoundEngine.PlaySound(crunch with
				{
					Volume = 0.4f,
					Pitch = -0.1f * (float)i,
					MaxInstances = 3
				}, base.Projectile.Center);
			}
			SoundStyle crunch2 = new SoundStyle("CalamityMod/Sounds/NPCHit/RavagerHurt3");
			for (int j = 0; j < 2; j++)
			{
				SoundEngine.PlaySound(crunch2 with
				{
					Volume = 0.8f,
					Pitch = -0.4f * (float)j,
					MaxInstances = 2
				}, base.Projectile.Center);
			}
			canDamage = true;
		}
		base.Projectile.Opacity = (float)Math.Pow(Utils.GetLerpValue(0f, 40f, base.Projectile.timeLeft, clamped: true), 4.0);
		if (base.Projectile.Opacity <= 0.4f)
		{
			canDamage = false;
		}
		time++;
		spawnedDust = false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		float damageMult = 1f;
		if (hitTimer != 0)
		{
			damageMult = 0.2f;
		}
		bool jawsJustSlammed = time >= (float)jawSlamTime && time <= (float)(jawSlamTime + 8);
		modifiers.SourceDamage *= (float)((!jawsJustSlammed) ? 1 : 12) * damageMult;
		if (hitTimer == 0)
		{
			hitTimer = base.Projectile.localNPCHitCooldown;
		}
		if (target.CanBeMoved(ignoreKBImmune: true))
		{
			target.velocity *= 0.05f;
		}
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
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 300);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0f)
		{
			return false;
		}
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Vector2 startPoint = base.Projectile.Center;
		int distance = jawLength;
		int travelLength = 35;
		_ = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * (float)distance;
		Vector2 direction = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
		for (int t = -1; t <= 1; t += 2)
		{
			SpriteEffects spriteFx = (SpriteEffects)(!isTopJaw);
			Color drawColor = base.Projectile.GetAlpha(lightColor);
			int toothNum = 0;
			for (int i = 0; i < distance; i += travelLength)
			{
				if (((isTopJaw && base.Projectile.direction == -1) || (!isTopJaw && base.Projectile.direction == 1)) && i == 0)
				{
					i += (int)((float)travelLength * 0.5f);
				}
				float jawRot = (isTopJaw ? topJawRot : bottomJawRot);
				float scale = base.Projectile.scale + 0.35f - 0.02f * (float)toothNum;
				float slamLerp = (float)Math.Pow(Utils.GetLerpValue(0f, (float)jawSlamTime * 1.5f, time, clamped: true), 4.0);
				float sine = (float)Math.Pow((float)Math.Sin((time + (float)(toothNum * 3)) / 10f), 4.0) * slamLerp;
				Vector2 val = startPoint + (float)i * direction.RotatedBy(jawRot) + direction.RotatedBy((float)Math.PI / 2f * (float)(isTopJaw ? 1 : (-1))) * (-25f * scale - 65f * sine * scale);
				Vector2 toMouse = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
				Vector2 finalDrawPos = val + toMouse.RotatedBy(jawRot) * 5f * (1f - slamLerp) + Main.rand.NextVector2Circular(14f, 14f) * ((time >= (float)jawSlamTime) ? Utils.GetLerpValue(jawSlamTime + 30, jawSlamTime, time, clamped: true) : 0f);
				float rotation = base.Projectile.velocity.ToRotation() + jawRot + (isTopJaw ? ((float)Math.PI) : 0f);
				float toothOpacity = Math.Min((float)Math.Pow(Utils.GetLerpValue(0f, toothNum, time, clamped: true), 3.0), base.Projectile.Opacity);
				Main.EntitySpriteDraw(tex, finalDrawPos - Main.screenPosition, null, Color.Lerp(drawColor, Color.White, toothOpacity) * toothOpacity, rotation, new Vector2((float)(tex.Width / 2), (float)tex.Height), new Vector2(1f, 1f) * scale, spriteFx);
				toothNum++;
				if (!spawnedDust && toothNum > 1)
				{
					int type = ((!ChildSafety.Disabled) ? 16 : 5);
					Vector2? velocity = rotation.ToRotationVector2().RotatedBy(1.5707963705062866).RotatedByRandom(0.5)
						.RotatedBy(1.2f * (float)((!isTopJaw) ? 1 : (-1))) * Main.rand.NextFloat(5f, 8f);
					Color newColor = default(Color);
					Dust dust = Dust.NewDustPerfect(finalDrawPos, type, velocity, 100, newColor, Main.rand.NextFloat(1.1f, 1.9f) * scale);
					dust.noGravity = true;
					dust.alpha = (int)(255f * (1f - toothOpacity));
					GeneralParticleHandler.SpawnParticle(new CustomSpark(finalDrawPos, rotation.ToRotationVector2().RotatedBy(1.5707963705062866).RotatedByRandom(0.4000000059604645)
						.RotatedBy(1.2f * (float)((!isTopJaw) ? 1 : (-1))) * Main.rand.NextFloat(5f, 8f), "CalamityMod/Particles/LargeBloom", affectedByGravity: false, 8, Main.rand.NextFloat(0.065f, 0.08f) * scale, ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Lerp(Color.DarkRed, Color.Black, Main.rand.NextFloat(0f, 0.5f))) * 0.7f * toothOpacity, new Vector2(1f, 1f), useAddativeBlend: false, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.9f));
					newColor = ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Lerp(Color.Red, Color.White, 0.85f));
					Lighting.AddLight(finalDrawPos, ((Color)(ref newColor)).ToVector3() * 1.2f * toothOpacity);
				}
			}
			isTopJaw = t == -1;
		}
		spawnedDust = true;
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (!canDamage)
		{
			return false;
		}
		Vector2 start = base.Projectile.Center;
		float length = jawLength;
		float size = 165f;
		float _ = float.NaN;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, start + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * length, size, ref _);
	}
}
