using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs.Yharon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class Infernado : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/Boss/Flarenado";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 12;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 320;
		base.Projectile.height = 88;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 720;
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
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		float scaleBase = 36f;
		float scaleMult = 1.5f;
		float baseWidth = 320f;
		float baseHeight = 88f;
		if (base.Projectile.velocity.X != 0f)
		{
			base.Projectile.direction = (base.Projectile.spriteDirection = -Math.Sign(base.Projectile.velocity.X));
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 2)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			base.Projectile.scale = (scaleBase - base.Projectile.ai[1]) * scaleMult / scaleBase;
			base.Projectile.ExpandHitboxBy((int)(baseWidth * base.Projectile.scale), (int)(baseHeight * base.Projectile.scale));
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[1] != -1f)
		{
			base.Projectile.scale = (scaleBase - base.Projectile.ai[1]) * scaleMult / scaleBase;
			base.Projectile.width = (int)(baseWidth * base.Projectile.scale);
			base.Projectile.height = (int)(baseHeight * base.Projectile.scale);
		}
		if (!Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			base.Projectile.alpha -= 30;
			if (base.Projectile.alpha < 60)
			{
				base.Projectile.alpha = 60;
			}
		}
		else
		{
			base.Projectile.alpha += 30;
			if (base.Projectile.alpha > 150)
			{
				base.Projectile.alpha = 150;
			}
		}
		if (base.Projectile.ai[0] > 0f)
		{
			base.Projectile.ai[0]--;
		}
		if (base.Projectile.ai[0] == 1f && base.Projectile.ai[1] > 0f && base.Projectile.owner == Main.myPlayer)
		{
			base.Projectile.netUpdate = true;
			Vector2 center = base.Projectile.Center;
			center.Y -= baseHeight * base.Projectile.scale / 2f;
			float finalProjHeight = (scaleBase - base.Projectile.ai[1] + 1f) * scaleMult / scaleBase;
			center.Y -= baseHeight * finalProjHeight / 2f;
			center.Y += 2f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), center, base.Projectile.velocity, base.Projectile.type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 11f, base.Projectile.ai[1] - 1f);
		}
		int tornadoSpeed = 15;
		int breakThreshold = 300;
		base.Projectile.localAI[1]++;
		bool breakapart = Main.zenithWorld && base.Projectile.localAI[1] >= (float)breakThreshold;
		if (base.Projectile.ai[0] <= 0f && !breakapart)
		{
			float smolWidth = (float)base.Projectile.width / 5f;
			smolWidth *= 2f;
			float projXChange = (float)(Math.Cos(0.10471975803375244 * (0.0 - (double)base.Projectile.ai[0])) - 0.5) * smolWidth;
			base.Projectile.position.X -= projXChange * (float)(-base.Projectile.direction);
			base.Projectile.ai[0]--;
			projXChange = (float)(Math.Cos(0.10471975803375244 * (0.0 - (double)base.Projectile.ai[0])) - 0.5) * smolWidth;
			base.Projectile.position.X += projXChange * (float)(-base.Projectile.direction);
		}
		if (base.Projectile.localAI[1] == (float)breakThreshold && Main.zenithWorld)
		{
			base.Projectile.velocity.X = (Main.rand.NextBool() ? (-tornadoSpeed) : tornadoSpeed);
		}
		if (base.Projectile.timeLeft == 600)
		{
			base.Projectile.damage = Yharon.TornadoDamage;
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.timeLeft <= 600;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 53, base.Projectile.alpha);
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

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && base.Projectile.timeLeft <= 600)
		{
			target.AddBuff(ModContent.BuffType<Dragonfire>(), 180);
		}
	}
}
