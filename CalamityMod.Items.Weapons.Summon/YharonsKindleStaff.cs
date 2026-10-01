using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

[LegacyName(new string[] { "AngryChickenStaff" })]
public class YharonsKindleStaff : ModItem, ILocalizedModType, IModType
{
	public const float ReboundRamDamageFactor = 2f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 5f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 80;
		base.Item.height = 74;
		base.Item.damage = 325;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.mana = 10;
		base.Item.useStyle = 1;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.noMelee = true;
		base.Item.knockBack = 7f;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.UseSound = CommonCalamitySounds.FlareSound;
		base.Item.autoReuse = true;
		base.Item.buffType = ModContent.BuffType<FieryDraconidBuff>();
		base.Item.shoot = ModContent.ProjectileType<FieryDraconid>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}
}
