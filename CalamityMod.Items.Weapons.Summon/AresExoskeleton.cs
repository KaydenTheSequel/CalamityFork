using CalamityMod.Particles;
using CalamityMod.Projectiles.Summon.SmallAresArms;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class AresExoskeleton : ModItem, ILocalizedModType, IModType
{
	public int FrameCounter;

	public int Frame;

	public const int BoxParticleLifetime = 95;

	public const int PlasmaCannonShootRate = 30;

	public const int TeslaCannonShootRate = 36;

	public const int LaserCannonNormalShootRate = 15;

	public const int GaussNukeShootRate = 240;

	public const float TargetingDistance = 1020f;

	public const float MinionSlotsPerCannon = 3f;

	public const float PlasmaCannonBlastFactor = 0.9f;

	public const float TeslaOrbDamageFactor = 1f;

	public const float LaserDamageFactor = 1.1f;

	public const float NukeDamageFactor = 1f;

	public const float MaxNukeExplosionRadius = 720f;

	public const float TeslaOrbDetatchDistance = 1500f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public static bool ArmExists(Player player)
	{
		if (player.ownedProjectileCounts[ModContent.ProjectileType<ExoskeletonPlasmaCannon>()] >= 1)
		{
			return true;
		}
		if (player.ownedProjectileCounts[ModContent.ProjectileType<ExoskeletonTeslaCannon>()] >= 1)
		{
			return true;
		}
		if (player.ownedProjectileCounts[ModContent.ProjectileType<ExoskeletonLaserCannon>()] >= 1)
		{
			return true;
		}
		if (player.ownedProjectileCounts[ModContent.ProjectileType<ExoskeletonGaussNukeCannon>()] >= 1)
		{
			return true;
		}
		return false;
	}

	public override void Load()
	{
		if (!Main.dedServ)
		{
			EquipLoader.AddEquipTexture(base.Mod, $"{Texture}_{EquipType.Body}", EquipType.Body, this);
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 36);
		base.Item.mana = 80;
		base.Item.damage = 625;
		base.Item.useStyle = 4;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.noMelee = true;
		base.Item.knockBack = 1f;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.UseSound = SoundID.Item117;
		base.Item.shoot = ModContent.ProjectileType<ExoskeletonPlasmaCannon>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = DamageClass.Summon;
	}

	public override bool? CanAutoReuseItem(Player player)
	{
		return false;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frameI, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Summon/AresExoskeleton", (AssetRequestMode)2).Value;
		if (!Main.gameMenu && ArmExists(Main.LocalPlayer))
		{
			texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Summon/AresExoskeletonRemote", (AssetRequestMode)2).Value;
			position.X += scale * 6f;
		}
		spriteBatch.Draw(texture, position, (Rectangle?)new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0f, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Summon/AresExoskeleton", (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)base.Item.GetCurrentFrame(ref Frame, ref FrameCounter, 1, 1), lightColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		int panelID = ModContent.ProjectileType<ExoskeletonPanel>();
		if (player.ownedProjectileCounts[panelID] >= 1)
		{
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == panelID && p.owner == player.whoAmI)
				{
					p.ai[0] = 1f;
					p.netUpdate = true;
				}
			}
		}
		else
		{
			int panel = Projectile.NewProjectile(source, position, Vector2.Zero, panelID, damage, 0f, player.whoAmI);
			if (Main.projectile.IndexInRange(panel))
			{
				Main.projectile[panel].originalDamage = base.Item.damage;
			}
			Vector2 boxVelocity = -Vector2.UnitY.RotatedByRandom(0.699999988079071) * 6f + Vector2.UnitX * (float)player.direction * 4f;
			GeneralParticleHandler.SpawnParticle(new AresSummonCrateParticle(player, boxVelocity, 95));
		}
		return false;
	}
}
