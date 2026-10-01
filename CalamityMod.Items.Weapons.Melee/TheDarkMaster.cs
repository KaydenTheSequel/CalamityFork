using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class TheDarkMaster : ModItem, ILocalizedModType, IModType
{
	public const float DamagePerHealth = 0.001f;

	[CloneByReference]
	public BloomRing ring;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 60;
		base.Item.height = 60;
		base.Item.damage = 50;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 26);
		base.Item.useStyle = 1;
		base.Item.useTurn = true;
		base.Item.knockBack = 7f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.shoot = ModContent.ProjectileType<DarkMasterBeam>();
		base.Item.shootSpeed = 16f;
		base.Item.Calamity().donorItem = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse != 2)
		{
			if ((float)player.statLife >= (float)player.statLifeMax2 * 0.75f)
			{
				SoundEngine.PlaySound(in SoundID.Item71, player.Center);
				int baseMaxHealth = 400;
				int bonusHealth = player.statLifeMax2 - baseMaxHealth;
				float bonusDamage = 0.001f * (float)bonusHealth;
				Projectile.NewProjectile(source, position, velocity, type, (int)((float)damage * (1f + bonusDamage)), knockback, player.whoAmI);
			}
			else if (player.ownedProjectileCounts[ModContent.ProjectileType<DarkMasterClone>()] > 0)
			{
				SoundEngine.PlaySound(in SoundID.Item71, player.Center);
			}
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == ModContent.ProjectileType<DarkMasterClone>() && p.owner == player.whoAmI)
				{
					p.ai[1] = 1f;
				}
			}
		}
		else if (player.ownedProjectileCounts[ModContent.ProjectileType<DarkMasterClone>()] <= 0)
		{
			ring = new BloomRing(player.Center, Vector2.Zero, Color.Red, 0.4f, 10);
			GeneralParticleHandler.SpawnParticle(ring);
			for (int i = 0; i < 3; i++)
			{
				Projectile.NewProjectileDirect(base.Item.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<DarkMasterClone>(), base.Item.damage, base.Item.knockBack, player.whoAmI, i).OriginalCritChance = base.Item.crit;
			}
		}
		return false;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<DarkMasterClone>()] > 0)
			{
				return false;
			}
			base.Item.UseSound = SoundID.Item104;
			base.Item.useStyle = 5;
			base.Item.useTurn = false;
			base.Item.noMelee = true;
		}
		else
		{
			base.Item.UseSound = SoundID.Item1;
			base.Item.useStyle = 1;
			base.Item.useTurn = true;
			base.Item.noMelee = false;
		}
		return base.CanUseItem(player);
	}

	public override void UpdateInventory(Player player)
	{
		if (ring != null)
		{
			ring.Scale *= 1.3f;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(723).AddIngredient(46).AddIngredient(521, 20)
			.AddIngredient<EssenceofHavoc>(3)
			.AddIngredient(178)
			.AddTile(16)
			.Register();
	}
}
