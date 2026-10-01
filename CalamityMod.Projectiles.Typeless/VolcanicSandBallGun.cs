using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class VolcanicSandBallGun : SandBall
{
	public override string Texture => "CalamityMod/Projectiles/Typeless/SandBallVolcanic";

	public override int TileType => ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.EutrophicSand>();

	public override int ItemType => ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.VolcanicSand>();

	public override int DustType => -1;

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.usesIDStaticNPCImmunity = false;
	}

	public override bool PreAI()
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[2] == 1f)
		{
			if (base.Projectile.ai[1] == 0f)
			{
				base.Projectile.scale = 0.75f;
				base.Projectile.penetrate = 1;
				base.Projectile.timeLeft = 30;
			}
		}
		else if (base.Projectile.ai[1] == 0f)
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				for (int i = 0; i < 5; i++)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.RotatedByRandom(0.5) * 0.75f, ModContent.ProjectileType<VolcanicSandBallGun>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, 1f);
				}
			}
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.5f);
		}
		return base.PreAI();
	}

	public override bool PreKill(int timeLeft)
	{
		if (base.Projectile.ai[2] == 1f)
		{
			base.Projectile.active = false;
		}
		return base.Projectile.active;
	}
}
