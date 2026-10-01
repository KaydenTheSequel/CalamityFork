using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class LeonidProgenitor : RogueWeapon
{
	public static readonly Color blueColor;

	public static readonly Color purpleColor;

	public override float StealthDamageMultiplier => 1.25f;

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 48;
		base.Item.damage = 57;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.knockBack = 3f;
		base.Item.useAnimation = (base.Item.useTime = 15);
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<LeonidProgenitorBombshell>();
		base.Item.shootSpeed = 12f;
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.UseSound = SoundID.Item61;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.Calamity().StealthStrikeAvailable() || player.altFunctionUse != 2)
		{
			base.Item.UseSound = SoundID.Item61;
			base.Item.shoot = ModContent.ProjectileType<LeonidProgenitorBombshell>();
		}
		else
		{
			base.Item.UseSound = SoundID.Item88;
			base.Item.shoot = ModContent.ProjectileType<LeonidCometSmall>();
		}
		return base.CanUseItem(player);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable() || player.altFunctionUse != 2)
		{
			int bomb = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (bomb.WithinBounds(Main.maxProjectiles) && player.Calamity().StealthStrikeAvailable())
			{
				Main.projectile[bomb].Calamity().stealthStrike = true;
				Main.projectile[bomb].extraUpdates = 1;
			}
			return false;
		}
		for (int i = 0; i < 2; i++)
		{
			Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.ToRadians(((float)i - 0.5f) * 2f));
			Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/LeonidProgenitorGlow", (AssetRequestMode)2).Value);
	}

	static LeonidProgenitor()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		blueColor = new Color(48, 208, 255);
		purpleColor = new Color(208, 125, 218);
	}
}
