using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class DesecratedBubble : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 120;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.scale += 0.002f;
		if (base.Projectile.alpha <= 0)
		{
			base.Projectile.alpha = 0;
		}
		else if (base.Projectile.alpha > 50)
		{
			base.Projectile.alpha -= 20;
		}
		if (base.Projectile.timeLeft <= 100)
		{
			base.Projectile.ai[1] = 0f;
		}
		else
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.995f;
		}
		if (Main.player[base.Projectile.owner].active && !Main.player[base.Projectile.owner].dead && base.Projectile.ai[1] == 0f)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 200f, 8f, 20f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item54, base.Projectile.position);
		int rando = Main.rand.Next(5, 9);
		for (int i = 0; i < rando; i++)
		{
			int dust = Dust.NewDust(base.Projectile.Center, 0, 0, 179, 0f, 0f, 100, default(Color), 1.4f);
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.8f;
			Main.dust[dust].position = Vector2.Lerp(Main.dust[dust].position, base.Projectile.Center, 0.5f);
			Main.dust[dust].noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.ai[0] == 1f)
		{
			target.AddBuff(69, 180);
			target.AddBuff(39, 180);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (base.Projectile.ai[0] == 1f)
		{
			target.AddBuff(69, 180);
			target.AddBuff(39, 180);
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.timeLeft >= 100)
		{
			return false;
		}
		return null;
	}

	public override bool CanHitPvp(Player target)
	{
		return base.Projectile.timeLeft < 100;
	}
}
