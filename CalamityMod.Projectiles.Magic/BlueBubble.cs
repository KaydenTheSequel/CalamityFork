using System;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class BlueBubble : ModProjectile, ILocalizedModType, IModType
{
	public Color mainColor;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.alpha = 255;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 120;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 90 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.99f;
		base.Projectile.scale += 0.005f;
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 30;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		Vector2 v = base.Projectile.ai[0].ToRotationVector2();
		float projRotate = base.Projectile.velocity.ToRotation();
		double projAngle = v.ToRotation() - projRotate;
		if (projAngle > 3.1415927410125732)
		{
			projAngle -= 6.2831854820251465;
		}
		if (projAngle < -3.1415927410125732)
		{
			projAngle += 6.2831854820251465;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		if (base.Projectile.timeLeft < 90)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 400f, 8f, 20f);
			if (Main.rand.NextBool(10))
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(5f, 5f) - base.Projectile.velocity, 66);
				dust.scale = Main.rand.NextFloat(0.7f, 0.85f);
				dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.5f);
				dust.color = (Main.rand.NextBool() ? Color.AliceBlue : Color.SkyBlue);
				dust.noGravity = true;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/BlueBubble", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color val = Color.Lerp(mainColor, Color.White, 0.2f);
		((Color)(ref val)).A = 0;
		Color drawColor = val;
		float drawRotation = base.Projectile.rotation;
		Vector2 rotationPoint = texture.Size() * 0.5f;
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], drawColor, 1, texture, drawCentered: true, shrink: true);
		Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in Effervescence.PopSound, base.Projectile.Center);
		GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Vector2.Zero, Color.SkyBlue, Color.DeepSkyBlue, Main.rand.NextFloat(1f, 1.1f), 30, 0.1f, 3f));
		int num190 = Main.rand.Next(5, 9);
		for (int i = 0; i < num190; i++)
		{
			int bubbly = Dust.NewDust(base.Projectile.Center, 0, 0, 206, 0f, 0f, 100, default(Color), 1.4f);
			Dust obj = Main.dust[bubbly];
			obj.velocity *= 0.8f;
			Main.dust[bubbly].position = Vector2.Lerp(Main.dust[bubbly].position, base.Projectile.Center, 0.5f);
			Main.dust[bubbly].noGravity = true;
		}
	}
}
