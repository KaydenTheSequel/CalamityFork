using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ValkyrieRay : ModItem, ILocalizedModType, IModType
{
	public const int ChargeFrames = 18;

	public const int CooldownFrames = 10;

	public const float GemDistance = 18f;

	public static readonly Color LightColor;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 54;
		base.Item.height = 52;
		base.Item.damage = 115;
		base.Item.knockBack = 8.5f;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 17;
		base.Item.useTime = 28;
		base.Item.useAnimation = 28;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.NPCDeath7 with
		{
			Volume = 0.7f
		};
		base.Item.useTurn = false;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<ValkyrieRayStaff>();
		base.Item.shootSpeed = 25f;
		base.Item.autoReuse = true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 11f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1225, 12).AddIngredient<AerialiteBar>(6).AddIngredient(178)
			.AddIngredient(547)
			.AddIngredient(548)
			.AddIngredient(549)
			.AddTile(134)
			.Register();
	}

	static ValkyrieRay()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		LightColor = new Color(235, 40, 121);
	}
}
