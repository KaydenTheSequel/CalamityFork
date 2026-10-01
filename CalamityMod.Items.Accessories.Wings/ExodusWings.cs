using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Dusts;
using CalamityMod.Items.Armor.Empyrean;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Wings;

[AutoloadEquip(new EquipType[] { EquipType.Wings })]
public class ExodusWings : BaseWings
{
	public override float BonusAscentWhileFalling => 0.85f;

	public override float BonusAscentWhileRising => 0.15f;

	public override float RisingSpeedThreshold => 1f;

	public override float MaxAscentSpeed => 3f;

	public override float BaseAscent => 0.135f;

	public override void SetStaticDefaults()
	{
		ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(180, 9f, 2.5f);
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Item.width = 22;
		base.Item.height = 20;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.rare = 10;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		if (player.armor[0].type == ModContent.ItemType<EmpyreanMask>() && player.armor[1].type == ModContent.ItemType<EmpyreanCloak>() && player.armor[2].type == ModContent.ItemType<EmpyreanCuisses>())
		{
			player.AddBuff(ModContent.BuffType<EmpyreanWrath>(), 2);
		}
		if (player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual)
		{
			Vector2 spawnPos = player.Center + new Vector2((float)(-25 * player.direction), 0f) + Main.rand.NextVector2Circular(20f, 20f);
			Vector2 spawnPos2 = player.Center + new Vector2((float)(15 * player.direction), 0f) + Main.rand.NextVector2Circular(20f, 20f);
			float partScale = Main.rand.NextFloat(0.3f, 0.8f);
			Vector2 partVel = Utils.RotatedBy(new Vector2(0f, 5f), (double)(0.5f * (float)player.direction), default(Vector2)).RotatedByRandom(0.5) * Main.rand.NextFloat(0.5f, 0.8f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(spawnPos, partVel, Color.Black, 13, partScale * 0.9f, 0.7f, Main.rand.NextFloat(-0.2f, 0.2f)));
			if (Main.rand.NextBool(player.controlJump ? 2 : 4))
			{
				Dust dust = Dust.NewDustPerfect(spawnPos, ModContent.DustType<VoidDustInverted>(), partVel, 0, default(Color), partScale * 2f);
				dust.noGravity = true;
				dust.color = Color.LightGreen;
			}
			if (Main.rand.NextBool())
			{
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(spawnPos2, partVel, Color.Black, 13, partScale * 0.6f, 0.7f, Main.rand.NextFloat(-0.2f, 0.2f)));
				if (Main.rand.NextBool(player.controlJump ? 2 : 4))
				{
					Dust dust2 = Dust.NewDustPerfect(spawnPos2, ModContent.DustType<VoidDustInverted>(), partVel, 0, default(Color), partScale * 1.7f);
					dust2.noGravity = true;
					dust2.color = Color.LightGreen;
				}
			}
		}
		player.wingTimeMax = 180;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(575, 20).AddIngredient<MeldConstruct>(14).AddIngredient(3467, 10)
			.AddTile(412)
			.Register();
	}
}
