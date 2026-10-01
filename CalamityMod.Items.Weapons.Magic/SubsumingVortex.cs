using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class SubsumingVortex : ModItem, ILocalizedModType, IModType
{
	public const int RightClickVortexCount = 3;

	public const int VortexReleaseRate = 27;

	public const int VortexShootDelay = 56;

	public const int LargeVortexChargeupTime = 240;

	public const float RightClickSpeedFactor = 1.3f;

	public const float RightClickDamageFactor = 0.3f;

	public const float SmallVortexTargetRange = 1300f;

	public const float GiantVortexMouseDriftFactor = 0.09f;

	public const float ReleaseSpeed = 33f;

	public const float ReleaseDamageFactor = 4.65f;

	public static readonly SoundStyle ExplosionSound = new SoundStyle("CalamityMod/Sounds/Custom/SubsumingVortexExplosion");

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override string Texture => "CalamityMod/Items/Weapons/Magic/SubsumingVortexSmall";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 86;
		base.Item.height = 104;
		base.Item.damage = 460;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.shootSpeed = 7f;
		base.Item.mana = 22;
		base.Item.knockBack = 5f;
		base.Item.shoot = ModContent.ProjectileType<EnormousConsumingVortex>();
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item84;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.autoReuse = true;
	}

	public override void HoldItem(Player player)
	{
		base.Item.channel = player.altFunctionUse == 2;
		player.Calamity().rightClickListener = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool? CanAutoReuseItem(Player player)
	{
		return true;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		Texture2D actualSprite = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/SubsumingVortex", (AssetRequestMode)2).Value;
		spriteBatch.DrawNewInventorySprite(actualSprite, new Vector2(48f, 52f), position, drawColor, origin, scale, (Vector2?)new Vector2(-6f, -6f));
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		Texture2D actualSprite = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/SubsumingVortex", (AssetRequestMode)2).Value;
		spriteBatch.Draw(actualSprite, base.Item.Center - Main.screenPosition, (Rectangle?)null, lightColor, rotation, actualSprite.Size() / 2f, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/SubsumingVortexGlow", (AssetRequestMode)2).Value);
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-6f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse != 2)
		{
			int vortexID = ModContent.ProjectileType<ExoVortex2>();
			for (int i = 0; i < 3; i++)
			{
				float hue = ((float)i / 2f + Main.rand.NextFloat(0.3f)) % 1f;
				Vector2 vortexVelocity = velocity * 1.3f + Main.rand.NextVector2Square(-2.5f, 2.5f);
				Projectile.NewProjectile(source, position, vortexVelocity, vortexID, (int)((float)damage * 0.3f), knockback, player.whoAmI, hue);
			}
			return false;
		}
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AuguroftheVoid>().AddIngredient<EventHorizon>().AddIngredient<TearsofHeaven>()
			.AddIngredient<MiracleMatter>()
			.AddTile<DraedonsForge>()
			.Register();
	}
}
