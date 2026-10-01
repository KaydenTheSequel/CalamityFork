using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Heresy : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 6));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 40;
		base.Item.damage = 960;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 15;
		base.Item.useAnimation = (base.Item.useTime = 120);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<HeresyProj>();
		base.Item.shootSpeed = 0f;
		base.Item.UseSound = SoundID.DD2_EtherianPortalDryadTouch;
		base.Item.useStyle = 5;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}
}
