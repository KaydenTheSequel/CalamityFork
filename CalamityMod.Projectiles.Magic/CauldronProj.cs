using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class CauldronProj : ModProjectile, ILocalizedModType, IModType
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
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 10f)
		{
			base.Projectile.ai[0] = 10f;
			if (base.Projectile.velocity.Y == 0f && base.Projectile.velocity.X != 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X * 0.97f;
				if (base.Projectile.velocity.X > -0.01f && base.Projectile.velocity.X < 0.01f)
				{
					base.Projectile.velocity.X = 0f;
					base.Projectile.netUpdate = true;
				}
			}
			base.Projectile.velocity.Y += 0.4f;
		}
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] > 3f)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.05f;
		Vector2 position = base.Projectile.position;
		int width = base.Projectile.width;
		int height = base.Projectile.height;
		float scale = Main.rand.NextFloat(1f, 1.4f);
		int dust = Dust.NewDust(position, width, height, 6, 0f, 0f, 0, default(Color), scale);
		Main.dust[dust].noGravity = true;
		Dust obj = Main.dust[dust];
		obj.velocity *= 0f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.DD2_ExplosiveTrapExplode, base.Projectile.Center);
		base.Projectile.ExpandHitboxBy(128);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.damage = (int)((float)base.Projectile.damage * 0.66f);
		base.Projectile.Damage();
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < 3; i++)
			{
				Vector2 smauldronSpeed = (Vector2.UnitY * Main.rand.NextFloat(-10f, -8f)).RotatedByRandom(MathHelper.ToRadians(30f));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, smauldronSpeed, ModContent.ProjectileType<CauldronProjSmall>(), (int)((float)base.Projectile.damage * 0.5f), 3f, base.Projectile.owner);
			}
		}
		for (int j = 0; j < 40; j++)
		{
			int size = 16;
			Vector2 center = base.Projectile.Center;
			Vector2 velocity = Main.rand.NextVector2Circular(size, size);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(center, velocity, Main.rand.NextFloat(0.2f, 0.3f), Color.Orange, Main.rand.Next(6, 9), 1f, 1.5f));
			Dust.NewDustPerfect(center, 6, velocity, 0, default(Color), Main.rand.NextFloat(1f, 2f)).noGravity = true;
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

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(24, 90);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(24, 90);
	}
}
