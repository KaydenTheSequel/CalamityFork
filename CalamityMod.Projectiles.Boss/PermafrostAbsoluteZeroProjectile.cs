using System;
using CalamityMod.NPCs.SupremeCalamitas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class PermafrostAbsoluteZeroProjectile : ModProjectile, ILocalizedModType, IModType
{
	public const float ZeroChargeDamageRatio = 0.36f;

	public const float ToothDamageRatio = 0.1666667f;

	public const int ToothShootRate = 5;

	public const int ChargeUpTime = 150;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/AbsoluteZero";

	public NPC Permafrost
	{
		get
		{
			if (!Main.npc.IndexInRange((int)base.Projectile.ai[2]))
			{
				return null;
			}
			return Main.npc[(int)base.Projectile.ai[2]];
		}
	}

	public ref float Time => ref base.Projectile.ai[0];

	public ref float ToothDamage => ref base.Projectile.ai[1];

	public float ChargeUpPower => MathHelper.Clamp((float)Math.Pow(Time / 150f, 1.6), 0f, 1f);

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 132;
		base.Projectile.height = 56;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.CooldownSlot = 1;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(origin: value.Size() * 0.5f, position: base.Projectile.Center - Main.screenPosition, effects: (SpriteEffects)(base.Projectile.spriteDirection != 1), texture: value, sourceRectangle: null, color: base.Projectile.GetAlpha(lightColor), rotation: base.Projectile.rotation, scale: base.Projectile.scale);
		return false;
	}

	public override void AI()
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		if (Permafrost == null || !Permafrost.active)
		{
			base.Projectile.Kill();
			return;
		}
		int permafrostBulletHellCounter = Permafrost.ModNPC<SupremeCalamitas>().bulletHellCounter2;
		if ((permafrostBulletHellCounter <= 1800 || permafrostBulletHellCounter >= 2700) && (permafrostBulletHellCounter <= 3600 || permafrostBulletHellCounter >= 4500))
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.damage = 3725;
		DetermineDamage();
		PlayChainsawSounds();
		Vector2 permafrostRotatedPosition = Permafrost.Center;
		float rotation = Permafrost.rotation;
		Vector2 vector = Permafrost.Bottom + new Vector2(0f, Permafrost.gfxOffY);
		Vector2 vector2 = new Vector2(0f, -4f) + Utils.RotatedBy(new Vector2(0f, 4f), (double)rotation, default(Vector2));
		permafrostRotatedPosition.Y += Permafrost.gfxOffY;
		permafrostRotatedPosition = vector + (permafrostRotatedPosition - vector).RotatedBy(rotation) + vector2;
		HandleMovement(permafrostRotatedPosition);
		DetermineVisuals(permafrostRotatedPosition);
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
		if (ToothDamage == 0f)
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

	public void DetermineVisuals(Vector2 permafrostRotatedPosition)
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
		base.Projectile.position = permafrostRotatedPosition - base.Projectile.Size * 0.5f + directionAngle.ToRotationVector2() * 30f;
		Projectile projectile = base.Projectile;
		projectile.position += Main.rand.NextVector2Circular(1.4f, 1.4f);
		base.Projectile.frameCounter += (int)MathHelper.SmoothStep(12f, 33f, ChargeUpPower);
		if (base.Projectile.frameCounter >= 32)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % 6;
			base.Projectile.frameCounter = 0;
		}
	}

	public void HandleMovement(Vector2 permafrostRotatedPosition)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		Vector2 idealAimDirection = (Main.player[Permafrost.target].Center - permafrostRotatedPosition).SafeNormalize(Vector2.UnitX * (float)Permafrost.direction);
		float angularAimVelocity = 0.03f;
		float directionAngularDisparity = base.Projectile.velocity.AngleBetween(idealAimDirection) / (float)Math.PI;
		angularAimVelocity += MathHelper.Lerp(0f, 0.05f, Utils.GetLerpValue(0.28f, 0.08f, directionAngularDisparity, clamped: true));
		if (directionAngularDisparity > 0.02f)
		{
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, idealAimDirection, angularAimVelocity);
		}
		else
		{
			base.Projectile.velocity = idealAimDirection;
		}
		base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX * (float)Permafrost.direction);
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
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity * 35f + Main.rand.NextVector2CircularEdge(9f, 35f).RotatedBy(base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f), 261);
				dust.velocity = base.Projectile.velocity * 3f + Main.rand.NextVector2CircularEdge(1.5f, 1.5f);
				dust.noGravity = true;
				dust.color = Color.HotPink;
				dust.scale = Main.rand.NextFloat(0.9f, 1.25f);
			}
		}
	}

	public void ReleasePrismTeeth()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item101, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			float shootReach = MathHelper.SmoothStep((float)base.Projectile.width * 1.8f, (float)base.Projectile.width * 5.3f + 16f, ChargeUpPower);
			float distanceFromTarget = Permafrost.Distance(Main.player[Permafrost.target].Center);
			if (distanceFromTarget < shootReach)
			{
				shootReach = ((!(distanceFromTarget > 40f)) ? 72f : (distanceFromTarget + 32f));
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Permafrost.Center, base.Projectile.velocity, ModContent.ProjectileType<PermafrostColdheartIcicle>(), (int)ToothDamage, 0f, base.Projectile.owner, shootReach, base.Projectile.whoAmI, base.Projectile.ai[2]);
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
}
