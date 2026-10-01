using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class AstralachneaStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 52;
		base.Item.damage = 55;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 19;
		base.Item.useTime = 21;
		base.Item.useAnimation = 21;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 6f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.Item46;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AstralachneaFang>();
		base.Item.shootSpeed = 13f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		Vector2 realPlayerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float mouseXDist = (float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X;
		float mouseYDist = (float)Main.mouseY + Main.screenPosition.Y - realPlayerPos.Y;
		int spikeAmount = Main.rand.Next(3, 5);
		Vector2 fangSpawn = default(Vector2);
		for (int j = 0; j < spikeAmount; j++)
		{
			((Vector2)(ref fangSpawn))._002Ector(mouseXDist, mouseYDist);
			fangSpawn.X += Main.rand.NextFloat(-20f, 20f) * (float)j;
			fangSpawn.Y += Main.rand.NextFloat(-20f, 20f) * (float)j;
			fangSpawn = fangSpawn.SafeNormalize(Vector2.UnitX) * base.Item.shootSpeed;
			Projectile.NewProjectile(source, realPlayerPos, fangSpawn, ModContent.ProjectileType<AstralachneaFang>(), damage, knockback, player.whoAmI);
		}
		return false;
	}
}
