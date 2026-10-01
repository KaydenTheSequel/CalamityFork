using System;
using CalamityMod.NPCs.Providence;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class ProfanedCrystalMageFireball : ModProjectile, ILocalizedModType, IModType
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

	private void Split(bool hit, bool chaseable)
	{
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		bool enrage = player.Calamity().pscState >= 3;
		player.Calamity().rollBabSpears(hit ? 1 : 0, chaseable);
		int outerSplits = (enrage ? 16 : 10);
		int innerSplits = (enrage ? 12 : 8);
		float mult = (enrage ? 0.3f : 0.6f);
		if (!hit)
		{
			mult = (enrage ? 0.2f : 0.1f);
		}
		int damage = (int)((float)base.Projectile.damage * 0.2f * mult);
		int origDmg = (int)((float)base.Projectile.originalDamage * 0.2f * mult);
		float outerAngleVariance = (float)Math.PI * 2f / (float)outerSplits;
		float outerOffsetAngle = (float)Math.PI / (2f * (float)outerSplits);
		float innerAngleVariance = (float)Math.PI * 2f / (float)innerSplits;
		float innerOffsetAngle = (float)Math.PI / (2f * (float)innerSplits);
		Vector2 outerPosVec = Utils.RotatedByRandom(new Vector2(8f, 0f), 6.2831854820251465);
		Vector2 innerPosVec = Utils.RotatedByRandom(new Vector2(5f, 0f), 6.2831854820251465);
		for (int i = 0; i < outerSplits; i++)
		{
			outerPosVec = outerPosVec.RotatedBy(outerAngleVariance);
			Vector2 velocity = Utils.RotatedBy(new Vector2(outerPosVec.X, outerPosVec.Y), (double)outerOffsetAngle, default(Vector2));
			((Vector2)(ref velocity)).Normalize();
			velocity *= 8f;
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + outerPosVec, velocity, ModContent.ProjectileType<ProfanedCrystalMageFireballSplit>(), damage, base.Projectile.knockBack, base.Projectile.owner);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].originalDamage = origDmg;
				Main.projectile[proj].DamageType = DamageClass.Summon;
			}
			if (innerSplits > 0)
			{
				innerPosVec = innerPosVec.RotatedBy(innerAngleVariance);
				velocity = Utils.RotatedBy(new Vector2(innerPosVec.X, innerPosVec.Y), (double)innerOffsetAngle, default(Vector2));
				((Vector2)(ref velocity)).Normalize();
				velocity *= 5f;
				proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + innerPosVec, velocity, ModContent.ProjectileType<ProfanedCrystalMageFireballSplit>(), damage, base.Projectile.knockBack, base.Projectile.owner);
				if (proj.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj].originalDamage = origDmg;
					Main.projectile[proj].DamageType = DamageClass.Summon;
				}
			}
			innerSplits--;
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
		base.Projectile.scale = 0.6f;
	}

	public override bool PreAI()
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
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
		if (base.Projectile.wet && !base.Projectile.lavaWet && base.Projectile.timeLeft < 70)
		{
			base.Projectile.Kill();
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		return false;
	}

	public override void AI()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dayTime)
		{
			_ = -1;
		}
		else
			_ = 0;
		int dustID = ProvUtils.GetDustID(!Main.dayTime);
		int num469 = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 100);
		Main.dust[num469].noGravity = true;
		Dust obj = Main.dust[num469];
		obj.velocity *= 0f;
		Player Owner = Main.player[base.Projectile.owner];
		base.Projectile.damage = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(base.Projectile.originalDamage);
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
		if (Main.myPlayer == base.Projectile.owner)
		{
			Split(hit: true, target.chaseable);
		}
		base.Projectile.active = false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				Split(hit: true, chaseable: true);
			}
			base.Projectile.active = false;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner && base.Projectile.ai[1] == 0f)
		{
			Split(hit: false, chaseable: false);
		}
		SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.position);
		int dustID = ProvUtils.GetDustID(!Main.dayTime);
		for (int num193 = 0; num193 < 6; num193++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 50, default(Color), 1.5f);
		}
		for (int i = 0; i < 60; i++)
		{
			int num195 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 0, default(Color), 2.5f);
			Main.dust[num195].noGravity = true;
			Dust obj = Main.dust[num195];
			obj.velocity *= 3f;
			num195 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 50, default(Color), 1.5f);
			Dust obj2 = Main.dust[num195];
			obj2.velocity *= 2f;
			Main.dust[num195].noGravity = true;
		}
	}
}
