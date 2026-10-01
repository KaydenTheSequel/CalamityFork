using System.Linq;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

[LegacyName(new string[] { "PrototypeAndromechaRing" })]
public class FlamsteedRing : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle CrippleSound = new SoundStyle("CalamityMod/Sounds/Custom/AndromedaCripple");

	public const int HalfSafeWidth = 4;

	public const int SafeHeight = 14;

	public const int CrippleTime = 360;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void Load()
	{
		if (!Main.dedServ)
		{
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/CalPlayer/DrawLayers/AndromedaWithout_Head", EquipType.Head, null, "HeadlessEquipTexture");
		}
	}

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			int equipSlotHead = EquipLoader.GetEquipSlot(base.Mod, "HeadlessEquipTexture", EquipType.Head);
			ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 28);
		base.Item.mana = 200;
		base.Item.damage = 1999;
		base.Item.useStyle = 4;
		base.Item.useAnimation = (base.Item.useTime = 9);
		base.Item.noMelee = true;
		base.Item.knockBack = 1f;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
		base.Item.UseSound = SoundID.Item117;
		base.Item.shoot = ModContent.ProjectileType<GiantIbanRobotOfDoom>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.Calamity().CannotBeEnchanted = true;
	}

	public override bool? CanAutoReuseItem(Player player)
	{
		return false;
	}

	public static bool SpaceForLargeMech(Player player, bool visuals = true)
	{
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		bool sufficientSpace = true;
		Point pos = default(Point);
		for (int i = 0; i < 8; i++)
		{
			for (int j = 1; j < 14; j++)
			{
				((Point)(ref pos))._002Ector(i + (int)player.Center.X / 16, (int)(player.Center.Y + (float)player.height / 2f) / 16 - j);
				if (Main.tile[pos].IsTileSolid())
				{
					sufficientSpace = false;
					if (!visuals)
					{
						return false;
					}
					Dust.NewDustPerfect(pos.ToVector2() * 16f + Vector2.One * 8f, 127, null, 0, default(Color), 1.2f);
					Dust.NewDustPerfect(pos.ToVector2() * 16f + Vector2.One * 8f, 114, Vector2.Zero, 0, default(Color), 1.4f).noGravity = true;
				}
			}
		}
		if (!sufficientSpace)
		{
			Rectangle displayZone = player.Hitbox;
			CombatText.NewText(displayZone, new Color(203, 157, 255), CalamityUtils.GetTextValueFromModItem<FlamsteedRing>("NoSpaceTextBottom"), dramatic: true);
			displayZone.Y -= 30;
			CombatText.NewText(displayZone, new Color(59, 194, 255), CalamityUtils.GetTextValueFromModItem<FlamsteedRing>("NoSpaceTextTop"), dramatic: true);
			return false;
		}
		return sufficientSpace;
	}

	public override bool CanUseItem(Player player)
	{
		if (Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<GiantIbanRobotOfDoom>()))
		{
			return true;
		}
		if (SpaceForLargeMech(player))
		{
			if (player.Calamity().andromedaCripple > 0)
			{
				return !CalamityPlayer.areThereAnyDamnBosses;
			}
			return true;
		}
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		if (player.ownedProjectileCounts[base.Item.shoot] > 0)
		{
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == base.Item.shoot && p.owner == player.whoAmI)
				{
					p.Kill();
				}
			}
			if (CalamityPlayer.areThereAnyDamnBosses)
			{
				player.Calamity().andromedaCripple = 360;
				player.AddBuff(ModContent.BuffType<AndromedaCripple>(), player.Calamity().andromedaCripple);
				SoundEngine.PlaySound(in CrippleSound, position);
			}
			return false;
		}
		return true;
	}

	internal static bool TransformItemUsage(Item item, Player player)
	{
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		if (player.whoAmI != Main.myPlayer)
		{
			return false;
		}
		int robotIndex = -1;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == ModContent.ProjectileType<GiantIbanRobotOfDoom>() && p.owner == player.whoAmI)
			{
				robotIndex = p.whoAmI;
				break;
			}
		}
		if (robotIndex != -1)
		{
			Projectile robot = Main.projectile[robotIndex];
			GiantIbanRobotOfDoom robotModProjectile = (GiantIbanRobotOfDoom)robot.ModProjectile;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<AndromedaRegislash>()] <= 0 && robotModProjectile.TopIconActive && (robotModProjectile.RightIconCooldown <= 480 || !robotModProjectile.RightIconActive))
			{
				IEntitySource source_ItemUse = player.GetSource_ItemUse(item);
				int damage = ((player.Calamity().andromedaState == AndromedaPlayerState.SmallRobot) ? 1897 : 5200);
				int slash = Projectile.NewProjectile(source_ItemUse, robot.Center + (float)((robot.spriteDirection > 0).ToDirectionInt() * robot.width / 2) * Vector2.UnitX, Vector2.Zero, ModContent.ProjectileType<AndromedaRegislash>(), damage, 15f, player.whoAmI, Projectile.GetByUUID(robot.owner, robot.whoAmI));
				Main.projectile[slash].originalDamage = damage;
			}
			if (!robotModProjectile.TopIconActive && (robotModProjectile.LeftBracketActive || robotModProjectile.RightBracketActive) && !robotModProjectile.BottomBracketActive && robotModProjectile.LaserCooldown <= 0 && (robotModProjectile.RightIconCooldown <= 480 || !robotModProjectile.RightIconActive))
			{
				robotModProjectile.LaserCooldown = 50;
				if (player.Calamity().andromedaState == AndromedaPlayerState.SmallRobot)
				{
					robotModProjectile.LaserCooldown = 26;
				}
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MawOfInfinity>().AddIngredient<CosmicViperEngine>().AddIngredient(3469)
			.AddIngredient<ShadowspecBar>(5)
			.AddIngredient<CosmiliteBar>(40)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
