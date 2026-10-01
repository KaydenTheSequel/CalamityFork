using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DarkEnergyBall2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/NPCs/CeaselessVoid/DarkEnergy";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = (base.Projectile.height = 64);
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.penetrate = -1;
		base.Projectile.Opacity = 0f;
		if (Main.zenithWorld)
		{
			base.Projectile.extraUpdates = 1;
		}
	}

	public override void AI()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.voidBoss < 0 || !Main.npc[CalamityGlobalNPC.voidBoss].active)
		{
			base.Projectile.active = false;
			base.Projectile.netUpdate = true;
			return;
		}
		if (Vector2.Distance(base.Projectile.Center, Main.npc[CalamityGlobalNPC.voidBoss].Center) < 80f)
		{
			base.Projectile.Kill();
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 10f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.05f;
		}
		if (base.Projectile.timeLeft < 30)
		{
			base.Projectile.Opacity = MathHelper.Clamp((float)base.Projectile.timeLeft / 30f, 0f, 1f);
		}
		else
		{
			base.Projectile.Opacity = MathHelper.Clamp(1f - (float)(base.Projectile.timeLeft - 570) / 30f, 0f, 1f);
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 5)
		{
			base.Projectile.frame = 0;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int drawStart = height * base.Projectile.frame;
		Vector2 origin = base.Projectile.Size / 2f;
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, drawStart, texture.Width, height), Color.White, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0, 0f);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 35f, targetHitbox);
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.Opacity == 1f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && base.Projectile.Opacity == 1f && Main.zenithWorld)
		{
			target.AddBuff(163, 30);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			int cosmiliteDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100, default(Color), 1.2f);
			Dust obj = Main.dust[cosmiliteDust];
			obj.velocity *= 3f;
			Main.dust[cosmiliteDust].noGravity = true;
			if (Main.rand.NextBool())
			{
				Main.dust[cosmiliteDust].scale = 0.5f;
				Main.dust[cosmiliteDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 5; j++)
		{
			int cosmiliteDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100, default(Color), 1.7f);
			Main.dust[cosmiliteDust2].noGravity = true;
			Dust obj2 = Main.dust[cosmiliteDust2];
			obj2.velocity *= 5f;
			cosmiliteDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100);
			Main.dust[cosmiliteDust2].noGravity = true;
			Dust obj3 = Main.dust[cosmiliteDust2];
			obj3.velocity *= 2f;
		}
	}
}
