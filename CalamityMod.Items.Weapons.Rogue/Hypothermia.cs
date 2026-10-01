using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Hypothermia : RogueWeapon
{
	private bool throwTwo = true;

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 32;
		base.Item.damage = 148;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.useTime = 4;
		base.Item.useAnimation = 12;
		base.Item.reuseDelay = 1;
		base.Item.useLimitPerAnimation = 3;
		base.Item.knockBack = 3f;
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item7;
		base.Item.shoot = ModContent.ProjectileType<HypothermiaShard>();
		base.Item.shootSpeed = 8f;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 16f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			for (int i = 0; i < 4; i++)
			{
				Vector2 chunkVelocity = velocity.RotatedByRandom(0.07000000029802322) * Main.rand.NextFloat(1.1f, 1.18f);
				int stealth = Projectile.NewProjectile(source, position, chunkVelocity, ModContent.ProjectileType<HypothermiaChunk>(), damage, knockback, player.whoAmI);
				if (stealth.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[stealth].Calamity().stealthStrike = true;
				}
			}
			return false;
		}
		int projAmt = ((!throwTwo) ? 1 : 2);
		throwTwo = !throwTwo;
		for (int j = 0; j < projAmt; j++)
		{
			Vector2 shardVel = velocity.RotatedByRandom(0.10471975803375244) * Main.rand.NextFloat(0.9f, 1.1f);
			int texID = Main.rand.Next(4);
			Projectile.NewProjectile(source, position, shardVel, type, damage, knockback, player.whoAmI, texID);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmiliteBar>(8).AddIngredient<EndothermicEnergy>(20).AddIngredient<RuinousSoul>(6)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
