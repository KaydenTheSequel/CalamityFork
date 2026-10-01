using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Hematemesis : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 48;
		base.Item.height = 54;
		base.Item.damage = 75;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 14;
		base.Item.rare = 8;
		base.Item.useTime = 22;
		base.Item.useAnimation = 22;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.75f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.UseSound = SoundID.Item21;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<BloodBlast>();
		base.Item.shootSpeed = 10f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		position = player.ClampedMouseWorld();
		for (int x = 0; x < 10; x++)
		{
			Projectile.NewProjectile(source, position.X + (float)Main.rand.Next(-150, 150), position.Y + 600f, 0f, -10f, type, damage, knockback, player.whoAmI);
		}
		return false;
	}
}
