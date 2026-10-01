using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class PlagueStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 46;
		base.Item.damage = 70;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 22;
		base.Item.useTime = 21;
		base.Item.useAnimation = 21;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 8f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = SoundID.Item43;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<PlagueFang>();
		base.Item.shootSpeed = 16f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			Vector2 spreadVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(5f)) * Main.rand.NextFloat(0.8f, 1.2f);
			Projectile.NewProjectile(source, position, spreadVelocity, type, damage, knockback, Main.myPlayer);
		}
		return true;
	}
}
