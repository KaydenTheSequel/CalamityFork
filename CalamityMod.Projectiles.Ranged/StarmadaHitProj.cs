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

public class StarmadaHitProj : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public Color c1;

	public Color c2;

	public Color c3;

	public Color shiftColor;

	public bool move;

	public NPC targeted;

	public Vector2 targetPos;

	public Vector2 distFromPos;

	public float squash;

	public bool setLaunchStats;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public bool launched
	{
		get
		{
			if (base.Projectile.localAI[2] == 5f)
			{
				return move;
			}
			return false;
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 25;
		base.Projectile.height = 25;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 3;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 800;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.tileCollide = false;
	}

	public void FindTarget()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] != -5f)
		{
			targeted = Main.npc[(int)base.Projectile.ai[0]];
		}
		if (targeted == null || !targeted.CanBeChasedBy(base.Projectile) || !targeted.active)
		{
			targeted = base.Projectile.Center.ClosestNPCAt(800f);
			base.Projectile.ai[0] = -5f;
		}
	}

	public override void AI()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			targetPos = base.Projectile.Center;
		}
		float rate = base.Projectile.ai[2] * 0.05f;
		List<Color> eColors = new List<Color> { c1, c2, c3 };
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		shiftColor = Color.Lerp(currentColor, nextColor, (rate % 2f >= 1f) ? 1f : (rate % 1f));
		base.Projectile.ai[2]++;
		float startTime = 120f + base.Projectile.ai[1];
		float endTime = startTime + 15f + base.Projectile.ai[1];
		if (launched)
		{
			if (setLaunchStats)
			{
				base.Projectile.extraUpdates = 12;
				base.Projectile.penetrate = 1;
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					base.Projectile.localNPCImmunity[i] = 0;
				}
				setLaunchStats = false;
			}
			base.Projectile.rotation += (float)Math.PI * 2f / (endTime / 3f);
			if (time % 2 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 28f, base.Projectile.velocity, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 13, 0.11f, shiftColor * 0.6f, new Vector2(launched ? 5.5f : 0.8f, 3f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, launched ? 1f : 0.5f));
			}
		}
		else if (move)
		{
			squash = MathHelper.Lerp(squash, ((float)time < endTime) ? 0.15f : 1f, 0.08f);
			if ((float)time == endTime)
			{
				base.Projectile.localAI[2] = 1f;
			}
			if ((float)time >= endTime)
			{
				if (Main.rand.NextBool(15))
				{
					int dustStyle = ModContent.DustType<SquashDust>();
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(8f, 8f), dustStyle);
					dust.scale = Main.rand.NextFloat(1.2f, 1.8f);
					dust.velocity = Vector2.One.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(5f, 7f);
					dust.noGravity = true;
					dust.color = shiftColor;
					dust.fadeIn = 2f;
				}
				Projectile projectile = base.Projectile;
				projectile.Center += base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 0.2f;
				if ((float)time < endTime * 2f)
				{
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 0.9f + base.Projectile.ai[1] * 0.005f;
				}
				base.Projectile.rotation += (float)Math.PI * 2f / (endTime / 3f);
			}
			else
			{
				base.Projectile.rotation = base.Projectile.rotation.AngleLerp(base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, 0.1f);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 28f, base.Projectile.velocity, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 15, 0.3f, shiftColor, new Vector2(0.8f, 1.3f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.5f));
			}
		}
		else
		{
			base.Projectile.rotation += (float)Math.PI * 2f / (startTime / 3f);
			FindTarget();
			if (targeted != null)
			{
				targetPos = targeted.Center;
			}
			if (distFromPos != Vector2.Zero)
			{
				base.Projectile.Center = targetPos + distFromPos;
			}
			else if (targeted != null)
			{
				distFromPos = base.Projectile.Center - targeted.Center;
			}
			distFromPos += -base.Projectile.velocity * 3.5f;
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 0.97f + base.Projectile.ai[1] * 0.0025f;
		}
		if ((float)time == startTime && setLaunchStats)
		{
			base.Projectile.timeLeft = (int)(endTime * 3f);
			move = true;
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 15f;
		}
		base.Projectile.scale = (float)Math.Pow(Math.Min(Utils.GetLerpValue(0f, 40f, time, clamped: true), Utils.GetLerpValue(0f, 40f, base.Projectile.timeLeft, clamped: true)), 3.0);
		time++;
	}

	public override bool ShouldUpdatePosition()
	{
		return move;
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
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		Texture2D star = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/SimpleStar", (AssetRequestMode)2).Value;
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmear", (AssetRequestMode)2).Value;
		_ = ModContent.Request<Texture2D>("CalamityMod/Particles/FadeStreak", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		base.Projectile.GetAlpha(lightColor);
		_ = base.Projectile.rotation;
		_ = base.Projectile.spriteDirection;
		_ = -1;
		Vector2 rotationPoint = star.Size() * 0.5f;
		_ = (float)base.Projectile.spriteDirection * Owner.gravDir;
		_ = -1f;
		Color white = shiftColor;
		((Color)(ref white)).A = 0;
		Main.EntitySpriteDraw(tex, drawPosition, null, white * Math.Max((float)Math.Pow(squash, 5.0) - 0.15f, 0f), base.Projectile.rotation * 1.6f, tex.Size() * 0.5f, base.Projectile.scale * Owner.gravDir * 0.39f, (SpriteEffects)0);
		for (int i = 0; i < 2; i++)
		{
			Color color;
			if (i != 0)
			{
				white = shiftColor;
				((Color)(ref white)).A = 0;
				color = white * 0.6f;
			}
			else
			{
				white = Color.White;
				((Color)(ref white)).A = 0;
				color = white * 0.3f;
			}
			Main.EntitySpriteDraw(star, drawPosition, null, color, base.Projectile.rotation, rotationPoint, new Vector2(1f * squash, 1f + (1f - squash)) * base.Projectile.scale * Owner.gravDir * ((i == 0) ? 0.12f : 0.2f), (SpriteEffects)0);
		}
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.1f;
		int hitsToMinMult = 2;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult * (float)((!launched) ? 1 : 2);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		if (!launched)
		{
			return;
		}
		float rot = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
		for (int b = 1; b <= 6; b++)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustStyle = ModContent.DustType<SquashDust>();
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustStyle);
				dust.scale = 8 - b;
				dust.velocity = -Vector2.UnitY.RotatedBy((float)Math.PI * 2f / 5f * (float)i + rot) * ((float)b * 1.75f + 2f);
				dust.noGravity = true;
				dust.color = GetRandomColor();
				dust.fadeIn = 6.5f - (float)b * 0.5f;
			}
		}
		GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 25, 1.2f, shiftColor, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0f, 1f, 0.85f));
	}

	public StarmadaHitProj()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		c1 = new Color(164, 47, 160);
		c2 = new Color(227, 97, 72);
		c3 = new Color(193, 255, 146);
		squash = 1f;
		setLaunchStats = true;
		base._002Ector();
	}
}
