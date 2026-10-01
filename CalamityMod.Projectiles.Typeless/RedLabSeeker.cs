using System;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class RedLabSeeker : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public ref float Time => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Items/LabFinders/RedSeekingMechanism";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 24);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 300;
	}

	public static void Behavior(Projectile projectile, Vector2 destination, Color dustColor, ref float time)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		projectile.ai[1] = 0f;
		if (time < 45f)
		{
			projectile.velocity = Vector2.UnitY * Utils.GetLerpValue(0f, 25f, time, clamped: true) * Utils.GetLerpValue(30f, 25f, time, clamped: true) * -4.5f;
		}
		else if (time < 80f)
		{
			projectile.rotation = (projectile.AngleTo(destination) + (float)Math.PI / 2f) * Utils.GetLerpValue(45f, 70f, time, clamped: true);
		}
		else if (projectile.WithinRange(destination, 420f))
		{
			projectile.ai[1] = 1f;
			projectile.velocity = Vector2.Lerp(projectile.velocity, Vector2.UnitY * (float)Math.Sin((time - 80f) / 24f) * 2.5f, 0.15f);
			if (!projectile.WithinRange(destination, 30f))
			{
				projectile.Center = projectile.Center.MoveTowards(destination, 10f);
			}
			projectile.rotation *= 0.95f;
			projectile.Center = Vector2.Lerp(projectile.Center, destination, 0.1f);
		}
		else
		{
			Dust dust = Dust.NewDustPerfect(projectile.Center + Main.rand.NextVector2Circular(8f, 8f), 267);
			dust.velocity = Main.rand.NextVector2Circular(2f, 2f);
			dust.noGravity = true;
			dust.color = dustColor;
			dust.scale = 1.6f;
			dust.fadeIn = 1.45f;
		}
		if (time > 80f && time < 120f)
		{
			projectile.velocity = (projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * Utils.GetLerpValue(80f, 105f, time, clamped: true) * 25f;
		}
		if (time < 80f)
		{
			projectile.oldPos = (Vector2[])(object)new Vector2[projectile.oldPos.Length];
		}
		Lighting.AddLight(projectile.Center, Vector3.One * 0.8f);
		time++;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		Behavior(base.Projectile, CalamityWorld.HellLabCenter, Color.Red, ref Time);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 8; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(16f, 16f), 267);
			dust.color = Color.Red;
			dust.scale = Main.rand.NextFloat(0.95f, 1.25f);
			dust.velocity = Main.rand.NextVector2Circular(2.5f, 2.5f);
			dust.velocity.Y -= 1.5f;
			dust.fadeIn = 1.2f;
			dust.noGravity = true;
		}
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		if (Time > 80f && base.Projectile.ai[1] == 0f)
		{
			for (int i = 0; i < 24; i += 2)
			{
				Vector2 drawPosition = base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.Zero) * (float)i * Utils.GetLerpValue(80f, 125f, Time, clamped: true) * 25f - Main.screenPosition;
				Main.EntitySpriteDraw(texture, drawPosition, null, base.Projectile.GetAlpha(lightColor) * (1f - (float)i / 24f), base.Projectile.rotation, texture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
			}
		}
		return true;
	}
}
