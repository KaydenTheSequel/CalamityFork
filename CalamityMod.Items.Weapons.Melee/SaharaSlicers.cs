using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Melee.Shortswords;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "AquaticDischarge" })]
public class SaharaSlicers : ModItem, ILocalizedModType, IModType
{
	public bool AltProjectile = true;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 43;
		base.Item.height = 34;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.damage = 23;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useTime = 12;
		base.Item.useAnimation = 12;
		base.Item.shoot = ModContent.ProjectileType<SaharaSlicersBolt>();
		base.Item.shootSpeed = 3.3f;
		base.Item.knockBack = 6f;
		base.Item.UseSound = null;
		base.Item.channel = true;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			base.Item.useStyle = 1;
			if (player.Calamity().saharaSlicersBolts > 0)
			{
				SoundStyle style = SoundID.DD2_GhastlyGlaivePierce with
				{
					Pitch = 1.5f
				};
				SoundEngine.PlaySound(in style, player.Center);
				Projectile.NewProjectile(source, position, velocity * 2f, ModContent.ProjectileType<SaharaSlicersBolt>(), (int)((double)damage * 1.2), knockback * 1.2f, player.whoAmI, 1f);
				player.Calamity().saharaSlicersBolts--;
			}
			else
			{
				SoundStyle style = SoundID.Item7 with
				{
					Pitch = 0.2f
				};
				SoundEngine.PlaySound(in style, player.Center);
			}
		}
		else
		{
			base.Item.useStyle = 5;
			if (AltProjectile)
			{
				SoundStyle style = SoundID.Item1 with
				{
					Pitch = 0.4f
				};
				SoundEngine.PlaySound(in style, player.Center);
				Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SaharaSlicersBladeAlt>(), damage, knockback, player.whoAmI);
			}
			else
			{
				SoundStyle style = SoundID.Item1 with
				{
					Pitch = 0.9f
				};
				SoundEngine.PlaySound(in style, player.Center);
				Projectile.NewProjectile(source, position, velocity * 0.75f, ModContent.ProjectileType<SaharaSlicersBlade>(), damage, knockback, player.whoAmI);
			}
			AltProjectile = !AltProjectile;
		}
		return false;
	}

	public override bool MeleePrefix()
	{
		return true;
	}
}
