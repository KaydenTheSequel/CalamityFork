using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class PermafrostMeat : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Items/Potions/Food/DeliciousMeat";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 120;
		base.Projectile.aiStyle = 2;
		base.AIType = 48;
	}

	public override void AI()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f)
		{
			SoundStyle style = SoundID.NPCDeath43 with
			{
				Volume = SoundID.NPCDeath43.Volume * 0.35f,
				Pitch = 0.3f
			};
			SoundEngine.PlaySound(in style, base.Projectile.position);
			base.Projectile.ai[0] = 1f;
		}
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.aiStyle = -1;
			base.Projectile.tileCollide = false;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.NPCDeath43 with
		{
			Volume = SoundID.NPCDeath43.Volume * 0.35f
		};
		SoundEngine.PlaySound(in style, base.Projectile.position);
		for (int i = 0; i < 10; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 161, 0f, 0f, 0, default(Color), 1.2f);
			Dust obj = Main.dust[dust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[dust].scale = 0.5f;
				Main.dust[dust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 20; j++)
		{
			int dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 161, 0f, 0f, 0, default(Color), 1.7f);
			Main.dust[dust2].noGravity = true;
			Dust obj2 = Main.dust[dust2];
			obj2.velocity *= 5f;
			dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 161);
			Dust obj3 = Main.dust[dust2];
			obj3.velocity *= 2f;
		}
		if (base.Projectile.ai[2] != 0f || base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		int totalProjectiles = 3;
		float radians = (float)Math.PI * 2f / (float)totalProjectiles;
		float velocity = 8f;
		double angleA = (double)radians * 0.5;
		double angleB = (double)MathHelper.ToRadians(90f) - angleA;
		float velocityX2 = (float)((double)velocity * Math.Sin(angleA) / Math.Sin(angleB));
		Vector2 spinningPoint = (Main.rand.NextBool() ? new Vector2(0f, 0f - velocity) : new Vector2(0f - velocityX2, 0f - velocity));
		for (int k = 0; k < totalProjectiles; k++)
		{
			Vector2 velocity2 = spinningPoint.RotatedBy(radians * (float)k);
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + Vector2.Normalize(velocity2) * 10f, velocity2, base.Type, (int)Math.Round((double)base.Projectile.damage * 0.8), 0f, Main.myPlayer, 0f, 0f, 1f);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].tileCollide = false;
				Main.projectile[proj].aiStyle = -1;
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		base.Projectile.Kill();
	}
}
