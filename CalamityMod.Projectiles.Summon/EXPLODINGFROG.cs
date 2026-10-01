using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class EXPLODINGFROG : ModProjectile, ILocalizedModType, IModType
{
	public const float MinExplodeDistance = 96f;

	public const float ExplodeWaitTime = 120f;

	public const float ExplosionAngleVariance = 0.8f;

	private static Texture2D GlowOutline;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 44;
		base.Projectile.height = 26;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = true;
		base.Projectile.sentry = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[0] = 75f;
			base.Projectile.ai[1] = 1f;
		}
		if (base.Projectile.frameCounter++ > 5)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		Player player = Main.player[base.Projectile.owner];
		bool canExplode = base.Projectile.Center.ClosestNPCAt(96f) != null;
		if (player.HasMinionAttackTargetNPC && !canExplode)
		{
			canExplode = Main.npc[player.MinionAttackTargetNPC].Distance(base.Projectile.Center) < 96f;
		}
		if (base.Projectile.ai[0] < 120f)
		{
			base.Projectile.ai[0]++;
		}
		if (base.Projectile.ai[0] >= 120f && canExplode)
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				Vector2 rotOffset = -Vector2.UnitY;
				if (player.HasMinionAttackTargetNPC && Main.npc[player.MinionAttackTargetNPC].Distance(base.Projectile.Center) < 96f + Math.Max((float)Main.npc[player.MinionAttackTargetNPC].width * 0.5f, (float)Main.npc[player.MinionAttackTargetNPC].height * 0.5f))
				{
					rotOffset = base.Projectile.DirectionTo(Main.npc[player.MinionAttackTargetNPC].Center);
				}
				else
				{
					NPC tar = base.Projectile.Center.ClosestNPCAt(96f);
					if (tar != null)
					{
						rotOffset = base.Projectile.DirectionTo(tar.Center);
					}
				}
				Vector2 direction = Vector2.Lerp(-Vector2.UnitY, rotOffset, 0.25f).SafeNormalize(-Vector2.UnitY);
				for (int i = 0; i < 3; i++)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (direction * Main.rand.NextFloat(6f, 10f)).RotatedByRandom(0.800000011920929), ModContent.ProjectileType<FrogGore1>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (direction * Main.rand.NextFloat(6f, 10f)).RotatedByRandom(0.800000011920929), ModContent.ProjectileType<FrogGore2>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				}
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (direction * Main.rand.NextFloat(6f, 10f)).RotatedByRandom(0.800000011920929), ModContent.ProjectileType<FrogGore3>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (direction * Main.rand.NextFloat(6f, 10f)).RotatedByRandom(0.800000011920929), ModContent.ProjectileType<FrogGore4>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (direction * Main.rand.NextFloat(6f, 10f)).RotatedByRandom(0.800000011920929), ModContent.ProjectileType<FrogGore5>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
			SoundEngine.PlaySound(in SoundID.NPCDeath13, base.Projectile.Center);
			base.Projectile.ai[0] = 0f;
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 0.1f)
			{
				base.Projectile.velocity.Y -= 5f;
			}
		}
		base.Projectile.velocity.Y += 0.5f;
		if (base.Projectile.velocity.Y > 10f)
		{
			base.Projectile.velocity.Y = 10f;
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.8f;
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public static Texture2D GetGlowOutline()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		if (GlowOutline == null)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/EXPLODINGFROG", (AssetRequestMode)2).Value;
			GlowOutline = new Texture2D(Main.graphics.GraphicsDevice, texture.Width, texture.Height);
			Color[] BaseArray = (Color[])(object)new Color[GlowOutline.Width * GlowOutline.Height];
			Color[] ColorArray = (Color[])(object)new Color[GlowOutline.Width * GlowOutline.Height];
			texture.GetData<Color>(BaseArray);
			for (int i = 0; i < BaseArray.Length; i++)
			{
				ColorArray[i] = new Color(255, 255, 255) * ((float)(int)((Color)(ref BaseArray[i])).A / 255f);
			}
			GlowOutline.SetData<Color>(ColorArray);
		}
		return GlowOutline;
	}

	public void ApplyGlowOutline(Texture2D tex, Rectangle? frame = null, float rotationOffset = 0f, float borderPixels = 1f, float opacity = 1f)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		Color color = Color.GreenYellow;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition + new Vector2(2f, 0f) * borderPixels, frame, color * opacity, base.Projectile.rotation - rotationOffset, new Vector2((float)frame.Value.Width, (float)frame.Value.Height) * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection == -1));
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition + new Vector2(0f, 2f) * borderPixels, frame, color * opacity, base.Projectile.rotation - rotationOffset, new Vector2((float)frame.Value.Width, (float)frame.Value.Height) * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection == -1));
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition + new Vector2(-2f, 0f) * borderPixels, frame, color * opacity, base.Projectile.rotation - rotationOffset, new Vector2((float)frame.Value.Width, (float)frame.Value.Height) * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection == -1));
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition + new Vector2(0f, -2f) * borderPixels, frame, color * opacity, base.Projectile.rotation - rotationOffset, new Vector2((float)frame.Value.Width, (float)frame.Value.Height) * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection == -1));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = GetGlowOutline();
		Rectangle frame = tex.Frame(1, 5, 0, base.Projectile.frame);
		ApplyGlowOutline(tex, frame, 0f, MathF.Pow(Math.Clamp((base.Projectile.ai[0] - 90f) / 30f, 0f, 1f), 2f));
		Vector2 center = base.Projectile.Center;
		Color greenYellow = Color.GreenYellow;
		Lighting.AddLight(center, ((Color)(ref greenYellow)).ToVector3() * MathF.Pow(Math.Clamp((base.Projectile.ai[0] - 90f) / 30f, 0f, 1f), 2f));
		tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, frame, lightColor * base.Projectile.Opacity, base.Projectile.rotation, new Vector2((float)frame.Width, (float)frame.Height) * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection == -1));
		return false;
	}
}
