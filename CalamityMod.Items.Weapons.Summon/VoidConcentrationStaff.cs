using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class VoidConcentrationStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 3f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 72;
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.UseSound = SoundID.DD2_EtherianPortalOpen;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.mana = 10;
		base.Item.damage = 105;
		base.Item.knockBack = 4f;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.buffType = ModContent.BuffType<VoidConcentrationBuff>();
		base.Item.shoot = ModContent.ProjectileType<VoidConcentrationAura>();
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse != 2)
		{
			return player.ownedProjectileCounts[ModContent.ProjectileType<VoidConcentrationAura>()] == 0;
		}
		return base.CanUseItem(player);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse != 2)
		{
			player.AddBuff(base.Item.buffType, 2);
			Projectile.NewProjectileDirect(source, player.Center, Vector2.Zero, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		}
		return false;
	}

	public override bool AltFunctionUse(Player player)
	{
		return base.AltFunctionUse(player);
	}
}
