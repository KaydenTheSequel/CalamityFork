using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class CryoBlast : ModProjectile, ILocalizedModType, IModType
{
	private const float Spread = 0.15f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 62;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 90;
		base.Projectile.coldDamage = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.scale <= 2.5f)
		{
			base.Projectile.scale *= 1.02f;
			base.Projectile.ExpandHitboxBy((int)(35f * base.Projectile.scale));
		}
		else if (base.Projectile.ai[0] < 2f)
		{
			base.Projectile.ai[0]++;
			if (base.Projectile.owner == Main.myPlayer)
			{
				for (int i = 0; i < 2; i++)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.RotatedBy(-0.15f * (float)(i + 1)), base.Projectile.type, base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner, base.Projectile.ai[0]);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.RotatedBy(0.15f * (float)(i + 1)), base.Projectile.type, base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner, base.Projectile.ai[0]);
				}
			}
			base.Projectile.Kill();
		}
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0.5f, 0.5f);
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 4f || base.Projectile.ai[0] > 0f)
		{
			int ice = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 66, 0f, 0f, 100, default(Color), base.Projectile.scale * 0.5f);
			Main.dust[ice].noGravity = true;
			Dust obj = Main.dust[ice];
			obj.velocity *= 0f;
			int snow = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 185, 0f, 0f, 100, default(Color), base.Projectile.scale * 0.5f);
			Main.dust[snow].noGravity = true;
			Dust obj2 = Main.dust[snow];
			obj2.velocity *= 0f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		if ((base.Projectile.timeLeft > 596 && base.Projectile.ai[0] == 0f) || (base.Projectile.timeLeft > 599 && base.Projectile.ai[0] > 0f))
		{
			return false;
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		int height = texture.Height / Main.projFrames[base.Type];
		int frameHeight = height * base.Projectile.frame;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, frameHeight, texture.Width, height);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)texture.Width / 2f, (float)height / 2f);
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(texture, drawPos, rectangle, lightColor, base.Projectile.rotation, origin, base.Projectile.scale, spriteEffects);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.Center);
		for (int index1 = 0; index1 < 15; index1++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 88, 0f, 0f, 0, default(Color), 0.9f);
			Main.dust[index2].noGravity = true;
			Dust obj = Main.dust[index2];
			obj.velocity *= 1.5f;
		}
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		Vector2 shardPos = base.Projectile.oldPosition + 0.5f * base.Projectile.Size;
		Vector2 shardVel = default(Vector2);
		for (int i = 0; i < 3; i++)
		{
			((Vector2)(ref shardVel))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			while (shardVel.X == 0f && shardVel.Y == 0f)
			{
				((Vector2)(ref shardVel))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			}
			((Vector2)(ref shardVel)).Normalize();
			shardVel *= (float)Main.rand.Next(70, 101) * 0.1f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), shardPos, shardVel, 337, base.Projectile.damage / 2, base.Projectile.knockBack * 0.5f, base.Projectile.owner);
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.RotatingHitboxCollision(targetHitbox);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GlacialState>(), 30);
		target.AddBuff(324, 180);
	}
}
