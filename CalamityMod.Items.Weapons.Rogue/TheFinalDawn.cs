using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "FinalDawn" })]
public class TheFinalDawn : RogueWeapon
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/FinalDawnSlash");

	public override float StealthDamageMultiplier => 0.6f;

	public override void SetDefaults()
	{
		base.Item.width = 78;
		base.Item.height = 66;
		base.Item.damage = 2500;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.noMelee = true;
		base.Item.useAnimation = (base.Item.useTime = 15);
		base.Item.useStyle = 1;
		base.Item.knockBack = 4f;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.Calamity().donorItem = true;
		base.Item.autoReuse = false;
		base.Item.shoot = ModContent.ProjectileType<FinalDawnProjectile>();
		base.Item.shootSpeed = 1f;
		base.Item.useTurn = false;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] + player.ownedProjectileCounts[ModContent.ProjectileType<FinalDawnFireSlash>()] + player.ownedProjectileCounts[ModContent.ProjectileType<FinalDawnHorizontalSlash>()] + player.ownedProjectileCounts[ModContent.ProjectileType<FinalDawnThrow>()] + player.ownedProjectileCounts[ModContent.ProjectileType<FinalDawnThrow2>()] <= 0;
	}
}
