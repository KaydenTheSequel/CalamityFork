using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class AncientIceChunk : ModItem, ILocalizedModType, IModType
{
	public static int IFrames = 20;

	public static float MaxDistanceFromOwner = 400f;

	public static float DistanceToDash = 250f;

	public static float DistanceToStopDash = 800f;

	public static float MinVelocity = 12f;

	public static float TimeToShoot = 80f;

	public static float ProjectileDMGMultiplier = 1.5f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(6, 6));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 50;
		base.Item.damage = 25;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.buffType = ModContent.BuffType<IceClasperBuff>();
		base.Item.shoot = ModContent.ProjectileType<IceClasperMinion>();
		base.Item.knockBack = 2f;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.mana = 10;
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item30;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Main.rand.NextVector2Circular(1f, 1f), type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}
}
