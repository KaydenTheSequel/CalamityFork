using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class PoisonousSeawater : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 6;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0f / 255f, (float)(255 - base.Projectile.alpha) * 0.15f / 255f, (float)(255 - base.Projectile.alpha) * 0.4f / 255f);
		int randomDust = Main.rand.Next(4);
		randomDust = ((randomDust != 3) ? 33 : 89);
		for (int i = 0; i < 2; i++)
		{
			int seaDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDust, 0f, 0f, 100);
			if (randomDust == 89)
			{
				Main.dust[seaDust].scale *= 0.35f;
			}
			Dust obj = Main.dust[seaDust];
			obj.velocity *= 0f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(70, 60);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(70, 60);
	}
}
