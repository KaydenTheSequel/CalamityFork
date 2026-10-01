using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class AscendantAura : ModProjectile, ILocalizedModType, IModType
{
	public float beamWidth = 1.04f;

	public bool beamsize;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer moddedOwner => Owner.Calamity();

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
	}

	public override void SetDefaults()
	{
		base.Projectile.penetrate = -1;
		base.Projectile.width = (base.Projectile.height = 78);
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 240;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		if (beamWidth <= 0.96f)
		{
			beamsize = true;
		}
		if (beamWidth >= 1.04f)
		{
			beamsize = false;
		}
		beamWidth += (beamsize ? 0.015f : (-0.015f));
		base.Projectile.scale = beamWidth;
		if (base.Projectile.timeLeft >= 240)
		{
			int dustAmount = 200;
			for (int d = 0; d < dustAmount; d++)
			{
				Vector2 velocity = ((float)Math.PI * 2f / (float)dustAmount * (float)d).ToRotationVector2() * Main.rand.NextFloat(5f, 40f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 204, velocity);
				dust.noGravity = true;
				dust.scale = ((Vector2)(ref velocity)).Length() * 0.05f;
				dust.velocity *= 0.4f;
			}
		}
		base.Projectile.Center = Owner.MountedCenter + new Vector2(0f, -45f);
		_ = base.Projectile.Center + Main.rand.NextVector2Circular(5f, 5f);
		Main.rand.Next(3, 6);
		Main.rand.NextFloat(0.5f, 0.9f);
		if (!Main.rand.NextBool(3))
		{
			_ = Color.Khaki;
		}
		else
		{
			_ = Color.LightGreen;
		}
		Vector3 Light = default(Vector3);
		((Vector3)(ref Light))._002Ector(0.251f, 0.255f, 0.219f);
		Lighting.AddLight(base.Projectile.Center, Light * 5f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		float numberOfDusts = 40f;
		float rotFactor = 360f / numberOfDusts;
		for (int i = 0; (float)i < numberOfDusts; i++)
		{
			float rot = MathHelper.ToRadians((float)i * rotFactor);
			Vector2 offset = Utils.RotatedBy(new Vector2(8f, 0f), (double)rot, default(Vector2));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(10.5f, 0f), (double)rot, default(Vector2));
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, 278, (Vector2?)new Vector2(velOffset.X, velOffset.Y), 0, default(Color), 1f);
			dust.noGravity = true;
			dust.velocity = velOffset * Main.rand.NextFloat(0.2f, 1.1f);
			dust.scale = Main.rand.NextFloat(0.3f, 0.8f);
			dust.color = (Main.rand.NextBool(3) ? Color.LightGreen : Color.Khaki);
		}
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/AscendantActivate");
		style.Volume = 0.3f;
		style.Pitch = -0.9f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D rTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/AscendantAura", (AssetRequestMode)2).Value;
		Texture2D bTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSpark", (AssetRequestMode)2).Value;
		Color drawColor = Color.Lerp(Color.Khaki, Color.LightGreen, Utils.GetLerpValue(240f, -100f, base.Projectile.timeLeft, clamped: true));
		Vector2 bScale = new Vector2(Main.rand.NextFloat(0.15f, 0.2f), 0.5f) * base.Projectile.scale * 0.04f * Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true) * Main.rand.NextFloat(0.7f, 1.3f);
		Vector2 position = base.Projectile.Center - Main.screenPosition + new Vector2(-30f * Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true), 0f) * Main.rand.NextFloat(0.6f, 1.3f);
		Color val = drawColor;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(bTexture, position, null, val * Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true), MathHelper.ToRadians(90f), bTexture.Size() * 0.5f, bScale, (SpriteEffects)0);
		Vector2 position2 = base.Projectile.Center - Main.screenPosition + new Vector2(30f * Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true), 0f) * Main.rand.NextFloat(0.6f, 1.3f);
		val = drawColor;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(bTexture, position2, null, val * Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true), MathHelper.ToRadians(-90f), bTexture.Size() * 0.5f, bScale, (SpriteEffects)0);
		Vector2 position3 = base.Projectile.Center - Main.screenPosition + new Vector2(0f, -30f * Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true)) * Main.rand.NextFloat(0.6f, 1.3f);
		val = drawColor;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(bTexture, position3, null, val * Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true), 0f, bTexture.Size() * 0.5f, bScale, (SpriteEffects)0);
		Vector2 position4 = base.Projectile.Center - Main.screenPosition + new Vector2(0f, 30f * Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true)) * Main.rand.NextFloat(0.6f, 1.3f);
		val = drawColor;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(bTexture, position4, null, val * Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true), MathHelper.ToRadians(180f), bTexture.Size() * 0.5f, bScale, (SpriteEffects)0);
		for (int i = 0; i < 4; i++)
		{
			Vector2 bScale2 = new Vector2(0.1f, 0.8f) * base.Projectile.scale * 0.04f * Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true) * Main.rand.NextFloat(0.7f, 1.3f);
			Vector2 bVel = Utils.RotatedBy(new Vector2(20f * Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true), 0f), (double)MathHelper.ToRadians(45f + (float)(i * 90)), default(Vector2)) * Main.rand.NextFloat(0.6f, 1.3f);
			Vector2 position5 = base.Projectile.Center - Main.screenPosition + bVel;
			val = drawColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(bTexture, position5, null, val * Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true), bVel.ToRotation() + MathHelper.ToRadians(90f), bTexture.Size() * 0.5f, bScale2, (SpriteEffects)0);
		}
		Vector2 position6 = base.Projectile.Center - Main.screenPosition;
		val = drawColor;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(rTexture, position6, null, val * Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true), base.Projectile.rotation, rTexture.Size() * 0.5f, base.Projectile.scale * 0.4f * Utils.GetLerpValue(0f, 20f, base.Projectile.timeLeft, clamped: true), (SpriteEffects)0);
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
