using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

public class BloodRune : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 24;
		base.Item.damage = 1;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 10f;
		base.Item.value = Item.buyPrice(0, 1);
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<IceBarrageMain>();
		base.Item.shootSpeed = 0f;
		base.Item.ammo = base.Item.type;
	}
}
