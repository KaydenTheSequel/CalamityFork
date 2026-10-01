using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "Scorpion" })]
public class Scorpio : ModItem, ILocalizedModType, IModType
{
	public static int OriginalUseTime = 30;

	public static int TimeBetweenBursts = 8;

	public static int ProjectilesPerBurst = 8;

	public static float EnemyDetectionDistance = 2000f;

	public static float TrackingSpeed = 0.06f;

	public static float NukeEnemyDistanceDetection = 300f;

	public static float NukeRequiredRotationProximity = 0.96f;

	public static float NukeTrackingSpeed = 0.0095f;

	public static readonly SoundStyle RocketShoot = new SoundStyle("CalamityMod/Sounds/Item/ScorpioShot")
	{
		Volume = 0.45f
	};

	public static readonly SoundStyle RocketHit = new SoundStyle("CalamityMod/Sounds/Item/ScorpioHit")
	{
		Volume = 0.35f
	};

	public static readonly SoundStyle NukeHit = new SoundStyle("CalamityMod/Sounds/Item/ScorpioNukeHit")
	{
		Volume = 0.6f
	};

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.damage = 33;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = OriginalUseTime);
		base.Item.shoot = ModContent.ProjectileType<ScorpioHoldout>();
		base.Item.shootSpeed = 15f;
		base.Item.knockBack = 6.5f;
		base.Item.width = 96;
		base.Item.height = 42;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.useAmmo = AmmoID.Rocket;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.useStyle = 5;
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/DudFire")
		{
			Volume = 0.4f,
			Pitch = -0.9f,
			PitchVariance = 0.1f
		};
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] == 0;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] != 0;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseRotationListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, ModContent.ProjectileType<ScorpioHoldout>(), 0, 0f, player.whoAmI, -30f).velocity = (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.Zero);
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/Scorpio_Glow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1946).AddIngredient(3457, 6).AddIngredient(1346, 100)
			.AddTile(412)
			.Register();
	}
}
