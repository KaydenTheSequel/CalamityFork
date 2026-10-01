using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "Fabstaff" })]
public class Sylvestaff : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle FireSound = new SoundStyle("CalamityMod/Sounds/Item/SylvestaffFire", 3)
	{
		MaxInstances = 5
	};

	public static readonly SoundStyle BounceSound = new SoundStyle("CalamityMod/Sounds/Item/SylvestaffProjectileBounce", 3)
	{
		MaxInstances = 5
	};

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public static float StaffRecoilForce => 0.04f;

	public static int RayBoltShootRate => CalamityUtils.SecondsToFrames(0.0667f);

	public static float RayBoltTargetingRange => 272f;

	public static float TurnSpeedInterpolant => 0.276f;

	public override string Texture
	{
		get
		{
			if (WorldGen.SavedOreTiers.Gold == 8 || Main.gameMenu)
			{
				return "CalamityMod/Items/Weapons/Magic/SylvestaffGold";
			}
			return "CalamityMod/Items/Weapons/Magic/SylvestaffPlatinum";
		}
	}

	public override void SetStaticDefaults()
	{
		Item.staff[base.Item.type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 84;
		base.Item.height = 84;
		base.Item.damage = 140;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 30;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<SylvestaffHoldout>();
		base.Item.shootSpeed = 13.5f;
	}

	public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] <= 0)
		{
			mult *= 0f;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(495).AddIngredient(2756).AddRecipeGroup("AnyGoldBar", 5)
			.AddIngredient<Necroplasm>(10)
			.AddIngredient<ShadowspecBar>(5)
			.AddTile<DraedonsForge>()
			.Register();
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, position, (Rectangle?)frame, drawColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)null, lightColor, 0f, Vector2.Zero, scale, (SpriteEffects)0, 0f);
		return false;
	}
}
