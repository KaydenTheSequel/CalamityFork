using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class CosmicViperEngine : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 28;
		base.Item.damage = 255;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.useStyle = 4;
		base.Item.noMelee = true;
		base.Item.knockBack = 6f;
		base.Item.UseSound = SoundID.Item15;
		base.Item.autoReuse = true;
		base.Item.buffType = ModContent.BuffType<CosmicViperEngineBuff>();
		base.Item.shoot = ModContent.ProjectileType<CosmicViperSummon>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		float speed = base.Item.shootSpeed;
		Vector2 spawnPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float num = Main.screenPosition.X + (float)Main.mouseX - spawnPos.X;
		float yPos = Main.screenPosition.Y + (float)((player.gravDir == -1f) ? (Main.screenHeight - Main.mouseY) : Main.mouseY) - spawnPos.Y;
		Vector2 vel = Utils.SafeNormalize(new Vector2(num, yPos), Vector2.UnitX * (float)player.direction) * speed;
		spawnPos = player.ClampedMouseWorld();
		vel = vel.RotatedBy(1.5707963705062866);
		Projectile.NewProjectileDirect(source, spawnPos + vel, vel, type, damage, knockback, player.whoAmI, 0f, 1f).originalDamage = base.Item.damage;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<TacticalPlagueEngine>().AddIngredient<CosmiliteBar>(10).AddTile<CosmicAnvil>()
			.Register();
	}
}
