using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class DaedalusGolemStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 32);
		base.Item.damage = 70;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item67;
		base.Item.autoReuse = true;
		base.Item.buffType = ModContent.BuffType<DaedalusGolemBuff>();
		base.Item.shoot = ModContent.ProjectileType<DaedalusGolem>();
		base.Item.DamageType = DamageClass.Summon;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Vector2 mouse = player.ClampedMouseWorld();
		Point mouseTileCoords = mouse.ToTileCoordinates();
		if (!CalamityUtils.ParanoidTileRetrieval(mouseTileCoords.X, mouseTileCoords.Y).IsTileSolidGround())
		{
			Projectile.NewProjectileDirect(source, mouse, Vector2.UnitY * 4f, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryonicBar>(12).AddTile(134).Register();
	}
}
