using CalamityMod.Projectiles.Melee.Spears;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class EarthenPike : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 60;
		base.Item.height = 60;
		base.Item.damage = 85;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.noMelee = true;
		base.Item.useTurn = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 25;
		base.Item.useStyle = 5;
		base.Item.useTime = 25;
		base.Item.knockBack = 7f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<EarthenPikeSpear>();
		base.Item.shootSpeed = 8f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}
}
