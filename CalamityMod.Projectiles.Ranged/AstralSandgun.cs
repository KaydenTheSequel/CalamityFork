using System;
using CalamityMod.Tiles.AstralDesert;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class AstralSandgun : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.knockBack = 6f;
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.aiStyle = -1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		int tileX = (int)(base.Projectile.Center.X / 16f);
		int tileY = (int)(base.Projectile.Center.Y / 16f);
		if (Main.tile[tileX, tileY].IsHalfBlock && base.Projectile.velocity.Y > 0f && Math.Abs(base.Projectile.velocity.Y) > Math.Abs(base.Projectile.velocity.X))
		{
			tileY--;
		}
		if (!Main.tile[tileX, tileY].HasTile && Main.tile[tileX, tileY].TileType != 314)
		{
			WorldGen.PlaceTile(tileX, tileY, ModContent.TileType<AstralSand>(), mute: false, forced: true);
			WorldGen.SquareTileFrame(tileX, tileY);
		}
	}

	public override void AI()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool())
		{
			int i = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 108, 0f, base.Projectile.velocity.Y * 0.5f);
			Main.dust[i].velocity.X *= 0.2f;
		}
		base.Projectile.velocity.Y += 0.2f;
		base.Projectile.rotation += 0.1f;
		if (base.Projectile.velocity.Y > 10f)
		{
			base.Projectile.velocity.Y = 10f;
		}
		base.AI();
	}
}
