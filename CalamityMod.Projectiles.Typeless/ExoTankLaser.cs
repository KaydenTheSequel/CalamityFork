using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class ExoTankLaser : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/Summon/AtlasMunitionsLaserOverdrive";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 18);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = 240;
		base.Projectile.Opacity = 0f;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color red = Color.Red;
		Lighting.AddLight(center, ((Color)(ref red)).ToVector3() * base.Projectile.Opacity * 0.5f);
		base.Projectile.Opacity = Utils.GetLerpValue(240f, 235f, base.Projectile.timeLeft, clamped: true);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % Main.projFrames[base.Type];
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 18f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.03f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool())
		{
			int dustCount = 16;
			bool sideways = Main.rand.NextBool(6);
			float generalAngularOffset = Main.rand.NextFloatDirection() * (float)Math.PI / 24f;
			if (sideways)
			{
				dustCount += 36;
			}
			for (int i = 0; i < dustCount; i++)
			{
				float x = (float)Math.PI * 2f * (float)i / (float)dustCount;
				float unitOffsetX = MathF.Pow(MathF.Cos(x), 3f);
				float unitOffsetY = MathF.Pow(MathF.Sin(x), 3f);
				Vector2 puffDustVelocity = new Vector2(unitOffsetX, unitOffsetY) * 5f;
				Dust obj = Dust.NewDustPerfect(Velocity: (!sideways) ? puffDustVelocity.RotatedBy(generalAngularOffset) : (puffDustVelocity.RotatedBy(0.7853981852531433) * 1.7f), Position: target.Center, Type: 182);
				obj.scale = (sideways ? 1.8f : 1f);
				obj.fadeIn = 0.5f;
				obj.noGravity = true;
			}
		}
	}
}
