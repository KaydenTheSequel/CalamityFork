using CalamityMod.Projectiles.Magic;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Poseidon : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.damage = 52;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 12;
		base.Item.useTime = 49;
		base.Item.useAnimation = 49;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 6f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.UseSound = SoundID.Item84;
		base.Item.rare = 5;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<PoseidonTyphoon>();
		base.Item.shootSpeed = 18f;
	}
}
