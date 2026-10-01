using System;
using System.Collections.Generic;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using CalamityMod.Projectiles.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class PlasmaRifleShot : ModProjectile, ILocalizedModType, IModType
{
	public List<Vector2> TrailPos = new List<Vector2>();

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 5000;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 12);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.MaxUpdates = 10;
		base.Projectile.timeLeft = 30 * base.Projectile.MaxUpdates;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.scale -= 0.02f;
			base.Projectile.Opacity -= 0.02f;
			if (base.Projectile.Opacity <= 0f)
			{
				base.Projectile.Kill();
			}
			return;
		}
		if (base.Projectile.FinalExtraUpdate() || TrailPos.Count < 15)
		{
			TrailPos.Add(base.Projectile.Center);
		}
		if (base.Projectile.timeLeft < 50)
		{
			Explode();
		}
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.numHits != 0)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.numHits == 0)
		{
			Explode();
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (base.Projectile.numHits == 0)
		{
			Explode();
		}
		return false;
	}

	public void Explode()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.numHits++;
		base.Projectile.timeLeft = 50;
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.netUpdate = true;
		TrailPos.Add(base.Projectile.Center);
		if (!(base.Projectile.ai[0] > 0f))
		{
			SoundEngine.PlaySound(in AnomalysNanogunMPFBBoom.MPFBExplosion, base.Projectile.Center);
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<PlasmaRifleExplosion>(), (int)((float)base.Projectile.damage * 0.7f), base.Projectile.knockBack, base.Projectile.owner, 0f, 160f);
			}
			for (int k = 0; k < 30; k++)
			{
				float intensity = Main.rand.NextFloat(0.1f, 1f);
				Color BaseCol = Color.Lerp(Color.Lime, Color.Yellow, intensity);
				Vector2 velocity = Main.rand.NextVector2Unit() * (Main.rand.NextFloat(24f, 25f) - 20f * intensity);
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, velocity, BaseCol, Color.Chartreuse, Main.rand.NextFloat(1f, 1.6f) + 2f * intensity, 210f, Main.rand.NextFloat(0.03f, -0.03f)));
			}
		}
	}

	internal float HeadWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return base.Projectile.scale * ((base.Projectile.ai[0] > 0f) ? 2f : 4f) * Utils.GetLerpValue(0.5f, 0.25f, MathF.Abs(0.5f - completionRatio), clamped: true);
	}

	internal Color HeadColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return Color.LightSlateGray * base.Projectile.Opacity;
	}

	internal float TrailWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return base.Projectile.scale * ((base.Projectile.ai[0] > 0f) ? 2f : 4f);
	}

	internal Color TrailColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.Chartreuse, Color.SlateGray, Utils.Remap(base.Projectile.Opacity, 0.5f, 1f, 0f, 0.8f)) * base.Projectile.Opacity * 0.3f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (TrailPos == null)
		{
			return false;
		}
		PrimitiveRenderer.RenderTrail(TrailPos, new PrimitiveSettings(TrailWidthFunction, TrailColorFunction), 30);
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(HeadWidthFunction, HeadColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}), 12);
		return false;
	}
}
