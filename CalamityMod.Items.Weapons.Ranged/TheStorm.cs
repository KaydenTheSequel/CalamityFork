using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class TheStorm : ModItem, ILocalizedModType, IModType
{
	public int shots;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(5, 9));
	}

	public override void SetDefaults()
	{
		base.Item.width = 54;
		base.Item.height = 90;
		base.Item.damage = 40;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 2;
		base.Item.useAnimation = 20;
		base.Item.useLimitPerAnimation = 10;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.5f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<TheStormLightningShot>();
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Arrow;
		base.Item.consumeAmmoOnLastShotOnly = true;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		if (shots % 2 == 0)
		{
			SoundStyle style = SoundID.Item122 with
			{
				Volume = 0.6f,
				PitchVariance = 0.2f
			};
			SoundEngine.PlaySound(in style, position);
		}
		for (int i = 0; i < 2; i++)
		{
			float arrowVelAdjust = Main.rand.NextFloat(-40f, 40f);
			Vector2 arrowSpawnPos = new Vector2(MathHelper.Lerp(player.Calamity().mouseWorld.X, player.Center.X, 0.5f), player.Center.Y) + new Vector2(arrowVelAdjust, Main.rand.NextFloat(-560f, -660f));
			Vector2 arrowVel = (player.Calamity().mouseWorld - arrowSpawnPos).SafeNormalize(velocity).RotatedBy(arrowVelAdjust * -0.004f) * base.Item.shootSpeed;
			if (CalamityUtils.CheckWoodenAmmo(type, player))
			{
				Projectile.NewProjectileDirect(source, arrowSpawnPos, arrowVel, ModContent.ProjectileType<TheStormLightningShot>(), (int)((float)damage * ((i == 0) ? 1.5f : 1f)), knockback, -1, (i == 0) ? 5 : 0);
				continue;
			}
			Projectile projectile = Projectile.NewProjectileDirect(source, arrowSpawnPos, arrowVel, (i == 0) ? ModContent.ProjectileType<TheStormLightningShot>() : type, damage, knockback);
			projectile.noDropItem = true;
			projectile.tileCollide = false;
		}
		shots++;
		return false;
	}
}
