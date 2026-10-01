using System;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "NightsGaze" })]
public class Vega : RogueWeapon
{
	public static int StarburstCost = 20;

	public override float StealthDamageMultiplier => 1f;

	public override void SetDefaults()
	{
		base.Item.width = 82;
		base.Item.height = 82;
		base.Item.damage = 500;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 1;
		base.Item.useTime = 20;
		base.Item.knockBack = 1f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.maxStack = 1;
		base.Item.shoot = ModContent.ProjectileType<VegaProjectile>();
		base.Item.shootSpeed = 30f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override void HoldItem(Player player)
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
		player.Calamity().StratusStarburstResetTimer = (int)MathHelper.Max((float)player.Calamity().StratusStarburstResetTimer, 600f);
	}

	public override bool AltFunctionUse(Player player)
	{
		if (player.Calamity().AvaliableStarburst >= StarburstCost)
		{
			return true;
		}
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			player.Calamity().temporaryStealthTimer = 600;
			player.Calamity().temporaryStealthMax = 1f;
			player.Calamity().rogueStealth = Math.Max(player.Calamity().rogueStealthMax, player.Calamity().temporaryStealthMax);
			player.Calamity().StratusStarburst -= StarburstCost;
			int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, 1f);
			if (p.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[p].Calamity().stealthStrike = true;
				Main.projectile[p].extraUpdates++;
			}
			return false;
		}
		if (player.Calamity().StealthStrikeAvailable())
		{
			int p2 = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (p2.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[p2].Calamity().stealthStrike = true;
				Main.projectile[p2].extraUpdates++;
			}
			return false;
		}
		return true;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/VegaGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ProfanedPartisan>().AddIngredient<Lumenyl>(7).AddIngredient<RuinousSoul>(4)
			.AddIngredient<ExodiumCluster>(12)
			.AddTile(134)
			.Register();
	}
}
