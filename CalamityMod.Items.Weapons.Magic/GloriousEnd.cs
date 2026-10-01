using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Summon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class GloriousEnd : ModItem, ILocalizedModType, IModType
{
	public static int PlayerExplosionDmgMin = 60;

	public static int PlayerExplosionDmgMax = 80;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 58;
		base.Item.damage = 100;
		base.Item.knockBack = 10f;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.mana = 20;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.channel = true;
		base.Item.shootSpeed = 7f;
		base.Item.shoot = ModContent.ProjectileType<MeteorStar>();
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.UseSound = SoundID.Item9;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.Calamity().donorItem = true;
	}

	public override bool? CanAutoReuseItem(Player player)
	{
		return player.ownedProjectileCounts[ModContent.ProjectileType<GiantIbanRobotOfDoom>()] > 0;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}
}
