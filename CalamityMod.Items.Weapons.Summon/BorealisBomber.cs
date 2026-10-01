using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class BorealisBomber : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 48;
		base.Item.height = 56;
		base.Item.damage = 34;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.Item44;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AureusBomber>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = DamageClass.Summon;
	}

	public override Vector2? HoldoutOrigin()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(15f, 15f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse != 2)
		{
			position = player.ClampedMouseWorld();
			velocity.X = 0f;
			velocity.Y = 0f;
			int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = base.Item.damage;
			}
		}
		return false;
	}
}
