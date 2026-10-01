using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class BlissfulBombardier : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 88;
		base.Item.height = 48;
		base.Item.damage = 550;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 39;
		base.Item.useAnimation = 39;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.knockBack = 14f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/DudFire")
		{
			Volume = 0.4f,
			Pitch = -0.5f,
			PitchVariance = 0.1f
		};
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 24f;
		base.Item.shoot = ModContent.ProjectileType<BlissfulBombardierHoldout>();
		base.Item.useAmmo = AmmoID.Rocket;
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
		player.Calamity().mouseWorldListener = true;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2).Value);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, ModContent.ProjectileType<BlissfulBombardierHoldout>(), 0, 0f, player.whoAmI).velocity = (new Vector2(MathHelper.Lerp(player.Calamity().mouseWorld.X, player.Center.X, 0.55f), player.Center.Y) + new Vector2(0f, -500f) - player.Center).SafeNormalize(Vector2.Zero);
		return false;
	}
}
