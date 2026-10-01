using System;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.Items.Weapons.Melee;

public class GaelsGreatsword : ModItem, ILocalizedModType, IModType
{
	public static readonly int BaseDamage = 690;

	public static readonly float GiantSkullDamageMultiplier = 1.7f;

	public static readonly int SearchDistance = 1450;

	public static readonly int ImmunityFrames = 10;

	public static readonly float SkullsplosionDamageMultiplier = 1.5f;

	public static readonly float RagePerSecond = 0.025f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	internal static string SkullsplosionEntitySourceContext => "GaelsGreatswordRageSkullsplosion";

	public override void SetDefaults()
	{
		base.Item.width = 108;
		base.Item.height = 100;
		base.Item.damage = BaseDamage;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 13);
		base.Item.useTurn = true;
		base.Item.knockBack = 9f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.Calamity().devItem = true;
		base.Item.shoot = ModContent.ProjectileType<GaelSkull>();
		base.Item.shootSpeed = 15f;
		base.Item.useStyle = 1;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(12f, 12f);
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source = player.GetSource_ItemUse(base.Item);
		if (player.ownedProjectileCounts[ModContent.ProjectileType<LightningThing>()] < 3 && (float)player.statLife <= (float)player.statLifeMax2 * 0.5f && Main.myPlayer == player.whoAmI && WorldUtils.Find((player.Center + (float)Main.rand.Next(-300, 301) * Vector2.UnitX).ToTileCoordinates(), Searches.Chain(new Searches.Down(400), new Conditions.IsSolid()), out var spawnPosition))
		{
			Projectile.NewProjectile(source, spawnPosition.ToWorldCoordinates(8f, 0f), Vector2.Zero, ModContent.ProjectileType<LightningThing>(), 0, 0f, player.whoAmI);
		}
		if (player.itemAnimation != (int)((double)player.itemAnimationMax * 0.5))
		{
			return;
		}
		player.Calamity().gaelSwipes++;
		if (!((float)player.statLife <= (float)player.statLifeMax2 * 0.5f))
		{
			return;
		}
		for (int i = 0; i < 120; i++)
		{
			float r = (float)Math.Sqrt(Main.rand.NextDouble());
			Vector2 dustSpawn = (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2() * r * base.Item.Size;
			if (dustSpawn.X > (float)(base.Item.width / 2))
			{
				Dust.NewDustPerfect(player.MountedCenter + dustSpawn.RotatedBy(player.itemRotation) * (float)player.direction, 218, Vector2.Zero).noGravity = true;
				if (Main.rand.NextBool(100))
				{
					int damage = (int)player.GetTotalDamage<MeleeDamageClass>().ApplyTo(base.Item.damage);
					Projectile.NewProjectile(source, player.MountedCenter + dustSpawn.RotatedBy(player.itemRotation) * (float)player.direction, Vector2.Zero, ModContent.ProjectileType<GaelExplosion>(), damage, 0f, player.whoAmI);
				}
			}
			else
			{
				i--;
			}
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			return false;
		}
		switch (player.Calamity().gaelSwipes % 3)
		{
		case 0:
		{
			int numProj = 2;
			float rotation = MathHelper.ToRadians(10f);
			for (int i = 0; i < numProj; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)i / (float)(numProj - 1)));
				Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
			}
			break;
		}
		case 1:
		{
			int largeSkullDmg = (int)((float)damage * GiantSkullDamageMultiplier);
			int projectileIndex = Projectile.NewProjectile(source, position, velocity * 0.6f, type, largeSkullDmg, knockback, player.whoAmI, 0f, 1f);
			Main.projectile[projectileIndex].scale = 2f;
			break;
		}
		}
		return false;
	}
}
