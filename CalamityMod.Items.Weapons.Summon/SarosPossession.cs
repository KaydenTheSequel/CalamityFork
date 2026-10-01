using System.Linq;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class SarosPossession : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public static SoundStyle FiringSound
	{
		get
		{
			SoundStyle result = new SoundStyle("CalamityMod/Sounds/Item/Summon/SarosFiring");
			result.MaxInstances = 1;
			result.Volume = 0.3f;
			result.pitchVariance = 0.05f;
			result.pitch = 0.5f;
			return result;
		}
	}

	public static SoundStyle SpawnSound => new SoundStyle("CalamityMod/Sounds/Item/Summon/SarosSpawn");

	public static SoundStyle LoopSound => SoundID.DD2_BetsyFlameBreath with
	{
		Volume = 0.2f
	};

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 48;
		base.Item.damage = 66;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.15f;
		base.Item.UseSound = null;
		base.Item.buffType = ModContent.BuffType<SarosPossessionBuff>();
		base.Item.shoot = ModContent.ProjectileType<SarosAura>();
		base.Item.DamageType = DamageClass.Summon;
		base.Item.channel = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[ModContent.ProjectileType<SarosEclipseBeam>()] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		if (player.ownedProjectileCounts[type] > 0)
		{
			Projectile projectile = Main.projectile.First((Projectile x) => x.active && x.type == type && x.owner == player.whoAmI);
			projectile.ai[0]++;
			projectile.netUpdate = true;
			SoundStyle style = SpawnSound with
			{
				MaxInstances = 10,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew,
				pitchVariance = 0.05f
			};
			SoundEngine.PlaySound(in style, player.Center);
			return false;
		}
		player.channel = false;
		player.AddBuff(base.Item.buffType, 2);
		SoundEngine.PlaySound(SpawnSound with
		{
			MaxInstances = 10,
			SoundLimitBehavior = SoundLimitBehavior.IgnoreNew,
			pitchVariance = 0.05f
		}, player.Center);
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Sirius>().AddIngredient<AuricBar>(5).AddIngredient<DarksunFragment>(15)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
