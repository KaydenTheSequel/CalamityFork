using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

public class AqueousHunterDrone : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle Fire = new SoundStyle("CalamityMod/Sounds/Item/ShrimpFire");

	public static readonly SoundStyle Hit = new SoundStyle("CalamityMod/Sounds/Item/ShrimpMissileHit");

	public static readonly SoundStyle Sound1 = new SoundStyle("CalamityMod/Sounds/Item/ShrimpSound1");

	public static readonly SoundStyle Sound2 = new SoundStyle("CalamityMod/Sounds/Item/ShrimpSound2");

	public static readonly SoundStyle Surprise = new SoundStyle("CalamityMod/Sounds/Item/ShrimpSurprise");

	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override void SetDefaults()
	{
		base.Item.Calamity();
		base.Item.width = 34;
		base.Item.height = 32;
		base.Item.shootSpeed = 10f;
		base.Item.damage = 24;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 4;
		base.Item.noMelee = true;
		base.Item.knockBack = 2.25f;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AqueousHunterDroneSummon>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = DamageClass.Summon;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		Vector2 mouse = player.ClampedMouseWorld();
		Point mouseTileCoords = mouse.ToTileCoordinates();
		if (!CalamityUtils.ParanoidTileRetrieval(mouseTileCoords.X, mouseTileCoords.Y).HasTile)
		{
			int p = Projectile.NewProjectile(source, new Vector2(mouse.X, player.Center.Y - 600f), Vector2.Zero, type, damage, knockback, player.whoAmI);
			Main.projectile[p].localAI[2] = player.ownedProjectileCounts[base.Item.shoot];
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = base.Item.damage;
			}
		}
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(8).AddIngredient<DubiousPlating>(4).AddIngredient<AerialiteBar>(4)
			.AddIngredient<SeaPrism>(7)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(1, out var condition), condition)
			.AddTile(16)
			.Register();
	}
}
