using CalamityMod.Projectiles.Magic;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Keelhaul : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 42;
		base.Item.damage = 55;
		base.Item.mana = 40;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.noMelee = true;
		base.Item.useTime = (base.Item.useAnimation = 30);
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 2f;
		base.Item.UseSound = SoundID.Item102;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<KeelhaulBubble>();
		base.Item.shootSpeed = 15f;
	}
}
