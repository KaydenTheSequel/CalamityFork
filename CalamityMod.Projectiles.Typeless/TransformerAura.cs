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

public class TransformerAura : ModProjectile, ILocalizedModType, IModType
{
	public Color bColor;

	public int time;

	public float rotMult;

	public int direction;

	public float rot2;

	public float fullSine;

	public Vector2 squareRandomPos1;

	public Vector2 squareRandomPos2;

	public float fullChargeMult;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer moddedOwner => Owner.Calamity();

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 1);
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 10;
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
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		float rate = Main.GlobalTimeWrappedHourly * 5f;
		List<Color> eColors = new List<Color>
		{
			Color.LightSkyBlue,
			Color.DodgerBlue
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		bColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		base.Projectile.Center = Owner.MountedCenter;
		fullChargeMult = MathHelper.Lerp(fullChargeMult, (float)((Owner.ownedProjectileCounts[ModContent.ProjectileType<TransformerBlob>()] < 30) ? 1 : 0), 0.033f);
		if (moddedOwner.transformerVisual && moddedOwner.transformer)
		{
			if (moddedOwner.transformerCooldown == 0)
			{
				base.Projectile.timeLeft = MathHelper.Clamp(base.Projectile.timeLeft + 5, 0, 300);
			}
			else
			{
				base.Projectile.timeLeft = (int)((float)base.Projectile.timeLeft * 0.98f);
			}
		}
		else
		{
			base.Projectile.Kill();
		}
		if (Owner.dead)
		{
			base.Projectile.Kill();
		}
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref bColor)).ToVector3() * 0.9f);
		rot2 = Math.Abs((float)Math.Sin((float)time * 0.15f / (float)Math.PI) * 0.2f) + 0.8f;
		fullSine = (float)Math.Sin((float)time * 0.15f / (float)Math.PI);
		if (time % 2 == 0)
		{
			Vector2 dustVel = Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0);
			Dust dust = Dust.NewDustPerfect(Owner.Center + dustVel, ModContent.DustType<VoidDustInverted>(), dustVel * Main.rand.NextFloat(0.1f, 0.4f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.5f, 0.7f);
			dust.color = new Color(30, 30, 30);
			dust.noLightEmittence = true;
		}
		time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		Texture2D vTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomRingThinLarge", (AssetRequestMode)2).Value;
		Texture2D bTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Texture2D b2Texture = ModContent.Request<Texture2D>("CalamityMod/Particles/Light", (AssetRequestMode)2).Value;
		Color drawColor = bColor;
		float deathLerp = (float)Math.Pow(Utils.GetLerpValue(10f, 300f, base.Projectile.timeLeft), 2.0);
		float bScale2 = 0.95f;
		Vector2 position = Owner.Center - Main.screenPosition;
		Color val = drawColor;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(bTexture, position, null, val * deathLerp, 0f, bTexture.Size() * 0.5f, bScale2 * rot2 * base.Projectile.scale * deathLerp + 2.5f * (fullChargeMult - 1f), (SpriteEffects)0);
		Main.EntitySpriteDraw(b2Texture, Owner.Center - Main.screenPosition, null, new Color(30, 30, 30) * deathLerp, Main.rand.NextFloat(-2f, 2f), b2Texture.Size() * 0.5f, bScale2 * base.Projectile.scale * deathLerp * 1.2f * rot2, (SpriteEffects)0);
		Main.EntitySpriteDraw(b2Texture, Owner.Center - Main.screenPosition, null, Color.Black * deathLerp, Main.rand.NextFloat(-2f, 2f), b2Texture.Size() * 0.5f, bScale2 * base.Projectile.scale * deathLerp * 0.8f * rot2, (SpriteEffects)0);
		for (int i = 0; i < 2; i++)
		{
			_ = rot2;
			float rot = (float)Math.PI * 2f * (float)i / 3f + Main.GlobalTimeWrappedHourly;
			Vector2 position2 = Owner.Center - Main.screenPosition;
			val = drawColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(vTexture, position2, null, val * 0.08f * deathLerp, rot + MathHelper.ToRadians(-105f), vTexture.Size() * 0.5f, new Vector2(1f, 0.96f) * 0.16f * deathLerp * fullChargeMult, (SpriteEffects)0);
			Vector2 position3 = Owner.Center - Main.screenPosition;
			val = drawColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(vTexture, position3, null, val * 0.08f * deathLerp, rot * 2f + MathHelper.ToRadians(-105f), vTexture.Size() * 0.5f, new Vector2(1f, 0.96f) * 0.157f * deathLerp * fullChargeMult, (SpriteEffects)0);
		}
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public TransformerAura()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		bColor = Color.White;
		rotMult = 0.05f;
		direction = 1;
		fullSine = 1f;
		fullChargeMult = 1f;
		base._002Ector();
	}
}
