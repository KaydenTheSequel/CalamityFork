using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

[LegacyName(new string[] { "MatterModulator" })]
public class HolofibreImmolator : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle PlasmaSound = new SoundStyle("CalamityMod/Sounds/Item/PlasmaSmall");

	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.Calamity();
		base.Item.width = 26;
		base.Item.height = 78;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.damage = 30;
		base.Item.knockBack = 0.8f;
		base.Item.useTime = (base.Item.useAnimation = 14);
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = null;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<HolofibreImmolatorHoldout>();
		base.Item.shootSpeed = 12f;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void HoldItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI)
		{
			player.Calamity().rightClickListener = true;
		}
		player.Calamity().mouseWorldListener = true;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/DraedonsArsenal/HolofibreImmolatorGlow", (AssetRequestMode)2).Value);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, ModContent.ProjectileType<HolofibreImmolatorHoldout>(), damage, knockback, player.whoAmI, 0f, 1f).velocity = (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.Zero);
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 2);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(12).AddIngredient<DubiousPlating>(8).AddRecipeGroup("AnyMythrilBar", 10)
			.AddIngredient(547, 20)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(2, out var condition), condition)
			.AddTile(134)
			.Register();
	}
}
