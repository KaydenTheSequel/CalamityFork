using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class BurningSea : ModItem, ILocalizedModType, IModType
{
	public const float ChargeTime = 240f;

	public const float BurnOutTime = 560f;

	public const int BurnOutReuseDelay = 150;

	public const float FizzleOutTime = 40f;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 30;
		base.Item.damage = 69;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 8;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 4;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item20;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<IncineratingFireball>();
		base.Item.shootSpeed = 5f;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] <= 0)
		{
			return player.Calamity().burningSeaBurnOut <= 0;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(531).AddIngredient<UnholyCore>(5).AddTile(101)
			.Register();
	}
}
