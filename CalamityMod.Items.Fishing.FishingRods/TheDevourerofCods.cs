using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Fishing.AstralCatches;
using CalamityMod.Items.Fishing.BrimstoneCragCatches;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.FishingRods;

public class TheDevourerofCods : ModItem, ILocalizedModType, IModType
{
	public static List<int> FishToEat = new List<int>
	{
		2290,
		2299,
		4401,
		2302,
		2301,
		4402,
		2298,
		2316,
		2297,
		2300,
		ModContent.ItemType<CharredLasher>(),
		ModContent.ItemType<CragBullhead>(),
		ModContent.ItemType<ProcyonidPrawn>(),
		ModContent.ItemType<TwinklingPollox>(),
		ModContent.ItemType<PlantyMush>()
	};

	public new string LocalizationCategory => "Items.Fishing";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.CanFishInLava[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 28;
		base.Item.useAnimation = 8;
		base.Item.useTime = 8;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.fishingPole = 75;
		base.Item.shootSpeed = 20f;
		base.Item.shoot = ModContent.ProjectileType<DevourerofCodsBobber>();
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void HoldItem(Player player)
	{
		if (player.Calamity().SelectedFishingMinigame == CalamityPlayer.FishingMinigames.None)
		{
			player.Calamity().SelectedFishingMinigame = CalamityPlayer.FishingMinigames.DevourerofCods;
		}
		player.accFishingLine = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().SelectedFishingMinigame = CalamityPlayer.FishingMinigames.DevourerofCods;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 10; i++)
		{
			Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(18f)), type, 0, 0f, player.whoAmI, 0f, 0f, Main.rand.Next(2));
		}
		return false;
	}

	public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		lineOriginOffset = new Vector2(53f, -33f);
		if (bobber.ai[2] == 0f)
		{
			lineColor = new Color(252, 109, 202, 100);
		}
		else
		{
			lineColor = new Color(39, 151, 171, 100);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmiliteBar>(6).AddTile<CosmicAnvil>().Register();
	}
}
