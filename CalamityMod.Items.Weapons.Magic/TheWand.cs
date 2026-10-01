using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class TheWand : ModItem, ILocalizedModType, IModType
{
	public static int BaseDamage = 599;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 36;
		base.Item.damage = 14;
		base.Item.mana = 150;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.noMelee = true;
		base.Item.useAnimation = 19;
		base.Item.useTime = 19;
		base.Item.useTurn = true;
		base.Item.useStyle = 5;
		base.Item.knockBack = 0.5f;
		base.Item.UseSound = SoundID.Item102;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.shoot = ModContent.ProjectileType<SparkInfernal>();
		base.Item.shootSpeed = 24f;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool CanUseItem(Player player)
	{
		int num = player.ownedProjectileCounts[ModContent.ProjectileType<SparkInfernal>()];
		int numTornadoStarters = player.ownedProjectileCounts[ModContent.ProjectileType<InfernadoMarkFriendly>()];
		int numTornadoPieces = player.ownedProjectileCounts[ModContent.ProjectileType<InfernadoFriendly>()];
		return num + numTornadoStarters + numTornadoPieces < 1;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 6);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3069).AddIngredient<YharonSoulFragment>(8).AddTile<CosmicAnvil>()
			.Register();
	}
}
