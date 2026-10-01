using CalamityMod.Items.Placeables.Astral;
using CalamityMod.Tiles.AstralDesert;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class AstralSandBallGun : SandBall
{
	public override string Texture => "CalamityMod/Projectiles/Typeless/SandBallAstral";

	public override int TileType => ModContent.TileType<global::CalamityMod.Tiles.AstralDesert.AstralSand>();

	public override int ItemType => ModContent.ItemType<global::CalamityMod.Items.Placeables.Astral.AstralSand>();

	public override int DustType => 108;

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.penetrate = 1;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.usesIDStaticNPCImmunity = false;
	}

	public override bool PreAI()
	{
		if (base.Projectile.ai[2] == 1f)
		{
			base.Projectile.scale = 0.75f;
			if (base.Projectile.ai[1] == 0f)
			{
				base.Projectile.originalDamage = base.Projectile.damage;
				base.Projectile.damage = 0;
			}
			if (base.Projectile.ai[1] == 30f)
			{
				base.Projectile.Calamity().conditionalHomingRange = 600f;
				base.Projectile.damage = base.Projectile.originalDamage;
			}
		}
		return base.PreAI();
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[2] != 1f)
		{
			for (int i = 0; i < 4; i++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.RotatedByRandom(0.75) * Main.rand.NextFloat(0.5f, 0.75f), ModContent.ProjectileType<AstralSandBallGun>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, 1f);
			}
		}
	}

	public override bool PreKill(int timeLeft)
	{
		if (base.Projectile.penetrate == 0)
		{
			base.Projectile.active = false;
		}
		return base.Projectile.active;
	}
}
