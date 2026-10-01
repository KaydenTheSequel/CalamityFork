using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class PulseTurretShot : ModProjectile, ILocalizedModType, IModType
{
	public const int SpiralPrecision = 36;

	public const int SpiralRings = 6;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.SentryShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0] += MathHelper.ToRadians(6f);
		if (!Main.dedServ)
		{
			for (int i = 0; i < 3; i++)
			{
				float f = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
				float pulse = (float)Math.Sin(base.Projectile.ai[0] + (float)Math.PI * 2f / 3f * (float)i);
				Vector2 offset = f.ToRotationVector2().RotatedBy((float)Math.PI * 2f / 3f * (float)i) * pulse * 7f;
				Dust.NewDustPerfect(base.Projectile.Center + offset, 234, Vector2.Zero).noGravity = true;
				Dust.NewDustPerfect(base.Projectile.Center - offset, 234, Vector2.Zero).noGravity = true;
			}
		}
		if (base.Projectile.ai[1] == 1f)
		{
			NPC potentialTarget = base.Projectile.Center.MinionHoming(850f, Main.player[base.Projectile.owner], ignoreTiles: false);
			if (potentialTarget != null)
			{
				float speed = ((Vector2)(ref base.Projectile.velocity)).Length();
				float inertia = MathHelper.Lerp(18f, 6f, 1f - (float)base.Projectile.timeLeft / 300f);
				base.Projectile.velocity = (base.Projectile.velocity * inertia + base.Projectile.SafeDirectionTo(potentialTarget.Center) * speed) / (inertia + 1f);
				base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * speed;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		for (int i = 0; i < 36; i++)
		{
			for (int direction = -1; direction <= 1; direction += 2)
			{
				for (int j = 0; j < 6; j++)
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Vector2.UnitY.RotatedBy((float)j / 6f * ((float)Math.PI * 2f) * (float)direction).RotatedBy((float)i / 36f * ((float)Math.PI * 2f) / 6f * (float)direction) * 36f * (float)i / 36f, 173);
					dust.velocity = base.Projectile.SafeDirectionTo(dust.position) * 2.4f;
					dust.scale = 1.6f;
					dust.noGravity = true;
					dust = Dust.NewDustPerfect(base.Projectile.Center + Vector2.UnitY.RotatedBy((float)j / 6f * ((float)Math.PI * 2f) * (float)direction).RotatedBy((float)i / 36f * ((float)Math.PI * 2f) / 6f * (float)direction) * 36f * (float)i / 36f, 173);
					dust.velocity = base.Projectile.DirectionFrom(dust.position) * 4f;
					dust.scale = 1.6f;
					dust.noGravity = true;
				}
			}
		}
	}
}
