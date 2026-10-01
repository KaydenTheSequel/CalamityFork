using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Projectiles.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

[LegacyName(new string[] { "FrequencyManipulator" })]
public class PulseGrenade : RogueWeapon, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override void SetDefaults()
	{
		base.Item.Calamity();
		base.Item.width = 30;
		base.Item.height = 40;
		base.Item.damage = 59;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = 75;
		base.Item.useAnimation = 75;
		base.Item.autoReuse = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 8f;
		base.Item.channel = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shootSpeed = 16f;
		base.Item.shoot = ModContent.ProjectileType<PulseGrenadeProjectile>();
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] <= 0)
		{
			return player.ownedProjectileCounts[ModContent.ProjectileType<PulseGrenadeOrb>()] <= 0;
		}
		return false;
	}

	public override void HoldItem(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<PulseGrenadeOrb>()] > 0)
		{
			player.Calamity().rogueStealth = 0f;
		}
		player.Calamity().mouseWorldListener = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 2);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, position, velocity, type, damage, 0f, player.whoAmI);
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/DraedonsArsenal/PulseGrenadeGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(8).AddIngredient<DubiousPlating>(12).AddRecipeGroup("AnyMythrilBar", 10)
			.AddIngredient(549, 20)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(2, out var condition), condition)
			.AddTile(134)
			.Register();
	}
}
