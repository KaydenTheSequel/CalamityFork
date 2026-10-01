using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class PhantomMine : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 480;
		base.Projectile.Opacity = 0f;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Opacity = MathHelper.Lerp(0f, 1f, ((Vector2)(ref base.Projectile.velocity)).Length() / base.Projectile.ai[0]);
		if (!(((Vector2)(ref base.Projectile.velocity)).Length() < base.Projectile.ai[0]))
		{
			return;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= base.Projectile.ai[1];
		if (((Vector2)(ref base.Projectile.velocity)).Length() > base.Projectile.ai[0])
		{
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= base.Projectile.ai[0];
		}
		if (!Main.getGoodWorld || !(((Vector2)(ref base.Projectile.velocity)).Length() >= base.Projectile.ai[0]))
		{
			return;
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			int totalProjectiles = 8;
			float radians = (float)Math.PI * 2f / (float)totalProjectiles;
			for (int i = 0; i < totalProjectiles; i++)
			{
				Vector2 vector = Utils.RotatedBy(new Vector2(0f, -8f), (double)(radians * (float)i), default(Vector2));
				int type = (Main.rand.NextBool() ? ModContent.ProjectileType<PhantomShot2>() : ModContent.ProjectileType<PhantomShot>());
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vector, type, base.Projectile.damage, 0f, Main.myPlayer);
			}
		}
		base.Projectile.Kill();
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		return new Color((int)(byte)(200f * base.Projectile.Opacity), (int)(byte)(200f * base.Projectile.Opacity), (int)(byte)(200f * base.Projectile.Opacity), base.Projectile.alpha);
	}

	public override bool CanHitPlayer(Player target)
	{
		return ((Vector2)(ref base.Projectile.velocity)).Length() >= base.Projectile.ai[0];
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 12f, targetHitbox);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
		base.Projectile.width = 150;
		base.Projectile.height = 150;
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 15; i++)
		{
			int phantomDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 180, 0f, 0f, 100, default(Color), 1.2f);
			Dust obj = Main.dust[phantomDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[phantomDust].scale = 0.5f;
				Main.dust[phantomDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 30; j++)
		{
			int phantomDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 180, 0f, 0f, 100, default(Color), 1.7f);
			Main.dust[phantomDust2].noGravity = true;
			Dust obj2 = Main.dust[phantomDust2];
			obj2.velocity *= 5f;
			phantomDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 100);
			Dust obj3 = Main.dust[phantomDust2];
			obj3.velocity *= 2f;
		}
	}
}
