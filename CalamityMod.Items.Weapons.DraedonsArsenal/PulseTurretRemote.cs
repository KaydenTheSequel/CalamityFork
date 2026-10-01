using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

public class PulseTurretRemote : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override void SetDefaults()
	{
		base.Item.Calamity();
		base.Item.width = 28;
		base.Item.height = 26;
		base.Item.damage = 150;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.sentry = true;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<PulseTurret>();
		base.Item.shootSpeed = 1f;
		base.Item.UseSound = SoundID.Item15;
		base.Item.useStyle = 4;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.OnlyOneSentry(player, type);
		player.FindSentryRestingSpot(type, out var XPosition, out var YPosition, out var YOffset);
		YOffset -= 15;
		((Vector2)(ref position))._002Ector((float)XPosition, (float)(YPosition - YOffset));
		int turret = Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, player.whoAmI);
		if (Main.projectile.IndexInRange(turret))
		{
			Main.projectile[turret].originalDamage = base.Item.damage;
		}
		player.UpdateMaxTurrets();
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 3);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(12).AddIngredient<DubiousPlating>(18).AddIngredient<LifeAlloy>(5)
			.AddIngredient<InfectedArmorPlating>(10)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(3, out var condition), condition)
			.AddTile(134)
			.Register();
	}
}
