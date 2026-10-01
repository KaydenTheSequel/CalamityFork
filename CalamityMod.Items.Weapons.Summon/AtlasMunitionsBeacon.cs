using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class AtlasMunitionsBeacon : ModItem, ILocalizedModType, IModType
{
	public const float TargetRange = 2400f;

	public const float OverdriveModeRange = 720f;

	public const float PickupRange = 200f;

	public const int TurretShootRate = 9;

	public const int TurretShootRateOverdrive = 23;

	public const int HeldCannonShootRate = 9;

	public const int HeldCannonFadeoutTime = 156;

	public const int HeldCannonMaxDropTime = 720;

	public const float OverdriveProjectileDamageFactor = 1.18f;

	public const int ShotsNeededToReachMaxHeat = 100;

	public const int HeatDissipationTime = 180;

	public const float OverdriveProjectileAngularRandomness = 0.1f;

	public static readonly Color HeatGlowColor;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 38;
		base.Item.damage = 200;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 4.75f;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = 10;
		base.Item.UseSound = SoundID.Item82;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AtlasMunitionsDropPod>();
		base.Item.shootSpeed = 10f;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.sentry = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[ModContent.ProjectileType<AtlasMunitionsAutocannonHeld>()] < 1;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Summon/AtlasMunitionsBeaconGlow", (AssetRequestMode)2).Value);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse != 2)
		{
			CalamityUtils.KillShootProjectileMany(player, type, ModContent.ProjectileType<AtlasMunitionsAutocannon>(), ModContent.ProjectileType<AtlasMunitionsAutocannonHeld>());
			Vector2 mouse = player.ClampedMouseWorld();
			position = mouse - Vector2.UnitY * 1020f;
			velocity = (mouse - position).SafeNormalize(Vector2.UnitY) * Main.rand.NextFloat(9f, 10f);
			Projectile.NewProjectile(source, position, velocity, type, base.Item.damage, knockback, player.whoAmI, mouse.Y - 40f);
			player.UpdateMaxTurrets();
		}
		return false;
	}

	static AtlasMunitionsBeacon()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		Color orangeRed = Color.OrangeRed;
		((Color)(ref orangeRed)).A = 64;
		HeatGlowColor = orangeRed;
	}
}
