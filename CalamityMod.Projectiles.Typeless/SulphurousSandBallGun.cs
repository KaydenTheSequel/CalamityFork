using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Tiles.Abyss;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class SulphurousSandBallGun : SandBall
{
	public override string Texture => "CalamityMod/Projectiles/Typeless/SandBallSulphurous";

	public override int TileType => ModContent.TileType<global::CalamityMod.Tiles.Abyss.SulphurousSand>();

	public override int ItemType => ModContent.ItemType<global::CalamityMod.Items.Placeables.Abyss.SulphurousSand>();

	public override bool DropAsItem => true;

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.penetrate = 2;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 150);
	}
}
