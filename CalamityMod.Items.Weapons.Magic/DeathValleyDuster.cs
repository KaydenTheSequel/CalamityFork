using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "DeathValley" })]
public class DeathValleyDuster : ModItem, ILocalizedModType, IModType
{
	public static int BuffDefenseBoost = PrimordialEarth.BuffDefenseBoost;

	public static float BuffDamageBoost = PrimordialEarth.BuffDamageBoost;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BuffDefenseBoost, BuffDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 40;
		base.Item.damage = 130;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 38;
		base.Item.useTime = 50;
		base.Item.useAnimation = 50;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 8f;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/MagicRockSound")
		{
			Volume = 0.4f,
			Pitch = -0.1f
		};
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<DeathValleyDusterProjectile>();
		base.Item.shootSpeed = 6.5f;
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
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref player.velocity)).Length() <= 12f)
		{
			player.velocity += -velocity.SafeNormalize(Vector2.UnitX) * 5f;
		}
		bool MaxMana = player.statMana >= player.statManaMax2 - (int)((float)base.Item.mana * player.manaCost) && !player.HasBuff(94);
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, 0f, MaxMana ? 1f : 0f);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(531).AddIngredient(3380, 25).AddIngredient(3794, 2)
			.AddTile(101)
			.Register();
	}
}
