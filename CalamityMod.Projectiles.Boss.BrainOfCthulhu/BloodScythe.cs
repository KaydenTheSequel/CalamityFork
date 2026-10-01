using System;
using System.IO;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss.BrainOfCthulhu;

public class BloodScythe : ModProjectile, ILocalizedModType, IModType
{
	private Vector2 InitialVelocity;

	private Vector2 AcceleratingVelocity;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Particles/VerticalSmearRagged";

	private static float RotationSpeed => (float)Math.PI / 8f;

	private static float Acceleration => 0.175f;

	private static int Lifetime => 240;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 16;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 300;
		base.Projectile.height = 300;
		base.Projectile.penetrate = -1;
		base.Projectile.Opacity = 1f;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.damage = 10;
		base.Projectile.scale = 0.1f;
		base.Projectile.hostile = true;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		InitialVelocity = base.Projectile.velocity;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		for (int i = 0; i < 3; i++)
		{
			GeneralParticleHandler.SpawnParticle(new BloodParticle(base.Projectile.Center, base.Projectile.velocity.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 6f, (float)Math.PI / 6f)) * Main.rand.NextFloat(0.5f, 1f), 32, 1f, Color.Red));
		}
		GeneralParticleHandler.SpawnParticle(new BloodParticle2(base.Projectile.Center, base.Projectile.velocity * 0.75f, 16, 0.5f, Color.Red));
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		int num = Lifetime - base.Projectile.timeLeft;
		InitialVelocity *= 0.925f;
		if (num > 15)
		{
			AcceleratingVelocity += base.Projectile.velocity.SafeNormalize(InitialVelocity.SafeNormalize(Vector2.UnitX)) * Acceleration;
			if (Main.rand.NextBool(1 + base.Projectile.timeLeft / 32))
			{
				GeneralParticleHandler.SpawnParticle(new BloodParticle(base.Projectile.Center + Main.rand.NextVector2CircularEdge(32f, 32f), (-base.Projectile.velocity).RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 6f, (float)Math.PI / 6f)) * Main.rand.NextFloat(0.25f, 0.75f), Main.rand.Next(10, 17), 1f, Color.Red));
			}
		}
		base.Projectile.velocity = InitialVelocity + AcceleratingVelocity;
		base.Projectile.rotation += RotationSpeed;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.WritePackedVector2(InitialVelocity);
		writer.WritePackedVector2(AcceleratingVelocity);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		InitialVelocity = reader.ReadPackedVector2();
		AcceleratingVelocity = reader.ReadPackedVector2();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.SetBlendState(BlendState.Additive);
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition;
		Color drawColor = Color.Red;
		if (!ChildSafety.Disabled)
		{
			drawColor = Main.DiscoColor;
		}
		for (int i = 0; i < ((!CalamityClientConfig.Instance.Afterimages) ? 1 : base.Projectile.oldPos.Length); i++)
		{
			float afterimageRot = base.Projectile.oldRot[i];
			drawPos = base.Projectile.oldPos[i] + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
			if (i != 0)
			{
				drawColor *= 0.9f;
			}
			float interpolant = (float)(base.Projectile.oldPos.Length - i) / (float)base.Projectile.oldPos.Length;
			Main.spriteBatch.Draw(tex, drawPos, (Rectangle?)null, drawColor, afterimageRot, tex.Size() * 0.5f, base.Projectile.scale * interpolant, (SpriteEffects)0, 0f);
		}
		Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		return false;
	}

	public BloodScythe()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		InitialVelocity = Vector2.Zero;
		AcceleratingVelocity = Vector2.Zero;
		base._002Ector();
	}
}
