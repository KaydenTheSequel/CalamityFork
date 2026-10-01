using System;
using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class WarbannerLight : ModProjectile, ILocalizedModType, IModType
{
	public Color bColor;

	public int time;

	public float rotMult;

	public int direction;

	public float rot2;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer moddedOwner => Owner.Calamity();

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 1);
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		float rate = Main.GlobalTimeWrappedHourly * 5f;
		List<Color> eColors = new List<Color>
		{
			Color.Goldenrod,
			Color.Orange
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		bColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		base.Projectile.Center = Owner.MountedCenter;
		if (moddedOwner.warbannerGlow && moddedOwner.WarbanneroftheRighteous)
		{
			base.Projectile.timeLeft++;
		}
		else
		{
			base.Projectile.Kill();
		}
		if (Owner.dead)
		{
			base.Projectile.Kill();
		}
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Goldenrod;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 1.5f);
		rot2 = Math.Abs((float)Math.Sin((float)time * 0.15f / (float)Math.PI) * 0.2f) + 0.8f;
		if (time % 2 == 0)
		{
			Vector2 dustVel = Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0);
			Vector2 position = Owner.Center + dustVel;
			int type = ModContent.DustType<LightDust>();
			Vector2? velocity = dustVel * Main.rand.NextFloat(0.1f, 0.4f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.5f, 0.7f);
			dust.color = bColor;
			dust.noLightEmittence = true;
		}
		time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D rTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/ShatteredExplosion", (AssetRequestMode)2).Value;
		Texture2D bTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Color drawColor = bColor;
		Color color;
		for (int i = 0; i < 5; i++)
		{
			float bScale2 = 0.75f;
			Vector2 position = Owner.Center - Main.screenPosition;
			color = Color.Lerp(drawColor, Color.White, (float)i * 0.15f);
			((Color)(ref color)).A = 0;
			Main.EntitySpriteDraw(bTexture, position, null, color, 0f, bTexture.Size() * 0.5f, (bScale2 - (float)i * 0.15f) * rot2 * base.Projectile.scale, (SpriteEffects)0);
		}
		Vector2 position2 = Owner.Center - Main.screenPosition;
		color = drawColor;
		((Color)(ref color)).A = 0;
		Main.EntitySpriteDraw(rTexture, position2, null, color, Main.rand.NextFloat(-2f, 2f), rTexture.Size() * 0.5f, 0.03f * rot2 * base.Projectile.scale, (SpriteEffects)0);
		Vector2 position3 = Owner.Center - Main.screenPosition;
		color = drawColor;
		((Color)(ref color)).A = 0;
		Main.EntitySpriteDraw(rTexture, position3, null, color, Main.rand.NextFloat(-2f, 2f), rTexture.Size() * 0.5f, 0.04f * rot2 * base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public WarbannerLight()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		bColor = Color.White;
		rotMult = 0.05f;
		direction = 1;
		base._002Ector();
	}
}
