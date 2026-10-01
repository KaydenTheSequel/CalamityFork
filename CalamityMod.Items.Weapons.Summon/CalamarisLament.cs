using System.Collections.Generic;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class CalamarisLament : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle GFB = new SoundStyle("CalamityMod/Sounds/Item/Inkling", 5);

	public static float EnemyDistanceDetection = 8000f;

	public static float ShootingExtraTargettingSpeed = 10f;

	public static float ShootingMinionDistance = 320f;

	public static int ShootingFireRate = 30;

	public static float ShootingProjectileSpeed = 20f;

	public static float LatchingDistanceRequired = 400f;

	public static float LatchingExtraTargettingSpeed = 30f;

	public static float LatchingDamageMultiplier = 1.25f;

	public static int LatchingIFrames = 30;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 88;
		base.Item.height = 108;
		base.Item.damage = 110;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.knockBack = 0.25f;
		base.Item.buffType = ModContent.BuffType<CalamarisLamentBuff>();
		base.Item.shoot = ModContent.ProjectileType<CalamarisLamentMinion>();
		base.Item.shootSpeed = 1f;
		base.Item.UseSound = SoundID.Item85;
		base.Item.useStyle = 5;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), velocity, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.FindAndReplace("[GFB]", this.GetLocalizedValue(Main.zenithWorld ? "TooltipGFB" : "TooltipNormal"));
	}
}
