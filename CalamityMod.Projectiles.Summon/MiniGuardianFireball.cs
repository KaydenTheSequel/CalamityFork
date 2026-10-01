using System;
using CalamityMod.NPCs.Providence;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MiniGuardianFireball : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Boss/HolyBlast";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	private void Split()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		int totalProjectiles = ((base.Projectile.ai[0] == 0f) ? 8 : 4);
		float radians = (float)Math.PI * 2f / (float)totalProjectiles;
		int type = ModContent.ProjectileType<MiniGuardianFireballSplit>();
		float velocity = 5f;
		Vector2 spinningPoint = default(Vector2);
		((Vector2)(ref spinningPoint))._002Ector(0f, 0f - velocity);
		for (int k = 0; k < totalProjectiles; k++)
		{
			Vector2 velocity2 = spinningPoint.RotatedBy(radians * (float)k);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity2 + base.Projectile.velocity * 0.25f, type, (int)Math.Round((double)base.Projectile.originalDamage * 0.75), 0f, base.Projectile.owner, 1f);
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 180;
		base.Projectile.height = 180;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 75;
		base.Projectile.minion = true;
		base.Projectile.scale = 0.4f;
	}

	public override bool PreAI()
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Calamity().overridesMinionDamagePrevention = true;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.timeLeft == 50)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		return false;
	}

	public override void AI()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		base.Projectile.damage = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(base.Projectile.originalDamage);
		int num469 = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, ProvUtils.GetDustID(!Main.dayTime), 0f, 0f, 100);
		Main.dust[num469].noGravity = true;
		Dust obj = Main.dust[num469];
		obj.velocity *= 0f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return ProvUtils.GetColorBasedOnEnrage(!Main.dayTime, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = (Main.dayTime ? TextureAssets.Projectile[base.Type].Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/HolyBlastNight", (AssetRequestMode)2).Value);
		int num214 = texture.Height / Main.projFrames[base.Type];
		int y6 = num214 * base.Projectile.frame;
		base.Projectile.DrawBackglow(ProvUtils.GetColorBasedOnEnrage(!Main.dayTime, base.Projectile.alpha, Outline: true), 4f, texture, null, (SpriteEffects)0);
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture.Width, num214), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num214 / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Split();
		}
		SoundEngine.PlaySound(in HolyBlast.ImpactSound, base.Projectile.Center);
		base.Projectile.active = false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (info.Damage > 0)
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				Split();
			}
			SoundEngine.PlaySound(in HolyBlast.ImpactSound, base.Projectile.Center);
			base.Projectile.active = false;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Split();
		}
		SoundEngine.PlaySound(in HolyBlast.ImpactSound, base.Projectile.Center);
		int dustID = ProvUtils.GetDustID(!Main.dayTime);
		for (int num193 = 0; num193 < 6; num193++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 50, default(Color), Main.dayTime ? 1.5f : 0.5f);
		}
		for (int i = 0; i < 60; i++)
		{
			int num195 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 0, default(Color), Main.dayTime ? 2.5f : 0.5f);
			Main.dust[num195].noGravity = true;
			Dust obj = Main.dust[num195];
			obj.velocity *= 3f;
			num195 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 50, default(Color), Main.dayTime ? 1.5f : 0.5f);
			Dust obj2 = Main.dust[num195];
			obj2.velocity *= 2f;
			Main.dust[num195].noGravity = true;
		}
	}
}
