using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class RavagerNuke : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle ExplosionSound = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/RavagerMissileExplosion");

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 44;
		base.Projectile.height = 44;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		bool num = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		if (base.Projectile.timeLeft < 180)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 18)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 4)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		float inertia = (num ? 90f : 110f);
		float scaleFactor12 = (num ? 16f : 12f);
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 10;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		Lighting.AddLight(base.Projectile.Center, 1f, 0.7f, 0f);
		int playerTracker = (int)base.Projectile.ai[0];
		if (playerTracker >= 0 && Main.player[playerTracker].active && !Main.player[playerTracker].dead)
		{
			if (base.Projectile.Distance(Main.player[playerTracker].Center) > 320f)
			{
				Vector2 moveDirection = base.Projectile.SafeDirectionTo(Main.player[playerTracker].Center, Vector2.UnitY);
				base.Projectile.velocity = (base.Projectile.velocity * (inertia - 1f) + moveDirection * scaleFactor12) / inertia;
			}
			return;
		}
		if (base.Projectile.timeLeft > 30)
		{
			base.Projectile.timeLeft = 30;
		}
		if (base.Projectile.ai[0] != -1f)
		{
			base.Projectile.ai[0] = -1f;
			base.Projectile.netUpdate = true;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			if (DownedBossSystem.downedProvidence)
			{
				target.AddBuff(ModContent.BuffType<Laceration>(), 180);
			}
			else
			{
				target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in ExplosionSound, base.Projectile.Center);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 160);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.Damage();
		for (int i = 0; i < 30; i++)
		{
			int nukeDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[nukeDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[nukeDust].scale = 0.5f;
				Main.dust[nukeDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 40; j++)
		{
			int nukeDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100, default(Color), 3f);
			Main.dust[nukeDust2].noGravity = true;
			Dust obj2 = Main.dust[nukeDust2];
			obj2.velocity *= 5f;
			nukeDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[nukeDust2];
			obj3.velocity *= 2f;
		}
		if (Main.dedServ)
		{
			return;
		}
		Vector2 source = default(Vector2);
		((Vector2)(ref source))._002Ector(base.Projectile.Center.X - 24f, base.Projectile.Center.Y - 24f);
		for (int g = 1; g <= 3; g++)
		{
			float velocityMult = (float)g * 0.33f;
			for (int spawn = 0; spawn < 4; spawn++)
			{
				int type = Main.rand.Next(61, 64);
				int smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				Gore obj4 = Main.gore[smoke];
				obj4.velocity *= velocityMult;
				obj4.velocity.X++;
				obj4.velocity.Y++;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		int timeToStartWarning = 180;
		Color initialColor = lightColor;
		Color finalColor = Color.Lerp(Color.White, Color.Red, (float)Math.Abs(Math.Sin((float)(timeToStartWarning - base.Projectile.timeLeft) * ((float)Math.PI * 7f / 180f))));
		((Color)(ref finalColor)).A = (byte)(255 - base.Projectile.alpha);
		float colorTransitionRatio = MathHelper.Clamp((float)(timeToStartWarning - base.Projectile.timeLeft) / (float)timeToStartWarning, 0f, 1f);
		Color warningColor = ((base.Projectile.timeLeft > timeToStartWarning) ? initialColor : Color.Lerp(initialColor, finalColor, colorTransitionRatio));
		float strength = Utils.GetLerpValue(0f, (float)timeToStartWarning / 1.5f, timeToStartWarning - base.Projectile.timeLeft, clamped: true);
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(1, 5, 0, base.Projectile.frame);
		SpriteEffects sp = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		base.Projectile.DrawBackglow(new Color(64, 255, 255) * strength, 4.5f, null, frame, sp);
		Main.spriteBatch.EnterShaderRegion(BlendState.AlphaBlend);
		GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(colorTransitionRatio * 0.45f);
		GameShaders.Misc["CalamityMod:BasicTint"].UseColor(warningColor);
		GameShaders.Misc["CalamityMod:BasicTint"].Apply();
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, sp);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}
}
