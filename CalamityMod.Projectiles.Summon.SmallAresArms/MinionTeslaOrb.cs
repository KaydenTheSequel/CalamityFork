using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.SmallAresArms;

public class MinionTeslaOrb : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float Identity => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Projectiles/Boss/AresTeslaOrb";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 420;
		base.Projectile.Opacity = 0f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 7;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 21f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.012f;
		}
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 4f, Time, clamped: true) * Utils.GetLerpValue(0f, 15f, base.Projectile.timeLeft, clamped: true);
		Lighting.AddLight(base.Projectile.Center, 0.1f * base.Projectile.Opacity, 0.25f * base.Projectile.Opacity, 0.25f * base.Projectile.Opacity);
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % Main.projFrames[base.Type];
		Time++;
	}

	public Projectile GetOrbToAttachTo()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == base.Projectile.type && p.ai[0] == Identity + 1f && p.WithinRange(base.Projectile.Center, 1500f))
			{
				return p;
			}
		}
		return null;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.Lerp(0.75f, 1.85f, (float)Math.Sin((float)Math.PI * completionRatio)) * base.Projectile.scale;
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		float fadeToWhite = MathHelper.Lerp(0f, 0.65f, (float)Math.Sin((float)Math.PI * 2f * completionRatio + Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f);
		return Color.Lerp(Color.Lerp(Color.Cyan, Color.White, fadeToWhite), Color.LightBlue, ((float)Math.Sin((float)Math.PI * completionRatio + Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f) * 0.8f) * base.Projectile.Opacity;
	}

	internal float BackgroundWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return WidthFunction(completionRatio, vertexPos) * 4f;
	}

	internal Color BackgroundColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return Color.CornflowerBlue * base.Projectile.Opacity * 0.4f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		Projectile orbToAttachTo = GetOrbToAttachTo();
		if (orbToAttachTo != null)
		{
			List<Vector2> positions = AresTeslaOrb.DetermineElectricArcPoints(base.Projectile.Center, orbToAttachTo.Center, 117);
			PrimitiveRenderer.RenderTrail(positions, new PrimitiveSettings(BackgroundWidthFunction, BackgroundColorFunction, null, smoothen: false), 90);
			PrimitiveRenderer.RenderTrail(positions, new PrimitiveSettings(WidthFunction, ColorFunction, null, smoothen: false), 90);
		}
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).G = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float _ = 0f;
		Projectile orbToAttachTo = GetOrbToAttachTo();
		if (orbToAttachTo != null && Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, orbToAttachTo.Center, 8f, ref _))
		{
			return true;
		}
		return false;
	}
}
