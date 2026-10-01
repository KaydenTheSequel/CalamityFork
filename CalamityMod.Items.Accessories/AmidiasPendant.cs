using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class AmidiasPendant : ModItem, ILocalizedModType, IModType
{
	public const int ShardProjectiles = 2;

	public const float ShardAngleSpread = 90f;

	public int ShardCountdown;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 46;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		if (ShardCountdown <= 0)
		{
			ShardCountdown = 140;
		}
		if (ShardCountdown <= 0)
		{
			return;
		}
		ShardCountdown -= Main.rand.Next(1, 4);
		if (ShardCountdown <= 0 && player.whoAmI == Main.myPlayer)
		{
			IEntitySource source = player.GetSource_Accessory(base.Item);
			int speed2 = 25;
			float spawnX = (float)Main.rand.Next(-300, 301) + player.Center.X;
			float spawnY = -1000f + player.Center.Y;
			Vector2 baseSpawn = default(Vector2);
			((Vector2)(ref baseSpawn))._002Ector(spawnX, spawnY);
			Vector2 baseVelocity = player.Center - baseSpawn;
			((Vector2)(ref baseVelocity)).Normalize();
			baseVelocity *= (float)speed2;
			int spawnOffset = 30;
			float spread = -45f;
			for (int i = 0; i < 2; i++)
			{
				Vector2 spawn = baseSpawn;
				spawn.X = spawn.X + (float)(i * 30) - (float)spawnOffset;
				Vector2 velocity = baseVelocity.RotatedBy(MathHelper.ToRadians(spread + 90f * (float)i / 2f));
				velocity.X = velocity.X + 3f * Main.rand.NextFloat() - 1.5f;
				int finalDamage = (int)player.GetBestClassDamage().ApplyTo(30f);
				Projectile.NewProjectile(source, spawn.X, spawn.Y, velocity.X / 3f, velocity.Y / 2f, ModContent.ProjectileType<PearlAuraShard>(), finalDamage, 5f, Main.myPlayer);
			}
		}
	}
}
