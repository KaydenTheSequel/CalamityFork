using System;
using System.IO;
using CalamityMod.NPCs;
using CalamityMod.NPCs.Providence;
using CalamityMod.Particles;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ProvidenceCrystal : ModProjectile, ILocalizedModType, IModType
{
	public bool introAnimationDone;

	public float introAnimationProgress;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetDefaults()
	{
		base.Projectile.width = 160;
		base.Projectile.height = 160;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = (CalamityWorld.death ? 2100 : 3600);
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
	}

	private void CrystalExplosion()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		Player proviTarget = Main.player[Main.npc[CalamityGlobalNPC.holyBoss].target];
		for (int i = 0; i < 7; i++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, proviTarget.velocity, Color.Violet, "CalamityMod/Particles/BlastCone", new Vector2(Main.rand.NextFloat(2.5f, 6f), 3f), Main.rand.NextFloat((float)Math.PI * 2f), 1f, 0.5f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		for (int j = 0; j < 4; j++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, proviTarget.velocity, Color.Violet, "CalamityMod/Particles/BloomCircle", Vector2.One, 0f, 3f, 0f, 35, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
	}

	public override void AI()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		if (introAnimationProgress == 0f)
		{
			SoundEngine.PlaySound(SoundID.Item101.WithPitchOffset(-1f), base.Projectile.Center);
		}
		introAnimationProgress += 0.025f;
		introAnimationProgress = MathHelper.Clamp(introAnimationProgress, 0f, 1f);
		if (introAnimationProgress == 1f)
		{
			if (!introAnimationDone)
			{
				SoundEngine.PlaySound(SoundID.DD2_WitherBeastCrystalImpact.WithPitchOffset(-0.5f), base.Projectile.Center);
				SoundEngine.PlaySound(in SoundID.DD2_CrystalCartImpact, base.Projectile.Center);
				CrystalExplosion();
			}
			introAnimationDone = true;
		}
		if (CalamityGlobalNPC.holyBoss < 0 || !Main.npc[CalamityGlobalNPC.holyBoss].active)
		{
			base.Projectile.active = false;
			base.Projectile.netUpdate = true;
			return;
		}
		if (base.Projectile.ai[2] > 0f && (float)base.Projectile.timeLeft > base.Projectile.ai[2])
		{
			base.Projectile.timeLeft = (int)base.Projectile.ai[2];
		}
		Player proviTarget = Main.player[Main.npc[CalamityGlobalNPC.holyBoss].target];
		base.Projectile.position.X = proviTarget.Center.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = proviTarget.Center.Y - (float)(base.Projectile.height / 2) + proviTarget.gfxOffY - 360f;
		if (proviTarget.gravDir == -1f)
		{
			base.Projectile.position.Y += 400f;
			base.Projectile.rotation = (float)Math.PI;
		}
		else
		{
			base.Projectile.rotation = 0f;
		}
		base.Projectile.position.X = (int)base.Projectile.position.X;
		base.Projectile.position.Y = (int)base.Projectile.position.Y;
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.alpha -= 20;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.alpha == 0 && Main.rand.NextBool(15))
		{
			Color projectileColor = ProvUtils.GetProjectileColor(0);
			float Brightness = 0.8f;
			Color DustColor = Color.Lerp(projectileColor, Color.White, Brightness);
			Dust obj = Main.dust[Dust.NewDust(base.Projectile.Top, 0, 0, 267, 0f, 0f, 100, DustColor)];
			obj.velocity.X = 0f;
			obj.noGravity = true;
			obj.fadeIn = 1f;
			obj.position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(6.2831854820251465) * (4f * Main.rand.NextFloat() + 26f);
			obj.scale = 0.5f;
		}
		float lifeRatio = base.Projectile.ai[0];
		base.Projectile.localAI[0]++;
		bool standardAI = ProvUtils.StandardAI() || (Main.zenithWorld && base.Projectile.timeLeft < 1500);
		if (!(base.Projectile.localAI[0] >= (standardAI ? 300f : 30f)) || !((base.Projectile.localAI[0] % 30f == 0f) | standardAI))
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.Item109, base.Projectile.Center);
		base.Projectile.netUpdate = true;
		if (base.Projectile.owner == Main.myPlayer)
		{
			int totalProjectiles = (standardAI ? 15 : ((base.Projectile.localAI[0] % 60f == 0f) ? 15 : 10));
			float speedX = (standardAI ? (-21f) : (-15f));
			float speedAdjustment = Math.Abs(speedX * 2f / (float)(totalProjectiles - 1));
			float speedY = -3f;
			for (int i = 0; i < totalProjectiles; i++)
			{
				float x4 = (standardAI ? Main.rgbToHsl(new Color(255, 200, Main.DiscoB)).X : Main.rgbToHsl(new Color(Main.DiscoR, 200, 255)).X);
				float randomSpread = (standardAI ? 0f : ((float)Main.rand.Next(-150, 151) * 0.01f * (1f - lifeRatio)));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, speedX + speedAdjustment * (float)i + randomSpread, speedY, ModContent.ProjectileType<ProvidenceCrystalShard>(), base.Projectile.damage, base.Projectile.knockBack, Main.myPlayer, x4, base.Projectile.whoAmI);
			}
			CrystalExplosion();
		}
		if (base.Projectile.localAI[0] >= 60f)
		{
			base.Projectile.localAI[0] = 0f;
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		if (introAnimationDone)
		{
			Color colorArea = Lighting.GetColor((int)((double)base.Projectile.position.X + (double)base.Projectile.width * 0.5) / 16, (int)(((double)base.Projectile.position.Y + (double)base.Projectile.height * 0.5) / 16.0));
			Vector2 drawArea = base.Projectile.position + new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
			Texture2D texture2D34 = TextureAssets.Projectile[base.Type].Value;
			Rectangle textureRect = texture2D34.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
			base.Projectile.GetAlpha(colorArea);
			Vector2 halfRect = textureRect.Size() / 2f;
			float scaleFactor = (float)Math.Cos((float)Math.PI * 2f * (base.Projectile.localAI[0] / 60f)) + 3f + 3f;
			for (float i = 0f; i < 4f; i += 0.5f)
			{
				double angle = i * ((float)Math.PI / 2f);
				Main.spriteBatch.Draw(texture2D34, drawArea + Vector2.UnitY.RotatedBy(angle) * scaleFactor, (Rectangle?)textureRect, Color.Violet.MultiplyRGBA(new Color(1f, 1f, 1f, 0f)), base.Projectile.rotation, halfRect, base.Projectile.scale, (SpriteEffects)0, 0f);
			}
			Main.EntitySpriteDraw(texture2D34, base.Projectile.Center - Main.screenPosition, texture2D34.Frame(), Color.White, 0f, texture2D34.Frame().Center(), 1f, (SpriteEffects)0);
		}
		else
		{
			Asset<Texture2D> tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/ProvidenceCrystal_Halves", (AssetRequestMode)2);
			for (int j = 0; j < 2; j++)
			{
				float dir = ((j != 0) ? 1 : (-1));
				float ii = MathHelper.Lerp(60f, 0f, CalamityUtils.CircInEasing(introAnimationProgress, 1) - CalamityUtils.SineBumpEasing(introAnimationProgress, 1));
				Main.EntitySpriteDraw(tex.Value, base.Projectile.Center + Utils.RotatedBy(new Vector2(dir * ii), (double)MathHelper.ToRadians(MathHelper.Lerp(0f, -90f, CalamityUtils.CircInEasing(introAnimationProgress, 1))), default(Vector2)) - Main.screenPosition, (Rectangle?)new Rectangle(j * 50, 0, 50, tex.Height()), base.Projectile.GetAlpha(lightColor).MultiplyRGBA(new Color(1f, 1f, 1f, 1f)), MathHelper.ToRadians(MathHelper.Lerp(70f, 0f, introAnimationProgress)), new Vector2(25f, (float)(tex.Height() / 2)), 1f, (SpriteEffects)0, 0f);
			}
		}
		return false;
	}
}
