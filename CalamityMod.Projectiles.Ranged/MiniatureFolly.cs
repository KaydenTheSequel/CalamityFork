using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MiniatureFolly : ModProjectile, ILocalizedModType, IModType
{
	public List<Vector2> TrailPos = new List<Vector2>();

	public const int TrailLength = 12;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public bool SpawnedByFatFuck => base.Projectile.ai[2] == 1f;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Generic;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 180 * base.Projectile.MaxUpdates;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCHit51, base.Projectile.Center);
		for (int i = 0; i < 4; i++)
		{
			Color color = Color.Lerp(Color.Red, Color.Magenta, Main.rand.NextFloat(0f, 0.6f));
			Vector2 velocity = base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedByRandom(MathHelper.ToRadians(15f) * ((float)i + Main.rand.NextFloat(-0.5f, 0.5f))) * Main.rand.NextFloat(8f, 12f);
			GeneralParticleHandler.SpawnParticle(new BoltParticle(base.Projectile.Center, velocity, affectedByGravity: false, 18, Main.rand.NextFloat(0.4f, 0.6f), color, new Vector2(0.6f, 1f), glowCenter: true));
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<VermillionFlux>(), 90);
	}

	public override void AI()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % Main.projFrames[base.Type];
		if (Main.rand.NextBool(5))
		{
			Dust.NewDustPerfect(base.Projectile.Center, 244, Main.rand.NextVector2Circular(0.2f, 0.2f)).noLight = true;
		}
		if (base.Projectile.FinalExtraUpdate() || TrailPos == null)
		{
			Vector2 posOffset = base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 12f;
			if (TrailPos == null)
			{
				TrailPos = new List<Vector2>(12);
				for (int i = 0; i < 12; i++)
				{
					TrailPos.Add(base.Projectile.Center + posOffset);
				}
			}
			Vector2 randOffset = (Vector2.UnitY * Main.rand.NextFloat(-12f, 12f)).RotatedBy(base.Projectile.rotation);
			TrailPos.Insert(0, base.Projectile.Center + randOffset + posOffset);
			while (TrailPos.Count > 12)
			{
				TrailPos.RemoveAt(TrailPos.Count - 1);
			}
		}
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? ((float)Math.PI) : 0f);
		CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, SpawnedByFatFuck ? 960f : 300f, 10f, 20f);
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return (1f - completionRatio) * base.Projectile.scale * 10f;
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.Red, Color.Magenta, 0.7f * completionRatio + 0.1f * MathF.Sin(Main.GlobalTimeWrappedHourly * 20f)) * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		if (TrailPos == null)
		{
			return false;
		}
		GameShaders.Misc["CalamityMod:HeavenlyGaleLightningArc"].UseImage1("Images/Misc/Perlin");
		GameShaders.Misc["CalamityMod:HeavenlyGaleLightningArc"].Apply();
		PrimitiveRenderer.RenderTrail(TrailPos, new PrimitiveSettings(WidthFunction, ColorFunction, null, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:HeavenlyGaleLightningArc"]), 12);
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition, frame, Color.White, base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection == -1));
		return false;
	}
}
