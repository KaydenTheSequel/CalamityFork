using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class HadalUrnLamprey : ModProjectile, ILocalizedModType, IModType
{
	private int invistimer;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 18;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 5;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 60;
	}

	public override void AI()
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		invistimer++;
		if (invistimer > 4 && base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 50;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		base.Projectile.StickyProjAI(15);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.ai[0] == 0f)
		{
			if (base.Projectile.frame >= Main.projFrames[base.Type] / 2)
			{
				base.Projectile.frame = 0;
			}
			if (base.Projectile.timeLeft <= 420)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.98f;
			}
			if (base.Projectile.timeLeft <= 390)
			{
				base.Projectile.Kill();
			}
		}
		else if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = Main.projFrames[base.Type] / 2;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		base.Projectile.ModifyHitNPCSticky(4);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
		{
			((Rectangle)(ref targetHitbox)).Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		int textureheight = tex.Height / Main.projFrames[base.Type];
		int y = textureheight * base.Projectile.frame;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, y, tex.Width, textureheight), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)tex.Width, (float)tex.Height / 16f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 25; i++)
		{
			int hadalDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 14);
			Main.dust[hadalDust].position = (Main.dust[hadalDust].position + base.Projectile.position) / 2f;
			Main.dust[hadalDust].velocity = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			((Vector2)(ref Main.dust[hadalDust].velocity)).Normalize();
			Dust obj = Main.dust[hadalDust];
			obj.velocity *= (float)Main.rand.Next(1, 30) * 0.1f;
			Main.dust[hadalDust].alpha = base.Projectile.alpha;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 240);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 240);
	}

	public override bool? CanHitNPC(NPC target)
	{
		return null;
	}

	public override bool CanHitPvp(Player target)
	{
		return base.Projectile.ai[0] == 0f;
	}
}
