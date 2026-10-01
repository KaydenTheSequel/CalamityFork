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

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class RespiteblockHoldout : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 108;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 4;
		base.Projectile.noEnchantmentVisuals = true;
		base.Projectile.ArmorPenetration = 15;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(origin: frame.Size() * 0.5f, texture: value, position: drawPosition, sourceRectangle: frame, color: base.Projectile.GetAlpha(lightColor), rotation: base.Projectile.rotation, scale: base.Projectile.scale, effects: direction);
		return false;
	}

	public override void AI()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.damage = ((Owner.HeldItem != null) ? Owner.GetWeaponDamage(Owner.HeldItem) : 0);
		PlayChainsawSounds();
		Vector2 playerRotatedPosition = Owner.RotatedRelativePoint(Owner.MountedCenter);
		if (Main.myPlayer == base.Projectile.owner)
		{
			if (!Owner.CantUseHoldout() && Owner.active && !Owner.dead)
			{
				HandleChannelMovement(playerRotatedPosition);
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.White;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		for (int i = 0; i <= 3; i++)
		{
			Vector2 position = base.Projectile.Center + base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(45f * (float)base.Projectile.direction)) * 10f + Main.rand.NextVector2Circular(13f, 13f);
			int type = (Main.rand.NextBool() ? 89 : 86);
			Vector2? velocity = (base.Projectile.velocity * 30f).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.05f, 0.9f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor, Main.rand.NextFloat(0.4f, 0.95f));
			dust.noGravity = true;
			dust.noLight = true;
			dust.noLightEmittence = true;
			dust.alpha = 90;
		}
		if (Time % 4f == 0f)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(45f * (float)base.Projectile.direction)) * 10f + Main.rand.NextVector2Circular(13f, 13f) + base.Projectile.velocity * (float)Main.rand.Next(10, 21), base.Projectile.velocity.RotatedBy(Main.rand.NextFloat(-0.01f, -0.25f) * (float)base.Projectile.direction) * 4f, ModContent.ProjectileType<RespiteblockBlood>(), (int)((float)base.Projectile.damage * 0.4f), base.Projectile.knockBack, base.Projectile.owner);
		}
		DetermineVisuals(playerRotatedPosition);
		ManipulatePlayerValues();
		base.Projectile.timeLeft = 2;
		Time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 500);
		Player player = Main.player[base.Projectile.owner];
		_ = base.Projectile.Center + base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(45f * (float)base.Projectile.direction)) * 10f + Main.rand.NextVector2Circular(13f, 13f);
		for (int i = 0; i <= 3; i++)
		{
			Vector2 val = base.Projectile.Center + base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(45f * (float)base.Projectile.direction)) * 10f + Main.rand.NextVector2Circular(13f, 13f);
			GeneralParticleHandler.SpawnParticle(new PointParticle(val, (base.Projectile.velocity * 30f).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.2f, 0.9f) + new Vector2(0f, Main.rand.NextFloat(-7f, -1f)), affectedByGravity: true, 25, Main.rand.NextFloat(0.7f, 1.2f), (Main.rand.NextBool() ? Color.Green : Color.Purple) * 0.5f, AddativeBlend: false));
			Dust dust = Dust.NewDustPerfect(val, 263, (base.Projectile.velocity * 50f).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.05f, 0.9f), 0, default(Color), Main.rand.NextFloat(0.6f, 0.95f));
			dust.noGravity = true;
			dust.color = Color.White;
		}
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/WulfrumKnifeTileHit", 2);
		style.Volume = 0.55f;
		style.Pitch = 0.3f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		player.DoLifestealDirect(target, (!Main.rand.NextBool(4)) ? 1 : 2, 0.75f);
	}

	public void PlayChainsawSounds()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.soundDelay <= 0)
		{
			SoundStyle style = SoundID.Item22 with
			{
				Pitch = 0.3f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.soundDelay = 6;
		}
	}

	public void DetermineVisuals(Vector2 playerRotatedPosition)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		float directionAngle = base.Projectile.velocity.ToRotation();
		base.Projectile.rotation = directionAngle;
		int oldDirection = base.Projectile.spriteDirection;
		if (oldDirection == -1)
		{
			base.Projectile.rotation += (float)Math.PI;
		}
		base.Projectile.direction = (base.Projectile.spriteDirection = (Math.Cos(directionAngle) > 0.0).ToDirectionInt());
		if (base.Projectile.spriteDirection != oldDirection)
		{
			base.Projectile.rotation -= (float)Math.PI;
		}
		base.Projectile.position = playerRotatedPosition - base.Projectile.Size * 0.5f + directionAngle.ToRotationVector2().RotatedBy(MathHelper.ToRadians(-45f * (float)base.Projectile.direction)) * 10f + directionAngle.ToRotationVector2() * 30f;
		Projectile projectile = base.Projectile;
		projectile.position += Main.rand.NextVector2Circular(1.4f, 1.4f);
		base.Projectile.frameCounter += 33;
		if (base.Projectile.frameCounter >= 32)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % 4;
			base.Projectile.frameCounter = 0;
		}
	}

	public void HandleChannelMovement(Vector2 playerRotatedPosition)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		Vector2 idealAimDirection = (Main.MouseWorld - playerRotatedPosition).SafeNormalize(Vector2.UnitX * (float)Owner.direction);
		float angularAimVelocity = 0.15f;
		float directionAngularDisparity = base.Projectile.velocity.AngleBetween(idealAimDirection) / (float)Math.PI;
		angularAimVelocity += MathHelper.Lerp(0f, 0.25f, Utils.GetLerpValue(0.28f, 0.08f, directionAngularDisparity, clamped: true));
		if (directionAngularDisparity > 0.02f)
		{
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, idealAimDirection, angularAimVelocity);
		}
		else
		{
			base.Projectile.velocity = idealAimDirection;
		}
		base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX * (float)Owner.direction);
	}

	public void ManipulatePlayerValues()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Owner.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.ChangeDir(base.Projectile.direction);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		float _ = 0f;
		float width = base.Projectile.scale * 36f;
		Vector2 start = base.Projectile.Center;
		Vector2 end = base.Projectile.Center + base.Projectile.velocity * 60f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, width, ref _);
	}
}
