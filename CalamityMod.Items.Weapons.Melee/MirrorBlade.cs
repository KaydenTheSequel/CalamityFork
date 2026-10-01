using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class MirrorBlade : BaseSwordHoldoutItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	internal const float SlashProjectileDamageMultiplier = 0.5f;

	internal const int SlashProjectileLimit = 4;

	internal const int SlashCreationRate = 18;

	public int reflectTimer;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public bool HasFlavorTooltip => true;

	public override int ProjectileType => ModContent.ProjectileType<MirrorBladeProjectile>();

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(5, 5));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.Item.width = 114;
		base.Item.height = 128;
		base.Item.damage = 600;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = 40;
		base.Item.useTime = 40;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 7f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shootSpeed = 9f;
		base.Item.shoot = ModContent.ProjectileType<MirrorBlast>();
		base.SetDefaults();
	}

	public override bool CanUseItem(Player player)
	{
		return base.CanUseItem(player);
	}

	public override void UpdateInventory(Player player)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		if (reflectTimer <= 0)
		{
			return;
		}
		if (reflectTimer <= 50)
		{
			float coneLength = 96f;
			float maximumAngle = 1f;
			float coneRotation = player.DirectionTo(Main.MouseWorld).ToRotation();
			int shardCount = 0;
			Projectile[] projectile = Main.projectile;
			foreach (Projectile proj in projectile)
			{
				if (proj.active && proj.type == ModContent.ProjectileType<MirrorBlast>() && proj.owner == player.whoAmI && (proj.ModProjectile as MirrorBlast).isShard)
				{
					shardCount++;
				}
			}
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile proj2 = enumerator.Current;
				if (proj2.type == ModContent.ProjectileType<DoGLaserWalls>() && proj2.ModProjectile<DoGLaserWalls>().time >= 29f)
				{
					if (shardCount > 0)
					{
						SoundEngine.PlaySound(in SeekingScorcher.LightShatterSound, player.Center);
						SoundEngine.PlaySound(in SoundID.DD2_WitherBeastDeath, player.Center);
					}
					if (shardCount >= 10)
					{
						player.SetImmuneTimeForAllTypes(player.longInvince ? 60 : 30);
					}
					proj2.Calamity().multiplicativeDR += (float)shardCount / 10f;
					proj2.Calamity().multiplicativeDRTimer = 60;
					projectile = Main.projectile;
					foreach (Projectile proj3 in projectile)
					{
						if (proj3.active && proj3.type == ModContent.ProjectileType<MirrorBlast>() && proj3.owner == player.whoAmI && (proj3.ModProjectile as MirrorBlast).isShard)
						{
							proj3.damage = (int)((float)proj3.damage * ((shardCount >= 10) ? 3f : 2f));
							(proj3.ModProjectile as MirrorBlast).shardShield = 0;
							(proj3.ModProjectile as MirrorBlast).shardNum = 11;
							proj3.netUpdate = true;
						}
					}
					reflectTimer = 0;
					return;
				}
				if (!proj2.hostile || proj2.damage <= 0 || !proj2.Hitbox.IntersectsConeSlowMoreAccurate(player.Center, coneLength, coneRotation, maximumAngle))
				{
					continue;
				}
				if (shardCount > 0)
				{
					SoundEngine.PlaySound(in SeekingScorcher.LightShatterSound, player.Center);
					SoundEngine.PlaySound(in SoundID.DD2_WitherBeastDeath, player.Center);
				}
				if (shardCount >= 10)
				{
					player.SetImmuneTimeForAllTypes(player.longInvince ? 60 : 30);
				}
				proj2.Calamity().multiplicativeDR += (float)shardCount / 10f;
				proj2.Calamity().multiplicativeDRTimer = 60;
				projectile = Main.projectile;
				foreach (Projectile proj4 in projectile)
				{
					if (proj4.active && proj4.type == ModContent.ProjectileType<MirrorBlast>() && proj4.owner == player.whoAmI && (proj4.ModProjectile as MirrorBlast).isShard)
					{
						proj4.damage = (int)((float)proj4.damage * ((shardCount >= 10) ? 3f : 2f));
						(proj4.ModProjectile as MirrorBlast).shardShield = 0;
						(proj4.ModProjectile as MirrorBlast).shardNum = 11;
						proj4.netUpdate = true;
					}
				}
				reflectTimer = 0;
				return;
			}
		}
		reflectTimer--;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			bool alreadyReflecting = reflectTimer > 0;
			bool hasShard = false;
			reflectTimer = 60;
			Projectile[] projectile = Main.projectile;
			foreach (Projectile proj in projectile)
			{
				if (proj.active && proj.type == ModContent.ProjectileType<MirrorBlast>() && proj.owner == player.whoAmI && (proj.ModProjectile as MirrorBlast).isShard)
				{
					(proj.ModProjectile as MirrorBlast).shardShield = 60;
					proj.netUpdate = true;
					hasShard = true;
				}
			}
			if (!alreadyReflecting & hasShard)
			{
				SoundEngine.PlaySound(in SoundID.DD2_EtherianPortalSpawnEnemy, player.Center);
			}
			return false;
		}
		return base.Shoot(player, source, position, velocity, type, damage, knockback);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}
}
