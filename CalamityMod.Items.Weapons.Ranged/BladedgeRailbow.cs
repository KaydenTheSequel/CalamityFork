using System;
using System.Collections.Generic;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.DraedonsArsenal;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "BladedgeGreatbow" })]
public class BladedgeRailbow : ModItem, ILocalizedModType, IModType
{
	public static int[] arrowArr;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 74;
		base.Item.height = 24;
		base.Item.damage = 30;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = (base.Item.useAnimation = 28);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.5f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 14f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 4; i++)
		{
			float SpeedX = velocity.X + Main.rand.NextFloat(-3f, 3f);
			float SpeedY = velocity.Y + Main.rand.NextFloat(-3f, 3f);
			int index = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI);
			Main.projectile[index].noDropItem = true;
		}
		Vector2 realPlayerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		Vector2 arrowVel = velocity;
		((Vector2)(ref arrowVel)).Normalize();
		arrowVel *= 10f;
		bool arrowHitsTiles = Collision.CanHit(realPlayerPos, 0, 0, realPlayerPos + arrowVel, 0, 0);
		int numArrows = (Main.zenithWorld ? 4 : 2);
		for (int j = 0; j < numArrows; j++)
		{
			float arrowOffset = (float)j - 0.5f;
			Vector2 offsetSpawn = arrowVel.RotatedBy((float)Math.PI / 10f * arrowOffset);
			if (!arrowHitsTiles)
			{
				offsetSpawn -= arrowVel;
			}
			int projType = ((!Main.zenithWorld) ? 206 : arrowArr[Main.rand.Next(arrowArr.Length)]);
			int projectile = Projectile.NewProjectile(source, realPlayerPos.X + offsetSpawn.X, realPlayerPos.Y + offsetSpawn.Y, velocity.X, velocity.Y, projType, (int)((float)damage * 0.7f), 0f, player.whoAmI);
			if (projectile.WithinBounds(Main.maxProjectiles))
			{
				if (projType == 206 || projType == ModContent.ProjectileType<SlimeStream>() || projType == ModContent.ProjectileType<FriendlyLaserWallBeam>())
				{
					Main.projectile[projectile].DamageType = DamageClass.Ranged;
				}
				if (projType == ModContent.ProjectileType<AstrealArrow>() || projType == ModContent.ProjectileType<CorrodedShell>() || projType == ModContent.ProjectileType<Shell>())
				{
					Projectile obj = Main.projectile[projectile];
					obj.velocity /= 2f;
				}
				if (projType == ModContent.ProjectileType<FriendlyLaserWallBeam>())
				{
					Main.projectile[projectile].ai[0] = -0.25f;
					Main.projectile[projectile].ai[1] = 1f;
				}
			}
		}
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		string tooltip = (Main.zenithWorld ? this.GetLocalizedValue("TooltipGFB") : this.GetLocalizedValue("TooltipNormal"));
		tooltips.FindAndReplace("[GFB]", tooltip);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PerennialBar>(12).AddTile(134).Register();
	}

	static BladedgeRailbow()
	{
		int[] obj = new int[69]
		{
			1, 2, 4, 5, 41, 91, 103, 172, 225, 278,
			282, 474, 639, 1006, 710, 819, 932, 485, 120, 117,
			706, 357, 495, 469, 323, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0
		};
		obj[25] = ModContent.ProjectileType<BloodfireArrowProj>();
		obj[26] = ModContent.ProjectileType<CinderArrowProj>();
		obj[27] = ModContent.ProjectileType<ElysianArrowProj>();
		obj[28] = ModContent.ProjectileType<IcicleArrowProj>();
		obj[29] = ModContent.ProjectileType<SproutingArrowMain>();
		obj[30] = ModContent.ProjectileType<SproutingArrowSplit>();
		obj[31] = ModContent.ProjectileType<VanquisherArrowProj>();
		obj[32] = ModContent.ProjectileType<VeriumBoltProj>();
		obj[33] = ModContent.ProjectileType<TyphoonArrow>();
		obj[34] = ModContent.ProjectileType<MiniSharkron>();
		obj[35] = ModContent.ProjectileType<TorrentialArrow>();
		obj[36] = ModContent.ProjectileType<AstrealArrow>();
		obj[37] = ModContent.ProjectileType<BarinadeArrow>();
		obj[38] = ModContent.ProjectileType<BoltArrow>();
		obj[39] = ModContent.ProjectileType<LeafArrow>();
		obj[40] = ModContent.ProjectileType<SporeBomb>();
		obj[41] = ModContent.ProjectileType<BrimstoneBolt>();
		obj[42] = ModContent.ProjectileType<PrecisionBolt>();
		obj[43] = ModContent.ProjectileType<CondemnationArrow>();
		obj[44] = ModContent.ProjectileType<ContagionArrow>();
		obj[45] = ModContent.ProjectileType<CorrodedShell>();
		obj[46] = ModContent.ProjectileType<DaemonsFlameArrow>();
		obj[47] = ModContent.ProjectileType<FriendlyLaserWallBeam>();
		obj[48] = ModContent.ProjectileType<DrataliornusFlame>();
		obj[49] = ModContent.ProjectileType<FlareBat>();
		obj[50] = ModContent.ProjectileType<ImmolationArrow>();
		obj[51] = ModContent.ProjectileType<ImmolationSpray>();
		obj[52] = ModContent.ProjectileType<FeatherLarge>();
		obj[53] = ModContent.ProjectileType<SlimeStream>();
		obj[54] = ModContent.ProjectileType<ExoCrystalArrow>();
		obj[55] = ModContent.ProjectileType<MistArrow>();
		obj[56] = ModContent.ProjectileType<LunarBolt>();
		obj[57] = ModContent.ProjectileType<PlagueArrow>();
		obj[58] = ModContent.ProjectileType<PlanetaryAnnihilationProj>();
		obj[59] = ModContent.ProjectileType<Shell>();
		obj[60] = ModContent.ProjectileType<TelluricGlareArrow>();
		obj[61] = ModContent.ProjectileType<BallistaGreatArrow>();
		obj[62] = ModContent.ProjectileType<TheMaelstromShark>();
		obj[63] = ModContent.ProjectileType<TheStormLightningShot>();
		obj[64] = ModContent.ProjectileType<ToxicArrow>();
		obj[65] = ModContent.ProjectileType<UltimaBolt>();
		obj[66] = ModContent.ProjectileType<UltimaRay>();
		obj[67] = ModContent.ProjectileType<UltimaSpark>();
		obj[68] = ModContent.ProjectileType<VernalBolt>();
		arrowArr = obj;
	}
}
