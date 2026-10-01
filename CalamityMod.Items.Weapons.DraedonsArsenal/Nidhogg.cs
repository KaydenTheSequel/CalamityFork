using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

[LegacyName(new string[] { "GaussRifle", "CompactRailgun" })]
public class Nidhogg : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.Calamity();
		base.Item.width = 70;
		base.Item.height = 30;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.damage = 69;
		base.Item.knockBack = 7f;
		base.Item.useTime = (base.Item.useAnimation = 8);
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.shoot = ModContent.ProjectileType<NidhoggHoldout>();
		base.Item.shootSpeed = 7f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void HoldItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI)
		{
			player.Calamity().rightClickListener = true;
		}
		player.Calamity().mouseWorldListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		float gunType = 0f;
		if (player.Calamity().mouseRight && player.whoAmI == Main.myPlayer && !Main.mapFullscreen && !Main.blockMouse && player.Calamity().arsenalCooldown <= 0)
		{
			gunType = 5f;
		}
		Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, type, damage, knockback, player.whoAmI, 0f, 0f, gunType).velocity = (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.Zero);
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/DraedonsArsenal/NidhoggGlow", (AssetRequestMode)2).Value);
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 3);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(18).AddIngredient<DubiousPlating>(12).AddIngredient<LifeAlloy>(5)
			.AddIngredient<InfectedArmorPlating>(10)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(3, out var condition), condition)
			.AddTile(134)
			.Register();
	}
}
