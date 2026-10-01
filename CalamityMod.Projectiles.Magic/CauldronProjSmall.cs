using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class CauldronProjSmall : ModProjectile, ILocalizedModType, IModType
{
	public static Asset<Texture2D> Glow;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		if (!Main.dedServ)
		{
			Glow = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 120;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 8f)
		{
			base.Projectile.ai[0] = 8f;
			if (base.Projectile.velocity.Y == 0f && base.Projectile.velocity.X != 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X * 0.97f;
				if (base.Projectile.velocity.X > -0.01f && base.Projectile.velocity.X < 0.01f)
				{
					base.Projectile.velocity.X = 0f;
					base.Projectile.netUpdate = true;
				}
			}
			base.Projectile.velocity.Y += 0.38f;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.05f;
		Vector2 position = base.Projectile.position;
		int width = base.Projectile.width;
		int height = base.Projectile.height;
		float scale = Main.rand.NextFloat(0.6f, 0.8f);
		int dust = Dust.NewDust(position, width, height, 6, 0f, 0f, 0, default(Color), scale);
		Main.dust[dust].noGravity = true;
		Dust obj = Main.dust[dust];
		obj.velocity *= 0f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.DD2_ExplosiveTrapExplode with
		{
			Pitch = 0.8f,
			Volume = 0.6f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i < 10; i++)
		{
			int size = 10;
			Vector2 center = base.Projectile.Center;
			Vector2 velocity = Main.rand.NextVector2Circular(size, size);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(center, velocity, Main.rand.NextFloat(0.1f, 0.2f), Color.Orange, Main.rand.Next(5, 8), 1f, 1.5f));
			Dust.NewDustPerfect(center, 6, velocity, 0, default(Color), Main.rand.NextFloat(0.8f, 1.2f)).noGravity = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		Vector2 origin = Glow.Value.Size() / 2f;
		Main.EntitySpriteDraw(Glow.Value, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override bool? CanDamage()
	{
		if (!(base.Projectile.velocity.Y < 0f))
		{
			return null;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(24, 90);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(24, 90);
	}
}
