using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class DuneSandBallGun : SandBall
{
	public override string Texture => "CalamityMod/Projectiles/Typeless/SandBallDune";

	public override int TileType => ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.Dunesand>();

	public override int ItemType => ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.Dunesand>();

	public override int DustType => 32;

	public override void AI()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 0f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.5f;
		}
		if (Main.rand.NextBool())
		{
			int i = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, DustType);
			Main.dust[i].velocity.X *= 0.4f;
			Main.dust[i].velocity.Y += (Fired ? 0f : (base.Projectile.velocity.Y * 0.5f));
		}
		base.Projectile.ai[1]++;
		base.Projectile.rotation += 0.1f;
		if (base.Projectile.ai[1] >= 180f)
		{
			base.Projectile.ai[1] = 180f;
			base.Projectile.velocity.Y += 0.2f;
		}
		if (base.Projectile.velocity.Y > 10f)
		{
			base.Projectile.velocity.Y = 10f;
		}
		Point p = base.Projectile.Center.ToTileCoordinates();
		if (p.X >= 0 && p.X < Main.maxTilesX && p.Y >= 0 && p.Y < Main.maxTilesY)
		{
			Tile placer = Main.tile[p.X, p.Y + 1];
			if (placer.HasTile && TileID.Sets.Platforms[placer.TileType] && base.Projectile.ai[1] >= 60f)
			{
				base.Projectile.Kill();
			}
		}
	}
}
