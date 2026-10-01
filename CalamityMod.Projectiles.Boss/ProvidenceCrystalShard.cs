using System;
using System.IO;
using CalamityMod.NPCs;
using CalamityMod.NPCs.Providence;
using CalamityMod.Utilities;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ProvidenceCrystalShard : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 34;
		base.Projectile.height = 34;
		base.Projectile.hostile = true;
		base.Projectile.Opacity = 0f;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 600;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		ProvUtils.ApplyGFBDamage(base.Projectile, 0, 10);
		bool healerGuardianAlive = true;
		if (CalamityGlobalNPC.doughnutBossHealer < 0 || !Main.npc[CalamityGlobalNPC.doughnutBossHealer].active)
		{
			healerGuardianAlive = false;
		}
		Lighting.AddLight(base.Projectile.Center, 0.3f * base.Projectile.Opacity, 0.3f * base.Projectile.Opacity, 0.3f * base.Projectile.Opacity);
		if (!ProvUtils.StandardAI() | healerGuardianAlive)
		{
			base.Projectile.extraUpdates = 1;
		}
		if (base.Projectile.timeLeft < 300)
		{
			base.Projectile.tileCollide = true;
		}
		Color newColor2 = Main.hslToRgb(base.Projectile.ai[0], 1f, 0.5f);
		if (base.Projectile.Opacity < 1f)
		{
			base.Projectile.Opacity += 0.03f;
		}
		if (base.Projectile.Opacity > 1f)
		{
			base.Projectile.Opacity = 1f;
		}
		if (base.Projectile.Opacity == 1f)
		{
			Lighting.AddLight(base.Projectile.Center, ((Color)(ref newColor2)).ToVector3() * 0.5f);
		}
		base.Projectile.velocity.X *= 0.995f;
		if (base.Projectile.velocity.Y < 0f)
		{
			base.Projectile.velocity.Y *= 0.98f;
		}
		else
		{
			base.Projectile.velocity.Y *= 1.06f;
			float fallSpeed = ((CalamityWorld.revenge || base.Projectile.maxPenetrate != 0) ? 3.5f : 3f);
			if (base.Projectile.velocity.Y > fallSpeed)
			{
				base.Projectile.velocity.Y = fallSpeed;
			}
		}
		if (base.Projectile.velocity.Y > -0.5f && base.Projectile.localAI[1] == 0f)
		{
			base.Projectile.localAI[1] = 1f;
			base.Projectile.velocity.Y = 0.5f;
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) - (float)Math.PI / 2f;
		for (int i = 0; i < 2; i++)
		{
			if (Main.rand.NextBool(10))
			{
				Vector2 dustRotation = Vector2.UnitY.RotatedBy((float)i * (float)Math.PI).RotatedBy(base.Projectile.rotation);
				Dust obj = Main.dust[Dust.NewDust(base.Projectile.Center, 0, 0, 267, 0f, 0f, 225, newColor2, 1.5f)];
				obj.noGravity = true;
				obj.noLight = true;
				obj.scale = base.Projectile.Opacity * base.Projectile.localAI[0];
				obj.position = base.Projectile.Center;
				obj.velocity = dustRotation;
			}
		}
		for (int j = 0; j < 2; j++)
		{
			if (Main.rand.NextBool(10))
			{
				Vector2 dustRotate = Vector2.UnitY.RotatedBy((float)j * (float)Math.PI);
				Dust obj2 = Main.dust[Dust.NewDust(base.Projectile.Center, 0, 0, 267, 0f, 0f, 225, newColor2, 1.5f)];
				obj2.noGravity = true;
				obj2.noLight = true;
				obj2.scale = base.Projectile.Opacity * base.Projectile.localAI[0];
				obj2.position = base.Projectile.Center;
				obj2.velocity = dustRotate;
			}
		}
		if (Main.rand.NextBool(10))
		{
			float dustVelScale = 1f + Main.rand.NextFloat() * 2f;
			float fadeIn = 1f + Main.rand.NextFloat();
			float dustScale = 1f + Main.rand.NextFloat();
			Vector2 randomDustOffset = Utils.RandomVector2(Main.rand, -1f, 1f);
			if (randomDustOffset != Vector2.Zero)
			{
				((Vector2)(ref randomDustOffset)).Normalize();
			}
			randomDustOffset *= 16f + Main.rand.NextFloat() * 16f;
			Vector2 dustPos = base.Projectile.Center + randomDustOffset;
			Point dustTileCoords = dustPos.ToTileCoordinates();
			bool shouldSpawn = true;
			if (!WorldGen.InWorld(dustTileCoords.X, dustTileCoords.Y))
			{
				shouldSpawn = false;
			}
			if (shouldSpawn && WorldGen.SolidTile(dustTileCoords.X, dustTileCoords.Y))
			{
				shouldSpawn = false;
			}
			if (shouldSpawn)
			{
				Dust obj3 = Main.dust[Dust.NewDust(dustPos, 0, 0, 267, 0f, 0f, 127, newColor2)];
				obj3.noGravity = true;
				obj3.position = dustPos;
				obj3.velocity = -Vector2.UnitY * dustVelScale * (Main.rand.NextFloat() * 0.9f + 1.6f);
				obj3.fadeIn = fadeIn;
				obj3.scale = dustScale;
				obj3.noLight = true;
				Dust dust = DustExtensions.BetterCloneDust(obj3);
				dust.scale *= 0.65f;
				dust.fadeIn *= 0.65f;
				dust.color = new Color(255, 255, 255, 255);
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.Center);
		Vector2 spinningpoint = Utils.RotatedByRandom(new Vector2(0f, -3f), 3.1415927410125732);
		float dustAmt = Main.rand.Next(7, 13);
		Vector2 randomDustVelMod = default(Vector2);
		((Vector2)(ref randomDustVelMod))._002Ector(1.6f, 1.5f);
		Color newColor = Main.hslToRgb(base.Projectile.ai[0], 1f, 0.5f);
		((Color)(ref newColor)).A = byte.MaxValue;
		for (float i = 0f; i < dustAmt; i++)
		{
			int killDust = Dust.NewDust(base.Projectile.Center, 0, 0, 267, 0f, 0f, 0, newColor);
			Main.dust[killDust].position = base.Projectile.Center;
			Main.dust[killDust].velocity = spinningpoint.RotatedBy((float)Math.PI * 2f * i / dustAmt) * randomDustVelMod * (0.8f + Main.rand.NextFloat() * 0.4f);
			Main.dust[killDust].noGravity = true;
			Main.dust[killDust].scale = 2f;
			Main.dust[killDust].fadeIn = Main.rand.NextFloat() * 2f;
			Dust dust = DustExtensions.BetterCloneDust(killDust);
			dust.scale /= 2f;
			dust.fadeIn /= 2f;
			dust.color = new Color(255, 255, 255, 255);
		}
		for (float j = 0f; j < dustAmt; j++)
		{
			int killDust2 = Dust.NewDust(base.Projectile.Center, 0, 0, 267, 0f, 0f, 0, newColor);
			Main.dust[killDust2].position = base.Projectile.Center;
			Main.dust[killDust2].velocity = spinningpoint.RotatedBy((float)Math.PI * 2f * j / dustAmt) * randomDustVelMod * (0.8f + Main.rand.NextFloat() * 0.4f);
			Dust obj = Main.dust[killDust2];
			obj.velocity *= Main.rand.NextFloat() * 0.8f;
			obj.noGravity = true;
			obj.scale = Main.rand.NextFloat() * 1f;
			obj.fadeIn = Main.rand.NextFloat() * 2f;
			Dust dust2 = DustExtensions.BetterCloneDust(killDust2);
			dust2.scale /= 2f;
			dust2.fadeIn /= 2f;
			dust2.color = new Color(255, 255, 255, 255);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		for (int i = 1; i < base.Projectile.oldPos.Length; i++)
		{
			Main.EntitySpriteDraw(texture.Value, base.Projectile.oldPos[i] + base.Projectile.Size / 2f - Main.screenPosition, texture.Frame(), Color.Lerp(Color.Violet, new Color(42, 25, 60), (float)i / (float)base.Projectile.oldPos.Length).MultiplyRGBA(new Color(1f, 1f, 1f, 0f)), base.Projectile.oldRot[i], texture.Frame().Center(), base.Projectile.scale * MathHelper.Lerp(1.3f, 0.4f, (float)i / (float)base.Projectile.oldPos.Length), (SpriteEffects)0);
		}
		base.Projectile.DrawBackglow(Color.Violet.MultiplyRGBA(new Color(1f, 1f, 1f, 0f)), 4f, null, null, (SpriteEffects)0);
		Main.EntitySpriteDraw(texture.Value, base.Projectile.Center - Main.screenPosition, texture.Frame(), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, texture.Frame().Center(), base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255f * base.Projectile.Opacity, 255f * base.Projectile.Opacity, 255f * base.Projectile.Opacity, 255f * base.Projectile.Opacity);
	}
}
