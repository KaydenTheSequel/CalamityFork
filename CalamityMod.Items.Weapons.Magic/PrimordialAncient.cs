using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class PrimordialAncient : ModItem, ILocalizedModType, IModType
{
	public static float BuffDamageReductionBoost = 0.08f;

	public static float BuffDamageBoost = 0.18f;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BuffDamageReductionBoost.ToPercent(), BuffDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 56;
		base.Item.damage = 4350;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 95;
		base.Item.useTime = 78;
		base.Item.useAnimation = 78;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 14f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/MagicRockSound")
		{
			Volume = 0.4f,
			Pitch = -0.1f
		};
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<PrimordialAncientProjectile>();
		base.Item.shootSpeed = 8f;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref player.velocity)).Length() <= 16f)
		{
			player.velocity += -velocity.SafeNormalize(Vector2.UnitX) * 7f;
		}
		bool MaxMana = player.statMana >= player.statManaMax2 - (int)((float)base.Item.mana * player.manaCost) && !player.HasBuff(94);
		for (int i = -2; i <= 2; i++)
		{
			Vector2 vel = velocity.RotatedBy(0.1f * (float)i) * MathHelper.Lerp((float)(3 - Math.Abs(i)), 1f, 0.7f);
			Projectile.NewProjectileDirect(source, position, vel, type, damage, knockback, player.whoAmI, 0f, (i == 0) ? 1 : 0, MaxMana ? 1f : 0f).localAI[0] = (float)i * 0.45f;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PrimordialEarth>().AddIngredient<CosmiliteBar>(8).AddIngredient<EndothermicEnergy>(20)
			.AddTile(101)
			.Register();
	}
}
