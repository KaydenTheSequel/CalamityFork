using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "Starfall" })]
public class StarShower : ModItem, ILocalizedModType, IModType
{
	internal const float ShootSpeed = 28f;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 40;
		base.Item.damage = 60;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 15;
		base.Item.rare = 9;
		base.Item.useTime = 14;
		base.Item.useAnimation = 14;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.25f;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.UseSound = SoundID.Item105;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AstralStarMagic>();
		base.Item.shootSpeed = 28f;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 25f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		Vector2 mouse = player.ClampedMouseWorld();
		position = mouse - Vector2.UnitY * (mouse.Y - Main.screenPosition.Y + 80f);
		Vector2 cachedPosition = position;
		float maxRandomOffset = 16f;
		int totalProjectiles = 5;
		for (int i = 0; i < totalProjectiles; i++)
		{
			position.X += MathHelper.Lerp(-160f, 160f, (float)i / (float)(totalProjectiles - 1));
			position += Main.rand.NextVector2Circular(maxRandomOffset, maxRandomOffset);
			velocity = (mouse - position).SafeNormalize(Vector2.UnitY) * 28f * Main.rand.NextFloat(0.9f, 1.1f);
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			position = cachedPosition;
		}
		return false;
	}
}
