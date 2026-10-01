using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class CosmicImmaterializer : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 10f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 170;
		base.Item.height = 164;
		base.Item.damage = 560;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.knockBack = 0.25f;
		base.Item.buffType = ModContent.BuffType<CosmicEnergy>();
		base.Item.shoot = ModContent.ProjectileType<CosmicEnergySpiral>();
		base.Item.UseSound = SoundID.Item60;
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] <= 0)
		{
			return player.maxMinions >= 10;
		}
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		CalamityUtils.KillShootProjectiles(shouldBreak: true, type, player);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Summon/CosmicImmaterializerGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<LegionofCelestia>().AddIngredient<EtherealSubjugator>().AddIngredient<Cosmilamp>()
			.AddIngredient<CalamarisLament>()
			.AddIngredient<MiracleMatter>()
			.AddTile<DraedonsForge>()
			.Register();
	}
}
