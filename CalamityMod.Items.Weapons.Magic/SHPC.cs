using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Weapons.Magic;

public class SHPC : LegendaryItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle FireSound = new SoundStyle("CalamityMod/Sounds/Item/AnomalysNanogunMPFBShot");

	public static readonly SoundStyle VacuumStart = new SoundStyle("CalamityMod/Sounds/Item/SHPCVacuumStart")
	{
		Volume = 0.5f
	};

	public static readonly SoundStyle VacuumLoop = new SoundStyle("CalamityMod/Sounds/Item/SHPCVacuumLoop")
	{
		Volume = 0.5f
	};

	public static readonly SoundStyle VacuumEnd = new SoundStyle("CalamityMod/Sounds/Item/SHPCVacuumEnd")
	{
		Volume = 0.5f
	};

	public const int ShotsPerSoul = 50;

	public int storedSoulpower;

	public int storedSoulType = 520;

	public int recoilProgress;

	public const float LightExplosionSizeMult = 1.5f;

	public const float NightExplosionTimeMult = 2.5f;

	public const int FlightDirectHitFlightBoost = 25;

	public const float MightKnockbackStrength = 20f;

	public const float SightHomingRange = 320f;

	public const int FrightFlatDamage = 20;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override Color? TooltipExtensionColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(31, 251, 255);
		}
	}

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 124;
		base.Item.height = 52;
		base.Item.damage = 117;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 15;
		base.Item.useAnimation = (base.Item.useTime = 60);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.UseSound = FireSound;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<SHPB>();
		base.Item.shootSpeed = 20f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
	}

	public static int FindSoulForAmmo(Player player)
	{
		int soul = -1;
		bool foundItem = false;
		for (int s = 54; s < 58; s++)
		{
			if (player.inventory[s].stack > 0 && player.inventory[s].ammo == 520)
			{
				soul = player.inventory[s].type;
				foundItem = true;
				break;
			}
		}
		if (!foundItem)
		{
			for (int i = 0; i < 54; i++)
			{
				if (player.inventory[i].stack > 0 && player.inventory[i].ammo == 520)
				{
					soul = player.inventory[i].type;
					break;
				}
			}
		}
		return soul;
	}

	public Color FindColorForSoul()
	{
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		Color returnColor = default(Color);
		((Color)(ref returnColor))._002Ector(0, 0, 0);
		switch (storedSoulType)
		{
		case 520:
			((Color)(ref returnColor))._002Ector(240, 29, 196);
			break;
		case 521:
			((Color)(ref returnColor))._002Ector(123, 29, 220);
			break;
		case 575:
			((Color)(ref returnColor))._002Ector(106, 240, 250);
			break;
		case 548:
			((Color)(ref returnColor))._002Ector(4, 51, 222);
			break;
		case 549:
			((Color)(ref returnColor))._002Ector(79, 255, 124);
			break;
		case 547:
			((Color)(ref returnColor))._002Ector(255, 96, 20);
			break;
		}
		return returnColor;
	}

	public int TransferColorToProj()
	{
		int projai = 0;
		switch (storedSoulType)
		{
		case 520:
			projai = 0;
			break;
		case 521:
			projai = 1;
			break;
		case 575:
			projai = 2;
			break;
		case 548:
			projai = 3;
			break;
		case 549:
			projai = 4;
			break;
		case 547:
			projai = 5;
			break;
		}
		return projai;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-35f, -10f);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void OnCreated(ItemCreationContext context)
	{
		if (context is RecipeItemCreationContext)
		{
			storedSoulpower = 50;
		}
	}

	public override bool CanUseItem(Player player)
	{
		base.Item.channel = (base.Item.noUseGraphic = player.altFunctionUse == 2);
		base.Item.UseSound = ((player.altFunctionUse == 2) ? ((SoundStyle?)null) : new SoundStyle?(FireSound));
		if ((player.altFunctionUse == 0 && (storedSoulpower > 0 || FindSoulForAmmo(player) != -1)) || player.altFunctionUse == 2)
		{
			return player.ownedProjectileCounts[ModContent.ProjectileType<SHPV>()] <= 0;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		if (player.altFunctionUse != 2)
		{
			if (storedSoulpower > 0)
			{
				storedSoulpower--;
			}
			if (storedSoulpower <= 0)
			{
				bool ammoConsumed = false;
				if (FindSoulForAmmo(player) != -1)
				{
					int soulType = FindSoulForAmmo(player);
					player.ConsumeItem(soulType);
					storedSoulType = soulType;
					ammoConsumed = true;
				}
				if (ammoConsumed)
				{
					storedSoulpower = 50;
				}
			}
		}
		return base.UseItem(player);
	}

	public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
	{
		if (player.altFunctionUse == 2)
		{
			mult *= 0f;
		}
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			return 6f;
		}
		return 1f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SHPV>(), damage, 0f, player.whoAmI);
			return false;
		}
		Projectile.NewProjectile(source, position + new Vector2(0f, -10f) + velocity * 3f, velocity, ModContent.ProjectileType<SHPB>(), damage, knockback, player.whoAmI, TransferColorToProj());
		return false;
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		float barScale = 2.5f;
		Texture2D barBG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarBack", (AssetRequestMode)2).Value;
		Texture2D barFG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarFront", (AssetRequestMode)2).Value;
		Vector2 drawPos = position + new Vector2(((float)frame.Width - (float)barBG.Width * 0.5f) * scale, ((float)frame.Height + 45f) * scale);
		Rectangle frameCrop = default(Rectangle);
		((Rectangle)(ref frameCrop))._002Ector(0, 0, (int)((float)storedSoulpower / 50f * (float)barFG.Width), barFG.Height);
		Color colorBG = Color.Black;
		Color colorFG = Color.Lerp(Color.DarkGray, FindColorForSoul(), (float)storedSoulpower / 50f);
		spriteBatch.Draw(barBG, drawPos, (Rectangle?)null, colorBG, 0f, origin, scale * barScale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(barFG, drawPos, (Rectangle?)frameCrop, colorFG * 0.8f, 0f, origin, scale * barScale, (SpriteEffects)0, 0f);
		CalamityUtils.DrawBorderStringEightWay(spriteBatch, FontAssets.MouseText.Value, storedSoulpower.ToString(), drawPos + new Vector2(-200f, -60f) * scale, Color.GreenYellow, Color.Black, scale * 2.5f);
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
		player.Calamity().rightClickListener = true;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float itemRotation = player.compositeFrontArm.rotation + (float)Math.PI / 2f * player.gravDir;
		Vector2 itemPosition = player.MountedCenter + itemRotation.ToRotationVector2() * 35f;
		Vector2 itemSize = default(Vector2);
		((Vector2)(ref itemSize))._002Ector((float)base.Item.width, (float)base.Item.height);
		Vector2 itemOrigin = default(Vector2);
		((Vector2)(ref itemOrigin))._002Ector(-35f, 0f);
		if (player.altFunctionUse != 2)
		{
			recoilProgress++;
			if (recoilProgress < base.Item.useAnimation / 3)
			{
				itemPosition -= (player.Calamity().mouseWorld - player.Center).SafeNormalize(Vector2.UnitX) * (float)(base.Item.useAnimation / 3 - recoilProgress) * 0.75f;
			}
			else if (recoilProgress >= base.Item.useAnimation - 1)
			{
				recoilProgress = 0;
			}
		}
		CalamityUtils.CleanHoldStyle(player, itemRotation, itemPosition, itemSize, itemOrigin);
		base.UseStyle(player, heldItemFrame);
	}

	public override void UseItemFrame(Player player)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float rotation = (player.Center - player.Calamity().mouseWorld).ToRotation() * player.gravDir + (float)Math.PI / 2f;
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rotation);
	}

	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		if (Main.zenithWorld)
		{
			bool plantera = NPC.downedPlantBoss;
			bool golem = NPC.downedGolemBoss;
			bool cultist = NPC.downedAncientCultist;
			bool moonLord = NPC.downedMoonlord;
			bool providence = DownedBossSystem.downedProvidence;
			bool devourerOfGods = DownedBossSystem.downedDoG;
			bool yharon = DownedBossSystem.downedYharon;
			float damageMult = 1f + (plantera ? 0.1f : 0f) + (golem ? 0.15f : 0f) + (cultist ? 3.5f : 0f) + (moonLord ? 4.5f : 0f) + (providence ? 7.5f : 0f) + (devourerOfGods ? 2.5f : 0f) + (yharon ? 30f : 0f);
			damage *= damageMult;
		}
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		if (Main.zenithWorld)
		{
			list.FindAndReplace("[GFB]", Lang.SupportGlyphs(this.GetLocalizedValue("TooltipGFB")));
			return;
		}
		list.FindAndReplace("[GFB]", Lang.SupportGlyphs(this.GetLocalization("TooltipNormal").Format(this.GetLocalizedValue((storedSoulpower == 0) ? "NoSoul" : ("SoulDesc" + storedSoulType)))));
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PlasmaDriveCore>().AddIngredient<SuspiciousScrap>(4).AddRecipeGroup("AnyMythrilBar", 10)
			.AddIngredient(547, 5)
			.AddIngredient(548, 5)
			.AddIngredient(549, 5)
			.AddTile(134)
			.Register();
	}

	public override ModItem Clone(Item item)
	{
		ModItem modItem = base.Clone(item);
		if (modItem is SHPC a && item.ModItem is SHPC a2)
		{
			a.storedSoulpower = a2.storedSoulpower;
			a.storedSoulType = a2.storedSoulType;
		}
		return modItem;
	}

	public override void SaveData(TagCompound tag)
	{
		tag["ammoStored"] = storedSoulpower;
		tag["soulType"] = storedSoulType;
	}

	public override void LoadData(TagCompound tag)
	{
		storedSoulpower = tag.GetInt("ammoStored");
		storedSoulType = tag.GetInt("soulType");
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(storedSoulpower);
		writer.Write(storedSoulType);
	}

	public override void NetReceive(BinaryReader reader)
	{
		storedSoulpower = reader.ReadInt32();
		storedSoulType = reader.ReadInt32();
	}
}
