using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Photoviscerator : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/PhotoUseSound")
	{
		Volume = 0.35f
	};

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/Item/PhotoHitSound")
	{
		Volume = 0.4f
	};

	public static int AmmoSavedPercent = 95;

	public static int LightBombCooldown = 10;

	public static float RightClickVelocityMult = 2.5f;

	public static int RightClickCooldown = 25;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 208;
		base.Item.height = 66;
		base.Item.damage = 495;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = LightBombCooldown);
		base.Item.shootSpeed = 6f;
		base.Item.knockBack = 2f;
		base.Item.shoot = ModContent.ProjectileType<PhotovisceratorHoldout>();
		base.Item.useAmmo = AmmoID.Gel;
		base.Item.useStyle = 5;
		base.Item.channel = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] > 0;
	}

	public override void HoldItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI)
		{
			player.Calamity().rightClickListener = true;
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if ((float)player.altFunctionUse == 2f)
		{
			if (!player.Calamity().mouseRight || player.whoAmI != Main.myPlayer || Main.mapFullscreen || Main.blockMouse)
			{
				return false;
			}
			Projectile.NewProjectile(source, position, Vector2.Zero, type, 0, 0f, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(source, position, Vector2.Zero, type, 0, 0f, player.whoAmI);
		}
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/PhotovisceratorGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ChromaticEruption>().AddIngredient<HalleysInferno>().AddIngredient<DeadSunsWind>()
			.AddIngredient<MiracleMatter>()
			.AddTile<DraedonsForge>()
			.Register();
	}
}
