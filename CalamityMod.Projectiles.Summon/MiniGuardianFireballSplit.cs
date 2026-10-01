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

public class MiniGuardianFireballSplit : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Boss/HolyFire2";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.SummonTagDamageMultiplier[base.Type] = 0.3f;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.minion = true;
		base.Projectile.gfxOffY = -25f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override bool PreAI()
	{
		base.Projectile.Calamity().overridesMinionDamagePrevention = true;
		return true;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.3f, 0.225f, 0f);
		Player Owner = Main.player[base.Projectile.owner];
		base.Projectile.damage = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(base.Projectile.originalDamage);
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
		if (Math.Abs(base.Projectile.velocity.X) < 8f)
		{
			base.Projectile.velocity.X *= 1.05f;
		}
		NPC target = base.Projectile.Center.MinionHoming(2000f, Main.player[base.Projectile.owner]);
		if (target != null)
		{
			float scaleFactor2 = ((Vector2)(ref base.Projectile.velocity)).Length();
			Vector2 vector11 = target.Center - base.Projectile.Center;
			((Vector2)(ref vector11)).Normalize();
			vector11 *= scaleFactor2;
			float inertia = 15f;
			base.Projectile.velocity = (base.Projectile.velocity * (inertia - 1f) + vector11) / inertia;
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			Projectile projectile = base.Projectile;
			projectile.velocity *= scaleFactor2;
		}
		base.Projectile.rotation = base.Projectile.velocity.X * 0.025f;
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
		Texture2D texture = (Main.dayTime ? TextureAssets.Projectile[base.Type].Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/HolyFire2Night", (AssetRequestMode)2).Value);
		int num214 = texture.Height / Main.projFrames[base.Type];
		int y6 = num214 * base.Projectile.frame;
		base.Projectile.DrawBackglow(ProvUtils.GetColorBasedOnEnrage(!Main.dayTime, base.Projectile.alpha, Outline: true), 4f, texture, null, (SpriteEffects)0);
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture.Width, num214), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num214 / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
		base.Projectile.width = (base.Projectile.height = 200);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		int dust = ProvUtils.GetDustID(!Main.dayTime);
		for (int num621 = 0; num621 < 4; num621++)
		{
			int num622 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dust, 0f, 0f, 100, default(Color), Main.dayTime ? 2f : 0.5f);
			Dust obj = Main.dust[num622];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[num622].scale = 0.5f;
				Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 12; i++)
		{
			int num624 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dust, 0f, 0f, 100, default(Color), Main.dayTime ? 3f : 0.5f);
			Main.dust[num624].noGravity = true;
			Dust obj2 = Main.dust[num624];
			obj2.velocity *= 5f;
			num624 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dust, 0f, 0f, 100, default(Color), Main.dayTime ? 2f : 0.5f);
			Dust obj3 = Main.dust[num624];
			obj3.velocity *= 2f;
		}
	}
}
