using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class NebulousCore : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 16;
		base.Item.height = 14;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.expert = true;
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		float projLighting = (float)Main.rand.Next(90, 111) * 0.01f;
		projLighting *= Main.essScale;
		Lighting.AddLight((int)((base.Item.position.X + (float)(base.Item.width / 2)) / 16f), (int)((base.Item.position.Y + (float)(base.Item.height / 2)) / 16f), 0.35f * projLighting, 0.05f * projLighting, 0.35f * projLighting);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().nebulousCore = true;
		player.GetDamage<GenericDamageClass>() += 0.1f;
		if (!Main.rand.NextBool(15))
		{
			return;
		}
		int numProj = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.owner == player.whoAmI && p.type == ModContent.ProjectileType<NebulaStar>())
			{
				numProj++;
			}
		}
		if (Main.rand.Next(15) < numProj || numProj >= 10)
		{
			return;
		}
		int spawnRadius = 24;
		for (int j = 0; j < 50; j++)
		{
			float randomProjOffset = Main.rand.NextFloat(200 - j * 2, 400 + j * 2);
			Vector2 center = player.Center;
			center.X += Main.rand.NextFloat(0f - randomProjOffset, randomProjOffset + 1f);
			center.Y += Main.rand.NextFloat(0f - randomProjOffset, randomProjOffset + 1f);
			if (!Collision.SolidCollision(center, spawnRadius, spawnRadius) && !Collision.WetCollision(center, spawnRadius, spawnRadius))
			{
				center.X += spawnRadius / 2;
				center.Y += spawnRadius / 2;
				if ((Collision.CanHit(player.Center, 1, 1, center, 1, 1) || Collision.CanHit(player.Center - new Vector2(0f, 50f), 1, 1, center, 1, 1)) && Main.rand.NextBool(3) && Main.myPlayer == player.whoAmI)
				{
					IEntitySource source_Accessory = player.GetSource_Accessory(base.Item);
					int damage = (int)player.GetBestClassDamage().ApplyTo(250f);
					float knockBack = 3f;
					Projectile.NewProjectile(source_Accessory, center, Vector2.Zero, ModContent.ProjectileType<NebulaStar>(), damage, knockBack, player.whoAmI);
					break;
				}
			}
		}
	}
}
