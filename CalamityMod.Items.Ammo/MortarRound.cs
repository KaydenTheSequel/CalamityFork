using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

public class MortarRound : ModItem, ILocalizedModType, IModType
{
	internal static int TileBlastRadiusNormal = 3;

	internal static int TileBlastRadiusGFB = 5;

	public static int TileBlastRadius
	{
		get
		{
			if (!Main.getGoodWorld)
			{
				return TileBlastRadiusNormal;
			}
			return TileBlastRadiusGFB;
		}
	}

	public static int HitboxBlastRadius => TileBlastRadius * 16 + 12;

	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 14;
		base.Item.damage = 8;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 7f;
		base.Item.value = Item.sellPrice(0, 0, 0, 20);
		base.Item.rare = 8;
		base.Item.ammo = AmmoID.Bullet;
		base.Item.shoot = ModContent.ProjectileType<MortarRoundProj>();
	}

	public override void AddRecipes()
	{
		CreateRecipe(100).AddIngredient(1432, 100).AddIngredient(1347, 4).AddIngredient<ScoriaBar>()
			.AddTile(18)
			.Register();
	}
}
