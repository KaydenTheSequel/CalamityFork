using System;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

[PierceResistException(true)]
public class AtlantisSpear : ModProjectile, ILocalizedModType, IModType
{
	private static int TotalSegments = 16;

	private float damageMultiplier = 1f;

	private int time;

	private float fade = 1f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 52);
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 4;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 8;
		base.Projectile.appliesImmunityTimeOnSingleHits = true;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.alpha -= 100;
			if (base.Projectile.alpha <= 0)
			{
				base.Projectile.alpha = 0;
				base.Projectile.ai[1] = 1f;
				if (base.Projectile.ai[0] == 0f || base.Projectile.ai[0] > (float)TotalSegments)
				{
					base.Projectile.ai[0]++;
					Projectile projectile = base.Projectile;
					projectile.position += base.Projectile.velocity;
				}
				if (Main.myPlayer == base.Projectile.owner && base.Projectile.ai[0] < (float)TotalSegments)
				{
					int nextSegment = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + base.Projectile.velocity, base.Projectile.velocity, base.Projectile.type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, base.Projectile.ai[0] + 1f, 0f, 5f);
					NetMessage.SendData(27, -1, -1, null, nextSegment);
				}
			}
		}
		else
		{
			int AlphaPerFrame = 12;
			base.Projectile.alpha += AlphaPerFrame;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.Kill();
			}
		}
		if (Main.rand.NextBool(7))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity * 0.5f, 278, base.Projectile.velocity.RotatedByRandom(0.8) * Main.rand.NextFloat(0.03f, 0.18f));
			dust.scale = Main.rand.NextFloat(0.3f, 0.5f);
			dust.color = (Main.rand.NextBool() ? Color.CornflowerBlue : Color.LightBlue);
			dust.noGravity = true;
		}
		time++;
		fade = Utils.GetLerpValue(25f, 12f, time, clamped: true);
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, base.Projectile.alpha);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= damageMultiplier;
		damageMultiplier *= 0.8f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[2] == 5f)
		{
			for (int k = 0; k < 2; k++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + base.Projectile.velocity * 0.5f, base.Projectile.velocity.RotatedByRandom(0.8) * Main.rand.NextFloat(0.1f, 0.6f), affectedByGravity: false, 23, Main.rand.NextFloat(0.7f, 1.1f), Color.LightBlue * 0.6f));
				GeneralParticleHandler.SpawnParticle(new PointParticle(base.Projectile.Center + base.Projectile.velocity * 0.5f, base.Projectile.velocity.RotatedByRandom(0.8) * Main.rand.NextFloat(0.1f, 0.6f), affectedByGravity: false, 23, Main.rand.NextFloat(0.7f, 1.1f), Color.LightBlue * 0.6f));
			}
			SoundStyle style = SoundID.DD2_WitherBeastCrystalImpact with
			{
				Volume = 0.3f,
				Pitch = base.Projectile.ai[0] * -0.02f - 0.1f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (!(base.Projectile.ai[0] > (float)TotalSegments) && Main.myPlayer == base.Projectile.owner)
		{
			int numProj = 2;
			float rotation = MathHelper.ToRadians(20f);
			for (int i = 0; i < numProj; i++)
			{
				Vector2 perturbedSpeed = base.Projectile.velocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)(i / (numProj - 1))));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, perturbedSpeed, base.Projectile.type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, (float)TotalSegments + 1f, 0f, i);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		if (time > 0)
		{
			Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor) * fade, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}
}
