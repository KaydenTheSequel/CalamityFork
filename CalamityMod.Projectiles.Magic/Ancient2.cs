using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class Ancient2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/Magic/PrimordialAncientProjectile";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 4;
		base.Projectile.extraUpdates = 12;
		base.Projectile.timeLeft = 30;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6;
		base.Projectile.scale = 0.5f;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.3f, 0.25f, 0f);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 3)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
		if (base.Projectile.ai[0] > 7f && base.Projectile.numUpdates % 2 == 0)
		{
			int dustType = 22;
			int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f, 100);
			Dust dust = Main.dust[idx];
			if (Main.rand.NextBool())
			{
				dust.noGravity = true;
				dust.scale *= 2f;
				dust.velocity.X *= 2f;
				dust.velocity.Y *= 2f;
			}
			else
			{
				dust.scale *= 1.25f;
			}
			dust.velocity.X *= 3f;
			dust.velocity.Y *= 3f;
			idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f, 100);
			if (Main.rand.NextBool(3))
			{
				dust.noGravity = true;
				dust.scale *= 2.5f;
				dust.velocity.X *= 1.5f;
				dust.velocity.Y *= 1.5f;
			}
			else
			{
				dust.scale *= 1.5f;
			}
			dust.velocity.X *= 1.1f;
			dust.velocity.Y *= 1.1f;
		}
		base.Projectile.ai[0]++;
		base.Projectile.rotation += 0.3f * (float)base.Projectile.direction;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		int height = texture.Height / Main.projFrames[base.Type];
		int frameHeight = height * base.Projectile.frame;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, frameHeight, texture.Width, height);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)texture.Width / 2f, (float)height / 2f);
		Main.EntitySpriteDraw(texture, drawPos, rectangle, lightColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		Circle dustCircle = new Circle(base.Projectile.Center, base.Projectile.width / 2);
		for (int i = 0; i < 20; i++)
		{
			Vector2 dustPos = dustCircle.RandomPointInCircle();
			Vector2 center = dustPos - base.Projectile.Center;
			if (((Vector2)(ref center)).Length() > 48f)
			{
				int dustIndex = Dust.NewDust(dustPos, 1, 1, 22);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].fadeIn = 1f;
				Vector2 dustVelocity = base.Projectile.Center - Main.dust[dustIndex].position;
				float distToCenter = ((Vector2)(ref dustVelocity)).Length();
				((Vector2)(ref dustVelocity)).Normalize();
				Vector2 spinningpoint = dustVelocity;
				double radians = MathHelper.ToRadians(-90f);
				center = default(Vector2);
				dustVelocity = spinningpoint.RotatedBy(radians, center);
				dustVelocity *= distToCenter * 0.04f;
				Main.dust[dustIndex].velocity = dustVelocity;
			}
		}
		return false;
	}
}
