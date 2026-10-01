using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.DraedonsArsenal;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

public class PlasmaCaster : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle FireSound = new SoundStyle("CalamityMod/Sounds/Item/PlasmaCasterFire");

	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.Calamity();
		base.Item.width = 62;
		base.Item.height = 30;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.damage = 705;
		base.Item.knockBack = 7f;
		base.Item.useTime = 45;
		base.Item.useAnimation = 45;
		base.Item.autoReuse = true;
		base.Item.mana = 24;
		base.Item.useStyle = 5;
		base.Item.UseSound = FireSound;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shoot = ModContent.ProjectileType<PlasmaCasterShot>();
		base.Item.shootSpeed = 5f;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			return 3f;
		}
		return 1f;
	}

	public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
	{
		if (player.altFunctionUse == 2)
		{
			mult /= 3f;
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref velocity)).Length() > 5f)
		{
			((Vector2)(ref velocity)).Normalize();
			velocity *= 5f;
		}
		float SpeedX = velocity.X + (float)Main.rand.Next(-3, 4) * 0.05f;
		float SpeedY = velocity.Y + (float)Main.rand.Next(-3, 4) * 0.05f;
		float damageMult = 1f;
		float kbMult = 1f;
		if (player.altFunctionUse == 2)
		{
			SpeedX = velocity.X + (float)Main.rand.Next(-15, 16) * 0.05f;
			SpeedY = velocity.Y + (float)Main.rand.Next(-15, 16) * 0.05f;
			damageMult = 0.3333f;
			kbMult = 0.42857143f;
		}
		Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), ModContent.ProjectileType<PlasmaCasterShot>(), (int)((float)damage * damageMult), knockback * kbMult, player.whoAmI);
		return false;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 4);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(18).AddIngredient<DubiousPlating>(12).AddIngredient<UelibloomBar>(8)
			.AddIngredient(3467, 4)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(4, out var condition), condition)
			.AddTile(134)
			.Register();
	}
}
