using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TyphonsGreedBubble : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.timeLeft < 210)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 450f, 20f, 20f);
		}
		float inertia = 50f * base.Projectile.ai[1];
		float speed = 10f * base.Projectile.ai[1];
		if (Math.Abs(base.Projectile.velocity.X) > 0.2f)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Lighting.AddLight(base.Projectile.Center, 0f, 0.1f, 0.7f);
		Player player = Main.player[base.Projectile.owner];
		if (player.active && !player.dead)
		{
			if (base.Projectile.Distance(player.Center) > 40f)
			{
				Vector2 moveDirection = base.Projectile.SafeDirectionTo(player.Center, Vector2.UnitY);
				base.Projectile.velocity = (base.Projectile.velocity * (inertia - 1f) + moveDirection * speed) / inertia;
			}
		}
		else if (base.Projectile.timeLeft > 30)
		{
			base.Projectile.timeLeft = 30;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Main.spriteBatch.Draw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture2D13.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)framing / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item21, base.Projectile.position);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 64);
		base.Projectile.position.X -= base.Projectile.width / 2;
		base.Projectile.position.Y -= base.Projectile.height / 2;
		for (int i = 0; i < 2; i++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
		}
		for (int j = 0; j < 6; j++)
		{
			int bubblyDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 186, 0f, 0f, 0, new Color(0, 255, 255), 2.5f);
			Main.dust[bubblyDust].noGravity = true;
			Dust obj = Main.dust[bubblyDust];
			obj.velocity *= 3f;
			bubblyDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 186, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
			Dust obj2 = Main.dust[bubblyDust];
			obj2.velocity *= 2f;
			Main.dust[bubblyDust].noGravity = true;
		}
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.damage /= 1;
		base.Projectile.Damage();
	}
}
