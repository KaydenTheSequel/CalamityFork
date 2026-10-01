using System;
using CalamityMod.Cooldowns;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Pets;
using CalamityMod.Items.SummonItems;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class UniverseSplitter : ModItem, ILocalizedModType, IModType
{
	public const float ItemUseDustMaxRadius = 36f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 14));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 54;
		base.Item.height = 76;
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.UseSound = SoundID.Item122;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.mana = 300;
		base.Item.damage = 9000;
		base.Item.knockBack = 7f;
		base.Item.useAnimation = (base.Item.useTime = 10);
		base.Item.shoot = ModContent.ProjectileType<UniverseSplitterField>();
		base.Item.shootSpeed = 10f;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse != 2)
		{
			if (!player.HasCooldown(global::CalamityMod.Cooldowns.UniverseSplitter.ID))
			{
				player.AddCooldown(global::CalamityMod.Cooldowns.UniverseSplitter.ID, CalamityUtils.SecondsToFrames(30));
				int p = Projectile.NewProjectile(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI);
				if (Main.projectile.IndexInRange(p))
				{
					Main.projectile[p].originalDamage = base.Item.damage;
				}
				for (int i = 0; i < 36; i++)
				{
					float angle = (float)Math.PI / 18f * (float)i + Main.rand.NextFloat((float)Math.PI / 18f);
					Dust dust = Dust.NewDustPerfect(position + angle.ToRotationVector2() * 36f, 247);
					dust.velocity = Vector2.Normalize(angle.ToRotationVector2()) * 2.5f;
					dust.noGravity = true;
					dust.scale = 0.8f;
				}
			}
			else
			{
				Projectile.NewProjectile(source, position, Vector2.UnitY * 3f, 450, 5, 0f, player.whoAmI);
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BurrowerController>().AddIngredient<Abombination>().AddIngredient<ShadowspecBar>(5)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
