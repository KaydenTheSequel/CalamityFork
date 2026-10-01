using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class PlantationStaff : ModItem, ILocalizedModType, IModType
{
	public static float EnemyDistanceDetection = 1600f;

	public static float ChargingSpeed = 35f;

	public static int ThornballAmount = 2;

	public static float ThornballFireRate = 90f;

	public static float ThornballSpeed = 20f;

	public static float SeedBurstDelay = 30f;

	public static float SeedBetweenBurstDelay = 10f;

	public static float SeedSpeed = 25f;

	public static int SeedAmountPerBurst = 3;

	public static int SeedBurstAmount = 3;

	public static float SporeStartVelocity = 3f;

	public static float TimeBeforeRamming = 15f;

	public static float RamTime = 240f;

	public static float TentacleSpeed = 25f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 3f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 50;
		base.Item.damage = 58;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.buffType = ModContent.BuffType<PlantationStaffBuff>();
		base.Item.shoot = ModContent.ProjectileType<PlantationStaffSummon>();
		base.Item.knockBack = 1f;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item76;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Main.rand.NextVector2Circular(2f, 2f), type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Summon/PlantationStaffGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<EyeOfNight>().AddIngredient<LivingShard>(12).AddTile(134)
			.Register();
	}
}
