using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.BrimstoneCragCatches;

public class DragoonDrizzlefish : ModItem, ILocalizedModType, IModType
{
	public bool ballShot = true;

	public int shotCounter;

	public new string LocalizationCategory => "Items.Fishing";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 38;
		base.Item.damage = 18;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.1f;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.UseSound = SoundID.Item20;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<DrizzlefishFireball>();
		base.Item.shootSpeed = 11f;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.FindAndReplace("[GFB]", this.GetLocalizedValue(Main.zenithWorld ? "TooltipGFB" : "TooltipNormal"));
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			if (!Main.zenithWorld || player.Calamity().dragoonDrizzlefishGelBoost >= 6)
			{
				return false;
			}
			Item gelFodder = new Item();
			gelFodder.useAmmo = AmmoID.Gel;
			return player.HasAmmo(gelFodder);
		}
		return true;
	}

	public override Vector2? HoldoutOrigin()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(7f, 7f);
	}

	public override bool AltFunctionUse(Player player)
	{
		return Main.zenithWorld;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			Item gelFodder = new Item();
			gelFodder.useAmmo = AmmoID.Gel;
			player.PickAmmo(gelFodder, out var _, out var _, out var _, out var _, out var _);
			player.Calamity().dragoonDrizzlefishGelBoost++;
			SoundStyle style = Sunskater.DeathSound with
			{
				Pitch = 0.3f
			};
			SoundEngine.PlaySound(in style, player.Center);
			style = SoundID.Item2 with
			{
				Pitch = 0.3f
			};
			SoundEngine.PlaySound(in style, player.Center);
			CombatText.NewText(player.Hitbox, Color.OrangeRed, ":)");
			for (int i = 0; i <= 11; i++)
			{
				Vector2 hVelocity = Main.rand.NextVector2Unit();
				hVelocity.X *= 0.66f;
				hVelocity *= Main.rand.NextFloat(1f, 2f);
				int heart = Gore.NewGore(player.GetSource_FromThis(), player.Center + Main.rand.NextVector2Circular(14f, 14f), hVelocity, 331, Main.rand.NextFloat(0.2f, 1.3f));
				Main.gore[heart].sticky = false;
				Gore obj = Main.gore[heart];
				obj.velocity *= 0.4f;
				Main.gore[heart].velocity.Y -= 0.7f;
			}
		}
		else
		{
			if (Main.zenithWorld)
			{
				if (Main.rand.NextBool(25) && player.Calamity().dragoonDrizzlefishGelBoost > 1)
				{
					SoundStyle roar = Sunskater.DeathSound;
					SoundEngine.PlaySound(in roar, player.Center);
					player.Calamity().dragoonDrizzlefishGelBoost--;
				}
				if (player.Calamity().dragoonDrizzlefishGelBoost == 1 && Main.rand.NextBool(3))
				{
					bool MADFISH = Main.rand.NextBool(4);
					CombatText.NewText(player.Hitbox, Color.OrangeRed, MADFISH ? ">:(" : ":(");
					SoundEngine.PlaySound(Sunskater.DeathSound with
					{
						Pitch = -0.3f
					}, player.Center);
					if (MADFISH)
					{
						player.AddBuff(ModContent.BuffType<Dragonfire>(), 240);
					}
					return false;
				}
			}
			velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5.5f));
			int shotType = ModContent.ProjectileType<DrizzlefishFireball>();
			if (shotCounter < 3)
			{
				shotType = ModContent.ProjectileType<DrizzlefishFireball>();
				shotCounter++;
			}
			else
			{
				shotType = ModContent.ProjectileType<DrizzlefishFire>();
				shotCounter = 0;
			}
			Projectile.NewProjectile(source, position, velocity, shotType, damage, knockback, player.whoAmI, 0f, Main.rand.Next(2));
		}
		return false;
	}
}
