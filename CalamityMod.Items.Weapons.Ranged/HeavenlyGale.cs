using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class HeavenlyGale : ModItem, ILocalizedModType, IModType
{
	public const int ShootDelay = 32;

	public const int ArrowsPerBurst = 10;

	public const int ArrowShootRate = 4;

	public const int ArrowShootTime = 40;

	public const int MaxChargeTime = 300;

	public const float ArrowTargetingRange = 1100f;

	public const float MaxChargeDamageBoost = 3.5f;

	public const float LightningDamageFactor = 0.36f;

	public const float ChargeLightningCreationThreshold = 0.8f;

	public static readonly SoundStyle FireSound = new SoundStyle("CalamityMod/Sounds/Item/HeavenlyGaleFire");

	public static readonly SoundStyle LightningStrikeSound = new SoundStyle("CalamityMod/Sounds/Custom/HeavenlyGaleLightningStrike");

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 138;
		base.Item.height = 176;
		base.Item.damage = 256;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 40);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = 1;
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Arrow;
		base.Item.useTurn = true;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
	}

	public override bool CanShoot(Player player)
	{
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/HeavenlyGaleGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PlanetaryAnnihilation>().AddIngredient<TelluricGlare>().AddIngredient<ClockworkBow>()
			.AddIngredient<TheBallista>()
			.AddIngredient<MiracleMatter>()
			.AddTile<DraedonsForge>()
			.Register();
	}
}
