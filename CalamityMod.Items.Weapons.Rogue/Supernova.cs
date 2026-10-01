using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
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

namespace CalamityMod.Items.Weapons.Rogue;

public class Supernova : RogueWeapon
{
	public static readonly SoundStyle ExplosionSound = new SoundStyle("CalamityMod/Sounds/Item/SupernovaBoom")
	{
		Volume = 0.9f
	};

	public static readonly SoundStyle StealthExplosionSound = new SoundStyle("CalamityMod/Sounds/Item/SupernovaStealthExplode")
	{
		Volume = 1f
	};

	public static readonly SoundStyle StealthChargeSound = new SoundStyle("CalamityMod/Sounds/Item/SupernovaStealthCharge")
	{
		Volume = 1f
	};

	public override float StealthDamageMultiplier => 0.7f;

	public override void SetDefaults()
	{
		base.Item.width = 106;
		base.Item.height = 112;
		base.Item.damage = 5036;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 70;
		base.Item.useStyle = 1;
		base.Item.useTime = 70;
		base.Item.knockBack = 18f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.shoot = ModContent.ProjectileType<SupernovaBomb>();
		base.Item.shootSpeed = 16f;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int stealth = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
			}
			return false;
		}
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/SupernovaGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SealedSingularity>().AddIngredient<StarofDestruction>().AddIngredient<TotalityBreakers>()
			.AddIngredient<BallisticPoisonBomb>()
			.AddIngredient<MiracleMatter>()
			.AddTile<DraedonsForge>()
			.Register();
	}
}
