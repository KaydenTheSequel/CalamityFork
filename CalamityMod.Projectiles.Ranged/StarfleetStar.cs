using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class StarfleetStar : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public Color c1;

	public Color c2;

	public Color c3;

	public Color shiftColor;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 25;
		base.Projectile.height = 25;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 1;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 600;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0 && base.Projectile.ai[1] > 0f)
		{
			base.Projectile.extraUpdates = (int)base.Projectile.ai[1];
		}
		float rate = base.Projectile.ai[2] * 0.05f;
		List<Color> eColors = new List<Color> { c1, c2, c3 };
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		shiftColor = Color.Lerp(currentColor, nextColor, (rate % 2f >= 1f) ? 1f : (rate % 1f));
		base.Projectile.ai[2]++;
		if (time > 5 && time % 2 == 0)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 28f, base.Projectile.velocity, "CalamityMod/Particles/DualTrail", affectedByGravity: false, 15, 0.11f, shiftColor * 0.6f, new Vector2(0.9f, 1.1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.5f));
			if (time > 18)
			{
				int dustStyle = ModContent.DustType<SquashDust>();
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(8f, 8f), dustStyle);
				dust.scale = Main.rand.NextFloat(1.2f, 1.8f);
				dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(3f, 5f);
				dust.noGravity = true;
				dust.color = shiftColor;
				dust.fadeIn = 5f;
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		time++;
	}

	public Color GetRandomColor()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		return (Color)(Main.rand.Next(4) switch
		{
			0 => c1, 
			1 => c2, 
			_ => c3, 
		});
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/LeonidStar", (AssetRequestMode)2).Value;
		Texture2D tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/FadeStreak", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		base.Projectile.GetAlpha(lightColor);
		_ = base.Projectile.rotation;
		_ = base.Projectile.spriteDirection;
		_ = -1;
		_ = value.Size() * 0.5f;
		_ = (float)base.Projectile.spriteDirection * Owner.gravDir;
		_ = -1f;
		for (int i = 0; i < 5; i++)
		{
			Vector2 offset = ((float)Math.PI * 2f * (float)i / 5f).ToRotationVector2().RotatedBy(base.Projectile.rotation + (float)Math.PI);
			Color white;
			for (int t = 0; t < 2; t++)
			{
				white = shiftColor;
				((Color)(ref white)).A = 0;
				Main.EntitySpriteDraw(tex2, drawPosition, null, white * 0.4f, offset.ToRotation(), new Vector2((float)tex2.Width * 0.5f, 0f), new Vector2((2.3f + (float)t * 0.03f) * i switch
				{
					3 => 1.8f, 
					2 => 1.8f, 
					_ => 1f, 
				}, (1.1f + (float)t * 0.03f) * i switch
				{
					3 => 0.75f, 
					2 => 0.75f, 
					_ => 1f, 
				}) * base.Projectile.scale * Owner.gravDir * 0.3f, (SpriteEffects)2);
			}
			white = Color.White;
			((Color)(ref white)).A = 0;
			Main.EntitySpriteDraw(tex2, drawPosition, null, white * 0.4f, offset.ToRotation(), new Vector2((float)tex2.Width * 0.5f, 0f), new Vector2(1.5f, 0.7f) * base.Projectile.scale * Owner.gravDir * 0.3f, (SpriteEffects)2);
		}
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.1f;
		int hitsToMinMult = 2;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 15; i++)
		{
			float variance = Main.rand.NextFloat(-0.5f, 0.5f);
			int dustStyle = ModContent.DustType<SquashDust>();
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustStyle);
			dust.scale = (Main.rand.NextFloat(1.4f, 1.8f) - Math.Abs(variance)) * 1.5f;
			dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(variance) * Main.rand.NextFloat(18f, 19f) * (float)Math.Pow(1f - Math.Abs(variance), 2.0);
			dust.noGravity = true;
			dust.color = GetRandomColor();
			dust.fadeIn = 2.5f;
		}
	}

	public StarfleetStar()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		c1 = new Color(146, 255, 211);
		c2 = new Color(222, 225, 146);
		c3 = new Color(255, 233, 146);
		base._002Ector();
	}
}
