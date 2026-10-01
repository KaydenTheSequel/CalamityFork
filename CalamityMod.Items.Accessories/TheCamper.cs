using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Back })]
public class TheCamper : ModItem, ILocalizedModType, IModType
{
	private int auraCounter;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.accessory = true;
		base.Item.defense = 10;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source = player.GetSource_Accessory(base.Item);
		CalamityPlayer modPlayer = player.Calamity();
		modPlayer.camper = true;
		player.AddBuff(89, 60);
		Main.SceneMetrics.HasHeartLantern = true;
		player.AddBuff(87, 60);
		Main.SceneMetrics.HasCampfire = true;
		if (!player.HasBuff(207))
		{
			player.AddBuff(207, 80);
		}
		else
		{
			for (int l = 0; l < Player.MaxBuffs; l++)
			{
				if (player.buffType[l] == 207 && player.buffTime[l] < 80)
				{
					player.buffTime[l] = 80;
				}
			}
		}
		Lighting.AddLight(player.Center, 0.825f, 0.66f, 0f);
		if (Main.myPlayer != player.whoAmI)
		{
			return;
		}
		if (player.StandingStill())
		{
			player.GetDamage<GenericDamageClass>() += 0.15f;
			auraCounter++;
			float range = 200f;
			if (auraCounter == 9)
			{
				auraCounter = 0;
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC npc = enumerator.Current;
					if (npc.IsAnEnemy() && !npc.dontTakeDamage && Vector2.Distance(player.Center, npc.Center) <= range)
					{
						int campingFireDamage = (int)player.GetBestClassDamage().ApplyTo(Main.rand.Next(100, 121));
						Projectile.NewProjectile(source, npc.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), campingFireDamage, 0f, player.whoAmI, npc.whoAmI);
					}
				}
			}
			if (player.HeldItem != null && !player.HeldItem.IsAir && player.HeldItem.stack > 0)
			{
				bool num = player.HeldItem.CountsAsClass<SummonDamageClass>();
				bool rogue = player.HeldItem.CountsAsClass<ThrowingDamageClass>();
				bool melee = player.HeldItem.CountsAsClass<MeleeDamageClass>();
				bool ranged = player.HeldItem.CountsAsClass<RangedDamageClass>();
				bool magic = player.HeldItem.CountsAsClass<MagicDamageClass>();
				if (num)
				{
					player.GetKnockback<SummonDamageClass>() += 0.1f;
					player.AddBuff(150, 60);
				}
				else if (rogue)
				{
					modPlayer.rogueVelocity += 0.1f;
				}
				else if (melee)
				{
					player.GetAttackSpeed<MeleeDamageClass>() += 0.1f;
					player.AddBuff(159, 60);
				}
				else if (ranged)
				{
					player.AddBuff(93, 60);
				}
				else if (magic)
				{
					player.AddBuff(29, 60);
				}
			}
		}
		else
		{
			auraCounter = 0;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3337).AddIngredient(966, 10).AddIngredient(1859, 5)
			.AddIngredient(3198)
			.AddIngredient(487)
			.AddIngredient(2177)
			.AddIngredient(2999)
			.AddRecipeGroup("AnyFood", 50)
			.AddTile(114)
			.Register();
	}
}
