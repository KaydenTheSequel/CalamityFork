using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class FrostShardFriendly : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.aiStyle = 1;
		base.Projectile.coldDamage = true;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.coldDamage = true;
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public override void AI()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
		}
		if (base.Projectile.frame >= 5)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.velocity.Y += 0.2f;
		if (base.Projectile.localAI[0] == 0f || base.Projectile.localAI[0] == 2f)
		{
			base.Projectile.scale += 0.01f;
			base.Projectile.alpha -= 50;
			if (base.Projectile.alpha <= 0)
			{
				base.Projectile.localAI[0] = 1f;
				base.Projectile.alpha = 0;
			}
		}
		else if ((double)base.Projectile.localAI[0] == 1.0)
		{
			base.Projectile.scale -= 0.01f;
			base.Projectile.alpha += 50;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.localAI[0] = 2f;
				base.Projectile.alpha = 255;
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, base.Projectile.alpha);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] != 1f)
		{
			SoundStyle style = SoundID.Item27 with
			{
				Volume = SoundID.Item12.Volume * 0.7f
			};
			SoundEngine.PlaySound(in style, base.Projectile.position);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		for (int index1 = 0; index1 < 3; index1++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 76);
			Main.dust[index2].noGravity = true;
			Main.dust[index2].noLight = true;
			Main.dust[index2].scale = 0.7f;
		}
	}
}
