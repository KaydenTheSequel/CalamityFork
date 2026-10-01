using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class EnchantedKnifeStaff : ModItem, ILocalizedModType, IModType
{
	public static float SwingTime = 20f;

	public static float SwingWait = 10f;

	public static float ProjectileSpeed = 15f;

	public static float DashCooldown = 30f;

	public static float DashSpeed = 16f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.damage = 10;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.shoot = ModContent.ProjectileType<EnchantedKnifeSummon>();
		base.Item.knockBack = 2f;
		base.Item.useTime = (base.Item.useAnimation = 15);
		base.Item.mana = 10;
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
		base.Item.width = 50;
		base.Item.height = 50;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item8;
		base.Item.shootSpeed = 1f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, Main.MouseWorld, Vector2.Zero, type, damage, knockback, player.whoAmI);
		return false;
	}
}
