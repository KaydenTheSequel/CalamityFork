using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class EutrophicSandBallGun : SandBall
{
	public override string Texture => "CalamityMod/Projectiles/Typeless/SandBallEutrophic";

	public override int TileType => ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.EutrophicSand>();

	public override int ItemType => ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.EutrophicSand>();

	public override bool DropAsItem => true;

	public override int DustType => 108;

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.penetrate = 1;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Eutrophication>(), 300);
	}
}
