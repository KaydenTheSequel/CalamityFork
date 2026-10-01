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

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class PhotonRipperProjectile : ModProjectile, ILocalizedModType, IModType
{
	public const float ZeroChargeDamageRatio = 0.36f;

	public const float ToothDamageRatio = 0.1666667f;

	public const int ToothShootRate = 5;

	public const int ChargeUpTime = 150;

	public new string LocalizationCategory => "Projectiles.Melee";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

	public ref float ToothDamage => ref base.Projectile.ai[1];

	public float ChargeUpPower => MathHelper.Clamp((float)Math.Pow(Time / 150f, 1.6), 0f, 1f);

	public override void SetDefaults()
	{
		base.Projectile.width = 132;
		base.Projectile.height = 56;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Texture2D glowmaskTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/PhotonRipperGlowmask", (AssetRequestMode)2).Value;
		Rectangle glowmaskRectangle = glowmaskTexture.Frame(1, 6, 0, base.Projectile.frame);
		Vector2 origin = value.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		Main.EntitySpriteDraw(value, drawPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, direction);
		Main.EntitySpriteDraw(glowmaskTexture, drawPosition, glowmaskRectangle, Color.White, base.Projectile.rotation, origin, base.Projectile.scale, direction);
		return false;
	}

	public override void AI()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.damage = ((Owner.HeldItem != null) ? Owner.GetWeaponDamage(Owner.HeldItem) : 0);
		DetermineDamage();
		PlayChainsawSounds();
		Vector2 playerRotatedPosition = Owner.RotatedRelativePoint(Owner.MountedCenter);
		if (Main.myPlayer == base.Projectile.owner)
		{
			if ((!Owner.CantUseHoldout() && base.Projectile.ai[2] == 1f) || (base.Projectile.ai[2] == 0f && Owner.Calamity().mouseRight && Owner.active && !Owner.dead))
			{
				HandleChannelMovement(playerRotatedPosition);
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		DetermineVisuals(playerRotatedPosition);
		ManipulatePlayerValues();
		EmitPrettyDust();
		if (Time % 5f == 4f)
		{
			ReleasePrismTeeth();
		}
		base.Projectile.timeLeft = 2;
		Time++;
	}

	public void PlayChainsawSounds()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.soundDelay <= 0)
		{
			SoundEngine.PlaySound(in SoundID.Item22, base.Projectile.Center);
			base.Projectile.soundDelay = (int)MathHelper.Lerp(30f, 12f, ChargeUpPower);
		}
	}

	public void DetermineDamage()
	{
		if (Main.myPlayer == base.Projectile.owner && ToothDamage == 0f)
		{
			ToothDamage = 0.1666667f * (float)base.Projectile.damage;
			base.Projectile.netUpdate = true;
		}
		if (ToothDamage != 0f)
		{
			float fullMult = 0.1666667f;
			float zeroMult = 0.060000014f;
			ToothDamage = (int)MathHelper.SmoothStep((float)base.Projectile.damage * zeroMult, (float)base.Projectile.damage * fullMult, ChargeUpPower);
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
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
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
		base.Projectile.position = playerRotatedPosition - base.Projectile.Size * 0.5f + directionAngle.ToRotationVector2() * 30f;
		Projectile projectile = base.Projectile;
		projectile.position += Main.rand.NextVector2Circular(1.4f, 1.4f);
		base.Projectile.frameCounter += (int)MathHelper.SmoothStep(12f, 33f, ChargeUpPower);
		if (base.Projectile.frameCounter >= 32)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % 6;
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

	public void EmitPrettyDust()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity * 35f + Main.rand.NextVector2CircularEdge(9f, 35f).RotatedBy(base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f), 261);
				dust.velocity = base.Projectile.velocity * 3f + Main.rand.NextVector2CircularEdge(1.5f, 1.5f);
				dust.noGravity = true;
				dust.color = Main.hslToRgb((Time / 40f + Main.rand.NextFloat(-0.1f, 0.1f)) % 1f, 0.95f, 0.8f);
				dust.scale = Main.rand.NextFloat(0.9f, 1.25f);
			}
		}
	}

	public void ReleasePrismTeeth()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item101, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			float shootReach = MathHelper.SmoothStep((float)base.Projectile.width * 1.8f, (float)base.Projectile.width * 5.3f + 16f, ChargeUpPower);
			shootReach *= Owner.HeldItem.shootSpeed;
			float distanceFromMouse = Owner.Distance(Main.MouseWorld);
			if (distanceFromMouse < shootReach)
			{
				shootReach = ((!(distanceFromMouse > 40f)) ? 72f : (distanceFromMouse + 32f));
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center, base.Projectile.velocity, ModContent.ProjectileType<PrismTooth>(), (int)ToothDamage, 0f, base.Projectile.owner, shootReach, base.Projectile.whoAmI, base.Projectile.ai[2]);
		}
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
		Vector2 end = base.Projectile.Center + base.Projectile.velocity * 70f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, width, ref _);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 500);
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/WulfrumKnifeTileHit", 2);
		style.Volume = 0.7f;
		style.Pitch = -0.1f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i < 20; i++)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(target.Center, ((Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * -40f).RotatedByRandom(0.55) * Main.rand.NextFloat(0.3f, 1f), affectedByGravity: false, 20, Main.rand.NextFloat(0.3f, 1.2f), Main.hslToRgb((Time / 40f + Main.rand.NextFloat(-0.1f, 0.1f)) % 1f, 0.95f, 0.8f)));
		}
		for (int j = 0; j < 3; j++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, Main.hslToRgb((Time / 40f + Main.rand.NextFloat(-0.1f, 0.1f)) % 1f, 0.95f, 0.8f), "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.4f, 0.9f * Main.rand.NextFloat(0.9f, 1.1f), 12, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
	}
}
