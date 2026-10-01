using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Violence : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 142);
		base.Item.damage = 485;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.noMelee = true;
		base.Item.useTurn = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 60;
		base.Item.useTime = 25;
		base.Item.useStyle = 5;
		base.Item.knockBack = 9f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.shoot = ModContent.ProjectileType<ViolenceThrownProjectile>();
		base.Item.shootSpeed = 15f;
		base.Item.channel = true;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
			player.itemAnimation = (player.itemAnimationMax = (player.itemTime = (player.itemTimeMax = 60)));
			Projectile.NewProjectile(source, position, velocity * 1.6666f, type, damage * 15, knockback, player.whoAmI, 0f, velocity.ToRotation());
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, velocity.ToRotation(), 1f);
		}
		return false;
	}
}
