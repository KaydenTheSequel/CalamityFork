using CalamityMod.NPCs.SlimeGod;
using CalamityMod.Projectiles.Magic;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class AbyssalTome : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 30;
		base.Item.damage = 22;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 15;
		base.Item.useTime = 25;
		base.Item.useAnimation = 25;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 6f;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.UseSound = SlimeGodCore.ShotSound;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AbyssBall>();
		base.Item.shootSpeed = 9f;
	}
}
