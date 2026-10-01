using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class PrimordialEarth : ModItem, ILocalizedModType, IModType
{
	public static int BuffDefenseBoost = 12;

	public static float BuffDamageBoost = 0.12f;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BuffDefenseBoost, BuffDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 42;
		base.Item.damage = 205;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 48;
		base.Item.useTime = (base.Item.useAnimation = 68);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 10f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/MagicRockSound")
		{
			Volume = 0.4f,
			Pitch = -0.1f
		};
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<PrimordialEarthProjectile>();
		base.Item.shootSpeed = 4.5f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref player.velocity)).Length() <= 14f)
		{
			player.velocity += -velocity.SafeNormalize(Vector2.UnitX) * 6f;
		}
		Vector2 staticSpeed = player.Center.DirectionTo(player.Calamity().mouseWorld) * player.Center.Distance(player.ClampedMouseWorld()) * 0.008f;
		bool MaxMana = player.statMana >= player.statManaMax2 - (int)((float)base.Item.mana * player.manaCost) && !player.HasBuff(94);
		float rotation = 0.4f;
		Projectile.NewProjectile(source, position, staticSpeed.RotatedBy(0f - rotation), type, damage / 2, knockback, player.whoAmI, 0f, 1f, MaxMana ? 1f : 0f);
		Projectile.NewProjectile(source, position, staticSpeed.RotatedBy(rotation), type, damage / 2, knockback, player.whoAmI, 0f, 0f, MaxMana ? 1f : 0f);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<DeathValleyDuster>().AddIngredient(999, 5).AddIngredient(1508, 5)
			.AddTile(101)
			.Register();
	}
}
