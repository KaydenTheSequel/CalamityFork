using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PrecisionBolt : ModProjectile, ILocalizedModType, IModType
{
	private NPC potentialTarget;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/PrecisionBolt";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 72;
		base.Projectile.height = 72;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 119;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
	}

	private Vector2 Recalibrate()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		float turnAngle = MathHelper.ToRadians((float)Math.Pow(MathHelper.Clamp((float)(base.Projectile.timeLeft - 40), 0f, 120f) / 120f, 4.0) * 75f);
		Vector2 leftTurnVelocity = base.Projectile.velocity.RotatedBy(0f - turnAngle);
		Vector2 righTurnVelocity = base.Projectile.velocity.RotatedBy(turnAngle);
		float num = leftTurnVelocity.AngleBetween(base.Projectile.SafeDirectionTo(potentialTarget.Center));
		float rightDirectionImprecision = righTurnVelocity.AngleBetween(base.Projectile.SafeDirectionTo(potentialTarget.Center));
		potentialTarget = base.Projectile.Center.ClosestNPCAt(512f);
		if (num < rightDirectionImprecision)
		{
			return leftTurnVelocity;
		}
		return righTurnVelocity;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.LightSteelBlue;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (potentialTarget == null)
		{
			potentialTarget = base.Projectile.Center.ClosestNPCAt(512f);
		}
		if (potentialTarget != null)
		{
			float angularTurnSpeed = MathHelper.ToRadians(2.5f);
			float idealDirection = base.Projectile.AngleTo(potentialTarget.Center);
			float updatedDirection = base.Projectile.velocity.ToRotation().AngleTowards(idealDirection, angularTurnSpeed);
			base.Projectile.velocity = updatedDirection.ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length();
			if (base.Projectile.timeLeft % 6 == 0)
			{
				SoundEngine.PlaySound(in SoundID.Item93, base.Projectile.Center);
				base.Projectile.velocity = Recalibrate();
			}
		}
		Vector2 center2 = base.Projectile.Center;
		newColor = default(Color);
		Dust dust = Dust.NewDustPerfect(center2, 267, null, 0, newColor);
		dust.velocity = Vector2.Zero;
		dust.color = Color.Yellow;
		dust.scale = Main.rand.NextFloat(1f, 1.1f);
		dust.noGravity = true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			SoundEngine.PlaySound(in SoundID.Item94, base.Projectile.Center);
			for (int i = 0; i < 10; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
				dust.velocity = base.Projectile.velocity;
				dust.color = Color.Yellow;
				dust.scale = Main.rand.NextFloat(1f, 1.1f);
				dust.noGravity = true;
			}
		}
	}
}
